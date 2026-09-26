using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace App_RumahSakit
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string passwordHash = HashPassword(TPASS.Text);
            DB.crud($"SELECT * FROM user INNER JOIN role on user.role_id = role.role_id WHERE username = '{tUSER.Text}' AND password = '" + passwordHash + "' ");
            int cekbaris = DB.ds.Tables[0].Rows.Count;

            if (cekbaris == 1)
            {
                DataRow baris = DB.ds.Tables[0].Rows[0];
                string nama = "" + baris["namalengkap"];
                string role = "" + baris["nama_role"];

                LoginInfo.LoggedInRole = role;
                LoginInfo.LoggedInNama = nama;

                Menu MA = new Menu();
                MA.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username atau Password salah!");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
