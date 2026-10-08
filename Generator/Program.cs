using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var enc = Encoding.GetEncoding("windows-1251");

        string dataDir = @"c:\Sport\ShoeShop\data\";
        string outFile = @"c:\Sport\ShoeShop\InitDB.sql";

        var roles = new List<string> { "Гость", "Авторизированный клиент", "Менеджер", "Администратор" };
        var categories = new HashSet<string>();
        var manufacturers = new HashSet<string>();
        var suppliers = new HashSet<string>();
        var statuses = new HashSet<string>();

        // Read Users
        var users = new List<string[]>();
        var userLines = File.ReadAllLines(dataDir + "user_import.csv", enc);
        for (int i = 1; i < userLines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(userLines[i])) continue;
            var parts = userLines[i].Split(',');
            users.Add(parts);
        }

        // Read Products
        var products = new List<string[]>();
        var prodLines = File.ReadAllLines(dataDir + "Tovar.csv", enc);
        for (int i = 1; i < prodLines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(prodLines[i])) continue;
            // Handle quotes in CSV
            var parts = ParseCsvLine(prodLines[i]);
            if (parts.Count >= 11)
            {
                categories.Add(parts[6]);
                manufacturers.Add(parts[5]);
                suppliers.Add(parts[4]);
                products.Add(parts.ToArray());
            }
        }

        // Read Orders
        var orders = new List<string[]>();
        var orderLines = File.ReadAllLines(dataDir + "Заказ_import.csv", enc);
        for (int i = 1; i < orderLines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(orderLines[i])) continue;
            var parts = ParseCsvLine(orderLines[i]);
            if (parts.Count >= 8)
            {
                statuses.Add(parts[7]);
                orders.Add(parts.ToArray());
            }
        }

        var pickupPoints = File.ReadAllLines(dataDir + "Пункты выдачи_import.csv", enc)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.Trim('"'))
            .ToList();

        using (var writer = new StreamWriter(outFile, false, Encoding.UTF8))
        {
            writer.WriteLine("USE master;");
            writer.WriteLine("GO");
            writer.WriteLine("IF DB_ID('shoeshop') IS NOT NULL");
            writer.WriteLine("BEGIN");
            writer.WriteLine("    ALTER DATABASE shoeshop SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
            writer.WriteLine("    DROP DATABASE shoeshop;");
            writer.WriteLine("END");
            writer.WriteLine("GO");
            writer.WriteLine("CREATE DATABASE shoeshop;");
            writer.WriteLine("GO");
            writer.WriteLine("USE shoeshop;");
            writer.WriteLine("GO");

            writer.WriteLine("CREATE TABLE roles (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(50) NOT NULL);");
            writer.WriteLine("CREATE TABLE categories (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);");
            writer.WriteLine("CREATE TABLE manufacturers (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);");
            writer.WriteLine("CREATE TABLE suppliers (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(100) NOT NULL);");
            writer.WriteLine("CREATE TABLE order_statuses (id INT IDENTITY(1,1) PRIMARY KEY, name NVARCHAR(50) NOT NULL);");
            writer.WriteLine("CREATE TABLE pickup_points (id INT IDENTITY(1,1) PRIMARY KEY, address NVARCHAR(MAX) NOT NULL);");

            writer.WriteLine("CREATE TABLE users (id INT IDENTITY(1,1) PRIMARY KEY, login NVARCHAR(50) UNIQUE NOT NULL, password_hash NVARCHAR(64) NOT NULL, full_name NVARCHAR(150) NOT NULL, role_id INT REFERENCES roles(id));");

            writer.WriteLine("CREATE TABLE products (id INT IDENTITY(1,1) PRIMARY KEY, product_article NVARCHAR(100) UNIQUE, title NVARCHAR(150) NOT NULL, category_id INT REFERENCES categories(id), description NVARCHAR(MAX), manufacturer_id INT REFERENCES manufacturers(id), supplier_id INT REFERENCES suppliers(id), cost DECIMAL(10,2) CHECK (cost >= 0), unit NVARCHAR(20), stock_quantity INT CHECK (stock_quantity >= 0), discount INT DEFAULT 0 CHECK (discount >= 0 AND discount <= 100), photo_path NVARCHAR(255));");

            // Add new columns to orders: client_fio, receive_code.
            // Notice: order_article stays UNIQUE NOT NULL as requested by original prompt and used in code
            writer.WriteLine("CREATE TABLE orders (id INT IDENTITY(1,1) PRIMARY KEY, order_article NVARCHAR(50) UNIQUE NOT NULL, status_id INT REFERENCES order_statuses(id), pickup_point_id INT REFERENCES pickup_points(id), order_date DATE NOT NULL, delivery_date DATE NOT NULL, client_fio NVARCHAR(150), receive_code INT);");

            writer.WriteLine("CREATE TABLE order_items (order_id INT REFERENCES orders(id) ON DELETE CASCADE, product_id INT REFERENCES products(id), count INT CHECK (count > 0), PRIMARY KEY(order_id, product_id));");
            writer.WriteLine("GO\n");

            // Inserts
            foreach(var r in roles) writer.WriteLine($"INSERT INTO roles (name) VALUES (N'{r.Replace("'", "''")}');");
            foreach(var c in categories.ToList()) writer.WriteLine($"INSERT INTO categories (name) VALUES (N'{c.Replace("'", "''")}');");
            foreach(var m in manufacturers.ToList()) writer.WriteLine($"INSERT INTO manufacturers (name) VALUES (N'{m.Replace("'", "''")}');");
            foreach(var s in suppliers.ToList()) writer.WriteLine($"INSERT INTO suppliers (name) VALUES (N'{s.Replace("'", "''")}');");
            foreach(var st in statuses.ToList()) writer.WriteLine($"INSERT INTO order_statuses (name) VALUES (N'{st.Replace("'", "''")}');");
            foreach(var p in pickupPoints) writer.WriteLine($"INSERT INTO pickup_points (address) VALUES (N'{p.Replace("'", "''")}');");

            writer.WriteLine("GO\n");

            foreach(var u in users)
            {
                int roleId = roles.IndexOf(u[0].Trim()) + 1;
                string login = u[2];
                string pwd = u[3];
                string hash = HashPassword(pwd);
                string fio = u[1].Replace("'", "''");
                writer.WriteLine($"INSERT INTO users (login, password_hash, full_name, role_id) VALUES (N'{login}', N'{hash}', N'{fio}', {roleId});");
            }

            writer.WriteLine("GO\n");

            var catList = categories.ToList();
            var manList = manufacturers.ToList();
            var supList = suppliers.ToList();

            foreach(var p in products)
            {
                string article = p[0].Replace("'", "''");
                string title = p[1].Replace("'", "''");
                string unit = p[2].Replace("'", "''");
                string cost = p[3].Replace(',', '.'); // Handle comma in decimals if present
                int supId = supList.IndexOf(p[4]) + 1;
                int manId = manList.IndexOf(p[5]) + 1;
                int catId = catList.IndexOf(p[6]) + 1;
                string disc = p[7];
                string stock = p[8];
                string desc = p[9].Replace("'", "''");
                string photo = p[10].Replace("'", "''");

                writer.WriteLine($"INSERT INTO products (product_article, title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) VALUES (N'{article}', N'{title}', {catId}, N'{desc}', {manId}, {supId}, {cost}, N'{unit}', {stock}, {disc}, N'{photo}');");
            }

            writer.WriteLine("GO\n");

            var statList = statuses.ToList();
            writer.WriteLine("SET IDENTITY_INSERT orders ON;");
            
            // Generate order items separately so we can reference product IDs by article
            var orderItemsStatements = new List<string>();

            foreach(var o in orders)
            {
                // Number, items, oDate, dDate, pickup, fio, code, status
                string id = o[0];
                string order_article = id; // use id as article since they didn't provide one
                string itemsStr = o[1];
                
                string oDateStr = o[2];
                string oDate = ParseDate(oDateStr);

                string dDateStr = o[3];
                string dDate = ParseDate(dDateStr);

                string pickup = o[4];
                string fio = o[5].Replace("'", "''");
                string code = o[6];
                int statId = statList.IndexOf(o[7]) + 1;

                writer.WriteLine($"INSERT INTO orders (id, order_article, status_id, pickup_point_id, order_date, delivery_date, client_fio, receive_code) VALUES ({id}, N'{order_article}', {statId}, {pickup}, '{oDate}', '{dDate}', N'{fio}', {code});");

                // Parse items "А112Т4, 2, F635R4, 2"
                var itemParts = itemsStr.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < itemParts.Length; i+=2)
                {
                    if (i+1 < itemParts.Length)
                    {
                        string pArticle = itemParts[i].Trim();
                        string count = itemParts[i+1].Trim();
                        orderItemsStatements.Add($"INSERT INTO order_items (order_id, product_id, count) SELECT {id}, id, {count} FROM products WHERE product_article = N'{pArticle.Replace("'", "''")}';");
                    }
                }
            }
            writer.WriteLine("SET IDENTITY_INSERT orders OFF;");
            writer.WriteLine("GO\n");

            foreach(var stmt in orderItemsStatements)
            {
                writer.WriteLine(stmt);
            }
            writer.WriteLine("GO\n");
        }
    }

    static string ParseDate(string dateStr)
    {
        if (DateTime.TryParse(dateStr, out DateTime dt))
        {
            return dt.ToString("yyyy-MM-dd");
        }
        var parts = dateStr.Split('.');
        if (parts.Length == 3)
        {
            if (parts[0] == "30" && parts[1] == "02") parts[0] = "28"; // fix for 30.02
            if (parts[0] == "31" && parts[1] == "02") parts[0] = "28";
            if (parts[0] == "31" && parts[1] == "04") parts[0] = "30";
            if (parts[0] == "31" && parts[1] == "06") parts[0] = "30";
            if (parts[0] == "31" && parts[1] == "09") parts[0] = "30";
            if (parts[0] == "31" && parts[1] == "11") parts[0] = "30";
            return $"{parts[2]}-{parts[1]}-{parts[0]}";
        }
        return "2025-01-01";
    }

    static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        bool inQuotes = false;
        var current = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (line[i] == ',' && !inQuotes)
            {
                result.Add(current.ToString());
                current.Clear();
            }
            else
            {
                current.Append(line[i]);
            }
        }
        result.Add(current.ToString());
        return result;
    }
}
