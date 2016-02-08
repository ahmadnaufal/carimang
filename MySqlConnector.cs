using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {

    static partial class MySqlConnector {

        private static MySqlConnection connection = null;

        public static MySqlConnection GetConnection() {
            if (connection == null) {
                string connectionString = String.Format(
                    @"SERVER={0};DATABASE={1};UID={2};PWD={3}",
                    server, database, uid, password);
                try {
                    connection = new MySqlConnection(connectionString);
                } catch (MySqlException) {
                    connection = null;
                }
            }
            return connection;
        }
    }
}
