using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanlybanhang1.Class;

namespace quanlybanhang1
{
    public partial class frmDangNhap : Form
    {
        DataTable tblDn;
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Khởi tạo kết nối đến CSDL
           // Functions.Connect();

            // Lấy dữ liệu từ các textbox
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            // Kiểm tra username và password không được để trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            // Tạo câu lệnh SQL để kiểm tra đăng nhập
            string sql = $"SELECT * FROM tblDangnhap WHERE Username = '{username}' AND Password = '{password}'";

            // Thực hiện truy vấn và lấy dữ liệu
            DataTable dt = Functions.GetDataToTable(sql);

            // Kiểm tra kết quả truy vấn
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Bạn có thể thực hiện các thao tác chuyển form hoặc làm gì đó tại đây
                frmMain frm = new frmMain();
                frm.ShowDialog();

            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }

            // Đóng kết nối
            //Functions.Disconnect();

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
