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
            string sql = @"SELECT i.product_id, p.sku, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price, i.last_updated
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
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                            LastUpdated = Convert.ToDateTime(reader["last_updated"])
                        });
                    }
                }
            }
            return result;
        }

        public InventoryItem GetInventory(int productId, int warehouseId)
        {
            InventoryItem item = null;
            string sql = @"SELECT i.product_id, p.sku, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price, i.last_updated
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
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                        UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                        LastUpdated = Convert.ToDateTime(reader["last_updated"])
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
            string sql = @"SELECT i.product_id, p.sku, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, p.sale_price, i.last_updated
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
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            Quantity = Convert.ToInt32(reader["quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            UnitPrice = Convert.ToDecimal(reader["sale_price"]),
                            LastUpdated = Convert.ToDateTime(reader["last_updated"])
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
                
                // 检查库存记录是否存在
                string checkSql = @"SELECT quantity FROM inventory 
                                   WHERE product_id = @productId AND warehouse_id = @warehouseId";
                MySqlCommand checkCmd = new MySqlCommand(checkSql, connection);
                checkCmd.Parameters.AddWithValue("@productId", productId);
                checkCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                
                object result = checkCmd.ExecuteScalar();
                
                if (result != null)
                {
                    // 库存记录存在，更新数量
                    int currentQuantity = Convert.ToInt32(result);
                    int newQuantity = currentQuantity + quantityChange;
                    
                    string updateSql = @"UPDATE inventory 
                                       SET quantity = @newQuantity, last_updated = @lastUpdated
                                       WHERE product_id = @productId AND warehouse_id = @warehouseId";
                    MySqlCommand updateCmd = new MySqlCommand(updateSql, connection);
                    updateCmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                    updateCmd.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
                    updateCmd.Parameters.AddWithValue("@productId", productId);
                    updateCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                    
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    // 库存记录不存在，创建新记录
                    string insertSql = @"INSERT INTO inventory (product_id, warehouse_id, quantity, last_updated)
                                       VALUES (@productId, @warehouseId, @quantity, @lastUpdated)";
                    MySqlCommand insertCmd = new MySqlCommand(insertSql, connection);
                    insertCmd.Parameters.AddWithValue("@productId", productId);
                    insertCmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                    insertCmd.Parameters.AddWithValue("@quantity", quantityChange);
                    insertCmd.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
                    
                    insertCmd.ExecuteNonQuery();
                }
                
                // 记录库存流水
                var transactionRepo = new InventoryTransactionRepository();
                transactionRepo.AddTransaction(productId, warehouseId, quantityChange, "采购入库", "采购订单", "采购订单完成后自动入库");
            }
        }
        
        /// <summary>
        /// 减少库存数量
        /// </summary>
        public bool ReduceInventory(int productId, int warehouseId, int quantity)
        {
            string sql = @"UPDATE inventory 
                           SET quantity = quantity - @quantity, 
                               last_updated = NOW()
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