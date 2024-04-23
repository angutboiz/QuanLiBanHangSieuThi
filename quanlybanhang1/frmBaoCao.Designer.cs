namespace quanlybanhang1
{
    partial class frmBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbDoanhThu = new System.Windows.Forms.TextBox();
            this.txbChi = new System.Windows.Forms.TextBox();
            this.txbLoiNhuan = new System.Windows.Forms.TextBox();
            this.dgvBaoCao = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbSPBanChayYear = new System.Windows.Forms.RadioButton();
            this.rbSPBanChayMonth = new System.Windows.Forms.RadioButton();
            this.rbNVYear = new System.Windows.Forms.RadioButton();
            this.rbMonthMuch = new System.Windows.Forms.RadioButton();
            this.rbNVThang = new System.Windows.Forms.RadioButton();
            this.rbYearMuch = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.lbDoanhThu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(361, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Báo cáo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(67, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Doanh thu";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(309, 390);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Chi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(558, 390);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lợi nhuận";
            // 
            // txbDoanhThu
            // 
            this.txbDoanhThu.Enabled = false;
            this.txbDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDoanhThu.Location = new System.Drawing.Point(149, 387);
            this.txbDoanhThu.Name = "txbDoanhThu";
            this.txbDoanhThu.Size = new System.Drawing.Size(100, 24);
            this.txbDoanhThu.TabIndex = 5;
            this.txbDoanhThu.Text = "0";
            // 
            // txbChi
            // 
            this.txbChi.Enabled = false;
            this.txbChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbChi.Location = new System.Drawing.Point(345, 387);
            this.txbChi.Name = "txbChi";
            this.txbChi.Size = new System.Drawing.Size(100, 24);
            this.txbChi.TabIndex = 6;
            this.txbChi.Text = "0";
            // 
            // txbLoiNhuan
            // 
            this.txbLoiNhuan.Enabled = false;
            this.txbLoiNhuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbLoiNhuan.Location = new System.Drawing.Point(636, 387);
            this.txbLoiNhuan.Name = "txbLoiNhuan";
            this.txbLoiNhuan.Size = new System.Drawing.Size(100, 24);
            this.txbLoiNhuan.TabIndex = 7;
            this.txbLoiNhuan.Text = "0";
            // 
            // dgvBaoCao
            // 
            this.dgvBaoCao.AllowUserToAddRows = false;
            this.dgvBaoCao.AllowUserToDeleteRows = false;
            this.dgvBaoCao.AllowUserToResizeColumns = false;
            this.dgvBaoCao.AllowUserToResizeRows = false;
            this.dgvBaoCao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvBaoCao.Location = new System.Drawing.Point(12, 124);
            this.dgvBaoCao.Name = "dgvBaoCao";
            this.dgvBaoCao.ReadOnly = true;
            this.dgvBaoCao.RowHeadersVisible = false;
            this.dgvBaoCao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBaoCao.Size = new System.Drawing.Size(776, 247);
            this.dgvBaoCao.TabIndex = 8;
            this.dgvBaoCao.DataSourceChanged += new System.EventHandler(this.dgvBaoCao_DataSourceChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbSPBanChayYear);
            this.groupBox1.Controls.Add(this.rbSPBanChayMonth);
            this.groupBox1.Controls.Add(this.rbNVYear);
            this.groupBox1.Controls.Add(this.rbMonthMuch);
            this.groupBox1.Controls.Add(this.rbNVThang);
            this.groupBox1.Controls.Add(this.rbYearMuch);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(776, 81);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Doanh thu";
            // 
            // rbSPBanChayYear
            // 
            this.rbSPBanChayYear.AutoSize = true;
            this.rbSPBanChayYear.Location = new System.Drawing.Point(374, 50);
            this.rbSPBanChayYear.Name = "rbSPBanChayYear";
            this.rbSPBanChayYear.Size = new System.Drawing.Size(167, 17);
            this.rbSPBanChayYear.TabIndex = 25;
            this.rbSPBanChayYear.TabStop = true;
            this.rbSPBanChayYear.Text = "Sản phẩm bán chạy theo năm";
            this.rbSPBanChayYear.UseVisualStyleBackColor = true;
            this.rbSPBanChayYear.CheckedChanged += new System.EventHandler(this.rbSPBanChayYear_CheckedChanged);
            // 
            // rbSPBanChayMonth
            // 
            this.rbSPBanChayMonth.AutoSize = true;
            this.rbSPBanChayMonth.Location = new System.Drawing.Point(374, 27);
            this.rbSPBanChayMonth.Name = "rbSPBanChayMonth";
            this.rbSPBanChayMonth.Size = new System.Drawing.Size(174, 17);
            this.rbSPBanChayMonth.TabIndex = 24;
            this.rbSPBanChayMonth.TabStop = true;
            this.rbSPBanChayMonth.Text = "Sản phẩm bán chạy theo tháng";
            this.rbSPBanChayMonth.UseVisualStyleBackColor = true;
            this.rbSPBanChayMonth.CheckedChanged += new System.EventHandler(this.rbSPBanChayMonth_CheckedChanged);
            // 
            // rbNVYear
            // 
            this.rbNVYear.AutoSize = true;
            this.rbNVYear.Location = new System.Drawing.Point(202, 50);
            this.rbNVYear.Name = "rbNVYear";
            this.rbNVYear.Size = new System.Drawing.Size(118, 17);
            this.rbNVYear.TabIndex = 23;
            this.rbNVYear.TabStop = true;
            this.rbNVYear.Text = "Nhân viên của năm";
            this.rbNVYear.UseVisualStyleBackColor = true;
            this.rbNVYear.CheckedChanged += new System.EventHandler(this.rbNVYear_CheckedChanged);
            // 
            // rbMonthMuch
            // 
            this.rbMonthMuch.AutoSize = true;
            this.rbMonthMuch.Location = new System.Drawing.Point(31, 27);
            this.rbMonthMuch.Name = "rbMonthMuch";
            this.rbMonthMuch.Size = new System.Drawing.Size(109, 17);
            this.rbMonthMuch.TabIndex = 22;
            this.rbMonthMuch.TabStop = true;
            this.rbMonthMuch.Text = "Tháng nhiều nhất";
            this.rbMonthMuch.UseVisualStyleBackColor = true;
            this.rbMonthMuch.CheckedChanged += new System.EventHandler(this.rbMonthMuch_CheckedChanged);
            // 
            // rbNVThang
            // 
            this.rbNVThang.AutoSize = true;
            this.rbNVThang.Location = new System.Drawing.Point(202, 27);
            this.rbNVThang.Name = "rbNVThang";
            this.rbNVThang.Size = new System.Drawing.Size(125, 17);
            this.rbNVThang.TabIndex = 22;
            this.rbNVThang.TabStop = true;
            this.rbNVThang.Text = "Nhân viên của tháng";
            this.rbNVThang.UseVisualStyleBackColor = true;
            this.rbNVThang.CheckedChanged += new System.EventHandler(this.rbNVThang_CheckedChanged);
            // 
            // rbYearMuch
            // 
            this.rbYearMuch.AutoSize = true;
            this.rbYearMuch.Location = new System.Drawing.Point(31, 50);
            this.rbYearMuch.Name = "rbYearMuch";
            this.rbYearMuch.Size = new System.Drawing.Size(100, 17);
            this.rbYearMuch.TabIndex = 22;
            this.rbYearMuch.TabStop = true;
            this.rbYearMuch.Text = "Năm nhiều nhất";
            this.rbYearMuch.UseVisualStyleBackColor = true;
            this.rbYearMuch.CheckedChanged += new System.EventHandler(this.rbYearMuch_CheckedChanged);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(681, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 55);
            this.button1.TabIndex = 15;
            this.button1.Text = "Tổng doanh thu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbDoanhThu
            // 
            this.lbDoanhThu.AutoSize = true;
            this.lbDoanhThu.ForeColor = System.Drawing.Color.Red;
            this.lbDoanhThu.Location = new System.Drawing.Point(67, 422);
            this.lbDoanhThu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDoanhThu.Name = "lbDoanhThu";
            this.lbDoanhThu.Size = new System.Drawing.Size(56, 13);
            this.lbDoanhThu.TabIndex = 14;
            this.lbDoanhThu.Text = "Bằng chữ:";
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbDoanhThu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvBaoCao);
            this.Controls.Add(this.txbLoiNhuan);
            this.Controls.Add(this.txbChi);
            this.Controls.Add(this.txbDoanhThu);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBaoCao";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoCao)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbDoanhThu;
        private System.Windows.Forms.TextBox txbChi;
        private System.Windows.Forms.TextBox txbLoiNhuan;
        private System.Windows.Forms.DataGridView dgvBaoCao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbDoanhThu;
        private System.Windows.Forms.RadioButton rbNVThang;
        private System.Windows.Forms.RadioButton rbMonthMuch;
        private System.Windows.Forms.RadioButton rbYearMuch;
        private System.Windows.Forms.RadioButton rbNVYear;
        private System.Windows.Forms.RadioButton rbSPBanChayMonth;
        private System.Windows.Forms.RadioButton rbSPBanChayYear;
    }
}