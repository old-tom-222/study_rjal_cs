using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class SalesReportRepository
    {
        /// <summary>
        /// 获取产品销售报表数据
        /// </summary>
        public List<SalesReportModel> GetProductSalesReport(DateTime startDate, DateTime endDate)
        {
            var result = new List<SalesReportModel>();
            string sql = @"
                SELECT 
                    t.product_id,
                    p.sku,
                    p.name AS product_name,
                    SUM(ABS(t.change_qty)) AS quantity_sold,
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_revenue,
                    COALESCE(AVG(sd.unit_price), 0) AS average_price,
                    p.category,
                    s.name AS supplier
                FROM inventory_transaction t
                INNER JOIN product p ON p.id = t.product_id
                LEFT JOIN sales_detail sd ON sd.product_id = t.product_id AND sd.reference = t.reference
                LEFT JOIN supplier s ON s.id = p.supplier_id
                WHERE t.type = 'sale'
                  AND t.created_at BETWEEN @startDate AND @endDate
                GROUP BY t.product_id, p.sku, p.name, p.category, s.name
                ORDER BY total_revenue DESC";

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
                        var totalRevenue = reader["total_revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_revenue"]);
                        var quantitySold = Convert.ToInt32(reader["quantity_sold"]);
                        var averagePrice = totalRevenue > 0 && quantitySold > 0 ? totalRevenue / quantitySold : 0;

                        var reportItem = new SalesReportModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            QuantitySold = quantitySold,
                            TotalRevenue = totalRevenue,
                            AveragePrice = averagePrice,
                            StartDate = startDate,
                            EndDate = endDate,
                            Category = reader["category"] == DBNull.Value ? string.Empty : reader["category"].ToString(),
                            Supplier = reader["supplier"] == DBNull.Value ? string.Empty : reader["supplier"].ToString(),
                            ProfitMargin = 30 // 默认利润率，实际应用中需要计算
                        };

                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取每日销售报表数据
        /// </summary>
        public List<DailySalesReportModel> GetDailySalesReport(DateTime startDate, DateTime endDate)
        {
            var result = new List<DailySalesReportModel>();
            string sql = @"
                SELECT 
                    DATE(t.created_at) AS sale_date,
                    COUNT(DISTINCT t.reference) AS total_orders,
                    SUM(ABS(t.change_qty)) AS total_items_sold,
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_revenue
                FROM inventory_transaction t
                LEFT JOIN sales_detail sd ON sd.product_id = t.product_id AND sd.reference = t.reference
                WHERE t.type = 'sale'
                  AND t.created_at BETWEEN @startDate AND @endDate
                GROUP BY DATE(t.created_at)
                ORDER BY sale_date ASC";

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
                        var totalRevenue = reader["total_revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_revenue"]);
                        var totalOrders = Convert.ToInt32(reader["total_orders"]);
                        var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

                        result.Add(new DailySalesReportModel
                        {
                            Date = Convert.ToDateTime(reader["sale_date"]),
                            TotalOrders = totalOrders,
                            TotalItemsSold = Convert.ToInt32(reader["total_items_sold"]),
                            TotalRevenue = totalRevenue,
                            AverageOrderValue = averageOrderValue
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取销售趋势数据
        /// </summary>
        public List<MonthlyTrendModel> GetSalesTrendReport(int year)
        {
            var result = new List<MonthlyTrendModel>();
            string sql = @"
                SELECT 
                    MONTH(t.created_at) AS month_number,
                    DATE_FORMAT(t.created_at, '%Y年%m月') AS month_name,
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS revenue,
                    COUNT(DISTINCT t.reference) AS orders_count
                FROM inventory_transaction t
                LEFT JOIN sales_detail sd ON sd.product_id = t.product_id AND sd.reference = t.reference
                WHERE t.type = 'sale'
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
                        var revenue = reader["revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue"]);
                        // 成本和利润需要从其他表计算，这里简化处理
                        var cost = revenue * 0.7m; // 假设成本是收入的70%
                        var profit = revenue - cost;

                        result.Add(new MonthlyTrendModel
                        {
                            MonthNumber = Convert.ToInt32(reader["month_number"]),
                            MonthName = reader["month_name"].ToString(),
                            Revenue = revenue,
                            Cost = cost,
                            Profit = profit,
                            OrdersCount = Convert.ToInt32(reader["orders_count"])
                        });
                    }
                }
            }
            return result;
        }
    }
}