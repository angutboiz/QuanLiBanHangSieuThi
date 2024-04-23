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

        string queryTable = "select manv,tennv,gioitinh,sdt,ngaysinh,diachi,username,password,role from nhanvien where isremove = 0 and role != 'admin'";

        public frmDMNhanvien()
        {
            InitializeComponent();
        }

        private void frmDMNhanvien_Load(object sender, EventArgs e)
        {
            ResetValues();
            if (Functions.DatabaseExists())
            {
                txtMaNhanVien.Enabled = false;
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
                    string query = "INSERT INTO NHANVIEN (TenNV,GioiTinh,SDT,NgaySinh,DiaChi,Username,Password,role) VALUES (N'"
                    +txtTenNhanVien.Text+"',N'"+cbSex.Text+"','"+txbDienThoai.Text+"','"+dtpNgaySinh.Value.ToString("yyyy-MM-dd")+"',N'"+txtDiaChi.Text+"','"+txtUser.Text+"','"+txtPass.Text+"','"+cbRole.Text+"')";
            
                    ExecCRUD(query,"Thêm thành công nhân viên: "+txtTenNhanVien.Text);
                    Query(queryTable);
                    
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
            txbDienThoai.Text = "";
            txtUser.Text = "";
            txtPass.Text = "";
            btnSua.Text = "Sửa NV: ";
            cbRole.SelectedIndex = 0;
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
            if (txbDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txbDienThoai.Focus();
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
                        "',SDT='" + txbDienThoai.Text.ToString() + "',GioiTinh=N'" + cbSex.Text.Trim() +
                        "',NgaySinh='" + dtpNgaySinh.Value.ToString("yyyy-MM-dd") +
                        "',Username='" + txtUser.Text +
                        "',Password='" + txtPass.Text +
                        "',Role='" + cbRole.Text +
                        "' WHERE MaNV=N'" + txtMaNhanVien.Text + "'";
                //Functions.RunSQL(query);
                //LoadDataGridView();
                ExecCRUD(query,"Cập nhật thành công NV: "+ txtTenNhanVien.Text);
                Query(queryTable);
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
           
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
        private void dgvNhanVien_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //bấm vào các dòng trên bảng để fill data vô textbox


            int row = e.RowIndex;
            txtMaNhanVien.Text = dt.Rows[row][0].ToString();
            txtTenNhanVien.Text = dt.Rows[row][1].ToString();
            cbSex.Text = dt.Rows[row][2].ToString();
            txbDienThoai.Text = dt.Rows[row][3].ToString();
            dtpNgaySinh.Text = dt.Rows[row][4].ToString();
            txtDiaChi.Text = dt.Rows[row][5].ToString();
            txtUser.Text = dt.Rows[row][6].ToString();
            txtPass.Text = dt.Rows[row][7].ToString();
            cbRole.Text = dt.Rows[row][8].ToString();


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

        private void txbDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }
    }
}
