using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShoeShop
{
    public static class AppHelper
    {
        public static Color ColorWhite = Color.White;
        public static Color ColorChartreuse = ColorTranslator.FromHtml("#7FFF00");
        public static Color ColorMediumSpringGreen = ColorTranslator.FromHtml("#00FA9A");
        public static Color ColorSeaGreen = ColorTranslator.FromHtml("#2E8B57");
        public static Color ColorLightBlue = ColorTranslator.FromHtml("#ADD8E6");
        public static Color ColorBlack = Color.Black;
        public static Color ColorRed = Color.Red;

        public static Font BaseFont = new Font("Times New Roman", 11F, FontStyle.Regular);
        public static Font BoldFont = new Font("Times New Roman", 11F, FontStyle.Bold);
        public static Font HeaderFont = new Font("Times New Roman", 13F, FontStyle.Bold);
        public static Font StrikeFont = new Font("Times New Roman", 11F, FontStyle.Strikeout);

        public static void SetAppIcon(Form form)
        {
            try
            {
                string iconPath = System.IO.Path.Combine(Application.StartupPath, "logo.png");
                if (System.IO.File.Exists(iconPath))
                {
                    using (Bitmap bmp = new Bitmap(iconPath))
                    {
                        form.Icon = Icon.FromHandle(bmp.GetHicon());
                    }
                }
            }
            catch { }
        }

        public static void ApplyTheme(Control control)
        {
            if (control is Form form)
            {
                form.BackColor = ColorWhite;
                form.Font = BaseFont;
                if (!form.Text.StartsWith("ООО «Обувь» — "))
                {
                    form.Text = "ООО «Обувь» — " + form.Text;
                }
            }

            foreach (Control child in control.Controls)
            {
                ApplyThemeToControl(child);
                ApplyTheme(child);
            }
        }

        private static void ApplyThemeToControl(Control c)
        {
            c.Font = BaseFont;

            if (c is Button btn)
            {
                btn.Font = BoldFont;
                if (btn.Name.Contains("Save") || btn.Name.Contains("Add") || btn.Name.Contains("Login") || btn.Name.Contains("Orders") || btn.Name.Contains("SelectPhoto"))
                {
                    btn.BackColor = ColorMediumSpringGreen;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.ForeColor = ColorBlack;
                }
            }
            else if (c is Panel pnl)
            {
                if (pnl.Name.Contains("Top") || pnl.Name.Contains("Header") || pnl.Name.Contains("Footer") || pnl.Name.Contains("Filters"))
                {
                    pnl.BackColor = ColorChartreuse;
                }
                else
                {
                    pnl.BackColor = ColorWhite;
                }
            }
            else if (c is DataGridView dgv)
            {
                dgv.BackgroundColor = ColorWhite;
                dgv.DefaultCellStyle.Font = BaseFont;
                dgv.ColumnHeadersDefaultCellStyle.Font = BoldFont;
            }
        }
    }
}
