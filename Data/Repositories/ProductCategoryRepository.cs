using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class ProductCategoryRepository
    {
        public List<ProductCategory> GetProductCategories(string categoryName = null, string status = null)
        {
            var result = new List<ProductCategory>();
            string sql = @"SELECT c.id AS CategoryId, c.name AS CategoryName, c.parent_id AS ParentCategoryId, 
                                 pc.name AS ParentCategoryName, c.status AS Status
                            FROM product_category c
                            LEFT JOIN product_category pc ON c.parent_id = pc.id
                            WHERE (@categoryName IS NULL OR c.name LIKE @categoryName)
                              AND (@status IS NULL OR c.status = @status)
                            ORDER BY c.name";
            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (categoryName != null)
                        {
                            cmd.Parameters.AddWithValue("@categoryName", $"%{categoryName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@categoryName", DBNull.Value);
                        }
                        if (status != null)
                        {
                            cmd.Parameters.AddWithValue("@status", status);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@status", DBNull.Value);
                        }
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new ProductCategory
                                {
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    ParentCategoryId = reader.IsDBNull(reader.GetOrdinal("ParentCategoryId")) ? (int?)null : Convert.ToInt32(reader["ParentCategoryId"]),
                                    ParentCategoryName = reader.IsDBNull(reader.GetOrdinal("ParentCategoryName")) ? null : reader["ParentCategoryName"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    CreatedDate = DateTime.MinValue,
                                    LastUpdated = DateTime.MinValue,
                                    Notes = null
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取商品分类列表失败: {ex.Message}", ex);
            }
        }
        
        public ProductCategory GetProductCategoryById(int categoryId)
        {
            ProductCategory category = null;
            string sql = @"SELECT c.id AS CategoryId, c.name AS CategoryName, c.parent_id AS ParentCategoryId, 
                                 pc.name AS ParentCategoryName, c.status AS Status
                            FROM product_category c
                            LEFT JOIN product_category pc ON c.parent_id = pc.id
                            WHERE c.id = @categoryId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new ProductCategory
                        {
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            ParentCategoryId = reader.IsDBNull(reader.GetOrdinal("ParentCategoryId")) ? (int?)null : Convert.ToInt32(reader["ParentCategoryId"]),
                            ParentCategoryName = reader.IsDBNull(reader.GetOrdinal("ParentCategoryName")) ? null : reader["ParentCategoryName"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.MinValue,
                            LastUpdated = DateTime.MinValue,
                            Notes = null
                        };
                    }
                }
            }
            return category;
        }
        
        public List<ProductCategory> GetAllProductCategories()
        {
            return GetProductCategories();
        }
        
        public List<ProductCategory> GetActiveProductCategories()
        {
            return GetProductCategories(status: "1");
        }
        
        public int CreateProductCategory(ProductCategory category)
        {
            string sql = @"INSERT INTO product_category (name, parent_id, status)
                            VALUES (@categoryName, @parentCategoryId, @status);
                            SELECT LAST_INSERT_ID();";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@parentCategoryId", category.ParentCategoryId.HasValue ? (object)category.ParentCategoryId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", category.Status ?? "1");
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateProductCategory(ProductCategory category)
        {
            string sql = @"UPDATE product_category 
                            SET name = @categoryName, 
                                parent_id = @parentCategoryId, 
                                status = @status
                            WHERE id = @categoryId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                cmd.Parameters.AddWithValue("@parentCategoryId", category.ParentCategoryId.HasValue ? (object)category.ParentCategoryId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", category.Status);
                cmd.Parameters.AddWithValue("@categoryId", category.CategoryId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteProductCategory(int categoryId)
        {
            string sql = "DELETE FROM product_category WHERE id = @categoryId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}