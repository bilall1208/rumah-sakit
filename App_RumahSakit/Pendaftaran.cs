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
    public partial class Pendaftaran : Form
    {
        public Pendaftaran()
        {
            InitializeComponent();
        }

        private void Pendaftaran_Load(object sender, EventArgs e)
        {
            LoadPoli();
            LoadDokter();
            LoadAntrian();
            dtpTTL.Value = DateTime.Now;
            rbLakiLaki.Checked = true;
        }

        private void LoadPoli()
        {
            cmbPoli.Items.Clear();
            cmbPoli.Items.Add("-- Pilih Poli --");
            cmbPoli.Items.AddRange(new string[] {
                "Poli Umum", "Poli Gigi", "Poli Anak", "Poli Kandungan", "Poli Mata"
            });
            cmbPoli.SelectedIndex = 0;
        }

        private void LoadDokter()
        {
            cmbDokter.Items.Clear();
            cmbDokter.Items.Add("-- Pilih Dokter --");
            cmbDokter.Items.AddRange(new string[] {
                "dr. Andi Wijaya", "drg. Maya Putri", "dr. Budi Santoso", "drg. Bilal Fahrezi"
            });
            cmbDokter.SelectedIndex = 0;
        }

        private string Escape(string input)
        {
            return input.Replace("'", "''");
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRM.Text))
            {
                MessageBox.Show("Masukkan No. RM terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT * FROM Pasien WHERE No_RM = '" + Escape(txtRM.Text.Trim()) + "'";
                DB.crud(query);

                if (DB.ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = DB.ds.Tables[0].Rows[0];
                    txtNL.Text = row["Nama"].ToString();
                    txtNIK.Text = row["NIK"] == DBNull.Value ? "" : row["NIK"].ToString();
                    txtAlamat.Text = row["Alamat"].ToString();
                    if (row["TglLahir"] != DBNull.Value)
                        dtpTTL.Value = Convert.ToDateTime(row["TglLahir"]);

                    string jk = row["JenisKelamin"].ToString();
                    if (jk == "P")
                        rbPerempuan.Checked = true;
                    else
                        rbLakiLaki.Checked = true;

                    btnSimpanPerubahan.Enabled = true;

                    MessageBox.Show("Data pasien ditemukan.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No. RM tidak ditemukan. Silakan daftar sebagai pasien baru.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            BersihkanForm();
            txtRM.Text = GenerateNoRM();
            txtNL.Focus();
        }

        private void BersihkanForm()
        {
            txtNL.Clear();
            txtNIK.Clear();
            txtAlamat.Clear();
            dtpTTL.Value = DateTime.Now;
            cmbPoli.SelectedIndex = -1;
            cmbDokter.SelectedIndex = -1;
            rbLakiLaki.Checked = true;
            btnSimpanPerubahan.Enabled = false;
        }

        private string GenerateNoRM()
        {
            DB.crud("SELECT No_RM FROM Pasien ORDER BY No_RM DESC LIMIT 1");

            int nomorBaru = 1;
            if (DB.ds.Tables[0].Rows.Count > 0)
            {
                string noRMTerakhir = DB.ds.Tables[0].Rows[0]["No_RM"].ToString();
                string angkaSaja = noRMTerakhir.Replace("RM", "");
                nomorBaru = int.Parse(angkaSaja) + 1;
            }
            return "RM" + nomorBaru.ToString("D4");
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRM.Text) ||
                string.IsNullOrWhiteSpace(txtNL.Text))
            {
                MessageBox.Show("No. RM dan Nama wajib diisi.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPoli.SelectedIndex == -1 || cmbDokter.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih Poli dan Dokter terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DB.crud("SELECT COUNT(*) AS Jumlah FROM Pasien WHERE No_RM = '" + Escape(txtRM.Text.Trim()) + "'");
                int jumlah = Convert.ToInt32(DB.ds.Tables[0].Rows[0]["Jumlah"]);

                string noRM = Escape(txtRM.Text.Trim());
                string nama = Escape(txtNL.Text.Trim());
                string alamat = Escape(txtAlamat.Text.Trim());
                string nik = Escape(txtNIK.Text.Trim());
                string tglLahir = dtpTTL.Value.ToString("yyyy-MM-dd");

                if (jumlah == 0)
                {
                    string jkBaru = rbPerempuan.Checked ? "P" : "L";
                    string insertQuery = "INSERT INTO Pasien (No_RM, Nama, JenisKelamin, TglLahir, Alamat, NIK) " +
                        "VALUES ('" + noRM + "', '" + nama + "', '" + jkBaru + "', '" + tglLahir + "', '" + alamat + "', '" + nik + "')";
                    DB.crud(insertQuery);
                }
                else
                {
                    string jkUpdate = rbPerempuan.Checked ? "P" : "L";
                    string updateQuery = "UPDATE Pasien SET Nama = '" + nama + "', JenisKelamin = '" + jkUpdate +
                        "', TglLahir = '" + tglLahir + "', Alamat = '" + alamat + "', NIK = '" + nik + "' WHERE No_RM = '" + noRM + "'";
                    DB.crud(updateQuery);
                }

                string poli = Escape(cmbPoli.SelectedItem.ToString());
                string dokter = Escape(cmbDokter.SelectedItem.ToString());
                string insertKunjungan = "INSERT INTO Kunjungan (No_RM, Poli, NamaDokter, Status) " +
                    "VALUES ('" + noRM + "', '" + poli + "', '" + dokter + "', 'Menunggu')";
                DB.crud(insertKunjungan);

                MessageBox.Show("Pasien berhasil didaftarkan dan masuk antrian.", "Berhasil",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                BersihkanForm();
                txtRM.Clear();
                LoadAntrian();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAntrian()
        {
            try
            {
                string query = @"SELECT 
                        k.ID_Kunjungan AS 'No. Antrian',
                        p.Nama AS 'Nama Pasien',
                        k.Poli,
                        k.NamaDokter AS 'Dokter',
                        k.Status
                    FROM Kunjungan k
                    JOIN Pasien p ON k.No_RM = p.No_RM
                    WHERE DATE(k.TglKunjungan) = CURDATE()
                    ORDER BY k.ID_Kunjungan ASC";

                DB.crud(query);
                guna2DataGridView1.DataSource = DB.ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat antrian: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAntrian();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (guna2DataGridView1.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.Format(
                    "[Nama Pasien] LIKE '%{0}%'", Escape(guna2TextBox1.Text));
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSimpanPerubahan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRM.Text) || string.IsNullOrWhiteSpace(txtNL.Text))
            {
                MessageBox.Show("Data tidak lengkap.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string noRM = Escape(txtRM.Text.Trim());
                string nama = Escape(txtNL.Text.Trim());
                string nik = Escape(txtNIK.Text.Trim());
                string alamat = Escape(txtAlamat.Text.Trim());
                string tglLahir = dtpTTL.Value.ToString("yyyy-MM-dd");
                string jk = rbPerempuan.Checked ? "P" : "L";

                string updateQuery = "UPDATE Pasien SET Nama = '" + nama + "', NIK = '" + nik +
                    "', JenisKelamin = '" + jk + "', TglLahir = '" + tglLahir +
                    "', Alamat = '" + alamat + "' WHERE No_RM = '" + noRM + "'";
                DB.crud(updateQuery);

                MessageBox.Show("Data pasien berhasil diperbarui.", "Berhasil",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadAntrian();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
