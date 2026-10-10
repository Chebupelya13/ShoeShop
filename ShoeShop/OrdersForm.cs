using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class OrdersForm : Form
    {
        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            AppHelper.SetAppIcon(this);
            AppHelper.ApplyTheme(this);
            if (LoginForm.CurrentUser.RoleId == 4)
            {
                btnAddOrder.Visible = true;
            }
            else
            {
                btnAddOrder.Visible = false;
            }

            LoadData();
        }

        private void LoadData()
        {
            flpOrders.Controls.Clear();
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT o.id, o.order_article, o.status_id, s.name, 
                               o.pickup_point_id, p.address, o.order_date, o.delivery_date
                        FROM orders o
                        JOIN order_statuses s ON o.status_id = s.id
                        JOIN pickup_points p ON o.pickup_point_id = p.id
                        ORDER BY o.order_date DESC
                    ";

                    using (var cmd = new SqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var order = new Order
                                {
                                    Id = reader.GetInt32(0),
                                    OrderArticle = reader.GetString(1),
                                    StatusId = reader.GetInt32(2),
                                    StatusName = reader.GetString(3),
                                    PickupPointId = reader.GetInt32(4),
                                    PickupPointAddress = reader.GetString(5),
                                    OrderDate = reader.GetDateTime(6),
                                    DeliveryDate = reader.GetDateTime(7)
                                };

                                OrderCard card = new OrderCard();
                                card.BindData(order);

                                if (LoginForm.CurrentUser.RoleId == 4)
                                {
                                    card.Cursor = Cursors.Hand;
                                    card.Click += (s, ev) => EditOrder(order);
                                    foreach (Control c in card.Controls)
                                    {
                                        c.Click += (s, ev) => EditOrder(order);
                                    }
                                }

                                flpOrders.Controls.Add(card);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}");
            }
        }

        private void BtnAddOrder_Click(object sender, EventArgs e)
        {
            OrderEditForm form = new OrderEditForm(null);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void EditOrder(Order o)
        {
            OrderEditForm form = new OrderEditForm(o);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
