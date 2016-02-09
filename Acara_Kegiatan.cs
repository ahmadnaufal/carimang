using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CariMang {
    class Acara_Kegiatan {
        private static string TBL_ACARA_KEGIATAN = "acara_kegiatan";

        private static string COL_ID_KEGIATAN = "id_kegiatan";
        private static string COL_ID_RUANGAN = "id_ruangan";
        private static string COL_TANGGAL_KEGIATAN = "tanggal_kegiatan";
        private static string COL_WAKTUMULAI_KEGIATAN = "waktu_mulai";
        private static string COL_WAKTUSELESAI_KEGIATAN = "waktu_selesai";

        private static string PRM_ID_KEGIATAN = "@idkegiatan";
        private static string PRM_ID_RUANGAN = "@idruangan";
        private static string PRM_TANGGAL_KEGIATAN = "@tanggal";
        private static string PRM_WAKTUMULAI_KEGIATAN = "@mulai";
        private static string PRM_WAKTUSELESAI_KEGIATAN = "@selesai";

        private int idkegiatan = 0;
        private int idruangan = 0;
        private DateTime tanggalkegiatan = DateTime.Now;
        private DateTime mulaikegiatan = DateTime.Now;
        private DateTime selesaikegiatan = DateTime.Now;

        private Acara_Kegiatan(int idkegiatan, int idruangan,
                                DateTime tanggalkegiatan, DateTime mulaikegiatan, DateTime selesaikegiatan) {
            this.idkegiatan = idkegiatan;
            this.idruangan = idruangan;
            this.tanggalkegiatan = tanggalkegiatan;
            this.mulaikegiatan = mulaikegiatan;
            this.selesaikegiatan = selesaikegiatan;
        }

        public static List<Acara_Kegiatan> GetAll() {
            List<Acara_Kegiatan> listAcaraKegiatan = new List<Acara_Kegiatan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_ACARA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        listAcaraKegiatan.Add(new Acara_Kegiatan(
                            (int)reader[COL_ID_KEGIATAN],
                            (int)reader[COL_ID_RUANGAN],
                            DateTime.Parse((string)reader[COL_TANGGAL_KEGIATAN]),
                            DateTime.Parse((string)reader[COL_WAKTUMULAI_KEGIATAN]),
                            DateTime.Parse((string)reader[COL_WAKTUSELESAI_KEGIATAN]))
                        );
                    }
                }
            }
            return listAcaraKegiatan;
        }

        public static Acara_Kegiatan Get(int kegiatanid, int ruanganid) {
            Acara_Kegiatan acarakegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2} AND {3}={4}",
                    TBL_ACARA_KEGIATAN,
                    COL_ID_KEGIATAN, PRM_ID_KEGIATAN,
                    COL_ID_RUANGAN, PRM_ID_RUANGAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_KEGIATAN, kegiatanid);
                command.Parameters.AddWithValue(PRM_ID_RUANGAN, ruanganid);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        acarakegiatan = new Acara_Kegiatan(
                            (int)reader[COL_ID_KEGIATAN],
                            (int)reader[COL_ID_RUANGAN],
                            DateTime.Parse((string)reader[COL_TANGGAL_KEGIATAN]),
                            DateTime.Parse((string)reader[COL_WAKTUMULAI_KEGIATAN]),
                            DateTime.Parse((string)reader[COL_WAKTUSELESAI_KEGIATAN])
                        );
                    }
                }
            }
            return acarakegiatan;
        }

        public static Acara_Kegiatan Add(int idkegiatan, int idruangan,
                                DateTime tanggalkegiatan, DateTime mulaikegiatan, DateTime selesaikegiatan) {
            Acara_Kegiatan acarakegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}) VALUES ({6}, {7}, {8}, {9}, {10})",
                    TBL_ACARA_KEGIATAN,
                    COL_ID_KEGIATAN, COL_ID_RUANGAN, COL_TANGGAL_KEGIATAN,
                    COL_WAKTUMULAI_KEGIATAN, COL_WAKTUSELESAI_KEGIATAN,
                    PRM_ID_KEGIATAN, PRM_ID_RUANGAN, PRM_TANGGAL_KEGIATAN,
                    PRM_WAKTUMULAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_KEGIATAN, idkegiatan);
                command.Parameters.AddWithValue(PRM_ID_RUANGAN, idruangan);
                command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, tanggalkegiatan.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, mulaikegiatan.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, selesaikegiatan.ToString("yyyy-MM-dd"));

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    acarakegiatan = new Acara_Kegiatan(
                        idkegiatan, idruangan, tanggalkegiatan,
                        mulaikegiatan, selesaikegiatan
                    );
            }
            return acarakegiatan;
        }

        public static bool Delete(int kegiatanid, int ruanganid) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2} AND {3}={4}",
                    TBL_ACARA_KEGIATAN,
                    COL_ID_KEGIATAN, PRM_ID_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_KEGIATAN, kegiatanid);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }
    }
}
