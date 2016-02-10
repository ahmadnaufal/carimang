using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {
    class Peminjam {

        private static string TBL_PEMINJAM = "Peminjam";

        private static string COL_ID_PEMINJAM = "id";
        private static string COL_NAMA_PEMINJAM = "nama_peminjam";

        private static string PRM_ID_PEMINJAM = "@id";
        private static string PRM_NAMA_PEMINJAM = "@nama_peminjam";

        private int id = 0;
        private string namapeminjam = "";

        private Peminjam(string namapeminjam) {
            this.namapeminjam = namapeminjam;
        }

        public static List<Peminjam> GetAll() {
            List<Peminjam> listPeminjam = new List<Peminjam>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_PEMINJAM);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        listPeminjam.Add(new Peminjam(
                            (string)reader[COL_NAMA_PEMINJAM]));
                    }
                }
            }
            return listPeminjam;
        }

        public static Peminjam Get(int id) {
            Peminjam peminjam = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2}",
                    TBL_PEMINJAM,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, id);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        peminjam = new Peminjam(
                            (string)reader[COL_NAMA_PEMINJAM]);
                    }
                }
            }
            return peminjam;
        }

        public static Peminjam Add(string namapeminjam) {
            Peminjam peminjam = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "INSERT INTO {0} ({1}) VALUES ({2})",
                    TBL_PEMINJAM,
                    COL_NAMA_PEMINJAM, PRM_NAMA_PEMINJAM);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_PEMINJAM, namapeminjam);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    peminjam = new Peminjam(namapeminjam);
            }
            return peminjam;
        }

        public static bool Delete(int id) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2}",
                    TBL_PEMINJAM,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, id);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }


        public string NamaPeminjam {
            get { return this.namapeminjam; }
            set {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                        TBL_PEMINJAM,
                        COL_NAMA_PEMINJAM, PRM_NAMA_PEMINJAM,
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_PEMINJAM, value);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.id);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.namapeminjam = value;
                }
            }
        }

        
    }
}
