using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class MainForm : Form
    {
        private List<Product> allProducts = new List<Product>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AppHelper.SetAppIcon(this);
            AppHelper.ApplyTheme(this);
            flpProducts.BringToFront();

            lblUserInfo.Text = $"{LoginForm.CurrentUser.FullName} ({LoginForm.CurrentUser.RoleName})";
            lblUserInfo.Left = btnLogout.Left - lblUserInfo.Width - 20;

            if (LoginForm.CurrentUser.RoleId == 1 || LoginForm.CurrentUser.RoleId == 2)
            {
                pnlFilters.Enabled = false;
                btnOrders.Visible = false;
                btnAddProduct.Visible = false;
            }
            else if (LoginForm.CurrentUser.RoleId == 3)
            {
                btnOrders.Visible = true;
                btnAddProduct.Visible = false;
            }
            else if (LoginForm.CurrentUser.RoleId == 4)
            {
                btnOrders.Visible = true;
                btnAddProduct.Visible = true;
            }

            LoadFilters();
            LoadData();
        }

        private void LoadFilters()
        {
            cmbSort.Items.Add("По умолчанию");
            cmbSort.Items.Add("Сначала мало на складе");
            cmbSort.Items.Add("Сначала много на складе");
            cmbSort.SelectedIndex = 0;

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT id, name FROM suppliers ORDER BY id", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cmbFilter.Items.Add(new DictionaryItem { Id = 0, Name = "Все поставщики" });
                        while (reader.Read())
                        {
                            cmbFilter.Items.Add(new DictionaryItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                        }
                    }
                }
                cmbFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки фильтров: {ex.Message}");
            }
        }

        public void LoadData()
        {
            allProducts.Clear();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT p.id, p.title, p.category_id, c.name as cat_name, 
                               p.description, p.manufacturer_id, m.name as man_name, 
                               p.supplier_id, s.name as sup_name, 
                               p.cost, p.unit, p.stock_quantity, p.discount, p.photo_path
                        FROM products p
                        JOIN categories c ON p.category_id = c.id
                        JOIN manufacturers m ON p.manufacturer_id = m.id
                        JOIN suppliers s ON p.supplier_id = s.id
                    ";
                    using (var cmd = new SqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allProducts.Add(new Product
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                CategoryId = reader.GetInt32(2),
                                CategoryName = reader.GetString(3),
                                Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                ManufacturerId = reader.GetInt32(5),
                                ManufacturerName = reader.GetString(6),
                                SupplierId = reader.GetInt32(7),
                                SupplierName = reader.GetString(8),
                                Cost = reader.GetDecimal(9),
                                Unit = reader.IsDBNull(10) ? "" : reader.GetString(10),
                                StockQuantity = reader.GetInt32(11),
                                Discount = reader.GetInt32(12),
                                PhotoPath = reader.IsDBNull(13) ? "" : reader.GetString(13)
                            });
                        }
                    }
                }
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void Filters_Changed(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = allProducts.AsEnumerable();

            string search = txtSearch.Text.ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                filtered = filtered.Where(p => 
                    p.Title.ToLower().Contains(search) || 
                    p.Description.ToLower().Contains(search) || 
                    p.CategoryName.ToLower().Contains(search) || 
                    p.ManufacturerName.ToLower().Contains(search));
            }

            if (cmbFilter.SelectedItem is DictionaryItem selectedSupplier && selectedSupplier.Id != 0)
            {
                filtered = filtered.Where(p => p.SupplierId == selectedSupplier.Id);
            }

            if (cmbSort.SelectedIndex == 1)
            {
                filtered = filtered.OrderBy(p => p.StockQuantity);
            }
            else if (cmbSort.SelectedIndex == 2)
            {
                filtered = filtered.OrderByDescending(p => p.StockQuantity);
            }
            else
            {
                filtered = filtered.OrderBy(p => p.Id);
            }

            var result = filtered.ToList();
            DisplayProducts(result);

            lblCount.Text = $"Отображено {result.Count} из {allProducts.Count} товаров";
        }

        private void DisplayProducts(List<Product> products)
        {
            flpProducts.Controls.Clear();
            foreach (var p in products)
            {
                ProductCard card = new ProductCard();
                card.BindData(p);
                if (LoginForm.CurrentUser.RoleId == 4)
                {
                    card.Cursor = Cursors.Hand;
                    card.Click += (s, e) => EditProduct(p);
                    foreach (Control c in card.Controls)
                    {
                        c.Click += (s, e) => EditProduct(p);
                    }
                }
                flpProducts.Controls.Add(card);
            }
        }

        private void EditProduct(Product p)
        {
            ProductEditForm form = new ProductEditForm(p);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            ProductEditForm form = new ProductEditForm(null);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOrders_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm();
            form.ShowDialog(this);
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
