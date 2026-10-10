using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string dir = @"c:\Sport\ShoeShop\ShoeShop";
        string[] files = Directory.GetFiles(dir, "*Form.cs");

        foreach (string file in files)
        {
            string content = File.ReadAllText(file, Encoding.UTF8);
            if (!content.Contains("AppHelper.ApplyTheme(this);") && content.Contains("AppHelper.SetAppIcon(this);"))
            {
                content = content.Replace("AppHelper.SetAppIcon(this);", "AppHelper.SetAppIcon(this);\r\n            AppHelper.ApplyTheme(this);");
                File.WriteAllText(file, content, Encoding.UTF8);
                Console.WriteLine("Patched: " + Path.GetFileName(file));
            }
        }
    }
}
