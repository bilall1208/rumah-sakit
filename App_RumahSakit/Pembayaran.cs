using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_RumahSakit
{
    public partial class Pembayaran : Form
    {
        private int idKunjunganTerpilih = 0;
        private int idPeriksaTerpilih = 0;
        private decimal totalBiaya = 0;
        private DataTable dtKunjungan;
        public Pembayaran()
        {
            InitializeComponent();
        }

        private void Pembayaran_Load(object sender, EventArgs e)
        {
            LoadKunjungan();
        }

        private string Escape(string input)
        {
            return input.Replace("'", "''");
        }

        private void LoadKunjungan()
        {
            string query = @"SELECT 
                    k.ID_Kunjungan AS 'No. Kunjungan',
                    ps.Nama AS 'Nama Pasien',
                    k.Poli,
                    k.Status,
                    k.No_RM
                FROM Kunjungan k
                JOIN Pasien ps ON k.No_RM = ps.No_RM
                WHERE k.Status = 'Diperiksa'
                ORDER BY k.ID_Kunjungan ASC";

            DB.crud(query);
            dtKunjungan = DB.ds.Tables[0];
            dgvKunjungan.DataSource = dtKunjungan;

            if (dgvKunjungan.Columns.Contains("No_RM"))
                dgvKunjungan.Columns["No_RM"].Visible = false;
        }

        private void txtCariKunjungan_TextChanged(object sender, EventArgs e)
        {
            if (dtKunjungan != null)
            {
                dtKunjungan.DefaultView.RowFilter =
                    "[Nama Pasien] LIKE '%" + Escape(txtCariKunjungan.Text) + "%'";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadKunjungan();
            ResetRincian();
        }

        private void dgvKunjungan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvKunjungan.Rows[e.RowIndex];
            idKunjunganTerpilih = Convert.ToInt32(row.Cells["No. Kunjungan"].Value);
            string noRM = row.Cells["No_RM"].Value.ToString();
            string nama = row.Cells["Nama Pasien"].Value.ToString();
            string poli = row.Cells["Poli"].Value.ToString();

            lblInfoPasien.Text = "No. RM: " + noRM + "   |   Nama: " + nama + "   |   Poli: " + poli;

            DB.crud("SELECT ID_Periksa, BiayaKonsultasi FROM Pemeriksaan WHERE ID_Kunjungan = " + idKunjunganTerpilih +
                     " ORDER BY ID_Periksa DESC LIMIT 1");

            if (DB.ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Data pemeriksaan untuk kunjungan ini belum ditemukan.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetRincian();
                return;
            }

            idPeriksaTerpilih = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["ID_Periksa"]);
            decimal biayaKonsultasi = Convert.ToDecimal(DB.ds.Tables[0].Rows[0]["BiayaKonsultasi"]);

            DB.crud("SELECT SUM(Subtotal) AS TotalObat FROM DetailResep WHERE ID_Periksa = " + idPeriksaTerpilih);
            decimal biayaObat = 0;
            if (DB.ds.Tables[0].Rows[0]["TotalObat"] != DBNull.Value)
                biayaObat = Convert.ToDecimal(DB.ds.Tables[0].Rows[0]["TotalObat"]);

            totalBiaya = biayaKonsultasi + biayaObat;

            lblBiayaKonsultasi.Text = "Rp " + biayaKonsultasi.ToString("N0");
            lblBiayaObat.Text = "Rp " + biayaObat.ToString("N0");
            lblTotal.Text = "Rp " + totalBiaya.ToString("N0");

            txtBayar.Clear();
            txtKembalian.Text = "0";
        }

        private void ResetRincian()
        {
            idKunjunganTerpilih = 0;
            idPeriksaTerpilih = 0;
            totalBiaya = 0;
            lblInfoPasien.Text = "No. RM: -   |   Nama: -   |   Poli: -";
            lblBiayaKonsultasi.Text = "Rp 0";
            lblBiayaObat.Text = "Rp 0";
            lblTotal.Text = "Rp 0";
            txtBayar.Clear();
            txtKembalian.Text = "0";
        }

        private void txtBayar_TextChanged(object sender, EventArgs e)
        {
            if(decimal.TryParse(txtBayar.Text, out decimal bayar))
            {
                decimal kembalian = bayar - totalBiaya;
                txtKembalian.Text = kembalian >= 0 ? kembalian.ToString("N0") : "0";
            }
            else
            {
                txtKembalian.Text = "0";
            }
        }

        private void btnProses_Click(object sender, EventArgs e)
        {
            if (idKunjunganTerpilih == 0)
            {
                MessageBox.Show("Pilih kunjungan yang mau dibayar terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtBayar.Text, out decimal bayar) || bayar < totalBiaya)
            {
                MessageBox.Show("Jumlah bayar belum mencukupi total tagihan.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string metodeBayar = "Tunai";
            if (rbBPJS.Checked) metodeBayar = "BPJS";
            else if (rbKartu.Checked) metodeBayar = "Kartu";

            decimal kembalian = bayar - totalBiaya;

            try
            {
                string totalStr = totalBiaya.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string bayarStr = bayar.ToString(System.Globalization.CultureInfo.InvariantCulture);
                string kembalianStr = kembalian.ToString(System.Globalization.CultureInfo.InvariantCulture);

                string insertBayar = "INSERT INTO Pembayaran (ID_Kunjungan, Total, MetodeBayar, Bayar, Kembalian) " +
                    "VALUES (" + idKunjunganTerpilih + ", " + totalStr + ", '" + metodeBayar + "', " +
                    bayarStr + ", " + kembalianStr + ")";
                DB.crud(insertBayar);

                DB.crud("UPDATE Kunjungan SET Status = 'Selesai' WHERE ID_Kunjungan = " + idKunjunganTerpilih);

                MessageBox.Show("Pembayaran berhasil diproses.\nKembalian: Rp " + kembalian.ToString("N0"),
                    "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ResetRincian();
                LoadKunjungan();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memproses pembayaran: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
