using ABCCarTraders.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ABCCarTraders.DataAccess
{
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Categories ORDER BY CategoryType, CategoryName";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), reader["CategoryType"].ToString())
                        });
                    }
                }
            }
            return categories;
        }

        public List<Category> GetCarCategories()
        {
            List<Category> categories = new List<Category>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Categories WHERE CategoryType = 'Car' ORDER BY CategoryName";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), reader["CategoryType"].ToString())
                        });
                    }
                }
            }
            return categories;
        }

        public List<Category> GetPartCategories()
        {
            List<Category> categories = new List<Category>();
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Categories WHERE CategoryType = 'Part' ORDER BY CategoryName";
                
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), reader["CategoryType"].ToString())
                        });
                    }
                }
            }
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Categories WHERE CategoryID = @categoryId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Category
                            {
                                CategoryID = Convert.ToInt32(reader["CategoryID"]),
                                CategoryName = reader["CategoryName"].ToString(),
                                CategoryType = (CategoryType)Enum.Parse(typeof(CategoryType), reader["CategoryType"].ToString())
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool AddCategory(Category category)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Categories (CategoryName, CategoryType) VALUES (@categoryName, @categoryType)";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@categoryType", category.CategoryType.ToString());
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateCategory(Category category)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = "UPDATE Categories SET CategoryName = @categoryName, CategoryType = @categoryType WHERE CategoryID = @categoryId";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", category.CategoryID);
                    command.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    command.Parameters.AddWithValue("@categoryType", category.CategoryType.ToString());
                    
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCategory(int categoryId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                
                // Check if category is in use
                string checkQuery = "SELECT COUNT(*) FROM Cars WHERE CategoryID = @categoryId UNION ALL SELECT COUNT(*) FROM CarParts WHERE CategoryID = @categoryId";
                using (var checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@categoryId", categoryId);
                    var result = checkCommand.ExecuteScalar();
                    
                    if (Convert.ToInt32(result) > 0)
                    {
                        return false; // Category is in use, cannot delete
                    }
                }
                
                string query = "DELETE FROM Categories WHERE CategoryID = @categoryId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool IsCategoryInUse(int categoryId)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                connection.Open();
                string query = @"SELECT 
                                (SELECT COUNT(*) FROM Cars WHERE CategoryID = @categoryId) +
                                (SELECT COUNT(*) FROM CarParts WHERE CategoryID = @categoryId) as TotalUsage";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
    }
}