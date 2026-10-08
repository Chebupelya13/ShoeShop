using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class ProductEditForm : Form
    {
        private Product currentProduct;
        private string newPhotoPath = null;

        public ProductEditForm(Product product)
        {
            InitializeComponent();
            currentProduct = product;
        }

        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            AppHelper.SetAppIcon(this);
            LoadDictionaries();

            if (currentProduct != null)
            {
                this.Text = "Редактирование товара";
                txtId.Text = currentProduct.Id.ToString();
                txtTitle.Text = currentProduct.Title;
                txtDescription.Text = currentProduct.Description;
                txtUnit.Text = currentProduct.Unit;
                numCost.Value = currentProduct.Cost;
                numStock.Value = currentProduct.StockQuantity;
                numDiscount.Value = currentProduct.Discount;

                SelectComboBoxItem(cmbCategory, currentProduct.CategoryId);
                SelectComboBoxItem(cmbManufacturer, currentProduct.ManufacturerId);
                SelectComboBoxItem(cmbSupplier, currentProduct.SupplierId);

                if (!string.IsNullOrEmpty(currentProduct.PhotoPath))
                {
                    string fullPath = Path.Combine(Application.StartupPath, currentProduct.PhotoPath);
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
                        }
                        catch
                        {
                        }
                    }
                }
                btnDelete.Visible = true;
            }
            else
            {
                this.Text = "Добавление товара";
                txtId.Text = "Автоматически";
                btnDelete.Visible = false;
            }
        }

        private void LoadDictionaries()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    LoadCombo(conn, "SELECT id, name FROM categories ORDER BY name", cmbCategory);
                    LoadCombo(conn, "SELECT id, name FROM manufacturers ORDER BY name", cmbManufacturer);
                    LoadCombo(conn, "SELECT id, name FROM suppliers ORDER BY name", cmbSupplier);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки справочников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCombo(SqlConnection conn, string query, ComboBox combo)
        {
            using (var cmd = new SqlCommand(query, conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        combo.Items.Add(new DictionaryItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            if (combo.Items.Count > 0)
            {
                combo.SelectedIndex = 0;
            }
        }

        private void SelectComboBoxItem(ComboBox combo, int id)
        {
            foreach (DictionaryItem item in combo.Items)
            {
                if (item.Id == id)
                {
                    combo.SelectedItem = item;
                    break;
                }
            }
        }

        private void BtnSelectPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (pbPhoto.Image != null)
                        {
                            pbPhoto.Image.Dispose();
                            pbPhoto.Image = null;
                        }

                        using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            using (var ms = new MemoryStream())
                            {
                                fs.CopyTo(ms);
                                ms.Position = 0;
                                pbPhoto.Image = Image.FromStream(ms);
                            }
                        }
                        newPhotoPath = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || numCost.Value < 0)
            {
                MessageBox.Show("Заполните обязательные поля и проверьте корректность цены.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string relativePhotoPath = currentProduct?.PhotoPath;

            if (newPhotoPath != null)
            {
                string imagesDir = Path.Combine(Application.StartupPath, "Images");
                if (!Directory.Exists(imagesDir))
                {
                    Directory.CreateDirectory(imagesDir);
                }

                string ext = Path.GetExtension(newPhotoPath);
                string fileName = Guid.NewGuid().ToString() + ext;
                string targetPath = Path.Combine(imagesDir, fileName);

                try
                {
                    using (var fs = new FileStream(newPhotoPath, FileMode.Open, FileAccess.Read))
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            ms.Position = 0;
                            using (Image original = Image.FromStream(ms))
                            {
                                using (Bitmap resized = new Bitmap(original, new Size(300, 200)))
                                {
                                    resized.Save(targetPath);
                                }
                            }
                        }
                    }
                    relativePhotoPath = Path.Combine("Images", fileName);

                    if (currentProduct != null && !string.IsNullOrEmpty(currentProduct.PhotoPath) && !currentProduct.PhotoPath.Contains("picture.png"))
                    {
                        string oldPath = Path.Combine(Application.StartupPath, currentProduct.PhotoPath);
                        if (File.Exists(oldPath))
                        {
                            File.Delete(oldPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения фото: {ex.Message}");
                    return;
                }
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query;
                    if (currentProduct == null)
                    {
                        query = @"INSERT INTO products (title, category_id, description, manufacturer_id, supplier_id, cost, unit, stock_quantity, discount, photo_path) 
                                  VALUES (@title, @cat, @desc, @man, @sup, @cost, @unit, @stock, @disc, @photo)";
                    }
                    else
                    {
                        query = @"UPDATE products SET title=@title, category_id=@cat, description=@desc, manufacturer_id=@man, supplier_id=@sup, 
                                  cost=@cost, unit=@unit, stock_quantity=@stock, discount=@disc, photo_path=@photo WHERE id=@id";
                    }

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        if (currentProduct != null)
                        {
                            cmd.Parameters.AddWithValue("@id", currentProduct.Id);
                        }
                        cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@cat", ((DictionaryItem)cmbCategory.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("@desc", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@man", ((DictionaryItem)cmbManufacturer.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("@sup", ((DictionaryItem)cmbSupplier.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("@cost", numCost.Value);
                        cmd.Parameters.AddWithValue("@unit", txtUnit.Text);
                        cmd.Parameters.AddWithValue("@stock", (int)numStock.Value);
                        cmd.Parameters.AddWithValue("@disc", (int)numDiscount.Value);
                        cmd.Parameters.AddWithValue("@photo", (object)relativePhotoPath ?? DBNull.Value);

                        cmd.ExecuteNonQuery();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentProduct == null)
            {
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM order_items WHERE product_id = @id", conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", currentProduct.Id);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Невозможно удалить товар, так как он присутствует в заказах.", "Запрет", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (MessageBox.Show("Вы уверены, что хотите удалить товар?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (var delCmd = new SqlCommand("DELETE FROM products WHERE id = @id", conn))
                        {
                            delCmd.Parameters.AddWithValue("@id", currentProduct.Id);
                            delCmd.ExecuteNonQuery();
                        }
                        
                        if (pbPhoto.Image != null)
                        {
                            pbPhoto.Image.Dispose();
                            pbPhoto.Image = null;
                        }

                        if (!string.IsNullOrEmpty(currentProduct.PhotoPath) && !currentProduct.PhotoPath.Contains("picture.png"))
                        {
                            string oldPath = Path.Combine(Application.StartupPath, currentProduct.PhotoPath);
                            if (File.Exists(oldPath))
                            {
                                File.Delete(oldPath);
                            }
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
