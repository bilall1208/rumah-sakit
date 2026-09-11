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
    public partial class Pemeriksaan : Form
    {
        private int idKunjunganTerpilih = 0;
        private string noRMTerpilih = "";

        private DataTable dtResep;

        public Pemeriksaan()
        {
            InitializeComponent();
        }

        private void Pemeriksaan_Load(object sender, EventArgs e)
        {
            LoadAntrian();
            LoadObat();
            SiapkanTabelResep();
        }

        private string Escape(string input)
        {
            return input.Replace("'", "''");
        }

        private void LoadAntrian()
        {
            string query = @"SELECT 
                    k.ID_Kunjungan AS 'No. Antrian',
                    p.Nama AS 'Nama Pasien',
                    k.Poli,
                    k.No_RM
                FROM Kunjungan k
                JOIN Pasien p ON k.No_RM = p.No_RM
                WHERE k.Status = 'Menunggu'
                ORDER BY k.ID_Kunjungan ASC";

            DB.crud(query);
            dgvAntrian.DataSource = DB.ds.Tables[0];

            if (dgvAntrian.Columns.Contains("No_RM"))
                dgvAntrian.Columns["No_RM"].Visible = false;
        }

        private void dgvAntrian_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvAntrian.Rows[e.RowIndex];
            idKunjunganTerpilih = Convert.ToInt32(row.Cells["No. Antrian"].Value);
            noRMTerpilih = row.Cells["No_RM"].Value.ToString();
            string nama = row.Cells["Nama Pasien"].Value.ToString();
            string poli = row.Cells["Poli"].Value.ToString();

            txtPasienTerpilih.Text = "No. RM: " + noRMTerpilih + "   |   Nama: " + nama + "   |   Poli: " + poli;

            // Reset form input tiap ganti pasien
            txtKeluhan.Clear();
            txtDiagnosa.Clear();
            txtBiayaKonsultasi.Clear();
            dtResep.Rows.Clear();
            UpdateTotalBiayaObat();
        }

        private void LoadObat()
        {
            DB.crud("SELECT ID_Obat, NamaObat, HargaSatuan, Stok FROM Obat ORDER BY NamaObat ASC");
            cmbObat.DataSource = DB.ds.Tables[0];
            cmbObat.DisplayMember = "NamaObat";
            cmbObat.ValueMember = "ID_Obat";
        }

        private void SiapkanTabelResep()
        {
            dtResep = new DataTable();
            dtResep.Columns.Add("ID_Obat", typeof(int));
            dtResep.Columns.Add("Nama Obat", typeof(string));
            dtResep.Columns.Add("Jumlah", typeof(int));
            dtResep.Columns.Add("Harga", typeof(decimal));
            dtResep.Columns.Add("Subtotal", typeof(decimal));

            dgvResep.DataSource = dtResep;

            // Sembunyikan kolom ID_Obat (internal aja)
            if (dgvResep.Columns.Contains("ID_Obat"))
                dgvResep.Columns["ID_Obat"].Visible = false;

            // Tambah kolom tombol "Hapus" di grid resep kalau belum ada
            if (!dgvResep.Columns.Contains("Aksi"))
            {
                DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn();
                btnHapus.Name = "Aksi";
                btnHapus.HeaderText = "Aksi";
                btnHapus.Text = "Hapus";
                btnHapus.UseColumnTextForButtonValue = true;
                dgvResep.Columns.Add(btnHapus);
            }
        }

        private void btnTambahResep_Click(object sender, EventArgs e)
        {
            if (idKunjunganTerpilih == 0)
            {
                MessageBox.Show("Pilih pasien dari antrian terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbObat.SelectedValue == null || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Pilih obat dan isi jumlah terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka lebih dari 0.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idObat = Convert.ToInt32(cmbObat.SelectedValue);
            DataRowView obatTerpilih = (DataRowView)cmbObat.SelectedItem;
            string namaObat = obatTerpilih["NamaObat"].ToString();
            decimal harga = Convert.ToDecimal(obatTerpilih["HargaSatuan"]);
            int stok = Convert.ToInt32(obatTerpilih["Stok"]);

            if (jumlah > stok)
            {
                MessageBox.Show("Stok obat tidak mencukupi. Stok tersedia: " + stok, "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow[] existing = dtResep.Select("ID_Obat = " + idObat);
            if (existing.Length > 0)
            {
                int jumlahBaru = Convert.ToInt32(existing[0]["Jumlah"]) + jumlah;
                existing[0]["Jumlah"] = jumlahBaru;
                existing[0]["Subtotal"] = jumlahBaru * harga;
            }
            else
            {
                DataRow newRow = dtResep.NewRow();
                newRow["ID_Obat"] = idObat;
                newRow["Nama Obat"] = namaObat;
                newRow["Jumlah"] = jumlah;
                newRow["Harga"] = harga;
                newRow["Subtotal"] = jumlah * harga;
                dtResep.Rows.Add(newRow);
            }

            txtJumlah.Clear();
            UpdateTotalBiayaObat();
        }

        private void dgvResep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvResep.Columns[e.ColumnIndex].Name == "Aksi")
            {
                dtResep.Rows.RemoveAt(e.RowIndex);
                UpdateTotalBiayaObat();
            }
        }

        private void UpdateTotalBiayaObat()
        {
            decimal total = 0;
            foreach (DataRow row in dtResep.Rows)
            {
                total += Convert.ToDecimal(row["Subtotal"]);
            }
            lblTotalBiayaObat.Text = "Rp " + total.ToString("N0");
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (idKunjunganTerpilih == 0)
            {
                MessageBox.Show("Pilih pasien dari antrian terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtKeluhan.Text) || string.IsNullOrWhiteSpace(txtDiagnosa.Text))
            {
                MessageBox.Show("Keluhan dan Diagnosa wajib diisi.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal biayaKonsultasi = 0;
            if (!string.IsNullOrWhiteSpace(txtBiayaKonsultasi.Text))
                decimal.TryParse(txtBiayaKonsultasi.Text, out biayaKonsultasi);

            try
            {
                string keluhan = Escape(txtKeluhan.Text.Trim());
                string diagnosa = Escape(txtDiagnosa.Text.Trim());

                // 1. Insert ke Pemeriksaan
                string insertPeriksa = "INSERT INTO Pemeriksaan (ID_Kunjungan, Keluhan, Diagnosa, BiayaKonsultasi) " +
                    "VALUES (" + idKunjunganTerpilih + ", '" + keluhan + "', '" + diagnosa + "', " + biayaKonsultasi + ")";
                DB.crud(insertPeriksa);

                // 2. Ambil ID_Periksa yang baru saja dibuat
                DB.crud("SELECT MAX(ID_Periksa) AS ID FROM Pemeriksaan WHERE ID_Kunjungan = " + idKunjunganTerpilih);
                int idPeriksa = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["ID"]);

                // 3. Insert semua obat di keranjang resep ke DetailResep
                foreach (DataRow row in dtResep.Rows)
                {
                    int idObat = Convert.ToInt32(row["ID_Obat"]);   
                    int jumlah = Convert.ToInt32(row["Jumlah"]);
                    decimal subtotal = Convert.ToDecimal(row["Subtotal"]);

                    string insertResep = "INSERT INTO DetailResep (ID_Periksa, ID_Obat, JumlahObat, Subtotal, StatusResep) " +
                        "VALUES (" + idPeriksa + ", " + idObat + ", " + jumlah + ", " + subtotal + ", 'Menunggu')";
                    DB.crud(insertResep);
                }

                // 4. Update status kunjungan jadi "Diperiksa"
                string updateStatus = "UPDATE Kunjungan SET Status = 'Diperiksa' WHERE ID_Kunjungan = " + idKunjunganTerpilih;
                DB.crud(updateStatus);

                MessageBox.Show("Data pemeriksaan berhasil disimpan.", "Berhasil",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BatalkanForm();
                LoadAntrian();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan pemeriksaan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            BatalkanForm();
        }

        private void BatalkanForm()
        {
            idKunjunganTerpilih = 0;
            noRMTerpilih = "";
            txtPasienTerpilih.Text = "";
            txtKeluhan.Clear();
            txtDiagnosa.Clear();
            txtBiayaKonsultasi.Clear();
            txtJumlah.Clear();
            dtResep.Rows.Clear();
            UpdateTotalBiayaObat();
        }

    }
}
