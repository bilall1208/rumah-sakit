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
    public partial class Farmasi : Form
    {
        private int idPeriksaTerpilih = 0;
        private DataTable dtResepMenunggu;
        private DataTable dtDetailResep;

        public Farmasi()
        {
            InitializeComponent();
        }

        private string Escape(string input)
        {
            return input.Replace("'", "''");
        }

        private void Farmasi_Load(object sender, EventArgs e)
        {
            LoadResepMenunggu();
            LoadObatUntukStok();
            SiapkanTabelDetail();
        }

        private void LoadResepMenunggu()
        {
            string query = @"SELECT DISTINCT 
                    pm.ID_Periksa AS 'No. Periksa',
                    ps.Nama AS 'Nama Pasien',
                    k.TglKunjungan AS 'Tanggal'
                FROM DetailResep dr
                JOIN Pemeriksaan pm ON dr.ID_Periksa = pm.ID_Periksa
                JOIN Kunjungan k ON pm.ID_Kunjungan = k.ID_Kunjungan
                JOIN Pasien ps ON k.No_RM = ps.No_RM
                WHERE dr.StatusResep = 'Menunggu'
                ORDER BY pm.ID_Periksa ASC";

            DB.crud(query);
            dtResepMenunggu = DB.ds.Tables[0];
            dgvResepMenunggu.DataSource = dtResepMenunggu;
        }

        private void txtCariResep_TextChanged(object sender, EventArgs e)
        {
            if (dtResepMenunggu != null)
            {
                dtResepMenunggu.DefaultView.RowFilter =
                    "[Nama Pasien] LIKE '%" + Escape(txtCariResep.Text) + "%'";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadResepMenunggu();
            SiapkanTabelDetail();
            idPeriksaTerpilih = 0;
            lblPasienTerpilih.Text = "Pasien terpilih";
            lblTotalBiayaObat.Text = "Rp 0";
        }

        private void dgvResepMenunggu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvResepMenunggu.Rows[e.RowIndex];
            idPeriksaTerpilih = Convert.ToInt32(row.Cells["No. Periksa"].Value);
            string namaPasien = row.Cells["Nama Pasien"].Value.ToString();

            lblPasienTerpilih.Text = "No. Periksa: " + idPeriksaTerpilih + "   |   Nama: " + namaPasien;

            LoadDetailResep(idPeriksaTerpilih);
        }

        private void LoadDetailResep(int idPeriksa)
        {
            string query = @"SELECT 
                    dr.ID_Obat,
                    o.NamaObat AS 'Nama Obat',
                    dr.JumlahObat AS 'Jumlah',
                    o.Stok AS 'Stok Ada',
                    dr.Subtotal
                FROM DetailResep dr
                JOIN Obat o ON dr.ID_Obat = o.ID_Obat
                WHERE dr.ID_Periksa = " + idPeriksa + @" AND dr.StatusResep = 'Menunggu'";

            DB.crud(query);
            dtDetailResep = DB.ds.Tables[0];
            dgvDetailResep.DataSource = dtDetailResep;

            if (dgvDetailResep.Columns.Contains("ID_Obat"))
                dgvDetailResep.Columns["ID_Obat"].Visible = false;

            decimal total = 0;
            foreach (DataRow row in dtDetailResep.Rows)
                total += Convert.ToDecimal(row["Subtotal"]);
            lblTotalBiayaObat.Text = "Rp " + total.ToString("N0");
        }

        private void dgvDetailResep_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtDetailResep == null || e.RowIndex < 0 || e.RowIndex >= dtDetailResep.Rows.Count) return;

            int jumlah = Convert.ToInt32(dtDetailResep.Rows[e.RowIndex]["Jumlah"]);
            int stok = Convert.ToInt32(dtDetailResep.Rows[e.RowIndex]["Stok Ada"]);

            if (stok < jumlah)
                dgvDetailResep.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
            else
                dgvDetailResep.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.White;
        }

        private void SiapkanTabelDetail()
        {
            dtDetailResep = new DataTable();
            dgvDetailResep.DataSource = dtDetailResep;
        }

        private void btnSerahkanObat_Click(object sender, EventArgs e)
        {
            if (idPeriksaTerpilih == 0 || dtDetailResep == null || dtDetailResep.Rows.Count == 0)
            {
                MessageBox.Show("Pilih resep dari daftar terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow row in dtDetailResep.Rows)
            {
                int jumlah = Convert.ToInt32(row["Jumlah"]);
                int stok = Convert.ToInt32(row["Stok Ada"]);
                if (stok < jumlah)
                {
                    MessageBox.Show("Stok obat '" + row["Nama Obat"] + "' tidak mencukupi. " +
                        "Stok tersedia: " + stok + ", dibutuhkan: " + jumlah,
                        "Stok Tidak Cukup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                foreach (DataRow row in dtDetailResep.Rows)
                {
                    int idObat = Convert.ToInt32(row["ID_Obat"]);
                    int jumlah = Convert.ToInt32(row["Jumlah"]);

                    DB.crud("UPDATE Obat SET Stok = Stok - " + jumlah + " WHERE ID_Obat = " + idObat);
                }

                DB.crud("UPDATE DetailResep SET StatusResep = 'Diserahkan' WHERE ID_Periksa = " + idPeriksaTerpilih);

                MessageBox.Show("Obat berhasil diserahkan ke pasien.", "Berhasil",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                idPeriksaTerpilih = 0;
                lblPasienTerpilih.Text = "Pasien terpilih";
                lblTotalBiayaObat.Text = "Rp 0";
                LoadResepMenunggu();
                LoadObatUntukStok();
                SiapkanTabelDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyerahkan obat: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadObatUntukStok()
        {
            DB.crud("SELECT ID_Obat, NamaObat, Stok FROM Obat ORDER BY NamaObat ASC");
            cmbObatStok.DataSource = DB.ds.Tables[0];
            cmbObatStok.DisplayMember = "NamaObat";
            cmbObatStok.ValueMember = "ID_Obat";
        }

        private void btnTambahStok_Click(object sender, EventArgs e)
        {
            if (cmbObatStok.SelectedValue == null || string.IsNullOrWhiteSpace(txtJumlahStok.Text))
            {
                MessageBox.Show("Pilih obat dan isi jumlah terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlahStok.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka lebih dari 0.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idObat = Convert.ToInt32(cmbObatStok.SelectedValue);
                DB.crud("UPDATE Obat SET Stok = Stok + " + jumlah + " WHERE ID_Obat = " + idObat);

                MessageBox.Show("Stok berhasil ditambahkan.", "Berhasil",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtJumlahStok.Clear();
                LoadObatUntukStok();

                if (idPeriksaTerpilih != 0)
                    LoadDetailResep(idPeriksaTerpilih);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menambah stok: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
