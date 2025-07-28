using ABCCarTraders.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ABCCarTraders.DataAccess
{
    public class CarRepository
    {
        public List<Car> GetAllCars()
        {
            List<Car> cars = new List<Car>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT c.*, cat.CategoryName 
                               FROM Cars c 
                               LEFT JOIN Categories cat ON c.CategoryID = cat.CategoryID 
                               ORDER BY c.CreatedDate DESC";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cars.Add(MapCarFromReader(reader));
                    }
                }
            }
            return cars;
        }

        public List<Car> SearchCars(string searchTerm, int categoryId)
        {
            List<Car> cars = new List<Car>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT c.*, cat.CategoryName 
                               FROM Cars c 
                               LEFT JOIN Categories cat ON c.CategoryID = cat.CategoryID 
                               WHERE c.IsAvailable = 1 
                               AND (@searchTerm = '' OR c.Brand LIKE @searchTerm OR c.Model LIKE @searchTerm)
                               AND (@categoryId = 0 OR c.CategoryID = @categoryId)";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(MapCarFromReader(reader));
                        }
                    }
                }
            }
            return cars;
        }

        public bool AddCar(Car car)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Cars (Brand, Model, Year, Price, Color, Mileage, FuelType, 
                               Transmission, Description, ImagePath, CategoryID) 
                               VALUES (@brand, @model, @year, @price, @color, @mileage, @fuelType, 
                               @transmission, @description, @imagePath, @categoryId)";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@brand", car.Brand);
                    command.Parameters.AddWithValue("@model", car.Model);
                    command.Parameters.AddWithValue("@year", car.Year);
                    command.Parameters.AddWithValue("@price", car.Price);
                    command.Parameters.AddWithValue("@color", car.Color ?? "");
                    command.Parameters.AddWithValue("@mileage", car.Mileage);
                    command.Parameters.AddWithValue("@fuelType", car.FuelType ?? "");
                    command.Parameters.AddWithValue("@transmission", car.Transmission ?? "");
                    command.Parameters.AddWithValue("@description", car.Description ?? "");
                    command.Parameters.AddWithValue("@imagePath", car.ImagePath ?? "");
                    command.Parameters.AddWithValue("@categoryId", car.CategoryID);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateCar(Car car)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"UPDATE Cars SET Brand = @brand, Model = @model, Year = @year, 
                               Price = @price, Color = @color, Mileage = @mileage, FuelType = @fuelType, 
                               Transmission = @transmission, Description = @description, ImagePath = @imagePath, 
                               CategoryID = @categoryId, IsAvailable = @isAvailable WHERE CarID = @carId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@carId", car.CarID);
                    command.Parameters.AddWithValue("@brand", car.Brand);
                    command.Parameters.AddWithValue("@model", car.Model);
                    command.Parameters.AddWithValue("@year", car.Year);
                    command.Parameters.AddWithValue("@price", car.Price);
                    command.Parameters.AddWithValue("@color", car.Color ?? "");
                    command.Parameters.AddWithValue("@mileage", car.Mileage);
                    command.Parameters.AddWithValue("@fuelType", car.FuelType ?? "");
                    command.Parameters.AddWithValue("@transmission", car.Transmission ?? "");
                    command.Parameters.AddWithValue("@description", car.Description ?? "");
                    command.Parameters.AddWithValue("@imagePath", car.ImagePath ?? "");
                    command.Parameters.AddWithValue("@categoryId", car.CategoryID);
                    command.Parameters.AddWithValue("@isAvailable", car.IsAvailable);
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCar(int carId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Cars WHERE CarID = @carId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@carId", carId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private Car MapCarFromReader(MySqlDataReader reader)
        {
            return new Car
            {
                CarID = Convert.ToInt32(reader["CarID"]),
                Brand = reader["Brand"].ToString(),
                Model = reader["Model"].ToString(),
                Year = Convert.ToInt32(reader["Year"]),
                Price = Convert.ToDecimal(reader["Price"]),
                Color = reader["Color"].ToString(),
                Mileage = Convert.ToInt32(reader["Mileage"]),
                FuelType = reader["FuelType"].ToString(),
                Transmission = reader["Transmission"].ToString(),
                Description = reader["Description"].ToString(),
                ImagePath = reader["ImagePath"].ToString(),
                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CategoryName = reader["CategoryName"].ToString()
            };
        }
    }
}
