using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class InventoryRepository
    {
        public List<InventoryItem> GetInventoryList(int? productId = null, int? warehouseId = null)
        {
            var result = new List<InventoryItem>();
            string sql = @"SELECT i.product_id, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price
                            FROM inventory i
                            INNER JOIN product p ON p.id = i.product_id
                            INNER JOIN warehouse w ON w.id = i.warehouse_id
                            WHERE (@productId IS NULL OR i.product_id = @productId)
                              AND (@warehouseId IS NULL OR i.warehouse_id = @warehouseId)
                            ORDER BY p.name, w.name";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@productId", (object)productId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@warehouseId", (object)warehouseId ?? DBNull.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryItem
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = string.Empty,
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                            LastUpdated = DateTime.Now
                        });
                    }
                }
            }
            return result;
        }

        public InventoryItem GetInventory(int productId, int warehouseId)
        {
            InventoryItem item = null;
            string sql = @"SELECT i.product_id, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price
                            FROM inventory i
                            INNER JOIN product p ON p.id = i.product_id
                            INNER JOIN warehouse w ON w.id = i.warehouse_id
                            WHERE i.product_id = @productId AND i.warehouse_id = @warehouseId";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        item = new InventoryItem
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = string.Empty,
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                            LastUpdated = DateTime.Now
                        };
                    }
                }
            }
            return item;
        }



        /// <summary>
        /// 获取指定产品在所有仓库中的库存总和
        /// </summary>
        public int GetTotalInventoryByProductId(int productId)
        {
            // 先检查产品是否存在
            string checkProductSql = "SELECT COUNT(*) FROM product WHERE id = @productId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            {
                conn.Open();
                
                // 检查产品存在性
                using (var checkCmd = new MySqlCommand(checkProductSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@productId", productId);
                    int productCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                    
                    if (productCount == 0)
                    {
                        // 产品不存在，返回-1
                        return -1;
                    }
                }
                
                // 产品存在，计算总库存
                string inventorySql = "SELECT COALESCE(SUM(quantity), 0) FROM inventory WHERE product_id = @productId";
                using (var inventoryCmd = new MySqlCommand(inventorySql, conn))
                {
                    inventoryCmd.Parameters.AddWithValue("@productId", productId);
                    return Convert.ToInt32(inventoryCmd.ExecuteScalar());
                }
            }
        }

        // 库存预警：数量低于安全库存
        public List<InventoryItem> GetLowStockWarnings()
        {
            var result = new List<InventoryItem>();
            string sql = @"SELECT i.product_id, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price
                            FROM inventory i
                            INNER JOIN product p ON p.id = i.product_id
                            INNER JOIN warehouse w ON w.id = i.warehouse_id
                            WHERE i.quantity < p.safe_stock
                            ORDER BY p.name, w.name";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryItem
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = string.Empty,
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                            LastUpdated = DateTime.Now
                        });
                    }
                }
            }
            return result;
        }

        // 更新库存数量（用于采购订单完成后自动更新库存）
        public void UpdateInventory(int productId, int warehouseId, int quantityChange)
        {
            string connectionString = DbHelper.GetConnectionString();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                UpdateInventory(productId, warehouseId, quantityChange, connection);
            }
        }
        
        // 重载方法：支持事务
        public void UpdateInventory(int productId, int warehouseId, int quantityChange, MySqlConnection connection)
        {
            UpdateInventory(productId, warehouseId, quantityChange, connection, null);
        }
        
        // 重载方法：支持事务和事务对象
        public void UpdateInventory(int productId, int warehouseId, int quantityChange, MySqlConnection connection, MySqlTransaction transaction)
        {
            // 检查库存记录是否存在
            string checkSql = @"SELECT quantity FROM inventory 
                               WHERE product_id = @productId AND warehouse_id = @warehouseId";
            MySqlCommand checkCmd = new MySqlCommand(checkSql, connection, transaction);
            checkCmd.Parameters.AddWithValue("@productId", productId);
            checkCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
            
            object result = checkCmd.ExecuteScalar();
            
            if (result != null)
            {
                // 库存记录存在，更新数量
                int currentQuantity = Convert.ToInt32(result);
                int newQuantity = currentQuantity + quantityChange;
                
                string updateSql = @"UPDATE inventory 
                                   SET quantity = @newQuantity
                                   WHERE product_id = @productId AND warehouse_id = @warehouseId";
                MySqlCommand updateCmd = new MySqlCommand(updateSql, connection, transaction);
                updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                updateCmd.Parameters.AddWithValue("@productId", productId);
                updateCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                
                updateCmd.ExecuteNonQuery();
            }
            else
            {
                // 库存记录不存在，创建新记录
                string insertSql = @"INSERT INTO inventory (product_id, warehouse_id, quantity)
                                   VALUES (@productId, @warehouseId, @quantity)";
                MySqlCommand insertCmd = new MySqlCommand(insertSql, connection, transaction);
                insertCmd.Parameters.AddWithValue("@productId", productId);
                insertCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                insertCmd.Parameters.AddWithValue("@quantity", quantityChange);
                
                insertCmd.ExecuteNonQuery();
            }
            
            // 记录库存流水
            var transactionRepo = new InventoryTransactionRepository();
            if (transaction != null)
            {
                // 在事务中，使用同一个连接和事务对象
                transactionRepo.AddTransaction(productId, warehouseId, quantityChange, "采购入库", "采购订单", "采购订单完成后自动入库", connection, transaction);
            }
            else
            {
                // 不在事务中，使用默认方式
                transactionRepo.AddTransaction(productId, warehouseId, quantityChange, "采购入库", "采购订单", "采购订单完成后自动入库");
            }
        }
        
        /// <summary>
        /// 获取当前库存数量
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <param name="warehouseId">仓库ID</param>
        /// <returns>当前库存数量</returns>
        public int GetCurrentStock(int productId, int warehouseId)
        {
            string connectionString = DbHelper.GetConnectionString();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return GetCurrentStock(productId, warehouseId, connection);
            }
        }
        
        /// <summary>
        /// 获取当前库存数量（支持事务）
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="connection">数据库连接</param>
        /// <returns>当前库存数量</returns>
        public int GetCurrentStock(int productId, int warehouseId, MySqlConnection connection)
        {
            return GetCurrentStock(productId, warehouseId, connection, null);
        }
        
        /// <summary>
        /// 获取当前库存数量（支持事务和事务对象）
        /// </summary>
        /// <param name="productId">产品ID</param>
        /// <param name="warehouseId">仓库ID</param>
        /// <param name="connection">数据库连接</param>
        /// <param name="transaction">事务对象</param>
        /// <returns>当前库存数量</returns>
        public int GetCurrentStock(int productId, int warehouseId, MySqlConnection connection, MySqlTransaction transaction)
        {
            string sql = @"SELECT quantity FROM inventory 
                           WHERE product_id = @productId AND warehouse_id = @warehouseId";
            MySqlCommand cmd = new MySqlCommand(sql, connection, transaction);
            cmd.Parameters.AddWithValue("@productId", productId);
            cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
            
            object result = cmd.ExecuteScalar();
            return result != null ? Convert.ToInt32(result) : 0;
        }
        
        /// <summary>
        /// 减少库存数量
        /// </summary>
        public bool ReduceInventory(int productId, int warehouseId, int quantity)
        {
            string sql = @"UPDATE inventory 
                           SET quantity = quantity - @quantity
                           WHERE product_id = @productId 
                             AND warehouse_id = @warehouseId 
                             AND quantity >= @quantity";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                
                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
    }
}