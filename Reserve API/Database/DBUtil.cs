using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Reserve_API.Database
{
    public class DBUtil
    {

        public string getConnectionString(string dbName)
        {
            string server = WebConfigurationManager.AppSettings["dbServer"];
            string port = WebConfigurationManager.AppSettings["dbPort"];
            string userID = WebConfigurationManager.AppSettings["dbUid"];
            string password = WebConfigurationManager.AppSettings["dbPassword"];
            string connectionString = $"{server}{port}Database={dbName};{userID}{password}";
            return connectionString;
        }

        public MySqlDataAdapter getConnection(string query, string dbName)
        {
            string connectionString = getConnectionString(dbName);
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter _adp = new MySqlDataAdapter(query, connection);
            connection.Close();
            return _adp;
        }

    }
}