using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {

    public class Perkuliahan {
        public enum DaftarHari {
            Senin, Selasa, Rabu, Kamis, Jumat
        };

        private static string TBL_PERKULIAHAN = "perkuliahan";

        private static string COL_KODE_KULIAH = "kode_kuliah";
        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_HARI_PERKULIAHAN = "hari";
        private static string COL_WAKTU_MULAI = "waktu_mulai";
        private static string COL_WAKTU_SELESAI = "waktu_selesai";
        private static string COL_PENANGGUNG_JAWAB = "penanggung_jawab";

        private static string PRM_KODE = "@kode";
        private static string PRM_NAMA = "@nama";        
        private static string PRM_HARI_PERKULIAHAN = "@hari_perkuliahan";
        private static string PRM_WAKTU_SELESAI = "@waktu_selesai";
        private static string PRM_WAKTU_MULAI = "@waktu_mulai";
        private static string PRM_TANGGUNG = "@tanggung";

        private Kuliah kuliah = null;
        private Ruangan ruangan = null;
        private int hariPerkuliahan = 0;
        private int waktuMulai = 0;
        private int waktuSelesai = 0;
        private string penanggungJawab = "";

        private Perkuliahan(
                Kuliah kuliah, Ruangan ruangan, int hariPerkuliahan,
                int waktuMulai, int waktuSelesai, string penanggungJawab) {
            this.kuliah = kuliah;
            this.ruangan = ruangan;
            this.hariPerkuliahan = hariPerkuliahan;
            this.waktuMulai = waktuMulai;
            this.waktuSelesai = waktuSelesai;
            this.penanggungJawab = penanggungJawab;
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            Perkuliahan perkuliahan = obj as Perkuliahan;
            return this.Equals(perkuliahan);
        }

        public bool Equals(Perkuliahan perkuliahan) {
            if ((object)perkuliahan == null)
                return false;

            return
                this.kuliah.Equals(perkuliahan.kuliah) &&
                this.ruangan.Equals(perkuliahan.ruangan) &&
                this.hariPerkuliahan.Equals(perkuliahan.hariPerkuliahan) &&
                this.waktuMulai.Equals(perkuliahan.waktuMulai) &&
                this.waktuSelesai.Equals(perkuliahan.waktuSelesai);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public static List<Perkuliahan> GetAll() {
            List<Perkuliahan> listPerkuliahan = new List<Perkuliahan>();

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    String query = String.Format(
                        "SELECT * FROM {0}", TBL_PERKULIAHAN);

                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Kuliah kuliah = Kuliah.Get((string)reader[COL_KODE_KULIAH]);
                            Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                            int hari = (int)reader[COL_HARI_PERKULIAHAN];
                            int mulai = (int)reader[COL_WAKTU_MULAI];
                            int selesai = (int)reader[COL_WAKTU_SELESAI];
                            string tanggung = (string)reader[COL_PENANGGUNG_JAWAB];
                            listPerkuliahan.Add(new Perkuliahan(kuliah, ruangan, hari, mulai, selesai, tanggung));
                        }
                    }
                }
            }
            catch (MySqlException) {
            }

            return listPerkuliahan;
        }

        public static List<Perkuliahan> GetAll(DateTime tanggal) {
            List<Perkuliahan> listPerkuliahan = new List<Perkuliahan>();

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    String query = String.Format(
                        "SELECT * FROM {0} WHERE {1}={2}",
                        TBL_PERKULIAHAN,
                        COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN);

                    int hariPerkuliahan = ((int)tanggal.Date.DayOfWeek + 6) % 7;
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, hariPerkuliahan);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            Kuliah kuliah = Kuliah.Get((string)reader[COL_KODE_KULIAH]);
                            Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                            int hari = (int)reader[COL_HARI_PERKULIAHAN];
                            int mulai = (int)reader[COL_WAKTU_MULAI];
                            int selesai = (int)reader[COL_WAKTU_SELESAI];
                            string tanggung = (string)reader[COL_PENANGGUNG_JAWAB];
                            listPerkuliahan.Add(new Perkuliahan(kuliah, ruangan, hari, mulai, selesai, tanggung));
                        }
                    }
                }
            }
            catch (MySqlException) {
            }

            return listPerkuliahan;
        }                

        public static Perkuliahan Add(
                Kuliah kuliah, Ruangan ruangan, int hariPerkuliahan,
                int waktuMulai, int waktuSelesai, string penanggungJawab) {
            Perkuliahan perkuliahan = null;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}) " +
                        "VALUES ({7}, {8}, {9}, {10}, {11}, {12})",
                        TBL_PERKULIAHAN,
                        COL_KODE_KULIAH, COL_NAMA_RUANGAN, COL_HARI_PERKULIAHAN,
                        COL_WAKTU_MULAI, COL_WAKTU_SELESAI, COL_PENANGGUNG_JAWAB,
                        PRM_KODE, PRM_NAMA, PRM_HARI_PERKULIAHAN,
                        PRM_WAKTU_MULAI, PRM_WAKTU_SELESAI, PRM_TANGGUNG);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_KODE, kuliah.Kode);
                    command.Parameters.AddWithValue(PRM_NAMA, ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, hariPerkuliahan);
                    command.Parameters.AddWithValue(PRM_WAKTU_MULAI, waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, waktuSelesai);
                    command.Parameters.AddWithValue(PRM_TANGGUNG, penanggungJawab);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        perkuliahan = new Perkuliahan(kuliah, ruangan, hariPerkuliahan,
                            waktuMulai, waktuSelesai, penanggungJawab);
                }
            }
            catch (MySqlException) {
            }

            return perkuliahan;
        }

        public static bool Delete(Perkuliahan perkuliahan) {
            bool result = false;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "DELETE FROM {0} WHERE {1}={2} AND {3}={4} AND {5}={6} AND {7}={8} AND {9}={10}",
                        TBL_PERKULIAHAN,
                        COL_KODE_KULIAH, PRM_KODE,
                        COL_NAMA_RUANGAN, PRM_NAMA,
                        COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                        COL_WAKTU_MULAI, PRM_WAKTU_MULAI,
                        COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_KODE, perkuliahan.kuliah.Kode);
                    command.Parameters.AddWithValue(PRM_NAMA, perkuliahan.ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, perkuliahan.HariPerkuliahan);
                    command.Parameters.AddWithValue(PRM_WAKTU_MULAI, perkuliahan.WaktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, perkuliahan.WaktuSelesai);

                    connection.Open();
                    result = command.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException) {
            }

            return result;
        }

        public static bool Exists(Ruangan ruangan, DateTime tanggal, int waktuMulai, int waktuSelesai) {
            bool result = false;            

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "SELECT COUNT(*) FROM {0} WHERE {1}={2} AND {3}={4} AND ( ({5} BETWEEN {6} AND {7}) OR ({8} BETWEEN {9} AND {10}) ) ",
                        TBL_PERKULIAHAN,
                        COL_NAMA_RUANGAN, PRM_NAMA,
                        COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                        PRM_WAKTU_MULAI, COL_WAKTU_MULAI, COL_WAKTU_SELESAI + "-1",
                        PRM_WAKTU_SELESAI, COL_WAKTU_MULAI + "+1", COL_WAKTU_SELESAI);
                    
                    MySqlCommand command = new MySqlCommand(query, connection);                    
                    command.Parameters.AddWithValue(PRM_NAMA, ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, ((int)tanggal.DayOfWeek + 6) % 7);
                    command.Parameters.AddWithValue(PRM_WAKTU_MULAI, waktuMulai);
                    command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, waktuSelesai);

                    connection.Open();
                    result = (long)command.ExecuteScalar() > 0;
                }
            }
            catch (MySqlException) {
            }

            return false;
        }

        public Kuliah Kuliah {
            get { return this.kuliah; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_KODE_KULIAH, PRM_KODE + "1",
                            COL_KODE_KULIAH, PRM_KODE + "2",
                            COL_NAMA_RUANGAN, PRM_NAMA,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_KODE + "1", value.Kode);
                        command.Parameters.AddWithValue(PRM_KODE + "2", this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_NAMA, this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI, this.waktuMulai);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, this.waktuSelesai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.kuliah = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public Ruangan Ruangan {
            get { return this.ruangan; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_NAMA_RUANGAN, PRM_NAMA + "1",
                            COL_NAMA_RUANGAN, PRM_NAMA + "2",
                            COL_KODE_KULIAH, PRM_KODE,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_NAMA + "1", value.Nama);
                        command.Parameters.AddWithValue(PRM_NAMA + "2", this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_KODE, this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI, this.waktuMulai);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, this.waktuSelesai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.ruangan = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public int HariPerkuliahan {
            get { return this.hariPerkuliahan; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN + "1",
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN + "2",
                            COL_KODE_KULIAH, PRM_KODE,
                            COL_NAMA_RUANGAN, PRM_NAMA,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN + "1", value);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN + "2", this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_KODE, this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_NAMA, this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI, this.waktuMulai);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, this.waktuSelesai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.hariPerkuliahan = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public int WaktuMulai {
            get { return this.waktuMulai; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI + "1",
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI + "2",
                            COL_KODE_KULIAH, PRM_KODE,
                            COL_NAMA_RUANGAN, PRM_NAMA,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI + "1", value);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI + "2", this.waktuMulai);
                        command.Parameters.AddWithValue(PRM_KODE, this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_NAMA, this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, this.waktuSelesai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.waktuMulai = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public int WaktuSelesai {
            get { return this.waktuSelesai; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI + "1",
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI + "2",
                            COL_KODE_KULIAH, PRM_KODE,
                            COL_NAMA_RUANGAN, PRM_NAMA,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI + "1", value);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI + "2", this.waktuSelesai);
                        command.Parameters.AddWithValue(PRM_KODE, this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_NAMA, this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI, this.waktuMulai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.waktuSelesai = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public string PenanggungJawab {
            get { return this.penanggungJawab; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8} AND {9}={10} AND {11}={12}",
                            TBL_PERKULIAHAN,
                            COL_PENANGGUNG_JAWAB, PRM_TANGGUNG,
                            COL_KODE_KULIAH, PRM_KODE,
                            COL_NAMA_RUANGAN, PRM_NAMA,
                            COL_HARI_PERKULIAHAN, PRM_HARI_PERKULIAHAN,
                            COL_WAKTU_MULAI, PRM_WAKTU_MULAI,
                            COL_WAKTU_SELESAI, PRM_WAKTU_SELESAI);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_TANGGUNG, value);
                        command.Parameters.AddWithValue(PRM_KODE, this.kuliah.Kode);
                        command.Parameters.AddWithValue(PRM_NAMA, this.ruangan.Nama);
                        command.Parameters.AddWithValue(PRM_HARI_PERKULIAHAN, this.hariPerkuliahan);
                        command.Parameters.AddWithValue(PRM_WAKTU_MULAI, this.waktuMulai);
                        command.Parameters.AddWithValue(PRM_WAKTU_SELESAI, this.waktuSelesai);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.penanggungJawab = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }
    }
}
