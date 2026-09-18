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
    public partial class KUser : Form
    {
        public KUser()
        {
            InitializeComponent();
        }

        public void tampildata()
        {
            guna2DataGridView1.Rows.Clear();
            DB.crud("SELECT * FROM user INNER JOIN role on user.role_id = role.role_id");
            foreach (DataRow brs in DB.ds.Tables[0].Rows)
            {
                string id = "" + brs["id_user"];
                string nm = "" + brs["namalengkap"];
                string usr = "" + brs["username"];
                string pass = "" + brs["password"];
                string role = "" + brs["nama_role"];
                guna2DataGridView1.Rows.Add(id, nm, usr, pass, role);
            }
        }

        public void bersih()
        {
            label3.Text = "User ID";
            txtNL.Text = "";
            txtUSER.Text = "";
            txtPASS.Text = "";
            txtKNFRPW.Text = "";
            cmbROLE.SelectedIndex = -1;
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
                txtNL.Text = BarisDipilih.Cells[1].Value.ToString();
                txtUSER.Text = BarisDipilih.Cells[2].Value.ToString();
                txtPASS.Text = BarisDipilih.Cells[3].Value.ToString();
                txtKNFRPW.Text = BarisDipilih.Cells[3].Value.ToString();
                if (BarisDipilih.Cells[4].Value != null)
                {
                    cmbROLE.Text = BarisDipilih.Cells[4].Value.ToString();
                }
            }
            if (kolom == 6)
            {
                DialogResult konfirmasi = MessageBox.Show(
                    $"Apakah anda yakin hapus user dengan ID `{id}`?",
                    "Konfirmasi Hapus",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning
                );

                if (konfirmasi == DialogResult.OK)
                {
                    string QueryDelete = $"DELETE FROM user WHERE id_user='{id}'";
                    DB.crud(QueryDelete);
                    MessageBox.Show("User berhasil dihapus!");
                    tampildata();
                    bersih();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            tampildata();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string nm = txtNL.Text;
            string usr = txtUSER.Text;
            string pass = txtPASS.Text;
            string knfr = txtKNFRPW.Text;
            int selectedRoleId = Convert.ToInt32(cmbROLE.SelectedValue);

            if (nm == "" || usr == "" || pass == "" || knfr == "" || cmbROLE.SelectedIndex == -1)
            {
                MessageBox.Show("Semua field harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pass != knfr)
            {
                MessageBox.Show("Password dan Konfirmasi Password tidak cocok!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DB.crud($"INSERT INTO user VALUES(null, '{usr}', '{pass}', '{nm}', '{selectedRoleId}')");
                MessageBox.Show("User berhasil disimpan!");
            }
            tampildata();
            bersih();
        }

        private void KUser_Load(object sender, EventArgs e)
        {
            tampildata();
            try
            {
                DB.crud("SELECT role_id, nama_role FROM role");

                cmbROLE.DataSource = DB.ds.Tables[0];

                cmbROLE.DisplayMember = "nama_role";
                cmbROLE.ValueMember = "role_id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat role: " + ex.Message);
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string pass = txtPASS.Text;
            string knfr = txtKNFRPW.Text;

            if (pass != knfr)
            {
                MessageBox.Show("Password dan Konfirmasi Password tidak cocok!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DB.crud($"UPDATE user set namalengkap = '{txtNL.Text}', username = '{txtUSER.Text}', password = '{txtPASS.Text}', role_id = '{cmbROLE.SelectedValue}' where id_user = '{label3.Text}' ");
            MessageBox.Show("User berhasil diperbarui!");
            tampildata();
            bersih();
        }

    }
}
