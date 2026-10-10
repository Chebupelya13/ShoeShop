using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class OrderEditForm : Form
    {
        private Order currentOrder;

        public OrderEditForm(Order order)
        {
            InitializeComponent();
            currentOrder = order;
        }

        private void OrderEditForm_Load(object sender, EventArgs e)
        {
            AppHelper.SetAppIcon(this);
            AppHelper.ApplyTheme(this);
            LoadDictionaries();

            if (currentOrder != null)
            {
                this.Text = "Редактирование заказа";
                txtArticle.Text = currentOrder.OrderArticle;
                SelectComboBoxItem(cmbStatus, currentOrder.StatusId);
                SelectComboBoxItem(cmbPickupPoint, currentOrder.PickupPointId);
                dtpOrderDate.Value = currentOrder.OrderDate;
                dtpDeliveryDate.Value = currentOrder.DeliveryDate;

                LoadOrderItems();
                btnDelete.Visible = true;
            }
            else
            {
                this.Text = "Добавление заказа";
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
                    LoadCombo(conn, "SELECT id, name FROM order_statuses", cmbStatus);
                    LoadCombo(conn, "SELECT id, address FROM pickup_points", cmbPickupPoint);
                    LoadProductColumn(conn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
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

        private void LoadProductColumn(SqlConnection conn)
        {
            var products = new List<DictionaryItem>();
            using (var cmd = new SqlCommand("SELECT id, title FROM products ORDER BY title", conn))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new DictionaryItem { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            ProductCol.DataSource = products;
            ProductCol.DisplayMember = "Name";
            ProductCol.ValueMember = "Id";
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

        private void LoadOrderItems()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT product_id, count FROM order_items WHERE order_id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", currentOrder.Id);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dgvItems.Rows.Add(reader.GetInt32(0), reader.GetInt32(1));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Введите артикул.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpDeliveryDate.Value.Date < dtpOrderDate.Value.Date)
            {
                MessageBox.Show("Дата доставки не может быть меньше даты заказа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool hasItems = false;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                
                if (row.Cells[0].Value == null || row.Cells[1].Value == null)
                {
                    continue;
                }

                if (!int.TryParse(row.Cells[1].Value.ToString(), out int count) || count <= 0)
                {
                    MessageBox.Show("Количество товара должно быть больше нуля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                hasItems = true;
            }

            if (!hasItems)
            {
                MessageBox.Show("Заказ должен содержать хотя бы один товар.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            int orderId;
                            if (currentOrder == null)
                            {
                                using (var cmd = new SqlCommand("INSERT INTO orders (order_article, status_id, pickup_point_id, order_date, delivery_date) VALUES (@art, @status, @point, @odate, @ddate); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@art", txtArticle.Text);
                                    cmd.Parameters.AddWithValue("@status", ((DictionaryItem)cmbStatus.SelectedItem).Id);
                                    cmd.Parameters.AddWithValue("@point", ((DictionaryItem)cmbPickupPoint.SelectedItem).Id);
                                    cmd.Parameters.AddWithValue("@odate", dtpOrderDate.Value.Date);
                                    cmd.Parameters.AddWithValue("@ddate", dtpDeliveryDate.Value.Date);
                                    orderId = (int)cmd.ExecuteScalar();
                                }
                            }
                            else
                            {
                                orderId = currentOrder.Id;
                                using (var cmd = new SqlCommand("UPDATE orders SET order_article=@art, status_id=@status, pickup_point_id=@point, order_date=@odate, delivery_date=@ddate WHERE id=@id", conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@id", orderId);
                                    cmd.Parameters.AddWithValue("@art", txtArticle.Text);
                                    cmd.Parameters.AddWithValue("@status", ((DictionaryItem)cmbStatus.SelectedItem).Id);
                                    cmd.Parameters.AddWithValue("@point", ((DictionaryItem)cmbPickupPoint.SelectedItem).Id);
                                    cmd.Parameters.AddWithValue("@odate", dtpOrderDate.Value.Date);
                                    cmd.Parameters.AddWithValue("@ddate", dtpDeliveryDate.Value.Date);
                                    cmd.ExecuteNonQuery();
                                }
                                
                                using (var delCmd = new SqlCommand("DELETE FROM order_items WHERE order_id=@id", conn, tran))
                                {
                                    delCmd.Parameters.AddWithValue("@id", orderId);
                                    delCmd.ExecuteNonQuery();
                                }
                            }

                            foreach (DataGridViewRow row in dgvItems.Rows)
                            {
                                if (row.IsNewRow)
                                {
                                    continue;
                                }

                                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                                {
                                    int pId = Convert.ToInt32(row.Cells[0].Value);
                                    int count = Convert.ToInt32(row.Cells[1].Value);

                                    using (var insCmd = new SqlCommand("INSERT INTO order_items (order_id, product_id, count) VALUES (@oid, @pid, @cnt)", conn, tran))
                                    {
                                        insCmd.Parameters.AddWithValue("@oid", orderId);
                                        insCmd.Parameters.AddWithValue("@pid", pId);
                                        insCmd.Parameters.AddWithValue("@cnt", count);
                                        insCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            tran.Commit();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw new Exception("Ошибка транзакции: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (currentOrder == null)
            {
                return;
            }

            if (MessageBox.Show("Удалить заказ и все его товары?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("DELETE FROM orders WHERE id = @id", conn))
                        {
                            cmd.Parameters.AddWithValue("@id", currentOrder.Id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
