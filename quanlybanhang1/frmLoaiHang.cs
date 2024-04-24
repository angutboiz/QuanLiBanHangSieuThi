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
    public partial class frmLoaiHang : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = "select malh,tenlh from loaihang";

        public frmLoaiHang()
        {
            InitializeComponent();
        }

        private void frmLoaiHang_Load(object sender, EventArgs e)
        {
            Query(queryTable);
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

                dgvLoaiHang.DataSource = dt;

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenLoaiHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên loại hàng");
                txtTenLoaiHang.Focus();
            }
            else
            {
                string query = "insert into loaihang (tenlh) values (N'"+txtTenLoaiHang.Text+"')";
                ExecCRUD(query,"Thêm thành công loại hàng: "+txtTenLoaiHang.Text);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaLoaiHang.Text == "")
            {
                MessageBox.Show("Vui lòng điền tên loại hàng");
                txtTenLoaiHang.Focus();
            }
            else
            {
                string query = "update loaihang set tenlh=N'" + txtTenLoaiHang.Text + "' where malh='"+txtMaLoaiHang.Text+"'";
                ExecCRUD(query, "Sửa thành công");
            }
        }

        private void dgvLoaiHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMaLoaiHang.Text = dt.Rows[row][0].ToString();
            txtTenLoaiHang.Text = dt.Rows[row][1].ToString();
            
        }

        private void dgvLoaiHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "delete from loaihang  WHERE malh ='" + txtMaLoaiHang.Text + "'";
                    ExecCRUD(query, "Xóa thành công loại hàng: " + txtTenLoaiHang.Text);

                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
