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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            AturAksiCepat();
            LoadRingkasan();
            LoadResepMenunggu();
        }

        private int Hitung(string query)
        {
            DB.crud(query);
            return Convert.ToInt32(DB.ds.Tables[0].Rows[0]["Jumlah"]);
        }

        private void LoadRingkasan()
        {
            lblPasienHariIni.Text = Hitung(
                "SELECT COUNT(*) AS Jumlah FROM Kunjungan WHERE DATE(TglKunjungan) = CURDATE()").ToString();

            lblMenungguDiperiksa.Text = Hitung(
                "SELECT COUNT(*) AS Jumlah FROM Kunjungan WHERE Status = 'Menunggu'").ToString();

            lblResepMenunggu.Text = Hitung(
                "SELECT COUNT(DISTINCT ID_Periksa) AS Jumlah FROM DetailResep WHERE StatusResep = 'Menunggu'").ToString();

            lblSiapDibayar.Text = Hitung(
                "SELECT COUNT(*) AS Jumlah FROM Kunjungan WHERE Status = 'Diperiksa'").ToString();
        }

        private void LoadResepMenunggu()
        {
            string query = @"SELECT DISTINCT 
                    pm.ID_Periksa AS 'No. Periksa',
                    ps.Nama AS 'Nama Pasien',
                    k.Poli,
                    k.TglKunjungan AS 'Tanggal'
                FROM DetailResep dr
                JOIN Pemeriksaan pm ON dr.ID_Periksa = pm.ID_Periksa
                JOIN Kunjungan k ON pm.ID_Kunjungan = k.ID_Kunjungan
                JOIN Pasien ps ON k.No_RM = ps.No_RM
                WHERE dr.StatusResep = 'Menunggu'
                ORDER BY pm.ID_Periksa DESC";

            DB.crud(query);
            dgvResepMenunggu.DataSource = DB.ds.Tables[0];
        }

        private Action aksi1;
        private Action aksi2;

        private void AturAksiCepat()
        {
            btnAksi2.Visible = true;

            switch (LoginInfo.LoggedInRole)
            {
                case "Admin":
                    btnAksi1.Text = "Kelola Obat";
                    aksi1 = () => BukaForm(new KObat()); 
                    btnAksi2.Text = "Laporan";
                    aksi2 = () => BukaForm(new Laporan());    
                    break;

                case "Pendaftaran":
                    btnAksi1.Text = "+ Daftar Pasien";
                    aksi1 = () => BukaForm(new Pendaftaran());
                    btnAksi2.Text = "Lihat Antrian";
                    aksi2 = () => BukaForm(new Pendaftaran());
                    break;

                case "Dokter":
                    btnAksi1.Text = "Mulai Pemeriksaan";
                    aksi1 = () => BukaForm(new Pemeriksaan());
                    btnAksi2.Visible = false; 
                    break;

                case "Farmasi":
                    btnAksi1.Text = "Proses Resep";
                    aksi1 = () => BukaForm(new Farmasi());
                    btnAksi2.Text = "Tambah Stok";
                    aksi2 = () => BukaForm(new Farmasi());
                    break;

                case "Kasir":
                    btnAksi1.Text = "Proses Pembayaran";
                    aksi1 = () => BukaForm(new Pembayaran());
                    btnAksi2.Visible = false;
                    break;
            }
        }

        private void btnAksi1_Click(object sender, EventArgs e)
        {
            if (aksi1 != null) aksi1();
        }

        private void btnAksi2_Click(object sender, EventArgs e)
        {
            if (aksi2 != null) aksi2();
        }

        private void BukaForm(Form f)
        {
            Menu menu = this.Parent.FindForm() as Menu;
            if (menu != null)
                menu.BukaFormDiPanel(f);
        }
    }
}
