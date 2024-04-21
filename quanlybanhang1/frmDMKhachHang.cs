﻿using System;
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
    public partial class frmDMKhachHang : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        SqlCommand cmd;
        string connectionString = @"Data Source=DESKTOP-8T8L9ET;Initial Catalog=QLBanHangSieuThi;Trusted_Connection=True";

        string queryTable = "select makh,tenkh,sdt,diachi from khachhang where isremove = 0";
        public frmDMKhachHang()
        {
            InitializeComponent();
            
        }

        private void frmDMKhachHang_Load(object sender, EventArgs e)
        {
            if (Functions.DatabaseExists())
            {
                txtMaKhach.Enabled = false;
                btnBoQua.Enabled = false;

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

                dgvKhachHang.DataSource = dt;

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

        private void txtMaKhach_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private bool CheckValidation()
        {
            if (txtTenKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhach.Focus();
                return false;

            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return false;

            }
            if (mtbDienThoai.Text == "(  )    -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mtbDienThoai.Focus();
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {

                string query = "INSERT INTO khachhang (tenkh,sdt,diachi) VALUES (N'" + txtTenKhach.Text.Trim() + "','" + mtbDienThoai.Text + "',N'" + txtDiaChi.Text.Trim() + "')";

                ExecCRUD(query, "Thêm thành công nhân viên: " + txtTenKhach.Text);
                Query(queryTable);
                btnSua.Enabled = false;
                btnBoQua.Enabled = true;
                btnThem.Enabled = false;
                ResetValues();
            }
        }


        private void ResetValues()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiaChi.Text = "";
            mtbDienThoai.Text = "";
        }
       
        private void btnLuu_Click(object sender, EventArgs e)
        {
            CheckValidation();
            string sql;
            
            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT MaKhach FROM tblKhach WHERE MaKhach=N'" + txtMaKhach.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach.Focus();
                return;
            }

            ResetValues();

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            txtMaKhach.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string query = "UPDATE khachhang SET tenkh=N'" + txtTenKhach.Text.Trim().ToString() + "',DiaChi=N'" +
                    txtDiaChi.Text.Trim().ToString() + "',sdt='" + mtbDienThoai.Text.ToString() +
                    "' WHERE makh=N'" + txtMaKhach.Text + "'";
                ExecCRUD(query,"Sửa thành công khách hàng: "+txtTenKhach.Text);
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
            btnSua.Enabled = true;
            txtMaKhach.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenKhach_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDiaChi_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void mtbDienThoai_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dgvKhachHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            txtMaKhach.Text = dt.Rows[row][0].ToString();
            txtTenKhach.Text = dt.Rows[row][1].ToString();
            txtDiaChi.Text = dt.Rows[row][2].ToString();
            mtbDienThoai.Text = dt.Rows[row][3].ToString();
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            (dgvKhachHang.DataSource as DataTable).DefaultView.RowFilter = string.Format("tenkh LIKE '%{0}%'", txtSearch.Text);

        }

        private void dgvKhachHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {

                DialogResult res = MessageBox.Show("Bạn có chắc muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    string query = "UPDATE khachhang SET isRemove = 1 WHERE makh ='" + txtMaKhach.Text + "'";
                    ExecCRUD(query, "Xóa thành công Khách hàng: " + txtTenKhach.Text);
                    Query(queryTable);
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);

            }
        }
    }
}
