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
        }

        public void BindData(Product product)
        {
            ProductData = product;

            lblTitle.Text = product.Title;
            lblDescription.Text = product.Description;
            lblManufacturer.Text = product.ManufacturerName;
            lblStock.Text = $"На складе: {product.StockQuantity}";

            if (product.Discount > 0)
            {
                lblCost.Text = product.Cost.ToString("C2");
                lblCost.Font = new Font(lblCost.Font, FontStyle.Strikeout);
                lblCost.ForeColor = Color.Red;
                
                lblDiscountCost.Text = product.DiscountedCost.ToString("C2");
                lblDiscountCost.Visible = true;
                
                lblDiscount.Text = $"{product.Discount}%";
                lblDiscount.Visible = true;
            }
            else
            {
                lblCost.Text = product.Cost.ToString("C2");
                lblCost.Font = new Font(lblCost.Font, FontStyle.Regular);
                lblCost.ForeColor = Color.Black;
                
                lblDiscountCost.Visible = false;
                lblDiscount.Visible = false;
            }

            if (product.StockQuantity == 0)
            {
                this.BackColor = Color.LightBlue;
            }
            else if (product.Discount > 15)
            {
                this.BackColor = Color.FromArgb(46, 139, 87);
                SetForeColor(Color.White);
            }
            else
            {
                this.BackColor = SystemColors.Control;
                SetForeColor(Color.Black);
            }

            if (!string.IsNullOrEmpty(product.PhotoPath) && File.Exists(Path.Combine(Application.StartupPath, product.PhotoPath)))
            {
                try { pbPhoto.Image = Image.FromFile(Path.Combine(Application.StartupPath, product.PhotoPath)); }
                catch { LoadDefaultImage(); }
            }
            else
            {
                LoadDefaultImage();
            }
        }

        private void SetForeColor(Color color)
        {
            lblTitle.ForeColor = color;
            lblDescription.ForeColor = color;
            lblManufacturer.ForeColor = color;
            lblStock.ForeColor = color;
            if (color == Color.White && lblCost.ForeColor == Color.Black)
                lblCost.ForeColor = color; 
            lblDiscountCost.ForeColor = color;
            lblDiscount.ForeColor = color;
        }

        private void LoadDefaultImage()
        {
            try
            {
                string defaultPath = Path.Combine(Application.StartupPath, "Images", "picture.png");
                if (File.Exists(defaultPath))
                    pbPhoto.Image = Image.FromFile(defaultPath);
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
            catch { }
        }
    }
}
