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
using CrystalDecisions.CrystalReports.Engine;
using App_RumahSakit.Report;
using System.IO;

namespace App_RumahSakit
{
    public partial class Laporan : Form
    {
        private ReportDocument laporanAktif;

        public Laporan()
        {
            InitializeComponent();
            if (cmbJenisLaporan.Items.Count == 0)
            {
                cmbJenisLaporan.Items.AddRange(new string[] {
                    "Kunjungan", "Pembayaran", "Obat Keluar"
                });
            }

            // Kalau dikunci dari constructor, pilihan harus dipasang SETELAH item terisi
            if (!cmbJenisLaporan.Enabled && cmbJenisLaporan.SelectedIndex == -1)
                cmbJenisLaporan.SelectedIndex = 0;
            else if (cmbJenisLaporan.SelectedIndex == -1)
                cmbJenisLaporan.SelectedIndex = 0;
        }

        public Laporan(string jenisTerkunci) : this()
        {
            cmbJenisLaporan.SelectedItem = jenisTerkunci;
            cmbJenisLaporan.Enabled = false;
        }
        private void Laporan_Load(object sender, EventArgs e)
        {
            // Default: dari awal bulan sampai hari ini
            dtpAwal.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpAkhir.Value = DateTime.Now; 
        }

        private void btnTampil_Click(object sender, EventArgs e)
        {
            if (cmbJenisLaporan.SelectedIndex == -1)
            {
                MessageBox.Show("Pilih jenis laporan terlebih dahulu.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpAwal.Value.Date > dtpAkhir.Value.Date)
            {
                MessageBox.Show("Tanggal awal tidak boleh lebih besar dari tanggal akhir.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tglAwal = dtpAwal.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string tglAkhir = dtpAkhir.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            string query = "";
            ReportDocument laporan = null;

            switch (cmbJenisLaporan.SelectedItem.ToString())
            {
                case "Kunjungan":
                    query = @"SELECT 
                            k.TglKunjungan,
                            p.No_RM,
                            p.Nama AS NamaPasien,
                            k.Poli,
                            k.NamaDokter,
                            k.Status
                        FROM Kunjungan k
                        JOIN Pasien p ON k.No_RM = p.No_RM
                        WHERE DATE(k.TglKunjungan) BETWEEN '" + tglAwal + "' AND '" + tglAkhir + @"'
                        ORDER BY k.TglKunjungan ASC";
                    laporan = new Kunjungan_Pasien();
                    break;

                case "Pembayaran":
                    query = @"SELECT 
                            pb.TglBayar,
                            p.Nama AS NamaPasien,
                            pb.Total,
                            pb.MetodeBayar
                        FROM Pembayaran pb
                        JOIN Kunjungan k ON pb.ID_Kunjungan = k.ID_Kunjungan
                        JOIN Pasien p ON k.No_RM = p.No_RM
                        WHERE DATE(pb.TglBayar) BETWEEN '" + tglAwal + "' AND '" + tglAkhir + @"'
                        ORDER BY pb.TglBayar ASC";
                    laporan = new Laporan_Pendapatan();
                    break;

                case "Obat Keluar":
                    query = @"SELECT 
                            o.NamaObat,
                            dr.JumlahObat,
                            dr.Subtotal,
                            p.Nama AS NamaPasien,
                            k.TglKunjungan,
                            dr.StatusResep
                        FROM DetailResep dr
                        JOIN Obat o ON dr.ID_Obat = o.ID_Obat
                        JOIN Pemeriksaan pm ON dr.ID_Periksa = pm.ID_Periksa
                        JOIN Kunjungan k ON pm.ID_Kunjungan = k.ID_Kunjungan
                        JOIN Pasien p ON k.No_RM = p.No_RM
                        WHERE dr.StatusResep = 'Diserahkan'
                          AND DATE(k.TglKunjungan) BETWEEN '" + tglAwal + "' AND '" + tglAkhir + @"'
                        ORDER BY o.NamaObat ASC";
                    laporan = new Laporan_Obat_Keluar();
                    break;
            }

            try
            {
                DB.crud(query);
                DataTable dt = DB.ds.Tables[0];

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Tidak ada data pada rentang tanggal tersebut.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Bersihkan report sebelumnya biar gak numpuk di memory
                if (laporanAktif != null)
                {
                    laporanAktif.Close();
                    laporanAktif.Dispose();
                }

                dt.TableName = "ObatKeluar";
                laporan.SetDataSource(dt);
                laporanAktif = laporan;

                crvLaporan.ReportSource = laporanAktif;
                crvLaporan.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan laporan: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Laporan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (laporanAktif != null)
            {
                laporanAktif.Close();
                laporanAktif.Dispose();
            }
        }
    }
}
