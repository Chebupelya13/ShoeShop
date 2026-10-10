using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class ProductCard : UserControl
    {
        public Product ProductData { get; private set; }

        public ProductCard()
        {
            InitializeComponent();
            AppHelper.ApplyTheme(this);
        }

        public void BindData(Product product)
        {
            ProductData = product;

            lblTitle.Font = AppHelper.HeaderFont;
            lblTitle.Text = $"{product.CategoryName} | {product.Title}";
            lblDescription.Text = product.Description;
            lblManufacturer.Text = $"Производитель: {product.ManufacturerName}";
            lblSupplier.Text = $"Поставщик: {product.SupplierName}";
            lblStock.Text = $"На складе: {product.StockQuantity} {product.Unit}";

            if (product.Discount > 0)
            {
                lblCost.Text = product.Cost.ToString("C2");
                lblCost.Font = AppHelper.StrikeFont;
                lblCost.ForeColor = AppHelper.ColorRed;
                lblDiscountCost.Text = product.DiscountedCost.ToString("C2");
                lblDiscountCost.Font = AppHelper.BoldFont;
                lblDiscountCost.ForeColor = AppHelper.ColorBlack;
                lblDiscountCost.Visible = true;
                lblDiscount.Text = $"{product.Discount}%";
                lblDiscount.Visible = true;
            }
            else
            {
                lblCost.Text = product.Cost.ToString("C2");
                lblCost.Font = AppHelper.BoldFont;
                lblCost.ForeColor = AppHelper.ColorBlack;
                lblDiscountCost.Visible = false;
                lblDiscount.Visible = false;
            }

            if (product.StockQuantity == 0)
            {
                this.BackColor = AppHelper.ColorLightBlue;
                SetForeColor(AppHelper.ColorBlack);
            }
            else if (product.Discount > 15)
            {
                this.BackColor = AppHelper.ColorSeaGreen;
                SetForeColor(AppHelper.ColorWhite);
            }
            else
            {
                this.BackColor = AppHelper.ColorWhite;
                SetForeColor(AppHelper.ColorBlack);
            }

            LoadImage(product.PhotoPath);
        }

        private void LoadImage(string photoPath)
        {
            if (pbPhoto.Image != null)
            {
                pbPhoto.Image.Dispose();
                pbPhoto.Image = null;
            }

            if (!string.IsNullOrEmpty(photoPath))
            {
                string fullPath = Path.Combine(Application.StartupPath, photoPath);
                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            using (var ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                ms.Position = 0;
                                pbPhoto.Image = Image.FromStream(ms);
                            }
                        }
                        return;
                    }
                    catch
                    {
                    }
                }
            }
            
            LoadDefaultImage();
        }

        private void SetForeColor(Color color)
        {
            lblTitle.ForeColor = color;
            lblDescription.ForeColor = color;
            lblManufacturer.ForeColor = color;
            lblSupplier.ForeColor = color;
            lblStock.ForeColor = color;
            if (color == Color.White && lblCost.ForeColor == Color.Black)
            {
                lblCost.ForeColor = color; 
            }
            lblDiscountCost.ForeColor = color;
            lblDiscount.ForeColor = color;
        }

        private void LoadDefaultImage()
        {
            string defaultPath = Path.Combine(Application.StartupPath, "Images", "picture.png");
            if (File.Exists(defaultPath))
            {
                try
                {
                    using (var fs = new FileStream(defaultPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            pbPhoto.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch
                {
                }
            }
            else
            {
                Bitmap bmp = new Bitmap(300, 200);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.LightGray);
                    g.DrawString("Нет фото", new Font("Arial", 16), Brushes.Black, new PointF(100, 80));
                }
                pbPhoto.Image = bmp;
            }
        }
    }
}
