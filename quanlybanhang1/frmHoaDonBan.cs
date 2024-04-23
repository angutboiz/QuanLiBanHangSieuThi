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
//using COMExcel = Microsoft.Office.Interop.Excel;

namespace quanlybanhang1
{
    public partial class frmHoaDonBan : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = @"SELECT
                                HoaDon.MaHD,
                                KhachHang.TenKH,
                                NhanVien.TenNV,
                                HangHoa.TenHH,                                
                                HoaDon.SoLuong,

                                HoaDon.NgayLap,
                                HoaDon.TongTien,                                
                                HoaDon.ThanhToan,                                
                                HoaDon.isRemove


                            FROM 
                                HoaDon
                            INNER JOIN 
                                KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                            INNER JOIN 
                                NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                            INNER JOIN 
                                HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                            WHERE 
                                ThanhToan = 0 and HoaDon.isRemove = 0";

        int dongia = 0;
        int soluong = 0;
        int giamgia = 0;
        int thanhtien = 0;
        decimal tongtien = 0;

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            if (Functions.DatabaseExists())
            {
                dtpNgayBan.Value = DateTime.Now;

                txtMaKhach.Enabled = false;

                Query(queryTable);
                string queryComboBox = "select manv,tennv from nhanvien";
                FillDataComboBox(queryComboBox,"tennv", cbTenNV);

                queryComboBox = "select mahh,tenhh,soluong from hanghoa where soluong > 0";
                FillDataComboBox(queryComboBox,"tenhh", cbTenHang);

                queryComboBox = "select makh,tenkh from khachhang";
                FillDataComboBox(queryComboBox, "tenkh", cbTenKH);

                cbLocKH.Items.Add("");
                foreach (var item in cbTenKH.Items)
                {
                    cbLocKH.Items.Add(item);
                }
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

                dgvHDBanHang.DataSource = dt;

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
                Query(queryTable);
                if (notify != "") MessageBox.Show(notify);

                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void FillDataComboBox(string query,string name, ComboBox cb)
        {
            cb.Items.Clear();
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();


                SqlCommand sqlCmd = new SqlCommand(query, cnn);
                SqlDataReader sqlReader = sqlCmd.ExecuteReader();



                while (sqlReader.Read())
                {
                    cb.Items.Add(sqlReader[name].ToString());
                }
                cnn.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }
        //hàm này kiểm tra xem người dùng chọn tên nhân viên nào, sau đó lấy ra id phù hợp
        private void CheckIDShowTextBox(string query,ComboBox cb,TextBox txb)
        {
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                cmd = new SqlCommand(query, cnn);

                cmd.Parameters.AddWithValue("@TenNV", cb.SelectedItem.ToString());

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    txb.Text = result.ToString();
                }


                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void CheckIDShowAllTextBox(string query, ComboBox cb, TextBox txb)
        {
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                cmd = new SqlCommand(query, cnn);

                cmd.Parameters.AddWithValue("@TenNV", cb.SelectedItem.ToString());

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    txb.Text = result.ToString();
                }

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtDiaChi.Text = reader["DiaChi"].ToString();
                        txtSoDienThoai.Text = reader["SDT"].ToString();

                       
                    }
                }


                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CountAmount()
        {
            tongtien = 0;
            foreach (DataGridViewRow row in dgvHDBanHang.Rows)
            {
                // Kiểm tra xem dòng đó có phải là dòng dữ liệu hợp lệ hay không
                if (row.IsNewRow) continue;

                // Lấy giá trị từ cột bạn quan tâm (ví dụ, cột có tên "Tổng tiền")
                if (row.Cells["Column7"].Value != null)
                {
                    decimal amount;
                    if (decimal.TryParse(row.Cells["Column7"].Value.ToString(), out amount))
                    {
                        tongtien += amount;
                    }
                }
            }
            txtTongTien.Text = tongtien.ToString();
            if (txtTongTien.Text == "0") return;
            else
            {

                lblBangChu.Text = "Bằng chữ: " + Functions.ConvertMoneyToWords(tongtien);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                int soluong = int.Parse(txtSoLuong.Text);
                int mahang = int.Parse(txbMaHang.Text);
                if (CheckValidation())
                {
                    string query = @"insert into hoadon (makh,manv,mahh,ngaylap,soluong,tongtien) values ('"
                                    +txtMaKhach.Text+"','"+txbMaNV.Text+"','"+txbMaHang.Text+"','"+dtpNgayBan.Value.ToString("yyyy-MM-dd HH:mm:ss")+"','"
                                    +txtSoLuong.Text+"','"+txtThanhTien.Text+"')";
                    ExecCRUD(query,"Thêm thành công Hóa đơn");

                
                    UpdateStockQuantity(soluong,mahang);

                }
                CountAmount();
                Query(queryTable);
                dtpNgayBan.Value = DateTime.Now;
            }
        }

        public void UpdateStockQuantity(int soluong, int mahang)
        {

            string query = @"UPDATE HangHoa
                 SET SoLuong = SoLuong - @SoLuong
                 WHERE MaHH = @MaHH";
            using (cnn = new SqlConnection(connectionString))
            {
                cnn.Open();

                // Update StockQuantity in Product table


                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    cmd.Parameters.AddWithValue("@SoLuong", soluong);
                    cmd.Parameters.AddWithValue("@MaHH", mahang);

                    cmd.ExecuteNonQuery();
                }

                cnn.Close();
            }
        }

        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cbTenNV.Text = "";
            cbTenKH.Text = "";
            txtTongTien.Text = "0";
            cbTenHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";


        }

        private bool CheckValidation()
        {
            if (cbTenHang.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn tên hàng hóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTenHang.Focus();
                return false;
            }
            if (cbTenKH.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải choọn tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTenKH.Focus();
                return false;
            }
            if (cbTenNV.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTenNV.Focus();
                return false;
            }
            return true;
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

     

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {

            string queryName = "select manv from nhanvien where tennv like N'%" + cbTenNV.Text + "%'";
            CheckIDShowTextBox(queryName, cbTenNV, txbMaNV);
        }

        private void cbTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string queryName = "select mahh,GiaBan,soluong from hanghoa where tenhh like N'%" + cbTenHang.Text + "%'";
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                cmd = new SqlCommand(queryName, cnn);

                cmd.Parameters.AddWithValue("@Tenhh", cbTenHang.SelectedItem.ToString());

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    txbMaHang.Text = result.ToString();

                   
                }

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtDonGiaBan.Text = reader["GiaBan"].ToString();
                        txbSLKho.Text = reader["soluong"].ToString();
                    }
                }


                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }

            dongia = int.Parse(txtDonGiaBan.Text);
            soluong = int.Parse(txtSoLuong.Text);
            giamgia = int.Parse(txtGiamGia.Text);
            thanhtien = (soluong * dongia) - ((soluong * dongia * giamgia) / 100);
            txtThanhTien.Text = thanhtien.ToString();

        }

        private void cbTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string queryName = "select makh,diachi,sdt from khachhang where tenkh like N'%" + cbTenKH.Text + "%'";
            try
            {
                cnn = new SqlConnection(connectionString);
                cnn.Open();

                cmd = new SqlCommand(queryName, cnn);

                cmd.Parameters.AddWithValue("@Tenkh", cbTenKH.SelectedItem.ToString());

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    txtMaKhach.Text = result.ToString();
                }

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        txtDiaChi.Text = reader["DiaChi"].ToString();
                        txtSoDienThoai.Text = reader["SDT"].ToString();


                    }
                }


                cnn.Close();

            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());

            }
        }

        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {


            if (txtSoLuong.Text == "")
            {
                return;
            }

            if (cbTenHang.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                int hangton = int.Parse(txbSLKho.Text);
                int soluong = int.Parse(txtSoLuong.Text);
                if (soluong > hangton)
                {
                    MessageBox.Show("Trong kho còn " + hangton + " cái\nVui lòng không nhập quá số lượng trong kho");

                }
                else
                {
                    dongia = int.Parse(txtDonGiaBan.Text);
                    soluong = int.Parse(txtSoLuong.Text);
                    giamgia = int.Parse(txtGiamGia.Text);
                    thanhtien = (soluong * dongia) - ((soluong * dongia * giamgia) / 100);
                    txtThanhTien.Text = thanhtien.ToString(); ;
                }

            }
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            if (cbTenHang.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn tên hàng");
                cbTenHang.SelectedIndex = 0;
            }
            else
            {
                dongia = int.Parse(txtDonGiaBan.Text);
                soluong = int.Parse(txtSoLuong.Text);
                giamgia = int.Parse(txtGiamGia.Text);
                thanhtien = (soluong * dongia) - ((soluong * dongia * giamgia) / 100);
                txtThanhTien.Text = thanhtien.ToString();
            }
        }

        private void dgvHDBanHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMaHDBan.Text = dt.Rows[row][0].ToString();
            cbTenKH.Text = dt.Rows[row][1].ToString();
            cbTenNV.Text = dt.Rows[row][2].ToString();
            cbTenHang.Text = dt.Rows[row][3].ToString();
            txtSoLuong.Text = dt.Rows[row][4].ToString();
            dtpNgayBan.Text = dt.Rows[row][5].ToString();
            txtThanhTien.Text = dt.Rows[row][6].ToString();
            
        }

        private void dgvHDBanHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {

                DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "UPDATE hoadon SET isRemove = 1 WHERE mahd ='" + txtMaHDBan.Text + "'";
                    ExecCRUD(query, "Xóa thành công hóa đơn");

                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string query = @"update hoadon set makh='"+txtMaKhach.Text+"',manv='"+txbMaNV.Text+"',mahh='"+txbMaHang.Text+"',ngaylap='"+ dtpNgayBan.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',soluong='"+txtSoLuong.Text+"',tongtien='"+txtTongTien.Text+"' where mahd='"+txtMaHDBan.Text+"'";
                ExecCRUD(query,"Sửa thành công");        

            }
        }

        private void dgvHDBanHang_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dgvHDBanHang_DataSourceChanged(object sender, EventArgs e)
        {
            CountAmount();
        }

        private void cbLocKH_MouseClick(object sender, MouseEventArgs e)
        {
            cbLocKH.Items.Clear();
            cbLocKH.Items.Add("");
            foreach (var item in cbTenKH.Items)
            {
                cbLocKH.Items.Add(item);
            }
        }

        private void cbLocKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            (dgvHDBanHang.DataSource as DataTable).DefaultView.RowFilter = string.Format("tenkh LIKE '%{0}%'", cbLocKH.Text);

        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string query = @"update hoadon set thanhtoan = 1 where mahd='"+txtMaHDBan.Text+"'";
                ExecCRUD(query, "Thanh toán thành công hóa đơn: "+cbTenHang.Text+"\nKhách hàng tên: "+cbTenKH.Text+"\nSố tiền: "+ string.Format("{0:#,##0}", thanhtien));
            }
        }

        private void dgvHDBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
