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
using quanlybanhang1.Class;

namespace quanlybanhang1
{
    public partial class frmMain : Form
    {
        public string Role { get; set; }

        public frmMain()
        {
           
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Role == "Nhân viên")
            {
                btnNhanVien.Enabled = false;
            }
            else if (Role == "Admin")
            {
                btnNhanVien.Enabled = true;
            }

        }

        
      

        private void button1_Click(object sender, EventArgs e)
        {
            //Functions.Disconnect(); //Đóng kết nối
            Application.Exit(); //Thoát
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmDMNhanvien dMNhanvien = new frmDMNhanvien();
            dMNhanvien.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frmDMKhachHang = new frmDMKhachHang();
            frmDMKhachHang.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmDMHang frmDMHang = new frmDMHang();
            frmDMHang.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmHoaDonBan dMHoaDonBan = new frmHoaDonBan();
            dMHoaDonBan.ShowDialog();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmBaoCao frmBaoCao = new frmBaoCao();
            frmBaoCao.ShowDialog();
        }
    }
    
}
