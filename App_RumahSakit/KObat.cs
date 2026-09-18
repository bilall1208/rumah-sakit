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
    public partial class KObat : Form
    {
        public KObat()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM obat");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string id = "" + brs["id_obat"];
                string nm = "" + brs["namaobat"];
                string stn = "" + brs["satuan"];
                string hstn = "" + brs["hargasatuan"];
                string stk = "" + brs["stok"];
                guna2DataGridView1.Rows.Add(id, nm, stn, hstn, stk);
            }
        }

        public void bersih()
        {
            label3.Text = "ID Obat";
            txtNO.Text = "";
            txtHRGST.Text = "";
            txtSTK.Text = "";
            cmbSTUAN.SelectedIndex = -1;
        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;
            string id = guna2DataGridView1.Rows[baris].Cells[0].Value.ToString();
            DataGridViewRow BarisDipilih = guna2DataGridView1.Rows[baris];

            if (kolom == 5)
            {
                label3.Text = BarisDipilih.Cells[0].Value.ToString();
                txtNO.Text = BarisDipilih.Cells[1].Value.ToString();
                cmbSTUAN.Text = BarisDipilih.Cells[2].Value.ToString();
                txtHRGST.Text = BarisDipilih.Cells[3].Value.ToString();
                txtSTK.Text = BarisDipilih.Cells[4].Value.ToString();
            }
            if (kolom == 6)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus obat dengan ID ` {id}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    string QueryDelete = $"DELETE FROM obat WHERE id_obat='{id}'";
                    DB.crud(QueryDelete);
                    MessageBox.Show("Obat berhasil dihapus!");
                    tampildata();
                    bersih();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tampildata();
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DB.crud($"UPDATE obat set namaobat = '{txtNO.Text}', satuan = '{cmbSTUAN.Text}', hargasatuan = '{txtHRGST.Text}', stok = '{txtSTK.Text}' where id_obat = '{label3.Text}' ");
            MessageBox.Show("Obat berhasil diperbarui!");
            tampildata();
            bersih();
        }

        private void btnSIMPAN_Click(object sender, EventArgs e)
        {
            string no = txtNO.Text;
            string stn = cmbSTUAN.Text;
            string hs = txtHRGST.Text;
            string stk = txtSTK.Text;

            if (txtNO.Text == "" || cmbSTUAN.SelectedIndex == 0 || txtHRGST.Text == "" || txtSTK.Text == "" )
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DB.crud($"INSERT INTO obat VALUES(null, '{no}', '{stn}', '{hs}', '{stk}')");
                MessageBox.Show("Obat berhasil disimpan!");
            }
            tampildata();
            bersih();
        }

        private void KObat_Load(object sender, EventArgs e)
        {
            cmbSTUAN.Items.Clear();
            cmbSTUAN.Items.Add("-- Pilih Satuan --");
            cmbSTUAN.Items.Add("Kaplet");
            cmbSTUAN.Items.Add("Kapsul");
            cmbSTUAN.Items.Add("Tablet");
            cmbSTUAN.Items.Add("Strip");
            cmbSTUAN.Items.Add("Botol");
            cmbSTUAN.SelectedIndex = 0;
            tampildata();
        }
    }
}
