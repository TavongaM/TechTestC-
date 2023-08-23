using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using AnyCompany.DTO;

namespace AnyCompany
{
    public class OrderRepository
    {
        public static Order Load(int orderId)
        {
            try
            {
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();

                command.CommandText = $@"SELECT * FROM Orders WHERE Id = {orderId} LIMIT 1;";
                SQLiteDataReader reader = command.ExecuteReader();

                bool success = false;
                Order order = new Order();
                while (reader.Read())
                {
                    int orderID = 0;
                    double vatAmount = 0;
                    double amount = 0;
                    int customerid = 0;

                    success = int.TryParse(reader["Id"].ToString(), out orderID);
                    success = double.TryParse(reader["VAT"].ToString(), out vatAmount);
                    success = double.TryParse(reader["Amount"].ToString(), out amount);
                    success = int.TryParse(reader["CustomerId"].ToString(), out customerid);

                    order.Id = orderId;
                    order.Amount = amount;
                    order.VAT = vatAmount;
                    order.CustomerId = customerid;
                }

                AnyCompany.Helpers.DBHelper.CloseDbConnection();
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearAll()
        {
            SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
            SQLiteCommand command = dbConnection.CreateCommand();
            command.CommandText = $@"Delete From Orders;";
            command.ExecuteNonQuery();
        }

        public static List<Order> LoadCustomerOrders(int customerId)
        {
            try
            {
                var ordersList = new List<Order>();
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();

                command.CommandText = $@"SELECT * FROM Orders Where CustomerId = {customerId};";
                SQLiteDataReader reader = command.ExecuteReader();

                bool success = false;
                while (reader.Read())
                {
                    Order order = new Order();
                    int orderId = 0;
                    double vatAmount = 0;
                    double amount = 0;
                    int customerid = 0;
                    success = int.TryParse(reader["Id"].ToString(), out orderId);
                    success = double.TryParse(reader["VAT"].ToString(), out vatAmount);
                    success = double.TryParse(reader["Amount"].ToString(), out amount);
                    success = int.TryParse(reader["CustomerId"].ToString(), out customerid);

                    order.Id = orderId;
                    order.Amount = amount;
                    order.VAT = vatAmount;
                    order.CustomerId = customerid;
                    ordersList.Add(order);
                }
                AnyCompany.Helpers.DBHelper.CloseDbConnection();
                return ordersList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order Save(OrderDTO order, int customerId)
        {
            try
            {
                SQLiteConnection dbConnection = AnyCompany.Helpers.DBHelper.CreateDbConnection();
                SQLiteCommand command = dbConnection.CreateCommand();
                command.CommandText = $@"INSERT INTO Orders (Amount,VAT,CustomerId) VALUES (@Amount, @VAT, @CustomerId)";
                command.Parameters.AddWithValue("@Amount", order.Amount);
                command.Parameters.AddWithValue("@VAT", order.VAT);
                command.Parameters.AddWithValue("@CustomerId", customerId);
                command.ExecuteNonQuery();

                SQLiteCommand command2 = dbConnection.CreateCommand();
                command2.CommandText = $@"SELECT ID FROM Orders WHERE (CustomerId=@CustomerId) And (Amount=@Amount) LIMIT 1;";
                command2.Parameters.AddWithValue("@CustomerId", customerId);
                command2.Parameters.AddWithValue("@Amount", order.Amount);
                Object obj = command2.ExecuteScalar();
                var inserted_OrderId = Convert.ToInt32(obj);

                if (inserted_OrderId > 0)
                {
                    AnyCompany.Helpers.DBHelper.CloseDbConnection();
                    command = null;
                    command2 = null;
                    return Load(inserted_OrderId);
                }
                else
                {
                    AnyCompany.Helpers.DBHelper.CloseDbConnection();
                    command = null;
                    command2 = null;
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
