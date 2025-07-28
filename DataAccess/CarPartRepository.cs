// DataAccess/CarPartRepository.cs - Complete Implementation with UpdateStock
using ABCCarTraders.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ABCCarTraders.DataAccess
{
    public class CarPartRepository
    {
        public List<CarPart> GetAllCarParts()
        {
            List<CarPart> parts = new List<CarPart>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT cp.*, cat.CategoryName 
                               FROM CarParts cp 
                               LEFT JOIN Categories cat ON cp.CategoryID = cat.CategoryID 
                               ORDER BY cp.CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parts.Add(MapCarPartFromReader(reader));
                    }
                }
            }
            return parts;
        }

        public List<CarPart> SearchCarParts(string searchTerm, int categoryId)
        {
            List<CarPart> parts = new List<CarPart>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT cp.*, cat.CategoryName 
                               FROM CarParts cp 
                               LEFT JOIN Categories cat ON cp.CategoryID = cat.CategoryID 
                               WHERE cp.IsAvailable = 1 
                               AND (@searchTerm = '' OR cp.PartName LIKE @searchTerm OR cp.Brand LIKE @searchTerm OR cp.PartNumber LIKE @searchTerm)
                               AND (@categoryId = 0 OR cp.CategoryID = @categoryId)
                               ORDER BY cp.PartName";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parts.Add(MapCarPartFromReader(reader));
                        }
                    }
                }
            }
            return parts;
        }

        public bool AddCarPart(CarPart part)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO CarParts (PartName, PartNumber, Brand, Price, Description, 
                               ImagePath, CategoryID, StockQuantity) 
                               VALUES (@partName, @partNumber, @brand, @price, @description, 
                               @imagePath, @categoryId, @stockQuantity)";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partName", part.PartName);
                    command.Parameters.AddWithValue("@partNumber", part.PartNumber ?? "");
                    command.Parameters.AddWithValue("@brand", part.Brand ?? "");
                    command.Parameters.AddWithValue("@price", part.Price);
                    command.Parameters.AddWithValue("@description", part.Description ?? "");
                    command.Parameters.AddWithValue("@imagePath", part.ImagePath ?? "");
                    command.Parameters.AddWithValue("@categoryId", part.CategoryID);
                    command.Parameters.AddWithValue("@stockQuantity", part.StockQuantity);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateCarPart(CarPart part)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE CarParts SET PartName = @partName, PartNumber = @partNumber, 
                               Brand = @brand, Price = @price, Description = @description, 
                               ImagePath = @imagePath, CategoryID = @categoryId, 
                               StockQuantity = @stockQuantity, IsAvailable = @isAvailable 
                               WHERE PartID = @partId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partId", part.PartID);
                    command.Parameters.AddWithValue("@partName", part.PartName);
                    command.Parameters.AddWithValue("@partNumber", part.PartNumber ?? "");
                    command.Parameters.AddWithValue("@brand", part.Brand ?? "");
                    command.Parameters.AddWithValue("@price", part.Price);
                    command.Parameters.AddWithValue("@description", part.Description ?? "");
                    command.Parameters.AddWithValue("@imagePath", part.ImagePath ?? "");
                    command.Parameters.AddWithValue("@categoryId", part.CategoryID);
                    command.Parameters.AddWithValue("@stockQuantity", part.StockQuantity);
                    command.Parameters.AddWithValue("@isAvailable", part.IsAvailable);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCarPart(int partId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM CarParts WHERE PartID = @partId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partId", partId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        // THIS IS THE MISSING METHOD THAT WAS CAUSING THE ERROR
        public bool UpdateStock(int partId, int newStock)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE CarParts SET StockQuantity = @stock WHERE PartID = @partId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partId", partId);
                    command.Parameters.AddWithValue("@stock", newStock);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public CarPart GetCarPartById(int partId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT cp.*, cat.CategoryName 
                               FROM CarParts cp 
                               LEFT JOIN Categories cat ON cp.CategoryID = cat.CategoryID 
                               WHERE cp.PartID = @partId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partId", partId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapCarPartFromReader(reader);
                        }
                    }
                }
            }
            return null;
        }

        public List<CarPart> GetLowStockParts(int threshold = 5)
        {
            List<CarPart> parts = new List<CarPart>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT cp.*, cat.CategoryName 
                               FROM CarParts cp 
                               LEFT JOIN Categories cat ON cp.CategoryID = cat.CategoryID 
                               WHERE cp.StockQuantity <= @threshold 
                               AND cp.IsAvailable = 1
                               ORDER BY cp.StockQuantity ASC";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@threshold", threshold);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            parts.Add(MapCarPartFromReader(reader));
                        }
                    }
                }
            }
            return parts;
        }

        public List<CarPart> GetOutOfStockParts()
        {
            List<CarPart> parts = new List<CarPart>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT cp.*, cat.CategoryName 
                               FROM CarParts cp 
                               LEFT JOIN Categories cat ON cp.CategoryID = cat.CategoryID 
                               WHERE cp.StockQuantity = 0 
                               AND cp.IsAvailable = 1
                               ORDER BY cp.PartName";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parts.Add(MapCarPartFromReader(reader));
                    }
                }
            }
            return parts;
        }

        public bool AdjustStock(int partId, int adjustment)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE CarParts 
                               SET StockQuantity = GREATEST(0, StockQuantity + @adjustment) 
                               WHERE PartID = @partId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@partId", partId);
                    command.Parameters.AddWithValue("@adjustment", adjustment);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public int GetTotalPartsCount()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM CarParts WHERE IsAvailable = 1";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public decimal GetTotalInventoryValue()
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT SUM(Price * StockQuantity) FROM CarParts WHERE IsAvailable = 1";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    var result = command.ExecuteScalar();
                    return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                }
            }
        }

        private CarPart MapCarPartFromReader(MySqlDataReader reader)
        {
            return new CarPart
            {
                PartID = Convert.ToInt32(reader["PartID"]),
                PartName = reader["PartName"].ToString(),
                PartNumber = reader["PartNumber"].ToString(),
                Brand = reader["Brand"].ToString(),
                Price = Convert.ToDecimal(reader["Price"]),
                Description = reader["Description"].ToString(),
                ImagePath = reader["ImagePath"].ToString(),
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                StockQuantity = Convert.ToInt32(reader["StockQuantity"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CategoryName = reader["CategoryName"]?.ToString()
            };
        }
    }
}