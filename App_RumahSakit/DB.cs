using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;

namespace App_RumahSakit
{
    public static class LoginInfo
    {
        public static string LoggedInRole;
        public static string LoggedInNama;
    }

    class DB
    {
        public static MySqlConnection koneksi = new MySqlConnection("server = 127.0.0.1; username = root; password = ; database = db_rumah_sakit");
        public static DataSet ds = new DataSet();
        public static MySqlDataAdapter da;
        public static MySqlCommand perintah;

        public static void crud(string query)
        {
            Console.WriteLine(query);
            ds.Tables.Clear();
            perintah = new MySqlCommand(query, koneksi);
            da = new MySqlDataAdapter(perintah);
            da.Fill(ds);
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
    }
}
