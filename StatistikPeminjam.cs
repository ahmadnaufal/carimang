using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang
{
    public class StatistikPeminjam
    {
        private static string TBL_PEMINJAM = "peminjam";
        private static string TBL_KEGIATAN = "kegiatan";

        private static string COL_ID_PEMINJAM = "id_peminjam";
        private static string COL_NAMA_PEMINJAM = "nama_peminjam";
        private static string COL_JUMLAH_PEMINJAM = "jumlah_peminjam";
        private static string COL_TANGGAL_KEGIATAN = "tanggal";

        private static string PRM_TANGGAL_MULAI = "@tanggal_mulai";
        private static string PRM_TANGGAL_SELESAI = "@tanggal_selesai";

        private string namapeminjam = "";
        private long jumlahpeminjam = 0;

        private StatistikPeminjam(string namapeminjam, long jumlahpeminjam)
        {
            this.namapeminjam = namapeminjam;
            this.jumlahpeminjam = jumlahpeminjam;
        }

        public static List<StatistikPeminjam> GetStatistik(DateTime tanggalAwal, DateTime tanggalAkhir)
        {
            List<StatistikPeminjam> listStatistikPeminjam = new List<StatistikPeminjam>();

            using (MySqlConnection connection = MySqlConnector.GetConnection())
            {
                String query = String.Format(
                    "SELECT {0}, COUNT(*) AS {1} FROM {2} NATURAL JOIN {3} WHERE ({4} >= {5} AND {4} <= {6}) GROUP BY {0} ORDER BY {1} DESC LIMIT 5",
                    COL_NAMA_PEMINJAM, COL_JUMLAH_PEMINJAM,
                    TBL_PEMINJAM, TBL_KEGIATAN,
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
                        listStatistikPeminjam.Add(new StatistikPeminjam(
                            (string)reader[COL_NAMA_PEMINJAM],
                            (long)reader[COL_JUMLAH_PEMINJAM]
                        ));
                    }
                }
            }
            return listStatistikPeminjam;
        }

        public string NamaPeminjam
        {
            get { return this.namapeminjam; }
            set { this.namapeminjam = value; }
        }

        public long JumlahPeminjam
        {
            get { return this.jumlahpeminjam; }
            set { this.jumlahpeminjam = value; }
        }
    }
}
