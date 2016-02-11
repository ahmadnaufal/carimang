﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {
    public class Perbaikan {

        private static string TBL_PERBAIKAN = "perbaikan";

        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_TANGGAL_MULAI = "tanggal_mulai";
        private static string COL_TANGGAL_SELESAI = "tanggal_selesai";
        private static string COL_DESKRIPSI_PERBAIKAN = "deskripsi";

        private static string PRM_NAMA_RUANGAN = "@nama_ruangan";
        private static string PRM_TANGGAL = "@tanggal";
        private static string PRM_TANGGAL_MULAI = "@tanggal_mulai";
        private static string PRM_TANGGAL_SELESAI = "@tanggal_selesai";
        private static string PRM_DESKRIPSI_PERBAIKAN = "@deskripsi";

        public static string FMT_TANGGAL = "yyyy-MM-dd";

        private Ruangan ruangan = null;
        private DateTime tanggalmulai = DateTime.Now;
        private DateTime tanggalselesai = DateTime.Now;
        private string deskripsi = "";

        private Perbaikan(Ruangan ruangan, DateTime tanggalmulai, DateTime tanggalselesai, string deskripsi)
        {
            this.ruangan = ruangan;
            this.tanggalmulai = tanggalmulai;
            this.tanggalselesai = tanggalselesai;
            this.deskripsi = deskripsi;
        }

        public static List<Perbaikan> GetAll()
        {
            List<Perbaikan> listPerbaikan = new List<Perbaikan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                String query = String.Format(
                    "SELECT * FROM {0}", TBL_PERBAIKAN);

                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        listPerbaikan.Add(new Perbaikan(
                            ruangan,
                            (DateTime)reader[COL_TANGGAL_MULAI],
                            (DateTime)reader[COL_TANGGAL_SELESAI],
                            (string)reader[COL_DESKRIPSI_PERBAIKAN]));
                    }
                }
            }
            return listPerbaikan;
        }

        public static List<Perbaikan> GetAll(DateTime tanggal) {
            List<Perbaikan> listPerbaikan = new List<Perbaikan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                String query = String.Format(
                    "SELECT * FROM {0} WHERE {1} BETWEEN {2} AND {3}", 
                    TBL_PERBAIKAN,
                    PRM_TANGGAL, COL_TANGGAL_MULAI, COL_TANGGAL_SELESAI);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_TANGGAL, tanggal.Date.ToString(FMT_TANGGAL));

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        listPerbaikan.Add(new Perbaikan(
                            ruangan,
                            (DateTime)reader[COL_TANGGAL_MULAI],
                            (DateTime)reader[COL_TANGGAL_SELESAI],
                            (string)reader[COL_DESKRIPSI_PERBAIKAN]));
                    }
                }
            }
            return listPerbaikan;
        }

        public static Perbaikan Get(string nama_ruangan)
        {
            Perbaikan perbaikan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "SELECT * FROM {0} WHERE {1}={2}",
                    TBL_PERBAIKAN,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, nama_ruangan);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Ruangan ruangan = Ruangan.Get((string)reader[COL_NAMA_RUANGAN]);
                        perbaikan = new Perbaikan(
                            ruangan,
                            (DateTime)reader[COL_TANGGAL_MULAI],
                            (DateTime)reader[COL_TANGGAL_SELESAI],
                            (string)reader[COL_DESKRIPSI_PERBAIKAN]);
                    }
                }
            }
            return perbaikan;
        }

        public static Perbaikan Add(Ruangan ruangan, DateTime tanggalmulai, DateTime tanggalselesai, string deskripsi)
        {
            Perbaikan perbaikan = null;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "INSERT INTO {0} ({1}, {2}, {3}, {4}) VALUES ({5}, {6}, {7}, {8})",
                    TBL_PERBAIKAN,
                    COL_NAMA_RUANGAN, COL_TANGGAL_MULAI,
                    COL_TANGGAL_SELESAI, COL_DESKRIPSI_PERBAIKAN,
                    PRM_NAMA_RUANGAN, PRM_TANGGAL_MULAI,
                    PRM_TANGGAL_SELESAI, PRM_DESKRIPSI_PERBAIKAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, tanggalmulai.Date.ToString(FMT_TANGGAL));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, tanggalselesai.Date.ToString(FMT_TANGGAL));
                command.Parameters.AddWithValue(PRM_DESKRIPSI_PERBAIKAN, deskripsi);

                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    perbaikan = new Perbaikan(ruangan, tanggalmulai, tanggalselesai, deskripsi);
            }
            return perbaikan;
        }
        
        public static bool Delete(Perbaikan perbaikan)
        {
            bool result = false;

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                string query = String.Format(
                    "DELETE FROM {0} WHERE {1}={2} AND {3}={4} AND {5}={6}",
                    TBL_PERBAIKAN,
                    COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                    COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI,
                    COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, perbaikan.Ruangan.Nama);
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, perbaikan.TanggalMulai.Date.ToString(FMT_TANGGAL));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, perbaikan.TanggalSelesai.Date.ToString(FMT_TANGGAL));

                connection.Open();
                result = command.ExecuteNonQuery() > 0;
            }
            return result;
        }

        public static bool Exists(Ruangan ruangan, DateTime tanggal) {
            bool result = false;

            try {
                using (MySqlConnection connection = MySqlConnector.GetConnection()) {
                    string query = String.Format(
                        "SELECT COUNT(*) FROM {0} WHERE {1}={2} AND ({3} BETWEEN {4} AND {5})",
                        TBL_PERBAIKAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        PRM_TANGGAL, COL_TANGGAL_MULAI, COL_TANGGAL_SELESAI);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL, tanggal.Date.ToString(FMT_TANGGAL));                    

                    connection.Open();
                    result = (long)command.ExecuteScalar() > 0;
                }
            }
            catch (MySqlException) {                
            }

            return result;
        }

        public Ruangan Ruangan
        {
            get { return this.ruangan; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8}",
                        TBL_PERBAIKAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "1",
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN + "2",
                        COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI,
                        COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "1", value.Nama);
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN + "2", this.Ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, this.tanggalmulai.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, this.tanggalselesai.Date.ToString(FMT_TANGGAL));

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.ruangan = value;
                }
            }
        }

        public DateTime TanggalMulai
        {
            get { return this.tanggalmulai; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8}",
                        TBL_PERBAIKAN,
                        COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI + "1",
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI + "2",
                        COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_TANGGAL_MULAI + "1", value.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.Ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_MULAI + "2", this.tanggalmulai.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, this.tanggalselesai.Date.ToString(FMT_TANGGAL));

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.tanggalmulai = value;
                }
            }
        }

        public DateTime TanggalSelesai
        {
            get { return this.tanggalselesai; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8}",
                        TBL_PERBAIKAN,
                        COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI + "1",
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI,
                        COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI + "2");

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI + "1", value.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.Ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, this.tanggalmulai.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI + "2", this.tanggalselesai.Date.ToString(FMT_TANGGAL));

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.tanggalselesai = value;
                }
            }
        }

        public string Deskripsi
        {
            get { return this.deskripsi; }
            set
            {
                using (MySqlConnection connection = MySqlConnector.GetConnection())
                {
                    string query = String.Format(
                        "UPDATE {0} SET {1}={2} WHERE {3}={4} AND {5}={6} AND {7}={8}",
                        TBL_PERBAIKAN,
                        COL_DESKRIPSI_PERBAIKAN, PRM_DESKRIPSI_PERBAIKAN,
                        COL_NAMA_RUANGAN, PRM_NAMA_RUANGAN,
                        COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI,
                        COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI);

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue(PRM_DESKRIPSI_PERBAIKAN, value.ToString());
                    command.Parameters.AddWithValue(PRM_NAMA_RUANGAN, this.Ruangan.Nama);
                    command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, this.tanggalmulai.Date.ToString(FMT_TANGGAL));
                    command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, this.tanggalselesai.Date.ToString(FMT_TANGGAL));

                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        this.deskripsi = value;
                }
            }
        }


    }
}
