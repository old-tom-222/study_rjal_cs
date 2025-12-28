using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class PurchaseReportRepository
    {
        /// <summary>
        /// 获取产品采购报表数据
        /// </summary>
        public List<PurchaseReportModel> GetProductPurchaseReport(DateTime startDate, DateTime endDate)
        {
            var result = new List<PurchaseReportModel>();
            string sql = @"
                SELECT 
                    t.product_id,
                    p.sku,
                    p.name AS product_name,
                    SUM(t.change_qty) AS quantity_purchased,
                    COALESCE(SUM(pt.unit_cost * t.change_qty), 0) AS total_cost,
                    COALESCE(AVG(pt.unit_cost), 0) AS average_cost,
                    p.supplier_id,
                    s.name AS supplier_name,
                    COUNT(DISTINCT t.reference) AS purchase_order_count,
                    p.category
                FROM inventory_transaction t
                INNER JOIN product p ON p.id = t.product_id
                LEFT JOIN purchase_transaction pt ON pt.product_id = t.product_id AND pt.reference = t.reference
                LEFT JOIN supplier s ON s.id = p.supplier_id
                WHERE t.type = 'purchase'
                  AND t.created_at BETWEEN @startDate AND @endDate
                GROUP BY t.product_id, p.sku, p.name, p.supplier_id, s.name, p.category
                ORDER BY total_cost DESC";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reportItem = new PurchaseReportModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            QuantityPurchased = Convert.ToInt32(reader["quantity_purchased"]),
                            TotalCost = reader["total_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_cost"]),
                            AverageCost = reader["average_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["average_cost"]),
                            StartDate = startDate,
                            EndDate = endDate,
                            SupplierId = reader["supplier_id"] == DBNull.Value ? 0 : Convert.ToInt32(reader["supplier_id"]),
                            SupplierName = reader["supplier_name"] == DBNull.Value ? string.Empty : reader["supplier_name"].ToString(),
                            PurchaseOrderCount = Convert.ToInt32(reader["purchase_order_count"]),
                            Category = reader["category"] == DBNull.Value ? string.Empty : reader["category"].ToString()
                        };

                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取供应商表现报表数据
        /// </summary>
        public List<SupplierPerformanceReportModel> GetSupplierPerformanceReport(DateTime startDate, DateTime endDate)
        {
            var result = new List<SupplierPerformanceReportModel>();
            string sql = @"
                SELECT 
                    s.id AS supplier_id,
                    s.name AS supplier_name,
                    COUNT(DISTINCT t.reference) AS total_orders,
                    COALESCE(SUM(pt.unit_cost * t.change_qty), 0) AS total_spent,
                    SUM(t.change_qty) AS total_items_received,
                    AVG(DATEDIFF(pt.received_date, pt.order_date)) AS average_delivery_time_days,
                    (COUNT(CASE WHEN DATEDIFF(pt.received_date, pt.promise_date) <= 0 THEN 1 END) * 100.0 / COUNT(*)) AS compliance_rate
                FROM supplier s
                LEFT JOIN product p ON p.supplier_id = s.id
                LEFT JOIN inventory_transaction t ON t.product_id = p.id AND t.type = 'purchase'
                LEFT JOIN purchase_transaction pt ON pt.reference = t.reference
                WHERE t.created_at BETWEEN @startDate AND @endDate
                GROUP BY s.id, s.name
                ORDER BY compliance_rate DESC";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SupplierPerformanceReportModel
                        {
                            SupplierId = Convert.ToInt32(reader["supplier_id"]),
                            SupplierName = reader["supplier_name"].ToString(),
                            TotalOrders = Convert.ToInt32(reader["total_orders"]),
                            TotalSpent = reader["total_spent"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_spent"]),
                            TotalItemsReceived = Convert.ToInt32(reader["total_items_received"]),
                            AverageDeliveryTimeDays = reader["average_delivery_time_days"] == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(reader["average_delivery_time_days"]), 2),
                            ComplianceRate = reader["compliance_rate"] == DBNull.Value ? 0 : Math.Round(Convert.ToDecimal(reader["compliance_rate"]), 2)
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取采购趋势数据
        /// </summary>
        public List<MonthlyTrendModel> GetPurchaseTrendReport(int year)
        {
            var result = new List<MonthlyTrendModel>();
            string sql = @"
                SELECT 
                    MONTH(t.created_at) AS month_number,
                    DATE_FORMAT(t.created_at, '%Y年%m月') AS month_name,
                    COALESCE(SUM(pt.unit_cost * t.change_qty), 0) AS cost,
                    COUNT(DISTINCT t.reference) AS orders_count
                FROM inventory_transaction t
                LEFT JOIN purchase_transaction pt ON pt.product_id = t.product_id AND pt.reference = t.reference
                WHERE t.type = 'purchase'
                  AND YEAR(t.created_at) = @year
                GROUP BY MONTH(t.created_at), DATE_FORMAT(t.created_at, '%Y年%m月')
                ORDER BY month_number ASC";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@year", year);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MonthlyTrendModel
                        {
                            MonthNumber = Convert.ToInt32(reader["month_number"]),
                            MonthName = reader["month_name"].ToString(),
                            Revenue = 0,
                            Cost = reader["cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["cost"]),
                            Profit = 0,
                            OrdersCount = Convert.ToInt32(reader["orders_count"])
                        });
                    }
                }
            }
            return result;
        }
    }
}