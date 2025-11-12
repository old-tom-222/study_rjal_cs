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
                                   i.quantity, p.safe_stock, i.last_updated
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
                                   i.quantity, p.safe_stock, i.last_updated
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
                            LastUpdated = Convert.ToDateTime(reader["last_updated"])
                        };
                    }
                }
            }
            return item;
        }



        // 库存预警：数量低于安全库存
        public List<InventoryItem> GetLowStockWarnings()
        {
            var result = new List<InventoryItem>();
            string sql = @"SELECT i.product_id, p.sku, p.name AS product_name,
                                   i.warehouse_id, w.name AS warehouse_name,
                                   i.quantity, p.safe_stock, i.last_updated
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
                            LastUpdated = Convert.ToDateTime(reader["last_updated"])
                        });
                    }
                }
            }
            return result;
        }
    }
}