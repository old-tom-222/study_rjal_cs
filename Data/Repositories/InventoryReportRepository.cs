using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class InventoryReportRepository
    {
        /// <summary>
        /// 获取库存概览数据
        /// </summary>
        public List<InventoryReportModel> GetInventoryOverview(int? warehouseId = null)
        {
            var result = new List<InventoryReportModel>();
            string sql = @"
                SELECT 
                    i.product_id, 
                    p.sku, 
                    p.name AS product_name,
                    i.warehouse_id,
                    w.name AS warehouse_name,
                    i.quantity AS current_quantity,
                    p.safe_stock,
                    COALESCE(p.reorder_quantity, 0) AS reorder_quantity,
                    COALESCE(AVG(pt.unit_cost), 0) AS average_cost,
                    MAX(t.created_at) AS last_stock_movement_date
                FROM inventory i
                INNER JOIN product p ON p.id = i.product_id
                INNER JOIN warehouse w ON w.id = i.warehouse_id
                LEFT JOIN inventory_transaction t ON t.product_id = i.product_id AND t.warehouse_id = i.warehouse_id
                LEFT JOIN purchase_transaction pt ON pt.product_id = i.product_id
                WHERE (@warehouseId IS NULL OR i.warehouse_id = @warehouseId)
                GROUP BY i.product_id, i.warehouse_id, p.sku, p.name, w.name, i.quantity, p.safe_stock, p.reorder_quantity
                ORDER BY p.name, w.name";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@warehouseId", (object)warehouseId ?? DBNull.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reportItem = new InventoryReportModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            CurrentQuantity = Convert.ToInt32(reader["current_quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            ReorderQuantity = Convert.ToInt32(reader["reorder_quantity"]),
                            AverageCost = reader["average_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["average_cost"]),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            LastStockMovementDate = reader["last_stock_movement_date"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["last_stock_movement_date"]),
                            // 实际应用中可以通过查询历史数据获取最低和最高库存
                            MinQuantity = 0,
                            MaxQuantity = Convert.ToInt32(reader["current_quantity"])
                        };
                        reportItem.TotalValue = reportItem.AverageCost * reportItem.CurrentQuantity;

                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取低库存产品数据
        /// </summary>
        public List<InventoryReportModel> GetLowStockItems(int? warehouseId = null)
        {
            var result = new List<InventoryReportModel>();
            string sql = @"
                SELECT 
                    i.product_id, 
                    p.sku, 
                    p.name AS product_name,
                    i.warehouse_id,
                    w.name AS warehouse_name,
                    i.quantity AS current_quantity,
                    p.safe_stock,
                    COALESCE(p.reorder_quantity, p.safe_stock) AS reorder_quantity,
                    COALESCE(AVG(pt.unit_cost), 0) AS average_cost
                FROM inventory i
                INNER JOIN product p ON p.id = i.product_id
                INNER JOIN warehouse w ON w.id = i.warehouse_id
                LEFT JOIN purchase_transaction pt ON pt.product_id = i.product_id
                WHERE i.quantity <= COALESCE(p.reorder_quantity, p.safe_stock)
                  AND (@warehouseId IS NULL OR i.warehouse_id = @warehouseId)
                GROUP BY i.product_id, i.warehouse_id, p.sku, p.name, w.name, i.quantity, p.safe_stock, p.reorder_quantity
                ORDER BY i.quantity ASC";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@warehouseId", (object)warehouseId ?? DBNull.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reportItem = new InventoryReportModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            CurrentQuantity = Convert.ToInt32(reader["current_quantity"]),
                            SafeStock = Convert.ToInt32(reader["safe_stock"]),
                            ReorderQuantity = Convert.ToInt32(reader["reorder_quantity"]),
                            AverageCost = reader["average_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["average_cost"]),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            MinQuantity = 0,
                            MaxQuantity = Convert.ToInt32(reader["current_quantity"])
                        };
                        reportItem.TotalValue = reportItem.AverageCost * reportItem.CurrentQuantity;

                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取库存周转数据
        /// </summary>
        public List<InventoryReportModel> GetInventoryTurnover(DateTime startDate, DateTime endDate, int? warehouseId = null)
        {
            // 简化实现，实际应用中需要更复杂的计算逻辑
            return GetInventoryOverview(warehouseId);
        }
    }
}