using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {
    public class RuanganStatus : Tuple<bool, string> {
        public RuanganStatus(bool availability, string reason): base(availability, reason) {

        }

        public bool Available {
            get { return this.Item1; }
            private set { }
        }
        public string Reason {
            get { return this.Item2;  }
            private set { }
        }
    }

    public class Ruangan {        

        public enum TipeRuangan {
            RuangKelas,
            Laboratorium
        }

        private static string TBL_RUANGAN = "ruangan";

        private static string COL_TIPE_RUANGAN = "tipe_ruangan";
        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_KAPASITAS = "kapasitas";

        private static string PRM_TIPE_RUANGAN = "@tipe";
        private static string PRM_NAMA_RUANGAN = "@nama";
        private static string PRM_KAPASITAS = "@kapasitas";

        private TipeRuangan tipe = TipeRuangan.RuangKelas;
        private int kapasitas = 0;
        private string nama = "";

        private Ruangan(TipeRuangan tipe, string nama, int kapasitas) {
            this.tipe = tipe;
            this.nama = nama;
            this.kapasitas = kapasitas;
        }

        public override bool Equals(object obj) {
            if (obj == null)
                return false;

            Ruangan ruangan = obj as Ruangan;
            return this.Equals(ruangan);
        }

        public bool Equals(Ruangan ruangan) {            
            if ((object)ruangan == null)
                return false;

            return this.nama.Equals(ruangan.nama);
        }

        public override int GetHashCode() {
            return this.nama.GetHashCode();
        }

        public override string ToString() {
            return this.nama;
        }

        public static List<Ruangan> GetAll() {
            List<Ruangan> listRuangan = new List<Ruangan>();

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    String query = String.Format(
                        "SELECT * FROM {0}", TBL_RUANGAN);

                    MySqlCommand command = new MySqlCommand(query, connection);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        while (reader.Read()) {
                            listRuangan.Add(new Ruangan(
                                (TipeRuangan)reader[COL_TIPE_RUANGAN],
                                (string)reader[COL_NAMA_RUANGAN],
                                (int)reader[COL_KAPASITAS]));
                        }
                    }
                }
            }
            catch (MySqlException) {
            }

            return listRuangan;
        }

        public static Ruangan Get(string nama) {
            Ruangan ruangan = null;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "SELECT * FROM {0} WHERE {1}={2}",
                        TBL_RUANGAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, nama);

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader()) {
                        if (reader.Read()) {
                            ruangan = new Ruangan(
                                (TipeRuangan)reader[COL_TIPE_RUANGAN],
                                (string)reader[COL_NAMA_RUANGAN],
                                (int)reader[COL_KAPASITAS]);
                        }
                    }
                }
            }
            catch (MySqlException) {
            }

            return ruangan;
        }

        public static Ruangan Add(TipeRuangan tipe, string nama, int kapasitas) {
            Ruangan ruangan = null;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "INSERT INTO {0} ({1}, {2}, {3}) VALUES ({4}, {5}, {6})",
                        TBL_RUANGAN,
                        COL_TIPE_RUANGAN, COL_NAMA_RUANGAN, COL_KAPASITAS,
                        PRM_TIPE_RUANGAN, PRM_NAMA_RUANGAN, PRM_KAPASITAS);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_TIPE_RUANGAN, tipe);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, nama);
                    command.Parameters.AddWithValue(PRM_KAPASITAS, kapasitas);

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        ruangan = new Ruangan(tipe, nama, kapasitas);
                }
            }
            catch (MySqlException) {
            }

            return ruangan;
        }

        public static bool Delete(Ruangan ruangan) {
            bool result = false;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "DELETE FROM {0} WHERE {1}={2}",
                        TBL_RUANGAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);

                    connection.Open();
                    result = command.ExecuteNonQuery() > 0;
                }
            }
            catch (MySqlException) {                
            }

            return result;
        }

        public TipeRuangan Tipe {
            get { return this.tipe; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                            TBL_RUANGAN,
                            COL_TIPE_RUANGAN, PRM_TIPE_RUANGAN,
                            COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_TIPE_RUANGAN, value);
                        command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.nama);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.tipe = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public string Nama {
            get { return this.nama; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                            TBL_RUANGAN,
                            COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "1",
                            COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "2");

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "1", value);
                        command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "2", this.nama);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.nama = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public int Kapasitas {
            get { return this.kapasitas; }
            set {
                try {
                    using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                        string query = String.Format(
                            "UPDATE {0} SET {1}={2} WHERE {3}={4}",
                            TBL_RUANGAN,
                            COL_KAPASITAS, PRM_KAPASITAS,
                            COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN);

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue(PRM_KAPASITAS, value);
                        command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.nama);

                        connection.Open();
                        if (command.ExecuteNonQuery() > 0)
                            this.kapasitas = value;
                    }
                }
                catch (MySqlException) {
                }
            }
        }

        public RuanganStatus Status(DateTime tanggal, int waktuMulai, int waktuSelesai) {
            if (Perkuliahan.Exists(this, tanggal, waktuMulai, waktuSelesai)) {
                return new RuanganStatus(false, "Ada perkuliahan.");
            }
            else if (Perbaikan.Exists(this, tanggal)) {
                return new RuanganStatus(false, "Ada perbaikan.");
            }
            else {
                return new RuanganStatus(true, null);
            }
        }
    }
}
