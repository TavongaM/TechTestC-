using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using AnyCompany.DTO;

namespace AnyCompany
{
    public static class CustomerRepository
    {
        public static Customer Load (int customerId)
        {
            try
            {
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();

                command.CommandText = $@"SELECT * FROM Customers WHERE Id = {customerId} ";
                SQLiteDataReader reader = command.ExecuteReader();

                Customer customer = new Customer();
                while (reader.Read())
                {
                    var dob = DateTime.Now;
                    var success = DateTime.TryParse(reader["DateOfBirth"].ToString(), out dob);
                    int id = 0;
                    success = Int32.TryParse(reader["Id"].ToString(), out id);

                    customer.Id = id;
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = dob;
                    customer.Country = reader["Country"].ToString();
                    customer.CustomerOrders = OrderRepository.LoadCustomerOrders(id);
                }

                AnyCompany.Helpers.DBHelper.CloseDbConnection();
                return customer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Customer> LoadAll()
        {
            try
            {
                var customersList = new List<Customer>();
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();

                command.CommandText = $@"SELECT * FROM Customers";
                SQLiteDataReader reader = command.ExecuteReader();

                
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    var dob = DateTime.Now;
                    var success = DateTime.TryParse(reader["DateOfBirth"].ToString(), out dob);
                    int id = 0;
                    success = Int32.TryParse(reader["Id"].ToString(), out id);

                    customer.Id = id;
                    customer.Name = reader["Name"].ToString();
                    customer.DateOfBirth = dob;
                    customer.Country = reader["Country"].ToString();
                    customer.CustomerOrders = OrderRepository.LoadCustomerOrders(id);
                    customersList.Add(customer);
                }

                AnyCompany.Helpers.DBHelper.CloseDbConnection();
                return customersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ClearAll()
        {
            SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
            SQLiteCommand command = dbConnection.CreateCommand();
            command.CommandText = $@"Delete From Customers;";
            command.ExecuteNonQuery();
        }

        public static Customer Save(CustomerDTO customer)
        {
            try
            {
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();
                command.CommandText = $@"INSERT INTO Customers (Country, DateOfBirth, Name) VALUES (@Country, @DateOfBirth, @Name);";
                command.Parameters.AddWithValue("@Country", customer.Country);
                command.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                command.Parameters.AddWithValue("@Name", customer.Name);
                command.ExecuteNonQuery();

                SQLiteCommand command2 = dbConnection.CreateCommand();
                command2.CommandText = $@"SELECT ID FROM Customers WHERE (Country=@Country) And (Name=@Name) LIMIT 1;";
                command2.Parameters.AddWithValue("@Country", customer.Country);
                command2.Parameters.AddWithValue("@Name", customer.Name);
                Object obj = command2.ExecuteScalar();
                var inserted_CustomerId  = Convert.ToInt32(obj);

                if (inserted_CustomerId > 0)
                {
                    AnyCompany.Helpers.DBHelper.CloseDbConnection();
                    command = null;
                    command2 = null;
                    return Load(inserted_CustomerId);
                }
                else
                {
                    AnyCompany.Helpers.DBHelper.CloseDbConnection();
                    command = null;
                    command2 = null;
                    return null;
                }

            } catch (Exception ex) {
                throw ex;
            }
        }

    }
}
