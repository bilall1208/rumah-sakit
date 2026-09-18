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
    public partial class KRole : Form
    {
        public KRole()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM role");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string id = "" + brs["role_id"];
                string nm = "" + brs["nama_role"];
                string ket = "" + brs["keterangan"];
                guna2DataGridView1.Rows.Add(id, nm, ket);
            }
        }

        public void bersih()
        {
            label3.Text = "Role ID";
            txtNMROLE.Text = "";
            txtKET.Text = "";
        }

        private void KRole_Load(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string nm = txtNMROLE.Text;
            string ket = txtKET.Text;

            if (txtNMROLE.Text == "")
            {
                MessageBox.Show("Tidak ada yang dapat disimpan!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DB.crud($"INSERT INTO role VALUES(null, '{nm}', '{ket}')");
                MessageBox.Show("Role berhasil disimpan!");
            }
            bersih();
            tampildata();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DB.crud($"UPDATE role set nama_role = '{txtNMROLE.Text}', keterangan = '{txtKET.Text}' where role_id = '{label3.Text}' ");
            tampildata();
            bersih();
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            tampildata();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
          
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DB.crud($"UPDATE role set nama_role = '{txtNMROLE.Text}', keterangan = '{txtKET.Text}' where role_id = '{label3.Text}' ");
            tampildata();
            bersih();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int baris = e.RowIndex;
            int kolom = e.ColumnIndex;
            string id = guna2DataGridView1.Rows[baris].Cells[0].Value.ToString();
            DataGridViewRow BarisDipilih = guna2DataGridView1.Rows[baris];

            if (kolom == 3)
            {
                label3.Text = BarisDipilih.Cells[0].Value.ToString();
                txtNMROLE.Text = BarisDipilih.Cells[1].Value.ToString();
                txtKET.Text = BarisDipilih.Cells[2].Value.ToString();
            }
            if (kolom == 4)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus role dengan ID `{id}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    string QueryDelete = $"DELETE FROM role WHERE role_id='{id}'";
                    DB.crud(QueryDelete);
                    tampildata();
                    bersih();
                }
            }
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
