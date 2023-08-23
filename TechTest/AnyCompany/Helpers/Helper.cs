using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace AnyCompany.Helpers
{
    public static class DBHelper
    {

        private static SQLiteConnection _dbConnection;
        private static string _connectionString = @"Data Source=AnyCompany.db; Version = 3; New = True; Compress = True;";
        private static bool _tableCheck = false;

        private static bool CreateDBTables_If_Neccesary()
        {
            try
            {
                SQLiteCommand cmd = _dbConnection.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER PRIMARY KEY, Country TEXT, DateOfBirth TEXT, Name TEXT)";
                cmd.ExecuteNonQuery();

                SQLiteCommand cmd2 = _dbConnection.CreateCommand();
                cmd2.CommandText = "CREATE TABLE IF NOT EXISTS Orders (Id INTEGER PRIMARY KEY, Amount REAL, VAT REAL, CustomerID INTEGER)";
                cmd2.ExecuteNonQuery();
                _tableCheck = true;
            }
            catch (Exception ex)
            {
                _tableCheck = false;
            }

            return _tableCheck;
        }


        public static SQLiteConnection CreateDbConnection()
        {
            // Create a new database connection:
            _dbConnection = new SQLiteConnection(_connectionString);

            // Open the connection:
            try
            {
                _dbConnection.Open();
                if (!_tableCheck) CreateDBTables_If_Neccesary();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _dbConnection;
        }

        public static void CloseDbConnection()
        {
            // Open the connection:
            try
            {
                if (_dbConnection != null) _dbConnection.Close();
            }
            catch (Exception ex)
            {
                _dbConnection = null;
            }
        }

    }
}
