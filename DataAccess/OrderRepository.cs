// DataAccess/OrderRepository.cs - Complete Implementation with All Methods
using ABCCarTraders.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABCCarTraders.DataAccess
{
    public class OrderRepository
    {
        public bool CreateOrder(Order order)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert order
                        string orderQuery = @"INSERT INTO Orders (CustomerID, TotalAmount, Status) 
                                           VALUES (@customerId, @totalAmount, @status);
                                           SELECT LAST_INSERT_ID();";
                        
                        int orderId;
                        using (var command = new MySqlCommand(orderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@customerId", order.CustomerID);
                            command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                            command.Parameters.AddWithValue("@status", order.Status.ToString());
                            
                            orderId = Convert.ToInt32(command.ExecuteScalar());
                        }

                        // Insert order items
                        foreach (var item in order.OrderItems)
                        {
                            string itemQuery = @"INSERT INTO OrderItems (OrderID, ItemType, ItemID, Quantity, Price) 
                                              VALUES (@orderId, @itemType, @itemId, @quantity, @price)";
                            
                            using (var command = new MySqlCommand(itemQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@orderId", orderId);
                                command.Parameters.AddWithValue("@itemType", item.ItemType.ToString());
                                command.Parameters.AddWithValue("@itemId", item.ItemID);
                                command.Parameters.AddWithValue("@quantity", item.Quantity);
                                command.Parameters.AddWithValue("@price", item.Price);
                                
                                command.ExecuteNonQuery();
                            }
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

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE o.CustomerID = @customerId 
                               ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerId", customerId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(MapOrderFromReader(reader));
                        }
                    }
                }
            }
            return orders;
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(MapOrderFromReader(reader));
                    }
                }
            }
            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE o.OrderID = @orderId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var order = MapOrderFromReader(reader);
                            // Load order items separately
                            order.OrderItems = GetOrderItems(orderId);
                            return order;
                        }
                    }
                }
            }
            return null;
        }

        public List<OrderItem> GetOrderItems(int orderId)
        {
            List<OrderItem> items = new List<OrderItem>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT oi.*, 
                               CASE 
                                 WHEN oi.ItemType = 'Car' THEN CONCAT(c.Brand, ' ', c.Model, ' (', c.Year, ')')
                                 WHEN oi.ItemType = 'Part' THEN CONCAT(cp.PartName, ' - ', cp.Brand)
                                 ELSE 'Unknown Item'
                               END as ItemName
                               FROM OrderItems oi
                               LEFT JOIN Cars c ON oi.ItemID = c.CarID AND oi.ItemType = 'Car'
                               LEFT JOIN CarParts cp ON oi.ItemID = cp.PartID AND oi.ItemType = 'Part'
                               WHERE oi.OrderID = @orderId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new OrderItem
                            {
                                OrderItemID = Convert.ToInt32(reader["OrderItemID"]),
                                OrderID = Convert.ToInt32(reader["OrderID"]),
                                ItemType = (ItemType)Enum.Parse(typeof(ItemType), reader["ItemType"].ToString()),
                                ItemID = Convert.ToInt32(reader["ItemID"]),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"]),
                                ItemName = reader["ItemName"].ToString()
                            });
                        }
                    }
                }
            }
            return items;
        }

        public bool DeleteOrder(int orderId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete order items first
                        string deleteItemsQuery = "DELETE FROM OrderItems WHERE OrderID = @orderId";
                        using (var command = new MySqlCommand(deleteItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
                            command.ExecuteNonQuery();
                        }

                        // Delete order
                        string deleteOrderQuery = "DELETE FROM Orders WHERE OrderID = @orderId";
                        using (var command = new MySqlCommand(deleteOrderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
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

        public bool UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Orders SET Status = @status WHERE OrderID = @orderId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@status", newStatus.ToString());
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public int GetOrderItemCount(int orderId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM OrderItems WHERE OrderID = @orderId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE o.Status = @status 
                               ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", status.ToString());
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(MapOrderFromReader(reader));
                        }
                    }
                }
            }
            return orders;
        }

        public List<Order> GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE o.OrderDate >= @fromDate AND o.OrderDate < @toDate 
                               ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fromDate", fromDate);
                    command.Parameters.AddWithValue("@toDate", toDate);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(MapOrderFromReader(reader));
                        }
                    }
                }
            }
            return orders;
        }

        public List<Order> SearchOrders(string searchTerm)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE o.OrderID LIKE @searchTerm 
                               OR u.FullName LIKE @searchTerm 
                               OR u.Username LIKE @searchTerm 
                               ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(MapOrderFromReader(reader));
                        }
                    }
                }
            }
            return orders;
        }

        public int GetTotalOrdersCount()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Orders";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int GetOrdersCountByStatus(OrderStatus status)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Orders WHERE Status = @status";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@status", status.ToString());
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public decimal GetTotalRevenue()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COALESCE(SUM(TotalAmount), 0) FROM Orders WHERE Status = 'Completed'";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToDecimal(command.ExecuteScalar());
                }
            }
        }

        public decimal GetRevenueByDateRange(DateTime fromDate, DateTime toDate)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT COALESCE(SUM(TotalAmount), 0) FROM Orders 
                               WHERE Status = 'Completed' 
                               AND OrderDate >= @fromDate AND OrderDate < @toDate";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fromDate", fromDate);
                    command.Parameters.AddWithValue("@toDate", toDate);
                    return Convert.ToDecimal(command.ExecuteScalar());
                }
            }
        }

        public List<Order> GetRecentOrders(int count = 10)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = $@"SELECT o.*, u.FullName as CustomerName 
                                FROM Orders o 
                                JOIN Users u ON o.CustomerID = u.UserID 
                                ORDER BY o.OrderDate DESC 
                                LIMIT {count}";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(MapOrderFromReader(reader));
                    }
                }
            }
            return orders;
        }

        public List<Order> GetPendingOrders()
        {
            return GetOrdersByStatus(OrderStatus.Pending);
        }

        public List<Order> GetCompletedOrders()
        {
            return GetOrdersByStatus(OrderStatus.Completed);
        }

        public List<Order> GetCancelledOrders()
        {
            return GetOrdersByStatus(OrderStatus.Cancelled);
        }

        public bool UpdateOrder(Order order)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE Orders SET TotalAmount = @totalAmount, Status = @status 
                               WHERE OrderID = @orderId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", order.OrderID);
                    command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                    command.Parameters.AddWithValue("@status", order.Status.ToString());
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public decimal GetAverageOrderValue()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COALESCE(AVG(TotalAmount), 0) FROM Orders WHERE Status = 'Completed'";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToDecimal(command.ExecuteScalar());
                }
            }
        }

        public Dictionary<string, int> GetOrderStatusCounts()
        {
            var statusCounts = new Dictionary<string, int>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT Status, COUNT(*) as Count FROM Orders GROUP BY Status";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        statusCounts.Add(reader["Status"].ToString(), Convert.ToInt32(reader["Count"]));
                    }
                }
            }
            return statusCounts;
        }

        public List<Order> GetOrdersForExport(DateTime? fromDate = null, DateTime? toDate = null, OrderStatus? status = null)
        {
            List<Order> orders = new List<Order>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT o.*, u.FullName as CustomerName, u.Email, u.Phone 
                               FROM Orders o 
                               JOIN Users u ON o.CustomerID = u.UserID 
                               WHERE 1=1";
                
                if (fromDate.HasValue)
                    query += " AND o.OrderDate >= @fromDate";
                if (toDate.HasValue)
                    query += " AND o.OrderDate < @toDate";
                if (status.HasValue)
                    query += " AND o.Status = @status";
                
                query += " ORDER BY o.OrderDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    if (fromDate.HasValue)
                        command.Parameters.AddWithValue("@fromDate", fromDate.Value);
                    if (toDate.HasValue)
                        command.Parameters.AddWithValue("@toDate", toDate.Value);
                    if (status.HasValue)
                        command.Parameters.AddWithValue("@status", status.Value.ToString());
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = MapOrderFromReader(reader);
                            // Add additional export fields if needed
                            orders.Add(order);
                        }
                    }
                }
            }
            return orders;
        }

        private Order MapOrderFromReader(MySqlDataReader reader)
        {
            return new Order
            {
                OrderID = Convert.ToInt32(reader["OrderID"]),
                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader["Status"].ToString()),
                CustomerName = reader["CustomerName"].ToString()
            };
        }
    }
}