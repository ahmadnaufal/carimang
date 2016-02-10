using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariMang {
    class Perbaikan {

        private static string TBL_PERBAIKAN = "perbaikan";

        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_TANGGAL_MULAI = "tanggal_mulai";
        private static string COL_TANGGAL_SELESAI = "tanggal_selesai";
        private static string COL_DESKRIPSI_PERBAIKAN = "deskripsi";

        private static string PRM_NAMA_RUANGAN = "@nama_ruangan";
        private static string PRM_TANGGAL_MULAI = "@tanggal_mulai";
        private static string PRM_TANGGAL_SELESAI = "@tanggal_selesai";
        private static string PRM_DESKRIPSI_PERBAIKAN = "@deskripsi";

        private string namaruangan = "";
        private DateTime tanggalmulai = DateTime.Now;
        private DateTime tanggalselesai = DateTime.Now;
        private string deskripsi = "";

        private Perbaikan(string namakegiatan, string namapenyelenggara)
        {
            this.namakegiatan = namakegiatan;
            this.namapenyelenggara = namapenyelenggara;
        }

        public static List<Kegiatan> GetAll()
        {
            List<Kegiatan> listKegiatan = new List<Kegiatan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listKegiatan.Add(new Kegiatan(
                            (string)reader[COL_NAMA_KEGIATAN],
                            (string)reader[COL_PENYELENGGARA_KEGIATAN]));
                    }
                }
            }
            return listKegiatan;
        }

        public static Kegiatan Get(int kegiatan_id)
        {
            Kegiatan kegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2}",
                    TBL_KEGIATAN,
                    COL_ID_KEGIATAN, PRM_ID_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_KEGIATAN, kegiatan_id);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kegiatan = new Kegiatan(
                            (string)reader[COL_NAMA_KEGIATAN],
                            (string)reader[COL_PENYELENGGARA_KEGIATAN]);
                    }
                }
            }
            return kegiatan;
        }

        public static Kegiatan Add(string namakegiatan, string namapenyelenggara)
        {
            Kegiatan kegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "INSERT INTO {0} ({1}, {2}) VALUES ({4}, {5})",
                    TBL_KEGIATAN,
                    COL_NAMA_KEGIATAN, COL_PENYELENGGARA_KEGIATAN,
                    PRM_NAMA_KEGIATAN, PRM_PENYELENGGARA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, namakegiatan);
                command.Parameters.AddWithValue(PRM_PENYELENGGARA_KEGIATAN, namapenyelenggara);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    kegiatan = new Kegiatan(namakegiatan, namapenyelenggara);
            }
            return kegiatan;
        }

        public static bool Delete(int kegiatan_id)
        {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2}",
                    TBL_KEGIATAN,
                    COL_ID_KEGIATAN, PRM_ID_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_KEGIATAN, kegiatan_id);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }


        public string NamaKegiatan
        {
            get { return this.namakegiatan; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_KEGIATAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_ID_KEGIATAN, PRM_ID_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, value);
                    command.Parameters.AddWithValue(PRM_ID_KEGIATAN, this.id);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.namakegiatan = value;
                }
            }
        }

        public string NamaPenyelenggara
        {
            get { return this.namapenyelenggara; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_KEGIATAN,
                        COL_PENYELENGGARA_KEGIATAN, PRM_PENYELENGGARA_KEGIATAN,
                        COL_ID_KEGIATAN, PRM_ID_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_PENYELENGGARA_KEGIATAN, value);
                    command.Parameters.AddWithValue(PRM_ID_KEGIATAN, this.id);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.namapenyelenggara = value;
                }
            }
        }

    }
}
