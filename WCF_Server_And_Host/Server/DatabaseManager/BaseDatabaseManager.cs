using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.DatabaseManager
{
    public class BaseDatabaseManager
    {
        protected BaseDatabaseManager() { }

        public static MySqlConnection connection
        {
            get {
                MySqlConnection connection = new MySqlConnection();
                string connectionString = "SERVER=localhost;"+"DATABASE=cardata;"+"UID=root;"+"PASSWORD=;"+"SSL MODE=none;";
                connection.ConnectionString = connectionString;
                return connection;
            }
        }

    }
}