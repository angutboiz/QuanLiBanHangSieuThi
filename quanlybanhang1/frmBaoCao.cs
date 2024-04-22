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
                                KhachHang.TenKH,
                                NhanVien.TenNV,
                                HangHoa.TenHH,                                
                                HangHoa.GiaNhap,
                                HangHoa.GiaBan,                                


                                HoaDon.NgayLap,
                                HoaDon.SoLuong,
                                HoaDon.TongTien                                


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
                // Kiểm tra xem dòng đó có phải là dòng dữ liệu hợp lệ hay không
                if (row.IsNewRow) continue;
                tongtienRow = decimal.Parse(row.Cells["tongtien"].Value.ToString());

                gianhap = decimal.Parse(row.Cells["gianhap"].Value.ToString());
                giaban = decimal.Parse(row.Cells["giaban"].Value.ToString());
                soluong = int.Parse(row.Cells["soluong"].Value.ToString());

                chi = (giaban - gianhap) * soluong;
                loinhuan = tongtien - chi;

                tongchi += chi;
                tongloinhuan += loinhuan;
                tongtien += tongtienRow;
            }





            txbDoanhThu.Text = string.Format("{0:#,##0}", tongtien);

            txbChi.Text = string.Format("{0:#,##0}", tongchi);

            txbLoiNhuan.Text = string.Format("{0:#,##0}", tongloinhuan);

            lbDoanhThu.Text = "Bằng chữ: " + Functions.ConvertMoneyToWords(tongtien);
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
                MessageBox.Show("Chưa có dữ liệu báo cáo!");

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

        private void dgvBaoCao_DataSourceChanged(object sender, EventArgs e)
        {
            CountAmount();
        }
    }
}
