using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using ShoeShop.Models;

namespace ShoeShop
{
    public partial class LoginForm : Form
    {
        public static User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            this.Load += (s, e) => {
                AppHelper.SetAppIcon(this);
                AppHelper.ApplyTheme(this);
            };
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hash = DatabaseHelper.HashPassword(password);

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT u.id, u.login, u.full_name, u.role_id, r.name FROM users u JOIN roles r ON u.role_id = r.id WHERE u.login = @login AND u.password_hash = @hash", conn))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@hash", hash);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CurrentUser = new User
                                {
                                    Id = reader.GetInt32(0),
                                    Login = reader.GetString(1),
                                    FullName = reader.GetString(2),
                                    RoleId = reader.GetInt32(3),
                                    RoleName = reader.GetString(4)
                                };

                                OpenMainForm();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль.", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuest_Click(object sender, EventArgs e)
        {
            CurrentUser = new User
            {
                Id = 0,
                Login = "guest",
                FullName = "Гость",
                RoleId = 1,
                RoleName = "Гость"
            };
            OpenMainForm();
        }

        private void OpenMainForm()
        {
            MainForm mainForm = new MainForm();
            this.Hide();
            mainForm.ShowDialog();
            
            this.Show();
            txtLogin.Clear();
            txtPassword.Clear();
            CurrentUser = null;
        }
    }
}
