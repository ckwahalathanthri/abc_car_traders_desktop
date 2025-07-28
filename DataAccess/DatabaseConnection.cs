using MySql.Data.MySqlClient;
using System;

namespace ABCCarTraders.DataAccess
{
    public class DatabaseConnection
    {
        private static string connectionString = "Server=localhost;Port=3307;Database=ABCCarTradersApp;Uid=root;Pwd=1234;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}