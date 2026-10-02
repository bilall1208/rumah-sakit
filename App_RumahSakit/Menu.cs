using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace App_RumahSakit
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnKUser_Click(object sender, EventArgs e)
        {
            KUser KU = new KUser()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(KU, PNLKNTN);

        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            KRole KR = new KRole()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(KR, PNLKNTN);

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult Setuju = MessageBox.Show("Apakah ingin logout?", "Pemberitahuan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Setuju == DialogResult.Yes)
            {
                Login Flogin = new Login();
                Flogin.Show();
                this.Hide();
            }
        }

        private void btnDshbrd_Click(object sender, EventArgs e)
        {
            Dashboard DSHB = new Dashboard()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(DSHB, PNLKNTN);
        }

        private void btnKObat_Click(object sender, EventArgs e)
        {
            KObat K0 = new KObat()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(K0, PNLKNTN);
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnPndftrn_Click(object sender, EventArgs e)
        {
            Pendaftaran FP = new Pendaftaran()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FP, PNLKNTN);

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label3.Text = "Selamat pagi, " + LoginInfo.LoggedInNama + " 👋";
            label1.Text = "• Shift " + LoginInfo.LoggedInRole;
            Dashboard DSHB = new Dashboard()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(DSHB, PNLKNTN);
            btnKUser.Visible = false;
            btnRole.Visible = false;
            btnKObat.Visible = false;
            btnLprn.Visible = false;
            btnPndftrn.Visible = false;
            btnPemeriksaan.Visible = false;
            btnFarmasi.Visible = false;
            btnPembayaran.Visible = false;

            switch (LoginInfo.LoggedInRole)
            {
                case "Admin":
                    btnKUser.Visible = true;
                    btnRole.Visible = true;
                    btnKObat.Visible = true;
                    btnLprn.Visible = false;
                    break;

                case "Pendaftaran":
                    btnPndftrn.Visible = true;
                    btnPndftrn.Top = 253;
                    break;

                case "Dokter":
                    btnPemeriksaan.Visible = true;
                    btnPemeriksaan.Top = 253;
                    break;

                case "Farmasi":
                    btnFarmasi.Visible = true;
                    btnFarmasi.Top = 253;
                    break;

                case "Kasir":
                    btnPembayaran.Visible = true;
                    btnPembayaran.Top = 253;
                    break;
            }

        }

        private void btnPemeriksaan_Click(object sender, EventArgs e)
        {
            Pemeriksaan FPR = new Pemeriksaan()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FPR, PNLKNTN);
        }

        private void btnFarmasi_Click(object sender, EventArgs e)
        {
            Farmasi FF = new Farmasi()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FF, PNLKNTN);
        }

        private void btnPembayaran_Click(object sender, EventArgs e)
        {
            Pembayaran FPB = new Pembayaran()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FPB, PNLKNTN);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime waktuSekarang = DateTime.Now;

            CultureInfo kulturIndonesia = new CultureInfo("id-ID");

            string formatWaktu = waktuSekarang.ToString("dddd, dd MMMM yyyy", kulturIndonesia);
            string formatJam = waktuSekarang.ToString("HH:mm:ss");

            lblWaktu.Text = formatWaktu;
            lblJam.Text = formatJam;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
