using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlybanhang1.Class;

namespace quanlybanhang1
{
    public partial class frmDangNhap : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            frmMain frm = new frmMain();

            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            } 
            else {

                string query = $"SELECT role FROM nhanvien WHERE Username = '{username}' AND Password = '{password}'";

                try
                {
                    cnn = new SqlConnection(connectionString);
                    cnn.Open();

                    da = new SqlDataAdapter(query, cnn);
                    dt = new DataTable();
                    da.Fill(dt);

                    

                    if (dt.Rows.Count > 0)
                    {
                        DataRow datarow = dt.Rows[0];
                        string role = datarow["Role"].ToString();
                        frm.Role = role;
                        frm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
                    }

                    cnn.Close();

                }
                catch (Exception es)
                {
                    MessageBox.Show(es.ToString());
                }
            }
        }
    }
}
