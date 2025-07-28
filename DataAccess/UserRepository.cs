// DataAccess/UserRepository.cs - Complete Implementation with All Methods
using ABCCarTraders.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ABCCarTraders.DataAccess
{
    public class UserRepository
    {
        public User GetUserByCredentials(string username, string password)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password AND IsActive = 1";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public bool CreateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Users (Username, Password, FullName, Email, Phone, Address, UserType) 
                               VALUES (@username, @password, @fullName, @email, @phone, @address, @userType)";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@password", user.Password);
                    command.Parameters.AddWithValue("@fullName", user.FullName);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@phone", user.Phone ?? "");
                    command.Parameters.AddWithValue("@address", user.Address ?? "");
                    command.Parameters.AddWithValue("@userType", user.UserType.ToString());
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE Users SET FullName = @fullName, Email = @email, 
                               Phone = @phone, Address = @address, IsActive = @isActive 
                               WHERE UserID = @userId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user.UserID);
                    command.Parameters.AddWithValue("@fullName", user.FullName);
                    command.Parameters.AddWithValue("@email", user.Email);
                    command.Parameters.AddWithValue("@phone", user.Phone ?? "");
                    command.Parameters.AddWithValue("@address", user.Address ?? "");
                    command.Parameters.AddWithValue("@isActive", user.IsActive);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete order items first
                        string deleteOrderItemsQuery = @"DELETE oi FROM OrderItems oi 
                                                       INNER JOIN Orders o ON oi.OrderID = o.OrderID 
                                                       WHERE o.CustomerID = @userId";
                        using (var command = new MySqlCommand(deleteOrderItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
                            command.ExecuteNonQuery();
                        }

                        // Delete orders
                        string deleteOrdersQuery = "DELETE FROM Orders WHERE CustomerID = @userId";
                        using (var command = new MySqlCommand(deleteOrdersQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
                            command.ExecuteNonQuery();
                        }

                        // Delete user
                        string deleteUserQuery = "DELETE FROM Users WHERE UserID = @userId";
                        using (var command = new MySqlCommand(deleteUserQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@userId", userId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<User> GetAllCustomers()
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserType = 'Customer' ORDER BY CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapUserFromReader(reader));
                    }
                }
            }
            return customers;
        }

        public List<User> GetActiveCustomers()
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserType = 'Customer' AND IsActive = 1 ORDER BY CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapUserFromReader(reader));
                    }
                }
            }
            return customers;
        }

        public List<User> GetInactiveCustomers()
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserType = 'Customer' AND IsActive = 0 ORDER BY CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapUserFromReader(reader));
                    }
                }
            }
            return customers;
        }

        public User GetUserById(int userId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE UserID = @userId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public User GetUserByUsername(string username)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @username";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public User GetUserByEmail(string email)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Email = @email";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUserFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public bool IsUsernameExists(string username)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public bool IsEmailExists(string email)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @email";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public bool ToggleUserStatus(int userId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Users SET IsActive = NOT IsActive WHERE UserID = @userId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateUserPassword(int userId, string newPassword)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Users SET Password = @password WHERE UserID = @userId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@password", newPassword);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<User> SearchCustomers(string searchTerm)
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT * FROM Users 
                               WHERE UserType = 'Customer' 
                               AND (FullName LIKE @searchTerm 
                                    OR Username LIKE @searchTerm 
                                    OR Email LIKE @searchTerm 
                                    OR Phone LIKE @searchTerm)
                               ORDER BY CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(MapUserFromReader(reader));
                        }
                    }
                }
            }
            return customers;
        }

        public List<User> GetCustomersByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT * FROM Users 
                               WHERE UserType = 'Customer' 
                               AND CreatedDate >= @fromDate 
                               AND CreatedDate < @toDate
                               ORDER BY CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fromDate", fromDate);
                    command.Parameters.AddWithValue("@toDate", toDate);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customers.Add(MapUserFromReader(reader));
                        }
                    }
                }
            }
            return customers;
        }

        public int GetTotalCustomersCount()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE UserType = 'Customer'";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int GetActiveCustomersCount()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE UserType = 'Customer' AND IsActive = 1";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int GetInactiveCustomersCount()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE UserType = 'Customer' AND IsActive = 0";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public List<User> GetRecentCustomers(int count = 10)
        {
            List<User> customers = new List<User>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = $"SELECT * FROM Users WHERE UserType = 'Customer' ORDER BY CreatedDate DESC LIMIT {count}";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(MapUserFromReader(reader));
                    }
                }
            }
            return customers;
        }

        private User MapUserFromReader(MySqlDataReader reader)
        {
            return new User
            {
                UserID = Convert.ToInt32(reader["UserID"]),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"].ToString(),
                Phone = reader["Phone"].ToString(),
                Address = reader["Address"].ToString(),
                UserType = (UserType)Enum.Parse(typeof(UserType), reader["UserType"].ToString()),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
            };
        }
		
		public User GetCustomerByName(string fullName)
{
    using (var connection = DatabaseConnection.GetConnection())
    {
        connection.Open();
        string query = "SELECT * FROM Users WHERE FullName = @fullName AND UserType = 'Customer'";
        
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@fullName", fullName);
            
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapUserFromReader(reader);
                }
            }
        }
    }
    return null;
}
    }
}