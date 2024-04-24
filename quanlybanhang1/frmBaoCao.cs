using quanlybanhang1.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlybanhang1
{
    public partial class frmBaoCao : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = @"SELECT
                                KhachHang.TenKH as ""Tên khách hàng"",
                                NhanVien.TenNV as ""Tên nhân viên"",
                                HangHoa.TenHH as ""Tên hàng hóa"",                                
                                FORMAT(HangHoa.GiaNhap, '#,##0') as ""Giá nhập"",
                                FORMAT(HangHoa.GiaBan, '#,##0') as ""Giá bán"",                                
                                HoaDon.NgayLap as ""Ngày lập"",
                                HoaDon.SoLuong as ""Số lượng"",
                                FORMAT(HoaDon.TongTien, '#,##0') as ""Tổng tiền""                                 


                            FROM 
                                HoaDon
                            INNER JOIN 
                                KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                            INNER JOIN 
                                NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                            INNER JOIN 
                                HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                            WHERE 
                                ThanhToan = 1 and HoaDon.isRemove = 0";
        public frmBaoCao()
        {
            InitializeComponent();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {

            Query(queryTable);
            CountAmount();

        }

        private void CountAmount()
        {
            decimal chi = 0;
            decimal loinhuan = 0;

            decimal tongtienRow = 0;
            decimal gianhap = 0;
            decimal giaban = 0;
            int soluong = 0;

            decimal tongtien = 0;
            decimal tongchi = 0;
            decimal tongloinhuan = 0;

            foreach (DataGridViewRow row in dgvBaoCao.Rows)
            {
                //Kiểm tra xem dòng đó có phải là dòng dữ liệu hợp lệ hay không
                if (row.IsNewRow) continue;

                tongtienRow = decimal.Parse(Functions.RemoveDot(row.Cells["Tổng tiền"].Value.ToString()));
                gianhap = decimal.Parse(Functions.RemoveDot(row.Cells["Giá nhập"].Value.ToString()));
                giaban = decimal.Parse(Functions.RemoveDot(row.Cells["Giá bán"].Value.ToString()));
                soluong = int.Parse(Functions.RemoveDot(row.Cells["Số lượng"].Value.ToString()));

                chi = tongtienRow - (gianhap * soluong);
                loinhuan = tongtienRow - chi;

                tongchi += chi;
                tongloinhuan += loinhuan;
                tongtien += tongtienRow;
                 //MessageBox.Show("Giá nhập: " + gianhap + "\nGía bán: " + giaban + "\nSo luog: " + soluong +"\nTong tien: "+tongtienRow+ "\nChi: " + chi + "\nloi nhuan: " + loinhuan);
            }




            txbDoanhThu.Text = string.Format("{0:#,##0}", tongtien);

            txbChi.Text = string.Format("{0:#,##0}", tongloinhuan);

            txbLoiNhuan.Text = string.Format("{0:#,##0}", tongchi);

            if (tongtien > 0)
            {
                lbDoanhThu.Text = "Bằng chữ: " + Functions.ConvertMoneyToWords(tongtien);

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

                dgvBaoCao.DataSource = dt;

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

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton rb)
                {
                    rb.Checked = false;
                }
            }

            Query(queryTable);
        }

        private void rbYearMuch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbYearMuch.Checked)
            {
                string query = @"SELECT
                                    YEAR(hoadon.NgayLap) AS ""Năm"",
                                    FORMAT(SUM(hoadon.TongTien), '#,##0') AS ""Tổng tiền năm kiếm được nhiều nhất""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                                INNER JOIN 
                                    NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                                INNER JOIN 
                                    HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0
                                GROUP BY 
                                    YEAR(hoadon.NgayLap)
                                ORDER BY 
                                    ""Tổng tiền năm kiếm được nhiều nhất"" DESC";

                Query(query);
            } 

        }

        private void rbMonthMuch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonthMuch.Checked)
            {
                string query = @"SELECT
                                    MONTH(hoadon.NgayLap) AS ""Tháng nhiều nhất"",
                                    YEAR(hoadon.NgayLap) AS ""Năm"",
                                    FORMAT(SUM(hoadon.TongTien), '#,##0') AS ""Tổng tiền tháng kiếm được""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                                INNER JOIN 
                                    NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                                INNER JOIN 
                                    HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0
                                GROUP BY 
                                    MONTH(hoadon.NgayLap),Year(hoadon.NgayLap)
                                ORDER BY 
                                    ""Tổng tiền tháng kiếm được"" DESC";

                Query(query);
            }
           


        }

        private void rbNVMuch_CheckedChanged(object sender, EventArgs e)
        {
            string query;
            if (rbYearMuch.Checked)
            {
                query = @"SELECT
                                    MONTH(hoadon.NgayLap) AS ""Tháng nhiều nhất"",
                                    SUM(hoadon.TongTien) AS ""Tổng tiền tháng kiếm được""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    KhachHang ON HoaDon.MaKH = KhachHang.MaKH
                                INNER JOIN 
                                    NhanVien ON HoaDon.MaNV = NhanVien.MaNV
                                INNER JOIN 
                                    HangHoa ON HoaDon.MaHH = HangHoa.MaHH
                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0
                                GROUP BY 
                                    MONTH(hoadon.NgayLap)
                                ORDER BY 
                                    ""Tổng tiền tháng kiếm được"" DESC";

            }
            else
            {
                query = "select * from hoadon";
            }

            Query(query);

        }

        private void rbNVThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNVThang.Checked)
            {
                string query = @"SELECT 
                                    NhanVien.TenNV AS ""Tên nhân viên"",
                                    Month(HoaDon.NgayLap) AS Tháng,                                    
                                    YEAR(HoaDon.NgayLap) AS Năm,

                                    SUM(HoaDon.SoLuong) AS ""Tổng số lượng bán được""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    NhanVien ON HoaDon.MaNV = NhanVien.MaNV

                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0

                                GROUP BY 
                                    NhanVien.TenNV, month(HoaDon.NgayLap), YEAR(HoaDon.NgayLap)

                                ORDER BY 
                                    ""Tổng số lượng bán được"" DESC";

                Query(query);
            }
        }

        private void rbNVYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNVYear.Checked)
            {
                string query = @"SELECT 
                                    NhanVien.TenNV AS ""Tên nhân viên"",
                                    YEAR(HoaDon.NgayLap) AS Năm,
                                    SUM(HoaDon.SoLuong) AS ""Tổng số lượng bán được trong năm""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    NhanVien ON HoaDon.MaNV = NhanVien.MaNV

                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0

                                GROUP BY 
                                    NhanVien.TenNV, YEAR(HoaDon.NgayLap)

                                ORDER BY 
                                    ""Tổng số lượng bán được trong năm"" DESC";

                Query(query);
            }
        }

        private void rbSPBanChayMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSPBanChayMonth.Checked)
            {
                string query = @"SELECT 
                                    HangHoa.TenHH AS ""Tên sản phẩm"",
                                    MONTH(HoaDon.NgayLap) AS ""Tháng"",
                                    SUM(HoaDon.SoLuong) AS ""Tổng số lượng bán chạy""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    HangHoa ON HoaDon.MaHH = HangHoa.MaHH

                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0

                                GROUP BY 
                                    HangHoa.TenHH, MONTH(HoaDon.NgayLap)

                                ORDER BY 
                                     ""Tổng số lượng bán chạy"" DESC";

                Query(query);
            }
        }

        private void rbSPBanChayYear_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSPBanChayYear.Checked)
            {
                string query = @"SELECT 
                                    HangHoa.TenHH AS ""Tên sản phẩm"",
                                    YEAR(HoaDon.NgayLap) AS ""Năm"",
                                    SUM(HoaDon.SoLuong) AS ""Tổng số lượng bán""

                                FROM 
                                    HoaDon
                                INNER JOIN 
                                    HangHoa ON HoaDon.MaHH = HangHoa.MaHH

                                WHERE 
                                    HoaDon.ThanhToan = 1 AND HoaDon.isRemove = 0

                                GROUP BY 
                                    HangHoa.TenHH, YEAR(HoaDon.NgayLap)

                                ORDER BY 
                                     ""Tổng số lượng bán"" DESC";

                Query(query);
            }
        }
    }
}
