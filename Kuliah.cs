using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {

    class Kuliah {

        private static string TBL_KULIAH = "kuliah";

        private static string COL_NAMA_KULIAH = "nama_kuliah";
        private static string COL_KODE_KULIAH = "kode_kuliah";
        private static string COL_PESERTA = "peserta";

        private static string PRM_NAMA_KULIAH = "@nama";
        private static string PRM_KODE_KULIAH = "@kode";
        private static string PRM_PESERTA = "@peserta";

        private string nama = "";
        private string kode = "";
        private int peserta = 0;

        private Kuliah(string nama, string kode, int peserta) {
            this.nama = nama;
            this.kode = kode;
            this.peserta = peserta;
        }

        public static List<Kuliah> GetAll() {
            List<Kuliah> listKuliah = new List<Kuliah>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_KULIAH);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        listKuliah.Add(new Kuliah(
                            (string)reader[COL_NAMA_KULIAH],
                            (string)reader[COL_KODE_KULIAH],
                            (int)reader[COL_PESERTA]));
                    }
                }
            }
            return listKuliah;
        }

        public static Kuliah Get(string kode) {
            Kuliah kuliah = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2}",
                    TBL_KULIAH,
                    COL_KODE_KULIAH, PRM_KODE_KULIAH);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_KODE_KULIAH, kode);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        kuliah = new Kuliah(
                            (string)reader[COL_NAMA_KULIAH],
                            (string)reader[COL_KODE_KULIAH],
                            (int)reader[COL_PESERTA]);
                    }
                }
            }
            return kuliah;
        }

        public static Kuliah Add(string nama, string kode, int peserta) {
            Kuliah kuliah = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "INSERT INTO {0} ({1}, {2}, {3}) VALUES ({4}, {5}, {6})",
                    TBL_KULIAH,
                    COL_NAMA_KULIAH, COL_KODE_KULIAH, COL_PESERTA,
                    PRM_NAMA_KULIAH, PRM_KODE_KULIAH, PRM_PESERTA);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_KULIAH, nama);
                command.Parameters.AddWithValue(PRM_KODE_KULIAH, kode);
                command.Parameters.AddWithValue(PRM_PESERTA, peserta);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    kuliah = new Kuliah(nama, kode, peserta);
            }
            return kuliah;
        }

        public static bool Delete(string kode) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2}",
                    TBL_KULIAH,
                    COL_KODE_KULIAH, PRM_KODE_KULIAH);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_KODE_KULIAH, kode);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }

        public string Nama {
            get { return this.nama; }
            set {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_KULIAH,
                        COL_NAMA_KULIAH, PRM_NAMA_KULIAH,
                        COL_KODE_KULIAH, PRM_KODE_KULIAH);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_KULIAH, value);
                    command.Parameters.AddWithValue(PRM_KODE_KULIAH, this.kode);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.nama = value;
                }
            }
        }

        public string Kode {
            get { return this.kode; }
            set {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_KULIAH,
                        COL_KODE_KULIAH, PRM_KODE_KULIAH + "1",
                        COL_KODE_KULIAH, PRM_KODE_KULIAH + "2");

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_KODE_KULIAH + "1", value);
                    command.Parameters.AddWithValue(PRM_KODE_KULIAH + "2", this.kode);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.kode = value;
                }
            }
        }

        public int Peserta {
            get { return this.peserta; }
            set {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_KULIAH,
                        COL_PESERTA, PRM_PESERTA,
                        COL_KODE_KULIAH, PRM_KODE_KULIAH);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_PESERTA, value);
                    command.Parameters.AddWithValue(PRM_KODE_KULIAH, this.kode);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.peserta = value;
                }
            }
        }
    }
}
