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
                                KhachHang.TenKH,
                                NhanVien.TenNV,
                                HangHoa.TenHH,
                                HoaDon.NgayLap,
                                HoaDon.SoLuong,
                                HoaDon.TongTien,
                                HoaDon.ThanhToan
                            FROM 
                                HoaDon
                            INNER JOIN 
                                KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                            INNER JOIN 
                                NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                            INNER JOIN 
                                HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                            WHERE 
                                ThanhToan = 0;";

        int dongia = 0;
        int soluong = 0;
        int giamgia = 0;
        int thanhtien = 0;

        public frmHoaDonBan()
        {
            InitializeComponent();
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            if (Functions.DatabaseExists())
            {
                txtMaKhach.Enabled = false;

                Query(queryTable);
                string queryComboBox = "select manv,tennv from nhanvien";
                FillDataComboBox(queryComboBox,"tennv", cbTenNV);

                queryComboBox = "select mahh,tenhh from hanghoa";
                FillDataComboBox(queryComboBox,"tenhh", cbTenHang);

                queryComboBox = "select makh,tenkh from khachhang";
                FillDataComboBox(queryComboBox, "tenkh", cbTenKH);
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }


        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cbTenNV.Text = "";
            //cboMaKhach.Text = "";
            txtTongTien.Text = "0";
            lblBangChu.Text = "Bằng chữ: ";
            cbTenHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";


        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            double sl, SLcon, tong, Tongmoi;
            sql = "SELECT MaHDBan FROM tblHDBan WHERE MaHDBan=N'" + txtMaHDBan.Text + "'";
            if (!Functions.CheckKey(sql))
            {
                
               /* if (cboMaNhanVien.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaNhanVien.Focus();
                    return;
                }
                if (cboMaKhach.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboMaKhach.Focus();
                    return;
                }
                sql = "INSERT INTO tblHDBan(MaHDBan, NgayBan, MaNhanVien, MaKhach, TongTien) VALUES (N'" + txtMaHDBan.Text.Trim() + "','" +
                        dtpNgayBan.Value + "',N'" + cboMaNhanVien.SelectedValue + "',N'" +
                        cboMaKhach.SelectedValue + "'," + txtTongTien.Text + ")";
                Functions.RunSQL(sql);*/
            }
            // Lưu thông tin của các mặt hàng
            if (cbTenHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbTenHang.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MaHang FROM tblChiTietHDBan WHERE MaHang=N'" + cbTenHang.SelectedValue + "' AND MaHDBan = N'" + txtMaHDBan.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cbTenHang.Focus();
                return;
            }
            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(Functions.GetFieldValues("SELECT SoLuong FROM tblHang WHERE MaHang = N'" + cbTenHang.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            sql = "INSERT INTO tblChiTietHDBan(MaHDBan,MaHang,SoLuong,DonGia, GiamGia,ThanhTien) VALUES(N'" + txtMaHDBan.Text.Trim() + "',N'" + cbTenHang.SelectedValue + "'," + txtSoLuong.Text + "," + txtDonGiaBan.Text + "," + txtGiamGia.Text + "," + txtThanhTien.Text + ")";
            //Functions.RunSQL(sql);
            // Cập nhật lại số lượng của mặt hàng vào bảng tblHang
            SLcon = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tblHang SET SoLuong =" + SLcon + " WHERE MaHang= N'" + cbTenHang.SelectedValue + "'";
           // Functions.RunSQL(sql);
            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(Functions.GetFieldValues("SELECT TongTien FROM tblHDBan WHERE MaHDBan = N'" + txtMaHDBan.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tblHDBan SET TongTien =" + Tongmoi + " WHERE MaHDBan = N'" + txtMaHDBan.Text + "'";
           // Functions.RunSQL(sql);
            txtTongTien.Text = Tongmoi.ToString();
            
            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnInHoaDon.Enabled = true;
        }


        private void ResetValuesHang()
        {

            cbTenHang.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)8))
                e.Handled = true;
        }

        private void cboMaHDBan_DropDown(object sender, EventArgs e)
        {
            Functions.FillCombo("SELECT MaHDBan FROM tblHDBan", cboMaHDBan, "MaHDBan", "MaHDBan");
            cboMaHDBan.SelectedIndex = -1;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGiaBan.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGiaBan.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cboMaKhach_TextChanged(object sender, EventArgs e)
        {
           /* string str;
            if (cboMaKhach.Text == "")
            {
                txtTenKhach.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TenKhach from tblKhach where MaKhach = N'" + cboMaKhach.SelectedValue + "'";
            txtTenKhach.Text = Functions.GetFieldValues(str);
            str = "Select DiaChi from tblKhach where MaKhach = N'" + cboMaKhach.SelectedValue + "'";
            txtDiaChi.Text = Functions.GetFieldValues(str);
            str = "Select DienThoai from tblKhach where MaKhach= N'" + cboMaKhach.SelectedValue + "'";
            txtDienThoai.Text = Functions.GetFieldValues(str);*/
        }

        private void cboMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            /*string str;
            if (cboMaNhanVien.Text == "")
                text.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select MaNhanVien from tblNhanVien where MaNhanVien =N'" + cboMaNhanVien.SelectedValue + "'";
            text.Text = Functions.GetFieldValues(str);*/
        }

        private void cbTenNV_SelectedIndexChanged(object sender, EventArgs e)
        {

            string queryName = "select manv from nhanvien where tennv like N'%" + cbTenNV.Text + "%'";
            CheckIDShowTextBox(queryName, cbTenNV, txbMaNV);
        }

        private void cbTenHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string queryName = "select mahh,GiaNhap from hanghoa where tenhh like N'%" + cbTenHang.Text + "%'";
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
                        txtDonGiaBan.Text = reader["GiaNhap"].ToString();
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
                thanhtien = (soluong * dongia) - ((soluong * dongia * giamgia)/100);
                txtThanhTien.Text = thanhtien.ToString(); ;

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
    }
}
