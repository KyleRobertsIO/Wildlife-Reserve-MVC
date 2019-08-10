using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Reserve_API.Database
{

    /*
        Author: Kyle 
    */

    public class DBUtil
    {

        public string getConnectionString(string dbName)
        {
            string server = WebConfigurationManager.AppSettings["dbServer"];
            string port = WebConfigurationManager.AppSettings["dbPort"];
            string userID = WebConfigurationManager.AppSettings["dbUid"];
            string password = WebConfigurationManager.AppSettings["dbPassword"];
            string connectionString = $"{server}{port}Database={dbName};{userID}{password};SslMode=none;";
            return connectionString;
        }

        public MySqlDataAdapter getConnectionSelect(string query, string dbName)
        {
            string connectionString = getConnectionString(dbName);
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlDataAdapter _adp = new MySqlDataAdapter(query, connection);
            connection.Close();
            return _adp;
        }

        public bool getConnectionUpdate(string query, string dbName)
        {
            try
            {
                string connectionString = getConnectionString(dbName);
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader;
                connection.Open();
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool getConnectionDelete(string query, string dbName)
        {
            try
            {
                string connectionString = getConnectionString(dbName);
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader;
                connection.Open();
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool getConnectionExecute(string query, string dbName)
        {
            try
            {
                string connectionString = getConnectionString(dbName);
                MySqlConnection connection = new MySqlConnection(connectionString);
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader;
                connection.Open();
                reader = command.ExecuteReader();
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}