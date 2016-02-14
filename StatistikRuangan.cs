using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang
{
    public class StatistikRuangan
    {
        private static string TBL_KEGIATAN = "kegiatan";

        private static string COL_NAMA_RUANGAN = "nama_ruangan";
        private static string COL_JUMLAH_PEMAKAIAN = "jumlah_pemakaian";
        private static string COL_TANGGAL_KEGIATAN = "tanggal";

        private static string PRM_TANGGAL_MULAI = "@tanggal_mulai";
        private static string PRM_TANGGAL_SELESAI = "@tanggal_selesai";

        private string namaruangan = "";
        private long jumlahpemakaian = 0;

        private StatistikRuangan(string namaruangan, long jumlahpemakaian)
        {
            this.namaruangan = namaruangan;
            this.jumlahpemakaian = jumlahpemakaian;
        }

        public static List<StatistikRuangan> GetStatistik(DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<StatistikRuangan> listStatistikRuangan = new List<StatistikRuangan>();

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                String query = String.Format(
                    "SELECT {0}, COUNT(*) AS {1} FROM {2} WHERE ({3} >= {4} AND {3} <= {5}) GROUP BY {0} ORDER BY {1} DESC LIMIT 5",
                    COL_NAMA_RUANGAN, COL_JUMLAH_PEMAKAIAN,
                    TBL_KEGIATAN,
                    COL_TANGGAL_KEGIATAN, PRM_TANGGAL_MULAI,
                    PRM_TANGGAL_SELESAI
                    );

                Console.WriteLine(query);

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue(PRM_TANGGAL_MULAI, tanggalAwal.Date.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue(PRM_TANGGAL_SELESAI, tanggalAkhir.Date.ToString("yyyy-MM-dd"));

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listStatistikRuangan.Add(new StatistikRuangan(
                            (string)reader[COL_NAMA_RUANGAN],
                            (long)reader[COL_JUMLAH_PEMAKAIAN]
                        ));
                    }
                }
            }
            return listStatistikRuangan;
        }

        public string NamaRuangan
        {
            get { return this.namaruangan; }
            set { this.namaruangan = value; }
        }

        public long JumlahPemakaian
        {
            get { return this.jumlahpemakaian; }
            set { this.jumlahpemakaian = value; }
        }
    }
}
