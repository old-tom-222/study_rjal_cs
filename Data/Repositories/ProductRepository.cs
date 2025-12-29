using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class ProductRepository
    {
        public List<Product> GetProducts(string productName = null, int? categoryId = null, string status = null)
        {
            var result = new List<Product>();
            string sql = @"SELECT p.id AS ProductId, p.name AS ProductName, 
                                 p.category_id AS CategoryId, pc.name AS CategoryName,
                                 p.price AS CostPrice, p.price AS SalePrice,
                                 p.stock_qty AS SafeStock, p.status AS Status
                            FROM product p
                            INNER JOIN product_category pc ON p.category_id = pc.id
                            WHERE (@productName IS NULL OR p.name LIKE @productName)
                              AND (@categoryId IS NULL OR p.category_id = @categoryId)
                              AND (@status IS NULL OR p.status = @status)
                            ORDER BY p.name";
            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (productName != null)
                        {
                            cmd.Parameters.AddWithValue("@productName", $"%{productName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@productName", DBNull.Value);
                        }
                        if (categoryId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@categoryId", categoryId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@categoryId", DBNull.Value);
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
                                result.Add(new Product
                                {
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Sku = string.Empty,
                                    ProductName = reader["ProductName"].ToString(),
                                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                    CategoryName = reader["CategoryName"].ToString(),
                                    CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                                    SalePrice = Convert.ToDecimal(reader["SalePrice"]),
                                    SafeStock = Convert.ToInt32(reader["SafeStock"]),
                                    Status = reader["Status"].ToString(),
                                    CreatedDate = DateTime.MinValue,
                                    LastUpdated = DateTime.MinValue
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取商品列表失败: {ex.Message}", ex);
            }
        }
        
        public Product GetProductById(int productId)
        {
            Product product = null;
            string sql = @"SELECT p.id AS ProductId, p.name AS ProductName, 
                                 p.category_id AS CategoryId, pc.name AS CategoryName,
                                 p.price AS CostPrice, p.price AS SalePrice,
                                 p.stock_qty AS SafeStock, p.status AS Status
                            FROM product p
                            INNER JOIN product_category pc ON p.category_id = pc.id
                            WHERE p.id = @productId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ProductId = Convert.ToInt32(reader["ProductId"]),
                            Sku = string.Empty,
                            ProductName = reader["ProductName"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            CategoryName = reader["CategoryName"].ToString(),
                            CostPrice = Convert.ToDecimal(reader["CostPrice"]),
                            SalePrice = Convert.ToDecimal(reader["SalePrice"]),
                            SafeStock = Convert.ToInt32(reader["SafeStock"]),
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.MinValue,
                            LastUpdated = DateTime.MinValue
                        };
                    }
                }
            }
            return product;
        }
        
        public List<Product> GetAllProducts()
        {
            return GetProducts();
        }
        
        public List<Product> GetActiveProducts()
        {
            return GetProducts(status: "1");
        }
        
        public int CreateProduct(Product product)
        {
            string sql = @"INSERT INTO product (name, category_id, price, stock_qty, status)
                            VALUES (@productName, @categoryId, @price, @stockQty, @status);
                            SELECT LAST_INSERT_ID();";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.CostPrice); // 只使用一个price字段
                cmd.Parameters.AddWithValue("@stockQty", product.SafeStock);
                cmd.Parameters.AddWithValue("@status", product.Status ?? "1");
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateProduct(Product product)
        {
            string sql = @"UPDATE product 
                            SET name = @productName, 
                                category_id = @categoryId,
                                price = @price,
                                stock_qty = @stockQty,
                                status = @status
                            WHERE id = @productId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@productName", product.ProductName);
                cmd.Parameters.AddWithValue("@categoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@price", product.CostPrice); // 只使用一个price字段
                cmd.Parameters.AddWithValue("@stockQty", product.SafeStock);
                cmd.Parameters.AddWithValue("@status", product.Status);
                cmd.Parameters.AddWithValue("@productId", product.ProductId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteProduct(int productId)
        {
            string sql = "DELETE FROM product WHERE id = @productId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}