using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace CariMang {

    public static partial class MySqlConnector {        

        public static MySqlConnection GetConnection() {
            MySqlConnection connection = null;            
            string connectionString = String.Format(
                @"SERVER={0};DATABASE={1};UID={2};PWD={3};Convert Zero Datetime=True",
                server, database, uid, password);
            try {
                connection = new MySqlConnection(connectionString);
            }
            catch (MySqlException) {
                connection = null;
            }            
            return connection;
        }

        public static bool CheckConnection() {
            bool result = false;
            var connection = GetConnection();
            if (connection == null)
                return result;
            try {
                connection.Open();
                result = true;
            }            
            catch (MySqlException e) {
                Console.WriteLine(e.Message);
            }
            finally {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return result;
        }
    }
}
