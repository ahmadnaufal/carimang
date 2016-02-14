using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {
    public class StatistikRusak {

        private static string TBL_PERBAIKAN = "perbaikan";

        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_JUMLAH_PERBAIKAN = "jumlah_perbaikan";
        private static string COL_TANGGAL_MULAI = "tanggal_mulai";
        private static string COL_TANGGAL_SELESAI = "tanggal_selesai";

        private static string PRM_TANGGAL_MULAI = "@tanggal_mulai";
        private static string PRM_TANGGAL_SELESAI = "@tanggal_selesai";

        private string namaruangan = "";
        private long jumlahperbaikan = 0;

        private StatistikRusak(string namaruangan, long jumlahperbaikan) {
            this.namaruangan = namaruangan;
            this.jumlahperbaikan = jumlahperbaikan;
        }

        public static List<StatistikRusak> GetStatistik(DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<StatistikRusak> listStatistikRusak = new List<StatistikRusak>();

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                String query = String.Format(
                    "SELECT {0}, COUNT(*) AS {1} FROM {2} WHERE ({3} >= {4} AND {5} <= {6}) OR ({7} >= {8} AND {9} <= {10}) OR ({11} <= {12} AND {13} >= {14}) GROUP BY {15} ORDER BY {16} DESC LIMIT 5",
                    COL_NAMA_RUANGAN, COL_JUMLAH_PERBAIKAN,
                    TBL_PERBAIKAN,
                    COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI + "1",
                    COL_TANGGAL_MULAI, PRM_TANGGAL_SELESAI + "1",
                    COL_TANGGAL_SELESAI, PRM_TANGGAL_MULAI + "2",
                    COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI + "2",
                    COL_TANGGAL_MULAI, PRM_TANGGAL_MULAI + "3",
                    COL_TANGGAL_SELESAI, PRM_TANGGAL_SELESAI + "3",
                    COL_NAMA_RUANGAN, COL_JUMLAH_PERBAIKAN);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI + "1", tanggalAwal.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI + "1", tanggalAkhir.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI + "2", tanggalAwal.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI + "2", tanggalAkhir.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI + "3", tanggalAwal.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI + "3", tanggalAkhir.Date.ToString("yyyy-MM-dd"));

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listStatistikRusak.Add(new StatistikRusak(
                            (string)reader[COL_NAMA_RUANGAN],
                            (long)reader[COL_JUMLAH_PERBAIKAN]
                        ));
                    }
                }
            }
            return listStatistikRusak;
        }

        public string NamaRuangan
        {
            get { return this.namaruangan; }
            set { this.namaruangan = value; }
        }

        public long JumlahPerbaikan
        {
            get { return this.jumlahperbaikan; }
            set { this.jumlahperbaikan = value; }
        }
    }
}
