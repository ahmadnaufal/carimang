using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariMang {
    class Kegiatan {
        private static string TBL_KEGIATAN = "kegiatan";

        private static string COL_ID_PEMINJAM = "id_peminjam";
        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_NAMA_KEGIATAN = "nama_kegiatan";
        private static string COL_TANGGAL_KEGIATAN = "tanggal_kegiatan";
        private static string COL_WAKTUMULAI_KEGIATAN = "waktu_mulai";
        private static string COL_WAKTUSELESAI_KEGIATAN = "waktu_selesai";

        private static string PRM_ID_PEMINJAM = "@idpeminjam";
        private static string PRM_NAMA_RUANGAN = "@namaruangan";
        private static string PRM_NAMA_KEGIATAN = "@namakegiatan";
        private static string PRM_TANGGAL_KEGIATAN = "@tanggal";
        private static string PRM_WAKTUMULAI_KEGIATAN = "@mulai";
        private static string PRM_WAKTUSELESAI_KEGIATAN = "@selesai";

        private int idpeminjam = 0;
        private string namaruangan = "";
        private string namakegiatan = "";
        private DateTime tanggalkegiatan = DateTime.Now;
        private int mulaikegiatan = 0;
        private int selesaikegiatan = 0;

        private Kegiatan(int idpeminjam, string namaruangan, string namakegiatan,
                                DateTime tanggalkegiatan, int mulaikegiatan, int selesaikegiatan) {
            this.idpeminjam = idpeminjam;
            this.namaruangan = namaruangan;
            this.namakegiatan = namakegiatan;
            this.tanggalkegiatan = tanggalkegiatan;
            this.mulaikegiatan = mulaikegiatan;
            this.selesaikegiatan = selesaikegiatan;
        }

        public static List<Kegiatan> GetAll() {
            List<Kegiatan> listKegiatan = new List<Kegiatan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);

                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        listKegiatan.Add(new Kegiatan(
                            (int)reader[COL_ID_PEMINJAM],
                            (string)reader[COL_NAMA_RUANGAN],
                            (string)reader[COL_NAMA_KEGIATAN],
                            DateTime.Parse((string)reader[COL_TANGGAL_KEGIATAN]),
                            (int)reader[COL_WAKTUMULAI_KEGIATAN],
                            (int)reader[COL_WAKTUSELESAI_KEGIATAN])
                        );
                    }
                }
            }
            return listKegiatan;
        }

        public static Kegiatan Get(int peminjamid, string namaruangan, string namakegiatan) {
            Kegiatan kegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2} AND {3}={4}",
                    TBL_KEGIATAN,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, peminjamid);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, namaruangan);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, namakegiatan);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        kegiatan = new Kegiatan(
                            (int)reader[COL_ID_PEMINJAM],
                            (string)reader[COL_NAMA_RUANGAN],
                            (string)reader[COL_NAMA_KEGIATAN],
                            DateTime.Parse((string)reader[COL_TANGGAL_KEGIATAN]),
                            (int)reader[COL_WAKTUMULAI_KEGIATAN],
                            (int)reader[COL_WAKTUSELESAI_KEGIATAN]
                        );
                    }
                }
            }
            return kegiatan;
        }

        public static Kegiatan Add(int idpeminjam, string namaruangan, string namakegiatan,
                                DateTime tanggalkegiatan, int mulaikegiatan, int selesaikegiatan) {
            Kegiatan kegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) VALUES ({7}, {8}, {9}, {10}, {11}, {12})",
                    TBL_KEGIATAN,
                    COL_ID_PEMINJAM, COL_NAMA_RUANGAN, COL_NAMA_KEGIATAN,
                    COL_TANGGAL_KEGIATAN, COL_WAKTUMULAI_KEGIATAN, COL_WAKTUSELESAI_KEGIATAN,
                    PRM_ID_PEMINJAM, PRM_NAMA_RUANGAN, PRM_NAMA_KEGIATAN,
                    PRM_TANGGAL_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, idpeminjam);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, namaruangan);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, namakegiatan);
                command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, tanggalkegiatan.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, mulaikegiatan);
                command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, selesaikegiatan);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    kegiatan = new Kegiatan(
                        idpeminjam, namaruangan, namakegiatan,
                        tanggalkegiatan, mulaikegiatan, selesaikegiatan
                    );
            }
            return kegiatan;
        }

        public static bool Delete(int idpeminjam, string namaruangan, string namakegiatan) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2} AND {3}={4} AND {5}={6}",
                    TBL_KEGIATAN,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, idpeminjam);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, namaruangan);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, namakegiatan);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }
    }
}
