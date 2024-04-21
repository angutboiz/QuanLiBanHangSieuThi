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
using System.Collections;
using System.Data.Linq;

namespace quanlybanhang1
{
    public partial class frmDMNhanvien : Form
    {
        SqlConnection cnn; 
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = "select manv,tennv,gioitinh,sdt,ngaysinh,diachi,username,password from nhanvien where isremove = 0";

        public frmDMNhanvien()
        {
            InitializeComponent();
        }

        private void frmDMNhanvien_Load(object sender, EventArgs e)
        {
            if (Functions.DatabaseExists())
            {
                txtMaNhanVien.Enabled = false;
                btnBoQua.Enabled = false;
                cbSex.SelectedIndex = 0;
                Query(queryTable);
            }
            else
            {
                MessageBox.Show("Cơ sở dữ liệu không tồn tại hoặc không thể kết nối.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
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

                dgvNhanVien.DataSource = dt;

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
                //kiểm tra xem tên đăng nhập này có chưa, không được trùng
                string sqlValidation = "SELECT username FROM NhanVien WHERE username=N'" + txtUser.Text.Trim() + "'";
                if (CheckKey(sqlValidation))
                {
                    MessageBox.Show("Tên đăng nhập này đã có, bạn phải nhập khác tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUser.Focus();
                    txtUser.Text = "";
                }
                else
                {
                    string query = "INSERT INTO NHANVIEN (TenNV,GioiTinh,SDT,NgaySinh,DiaChi,Username,Password) VALUES (N'"
                    +txtTenNhanVien.Text+"',N'"+cbSex.Text+"','"+mtbDienThoai.Text+"','"+dtpNgaySinh.Value.ToString("yyyy-MM-dd")+"',N'"+txtDiaChi.Text+"','"+txtUser.Text+"','"+txtPass.Text+"')";
            
                    ExecCRUD(query,"Thêm thành công nhân viên: "+txtTenNhanVien.Text);
                    Query(queryTable);
                    btnSua.Enabled = false;
                    btnBoQua.Enabled = true;
                    btnThem.Enabled = false;
                    ResetValues();
                    txtMaNhanVien.Enabled = false;
                    txtMaNhanVien.Focus();
                }
            }
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            cbSex.SelectedItem = 0;
            txtDiaChi.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            mtbDienThoai.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            btnSua.Text = "Sửa NV: ";

        }

        //kiểm tra các textbox có bỏ trống hay không?
        private bool CheckValidation()
        {
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return false;
            }
            /*if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }*/
            if (mtbDienThoai.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoai.Focus();
                return false;
            }


            if (cbSex.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return false;
            }

            if (txtUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải tên đăng nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return false;
            }

            if (txtPass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return false;
            }
           
            return true;
        }

        public bool CheckKey(string sqlQuery)
        {
            bool exists = false;

            using (cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                cmd = new SqlCommand(sqlQuery, cnn);
                using (dr = cmd.ExecuteReader()) if (dr.HasRows) exists = true;
            }

            return exists;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string query = "UPDATE nhanvien SET  TenNV=N'" + txtTenNhanVien.Text.Trim().ToString() +
                        "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                        "',SDT='" + mtbDienThoai.Text.ToString() + "',GioiTinh=N'" + cbSex.Text.Trim() +
                        "',NgaySinh='" + dtpNgaySinh.Value.ToString("yyyy-MM-dd") +
                        "',Username='" + txtUser.Text +
                        "',Password='" + txtPass.Text +
                        "' WHERE MaNV=N'" + txtMaNhanVien.Text + "'";
                //Functions.RunSQL(query);
                //LoadDataGridView();
                ExecCRUD(query,"Cập nhật thành công NV: "+ txtTenNhanVien.Text);
                Query(queryTable);
                ResetValues();
                btnBoQua.Enabled = false;
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            txtMaNhanVien.Enabled = false;
        }

        private void txtMaNhanVien_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            string maNhanVien = txtMaNhanVien.Text.Trim();  // Lấy mã hàng từ textbox
            string tenNhanVien = txtTenNhanVien.Text.Trim();  // Lấy tên hàng từ textbox

            // Xây dựng câu truy vấn SQL dựa trên mã hàng và tên hàng
            sql = "SELECT * FROM nhanvien WHERE 1=1";
            if (maNhanVien != "")
                sql += " AND manv LIKE '%" + maNhanVien + "%'";
            if (tenNhanVien != "")
                sql += " AND tennv LIKE N'%" + tenNhanVien + "%'";

            // Gọi phương thức GetDataToTable từ class Functions để thực hiện truy vấn
            DataTable dt = Functions.GetDataToTable(sql);
            // Gán dữ liệu vào DataGridView
            dgvNhanVien.DataSource = dt;
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

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvNhanVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //bấm vào các dòng trên bảng để fill data vô textbox


            int row = e.RowIndex;
            txtMaNhanVien.Text = dt.Rows[row][0].ToString();
            txtTenNhanVien.Text = dt.Rows[row][1].ToString();
            cbSex.Text = dt.Rows[row][2].ToString();
            mtbDienThoai.Text = dt.Rows[row][3].ToString();
            dtpNgaySinh.Text = dt.Rows[row][4].ToString();
            txtDiaChi.Text = dt.Rows[row][5].ToString();
            txtUser.Text = dt.Rows[row][6].ToString();
            txtPass.Text = dt.Rows[row][7].ToString();

            btnSua.Enabled = true;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            btnSua.Text = "Sửa NV: " +txtTenNhanVien.Text;

        }

        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            (dgvNhanVien.DataSource as DataTable).DefaultView.RowFilter = string.Format("tennv LIKE '%{0}%'", txbSearch.Text);

        }

        private void dgvNhanVien_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "UPDATE nhanvien SET isRemove = 1 WHERE manv ='" + txtMaNhanVien.Text + "'";
                    ExecCRUD(query, "Xóa thành công nhân viên: " + txtTenNhanVien.Text);

                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }
    }
}
