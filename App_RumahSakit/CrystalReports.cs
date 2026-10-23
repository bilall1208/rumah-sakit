using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;

namespace App_RumahSakit
{
    public partial class CrystalReports : Form
    {
        private ReportDocument DokumenLaporan;
        public CrystalReports()
        {
            InitializeComponent();
        }

        private void CrystalReports_Load(object sender, EventArgs e)
        {
            string reportpath = Path.Combine(Application.StartupPath, "Struk Pembayaran.rpt");
            try
            {
                if (!File.Exists(reportpath))
                {
                    MessageBox.Show(
                        "File report tidak ditemukan:\n" + reportpath,
                        "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DokumenLaporan = new ReportDocument();
                DokumenLaporan.Load(reportpath);

                crystalReportViewer1.ReportSource = DokumenLaporan;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menyimpan report:\n" + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                if (DokumenLaporan != null)
                {
                    DokumenLaporan.Clone();
                    DokumenLaporan.Dispose();
                    DokumenLaporan = null;
                }

                throw;
            }
        }
    }
}
