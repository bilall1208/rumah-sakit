
namespace App_RumahSakit
{
    partial class Laporan
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
            this.cmbJenisLaporan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.crvLaporan = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtpAwal = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpAkhir = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTampil = new Guna.UI2.WinForms.Guna2Button();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.guna2ShadowPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbJenisLaporan
            // 
            this.cmbJenisLaporan.BackColor = System.Drawing.Color.Transparent;
            this.cmbJenisLaporan.BorderRadius = 5;
            this.cmbJenisLaporan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbJenisLaporan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenisLaporan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbJenisLaporan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbJenisLaporan.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.cmbJenisLaporan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cmbJenisLaporan.IntegralHeight = false;
            this.cmbJenisLaporan.ItemHeight = 30;
            this.cmbJenisLaporan.Location = new System.Drawing.Point(38, 56);
            this.cmbJenisLaporan.MaxDropDownItems = 4;
            this.cmbJenisLaporan.Name = "cmbJenisLaporan";
            this.cmbJenisLaporan.Size = new System.Drawing.Size(1103, 36);
            this.cmbJenisLaporan.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "Jenis Laporan";
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.crvLaporan);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(38, 266);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 3;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.ShadowDepth = 50;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(1104, 314);
            this.guna2ShadowPanel1.TabIndex = 23;
            // 
            // crvLaporan
            // 
            this.crvLaporan.ActiveViewIndex = -1;
            this.crvLaporan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvLaporan.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvLaporan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvLaporan.Location = new System.Drawing.Point(0, 0);
            this.crvLaporan.Name = "crvLaporan";
            this.crvLaporan.Size = new System.Drawing.Size(1104, 314);
            this.crvLaporan.TabIndex = 0;
            // 
            // dtpAwal
            // 
            this.dtpAwal.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.dtpAwal.BorderRadius = 4;
            this.dtpAwal.BorderThickness = 1;
            this.dtpAwal.Checked = true;
            this.dtpAwal.FillColor = System.Drawing.Color.White;
            this.dtpAwal.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpAwal.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpAwal.HoverState.FillColor = System.Drawing.Color.White;
            this.dtpAwal.Location = new System.Drawing.Point(38, 131);
            this.dtpAwal.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpAwal.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpAwal.Name = "dtpAwal";
            this.dtpAwal.Size = new System.Drawing.Size(549, 36);
            this.dtpAwal.TabIndex = 24;
            this.dtpAwal.Value = new System.DateTime(2026, 11, 13, 9, 0, 0, 616);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "Dari tanggal";
            // 
            // dtpAkhir
            // 
            this.dtpAkhir.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.dtpAkhir.BorderRadius = 4;
            this.dtpAkhir.BorderThickness = 1;
            this.dtpAkhir.Checked = true;
            this.dtpAkhir.FillColor = System.Drawing.Color.White;
            this.dtpAkhir.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.dtpAkhir.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpAkhir.HoverState.FillColor = System.Drawing.Color.White;
            this.dtpAkhir.Location = new System.Drawing.Point(592, 131);
            this.dtpAkhir.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpAkhir.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpAkhir.Name = "dtpAkhir";
            this.dtpAkhir.Size = new System.Drawing.Size(549, 36);
            this.dtpAkhir.TabIndex = 26;
            this.dtpAkhir.Value = new System.DateTime(2026, 11, 13, 9, 0, 0, 616);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(588, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 19);
            this.label2.TabIndex = 27;
            this.label2.Text = "Sampai tanggal";
            // 
            // btnTampil
            // 
            this.btnTampil.BorderColor = System.Drawing.Color.Gray;
            this.btnTampil.BorderRadius = 4;
            this.btnTampil.BorderThickness = 1;
            this.btnTampil.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTampil.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTampil.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTampil.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTampil.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTampil.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTampil.ForeColor = System.Drawing.Color.Black;
            this.btnTampil.ImageOffset = new System.Drawing.Point(-2, 0);
            this.btnTampil.Location = new System.Drawing.Point(38, 189);
            this.btnTampil.Name = "btnTampil";
            this.btnTampil.Size = new System.Drawing.Size(1104, 36);
            this.btnTampil.TabIndex = 53;
            this.btnTampil.Text = "Tampilkan laporan";
            this.btnTampil.Click += new System.EventHandler(this.btnTampil_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 54;
            this.label3.Text = "Hasil laporan";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::App_RumahSakit.Properties.Resources.icons8_Edit_Row_32px;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 80;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::App_RumahSakit.Properties.Resources.icons8_Delete_Row_32px;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 80;
            // 
            // Laporan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1179, 749);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnTampil);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpAkhir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpAwal);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbJenisLaporan);
            this.Name = "Laporan";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Laporan_FormClosing);
            this.Load += new System.EventHandler(this.Laporan_Load);
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ComboBox cmbJenisLaporan;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpAwal;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpAkhir;
        private System.Windows.Forms.Label label2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvLaporan;
        private Guna.UI2.WinForms.Guna2Button btnTampil;
        private System.Windows.Forms.Label label3;
    }
}