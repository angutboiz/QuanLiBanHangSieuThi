using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using quanlybanhang1.Class;

namespace quanlybanhang1
{
    public partial class frmDMHang : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = "select mahh,tenhh,gianhap,giaban,soluong,ghichu,hinhanh from hanghoa where isRemove = 0";
        public frmDMHang()
        {
            InitializeComponent();
        }

        private void frmDMHang_Load(object sender, EventArgs e)
        {
            if (Functions.DatabaseExists())
            {
                txtMaHang.Enabled = false;
                btnBoQua.Enabled = false;

                Query(queryTable);
            }
            else
            {
                MessageBox.Show("Cơ sở dữ liệu không tồn tại hoặc không thể kết nối.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void ResetValues()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
           
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            
            txtGhiChu.Text = "";
            picAnh.Image = null;
            txtAnh.Text = "";
        }


        private void Query(string query)
        {
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                da = new SqlDataAdapter(query, cnn);
                dt = new DataTable();
                da.Fill(dt);

                dgvHang.DataSource = dt;

                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void ExecCRUD(string query, string notify)
        {
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                cmd = new SqlCommand(query, cnn);

                cmd.ExecuteNonQuery();

                if (notify != "") MessageBox.Show(notify);

                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string query = "INSERT INTO hanghoa (tenhh,gianhap,giaban,soluong,ghichu,hinhanh) VALUES(N'" + txtTenHang.Text +
                    "'," + float.Parse(txtDonGiaNhap.Text) + "," + float.Parse(txtDonGiaBan.Text) +
                    "," + int.Parse(txtSoLuong.Text) + ",'" + txtGhiChu.Text + "',N'" + txtAnh.Text.Trim() + "')";

                ExecCRUD(query, "Thêm thành công Mặt hàng: " +txtTenHang.Text);
                Query(queryTable);
                ResetValues();

                btnThem.Enabled = true;
                btnSua.Enabled = true;
                btnBoQua.Enabled = false;
                txtMaHang.Enabled = false;
            }

           /* btnSua.Enabled = false;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;*/
        }

        private bool CheckValidation()
        {
            if (txtTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenHang.Focus();
                return false;
            }

            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return false;

            }
            return true;

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
           
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
           
           if (CheckValidation())
            {
                string query = "UPDATE hanghoa SET tenhh=N'" + txtTenHang.Text.Trim() +
                "', gianhap=" + float.Parse(txtDonGiaNhap.Text) +
                ", giaban=" + float.Parse(txtDonGiaBan.Text) +
                ", SoLuong=" + int.Parse(txtSoLuong.Text) +
                ", ghichu=N'" + txtGhiChu.Text +
                "', hinhanh='" + txtAnh.Text +
                "' WHERE mahh=N'" + txtMaHang.Text + "'";

                ExecCRUD(query,"Sửa thành công Mặt hàng: "+ txtTenHang.Text);
                Query(queryTable);
                ResetValues();
                btnBoQua.Enabled = false;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            string sql;
          
            if (txtMaHang.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblHang WHERE MaHang=N'" + txtMaHang.Text + "'";
                Functions.RunSqlDel(sql);
                ResetValues();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            string maHang = txtMaHang.Text.Trim();  // Lấy mã hàng từ textbox
            string tenHang = txtTenHang.Text.Trim();  // Lấy tên hàng từ textbox

            // Xây dựng câu truy vấn SQL dựa trên mã hàng và tên hàng
            sql = "SELECT * FROM tblHang WHERE 1=1";
            if (maHang != "")
                sql += " AND MaHang LIKE '%" + maHang + "%'";
            if (tenHang != "")
                sql += " AND TenHang LIKE N'%" + tenHang + "%'";

            // Gọi phương thức GetDataToTable từ class Functions để thực hiện truy vấn
            DataTable dt = Functions.GetDataToTable(sql);
            // Gán dữ liệu vào DataGridView
            dgvHang.DataSource = dt;
            // Kiểm tra nếu không tìm thấy dữ liệu
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy hàng hóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + dt.Rows.Count + " hàng hóa được tìm thấy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaHang_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "UPDATE hanghoa SET isRemove = 1 WHERE mahh ='" + txtMaHang.Text + "'";
                    ExecCRUD(query, "Xóa thành công Khách hàng: " + txtTenHang.Text);
                    Query(queryTable);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }

        private void dgvHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMaHang.Text = dt.Rows[row][0].ToString();
            txtTenHang.Text = dt.Rows[row][1].ToString();
            txtDonGiaNhap.Text = dt.Rows[row][2].ToString();
            txtDonGiaBan.Text = dt.Rows[row][3].ToString();
            txtSoLuong.Text = dt.Rows[row][4].ToString();
            txtGhiChu.Text = dt.Rows[row][5].ToString();
            txtAnh.Text = dt.Rows[row][6].ToString();
            picAnh.ImageLocation = txtAnh.Text;
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnSua.Enabled = true;

        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

        private void txtDonGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

        private void txtDonGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dgvHang.DataSource as DataTable).DefaultView.RowFilter = string.Format("tenhh LIKE '%{0}%'", txtSearch.Text);

        }
    }
}
