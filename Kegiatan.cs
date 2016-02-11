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
        private static string COL_TANGGAL_KEGIATAN = "tanggal";
        private static string COL_WAKTUMULAI_KEGIATAN = "waktu_mulai";
        private static string COL_WAKTUSELESAI_KEGIATAN = "waktu_selesai";

        private static string PRM_ID_PEMINJAM = "@idpeminjam";
        private static string PRM_NAMA_RUANGAN = "@namaruangan";
        private static string PRM_NAMA_KEGIATAN = "@namakegiatan";
        private static string PRM_TANGGAL_KEGIATAN = "@tanggal";
        private static string PRM_WAKTUMULAI_KEGIATAN = "@mulai";
        private static string PRM_WAKTUSELESAI_KEGIATAN = "@selesai";

        private Peminjam peminjam = null;
        private Ruangan ruangan = null;
        private string nama = "";
        private DateTime tanggal = DateTime.Now;
        private int waktuMulai = 0;
        private int waktuSelesai = 0;

        private Kegiatan(Peminjam peminjam, Ruangan ruangan, string nama, DateTime tanggal, int waktuMulai, int waktuSelesai) {
            this.peminjam = peminjam;
            this.ruangan = ruangan;
            this.nama = nama;
            this.tanggal = tanggal;
            this.waktuMulai = waktuMulai;
            this.waktuSelesai = waktuSelesai;
        }

        public static List<Kegiatan> GetAll() {
            List<Kegiatan> listKegiatan = new List<Kegiatan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Peminjam peminjam = Peminjam.Get((int)reader[COL_ID_PEMINJAM]);
                        Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        listKegiatan.Add(new Kegiatan(
                            peminjam,
                            ruangan,
                            (string)reader[COL_NAMA_KEGIATAN],
                            (DateTime)reader[COL_TANGGAL_KEGIATAN],
                            (int)reader[COL_WAKTUMULAI_KEGIATAN],
                            (int)reader[COL_WAKTUSELESAI_KEGIATAN])
                        );
                    }
                }
            }
            return listKegiatan;
        }

        public static List<Kegiatan> GetAll(DateTime tanggal) {
            List<Kegiatan> listKegiatan = new List<Kegiatan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2}",
                    TBL_KEGIATAN,
                    COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, tanggal.Date.ToString("yyyy-MM-dd"));

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Peminjam peminjam = Peminjam.Get((int)reader[COL_ID_PEMINJAM]);
                        Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        listKegiatan.Add(new Kegiatan(
                            peminjam,
                            ruangan,
                            (string)reader[COL_NAMA_KEGIATAN],
                            (DateTime)reader[COL_TANGGAL_KEGIATAN],
                            (int)reader[COL_WAKTUMULAI_KEGIATAN],
                            (int)reader[COL_WAKTUSELESAI_KEGIATAN])
                        );
                    }
                }
            }
            return listKegiatan;
        }

        public static Kegiatan Get(Peminjam peminjam, Ruangan ruangan, string nama) {
            Kegiatan kegiatan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2} AND {3}={4}",
                    TBL_KEGIATAN,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, peminjam.Id);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, nama);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read()) {
                        Peminjam peminjamIni = Peminjam.Get((int)reader[COL_ID_PEMINJAM]);
                        Ruangan ruanganIni = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        kegiatan = new Kegiatan(
                            peminjamIni,
                            ruanganIni,
                            (string)reader[COL_NAMA_KEGIATAN],
                            (DateTime)reader[COL_TANGGAL_KEGIATAN],
                            (int)reader[COL_WAKTUMULAI_KEGIATAN],
                            (int)reader[COL_WAKTUSELESAI_KEGIATAN]
                        );
                    }
                }
            }
            return kegiatan;
        }

        public static Kegiatan Add(Peminjam peminjam, Ruangan ruangan, string namakegiatan,
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
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, peminjam.Id);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, namakegiatan);
                command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, tanggalkegiatan.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, mulaikegiatan);
                command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, selesaikegiatan);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    kegiatan = new Kegiatan(
                        peminjam, ruangan, namakegiatan,
                        tanggalkegiatan, mulaikegiatan, selesaikegiatan
                    );
            }
            return kegiatan;
        }

        public static bool Delete(Peminjam peminjam, Ruangan ruangan, String nama) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2} AND {3}={4} AND {5}={6}",
                    TBL_KEGIATAN,
                    COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_ID_PEMINJAM, peminjam.Id);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, nama);

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }

        public static bool Exists(Ruangan ruangan, DateTime tanggal, int waktuMulai, int waktuSelesai) {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                string query = String.Format(
                    "SELECT COUNT(*) FROM {0} WHERE {1}={2} AND {3}={4} AND ( ({5} BETWEEN {6} AND {7}) OR ({8} BETWEEN {9} AND {10}) )",
                    TBL_KEGIATAN,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                    PRM_WAKTUMULAI_KEGIATAN, COL_WAKTUMULAI_KEGIATAN, COL_WAKTUSELESAI_KEGIATAN + "-1",
                    PRM_WAKTUSELESAI_KEGIATAN, COL_WAKTUMULAI_KEGIATAN + "+1", COL_WAKTUSELESAI_KEGIATAN);
                
                MySqlCommand command = new MySqlCommand(query, connection);                
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, tanggal.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, waktuMulai);
                command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, waktuSelesai);

                connection.Open();
                result = (long)command.ExecuteScalar() > 0;                
            }
            return result;
        }

        public Peminjam Peminjam
        {
            get { return this.peminjam; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM + "2",
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM + "1", value.Id);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM + "2", this.peminjam.Id);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.peminjam = value;
                }
            }
        }

        public Ruangan Ruangan
        {
            get { return this.ruangan; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "2",
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "1", value.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "2", this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.peminjam.Id);                    
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.ruangan = value;
                }
            }
        }

        public string Nama
        {
            get { return this.nama; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN + "2",
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN + "1", value);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.peminjam.Id);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN + "2", this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.nama = value;
                }
            }
        }

        public DateTime Tanggal
        {
            get { return this.tanggal; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN + "2",
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN + "1", value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.peminjam.Id);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN + "2", this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.tanggal = value;
                }
            }
        }

        public int WaktuMulai
        {
            get { return this.waktuMulai; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN + "2",
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN + "1", value);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.peminjam.Id);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN + "2", this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN, this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.waktuMulai = value;
                }
            }
        }

        public int WaktuSelesai
        {
            get { return this.waktuSelesai; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12} AND {13}={14}",
                        TBL_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN + "1",
                        COL_ID_PEMINJAM, PRM_ID_PEMINJAM,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_NAMA_KEGIATAN, PRM_NAMA_KEGIATAN,
                        COL_TANGGAL_KEGIATAN, PRM_TANGGAL_KEGIATAN,
                        COL_WAKTUMULAI_KEGIATAN, PRM_WAKTUMULAI_KEGIATAN,
                        COL_WAKTUSELESAI_KEGIATAN, PRM_WAKTUSELESAI_KEGIATAN + "2");

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN + "1", value);
                    command.Parameters.AddWithValue(PRM_ID_PEMINJAM, this.peminjam.Id);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_KEGIATAN, this.nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_KEGIATAN, this.tanggal);
                    command.Parameters.AddWithValue(PRM_WAKTUMULAI_KEGIATAN, this.waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTUSELESAI_KEGIATAN + "2", this.waktuSelesai);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.waktuSelesai = value;
                }
            }
        }

    }
}
