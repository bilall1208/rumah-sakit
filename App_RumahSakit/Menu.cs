using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            label6.Visible = true;
            label6.Text = "Kelola User";
        }

        private void btnRole_Click(object sender, EventArgs e)
        {
            KRole KR = new KRole()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(KR, PNLKNTN);

            label6.Visible = true;
            label6.Text = "Kelola Role";
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

        }

        private void btnKObat_Click(object sender, EventArgs e)
        {
            KObat K0 = new KObat()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(K0, PNLKNTN);

            label6.Visible = true;
            label6.Text = "Kelola Obat";
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

            label6.Visible = true;
            label6.Text = "Pendaftaran";
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void btnPemeriksaan_Click(object sender, EventArgs e)
        {
            Pemeriksaan FPR = new Pemeriksaan()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FPR, PNLKNTN);

            label6.Visible = true;
            label6.Text = "Pemeriksaan";
        }

        private void btnFarmasi_Click(object sender, EventArgs e)
        {
            Farmasi FF = new Farmasi()
            {
                TopLevel = false,
                TopMost = true
            };
            KF.UntukFormBilal(FF, PNLKNTN);

            label6.Visible = true;
            label6.Text = "Farmasi";
        }
    }
}
