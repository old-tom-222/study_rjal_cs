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
                    COALESCE(SUM(oi.unit_price * ABS(t.change_qty)), 0) AS total_revenue,
                    COALESCE(AVG(oi.unit_price), 0) AS average_price,
                    p.category_id
                FROM inventory_transaction t
                INNER JOIN product p ON p.id = t.product_id
                LEFT JOIN sales_order_item oi ON oi.product_id = t.product_id 
                LEFT JOIN sales_order o ON o.id = oi.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales'
                  AND t.created_at BETWEEN @startDate AND @endDate
                GROUP BY t.product_id, p.sku, p.name, p.category_id
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
                            Category = reader["category_id"] == DBNull.Value ? string.Empty : reader["category_id"].ToString(),
                            Supplier = string.Empty, // 移除supplier列引用
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
                    COALESCE(SUM(oi.unit_price * ABS(t.change_qty)), 0) AS total_revenue
                FROM inventory_transaction t
                LEFT JOIN sales_order_item oi ON oi.product_id = t.product_id
                LEFT JOIN sales_order o ON o.id = oi.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales'
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
        public List<MonthlyTrendModel> GetSalesTrendReport(DateTime startDate, DateTime endDate, int granularity)
        {
            var result = new List<MonthlyTrendModel>();
            string sql = string.Empty;
            
            // 根据粒度选择不同的分组方式
            switch (granularity)
            {
                case 0: // 按日
                    sql = @"
                        SELECT 
                            DATE(t.created_at) AS period_date,
                            DATE_FORMAT(t.created_at, '%Y年%m月%d日') AS period_name,
                            COALESCE(SUM(oi.unit_price * ABS(t.change_qty)), 0) AS revenue,
                            COUNT(DISTINCT t.reference) AS orders_count,
                            SUM(ABS(t.change_qty)) AS quantity_sold
                        FROM inventory_transaction t
                        LEFT JOIN sales_order_item oi ON oi.product_id = t.product_id
                        LEFT JOIN sales_order o ON o.id = oi.order_id AND o.order_no = t.reference
                        WHERE t.type = 'sales'
                          AND t.created_at BETWEEN @startDate AND @endDate
                        GROUP BY DATE(t.created_at), DATE_FORMAT(t.created_at, '%Y年%m月%d日')
                        ORDER BY period_date ASC";
                    break;
                case 1: // 按周
                    sql = @"
                        SELECT 
                            YEARWEEK(t.created_at, 1) AS week_number,
                            WEEK(t.created_at, 1) AS week_num,
                            MIN(DATE_ADD(t.created_at, INTERVAL 1-DAYOFWEEK(t.created_at) DAY)) AS week_start_date,
                            CONCAT('第', WEEK(t.created_at, 1), '周') AS period_name,
                            DATE_FORMAT(MIN(DATE_ADD(t.created_at, INTERVAL 1-DAYOFWEEK(t.created_at) DAY)), '%Y-%m-%d') AS week_start,
                            COALESCE(SUM(oi.unit_price * ABS(t.change_qty)), 0) AS revenue,
                            COUNT(DISTINCT t.reference) AS orders_count,
                            SUM(ABS(t.change_qty)) AS quantity_sold
                        FROM inventory_transaction t
                        LEFT JOIN sales_order_item oi ON oi.product_id = t.product_id
                        LEFT JOIN sales_order o ON o.id = oi.order_id AND o.order_no = t.reference
                        WHERE t.type = 'sales'
                          AND t.created_at BETWEEN @startDate AND @endDate
                        GROUP BY YEARWEEK(t.created_at, 1), WEEK(t.created_at, 1)
                        ORDER BY week_start_date ASC";
                    break;
                case 2: // 按月
                default:
                    sql = @"
                        SELECT 
                            DATE_FORMAT(t.created_at, '%Y-%m') AS period_date,
                            DATE_FORMAT(t.created_at, '%Y年%m月') AS period_name,
                            COALESCE(SUM(oi.unit_price * ABS(t.change_qty)), 0) AS revenue,
                            COUNT(DISTINCT t.reference) AS orders_count,
                            SUM(ABS(t.change_qty)) AS quantity_sold
                        FROM inventory_transaction t
                        LEFT JOIN sales_order_item oi ON oi.product_id = t.product_id
                        LEFT JOIN sales_order o ON o.id = oi.order_id AND o.order_no = t.reference
                        WHERE t.type = 'sales'
                          AND t.created_at BETWEEN @startDate AND @endDate
                        GROUP BY DATE_FORMAT(t.created_at, '%Y-%m'), DATE_FORMAT(t.created_at, '%Y年%m月')
                        ORDER BY period_date ASC";
                    break;
            }

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
                        decimal revenue = reader["revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue"]);
                        string periodName = reader["period_name"].ToString();
                        int ordersCount = Convert.ToInt32(reader["orders_count"]);
                        int quantitySold = reader["quantity_sold"] == DBNull.Value ? 0 : Convert.ToInt32(reader["quantity_sold"]);
                        
                        // 计算成本和利润（基于简单的70%成本率）
                        decimal cost = revenue * 0.7m;
                        decimal profit = revenue - cost;
                        
                        // 对于按日和按周的情况，使用当前月份作为MonthNumber
                        int monthNumber = 1;
                        if (granularity == 0) // 按日
                        {
                            DateTime date = Convert.ToDateTime(reader["period_date"]);
                            monthNumber = date.Month;
                        }
                        else if (granularity == 1) // 按周
                        {
                            // 对于按周，使用周的开始日期的月份
                            DateTime weekStart = Convert.ToDateTime(reader["week_start_date"]);
                            monthNumber = weekStart.Month;
                        }
                        else // 按月
                        {
                            // 对于按月，从period_date中解析月份
                            string periodDate = reader["period_date"].ToString();
                            monthNumber = Convert.ToInt32(periodDate.Substring(5, 2));
                        }
                        
                        result.Add(new MonthlyTrendModel
                        {
                            MonthName = periodName,
                            MonthNumber = monthNumber,
                            Revenue = revenue,
                            OrdersCount = ordersCount,
                            Cost = cost,
                            Profit = profit,
                            // 注意：MonthlyTrendModel中没有QuantitySold字段，但我们在查询中获取了它
                        });
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// 获取客户销售排名
        /// </summary>
        public List<CustomerRankingModel> GetCustomerRankings(DateTime startDate, DateTime endDate, int topN = 10)
        {
            var result = new List<CustomerRankingModel>();
            string sql = @"SELECT c.id, c.name AS customer_name, 
                                   SUM(o.total_amount) AS total_spent, 
                            COUNT(DISTINCT o.id) AS order_count
                            FROM sales_order o
                            INNER JOIN customer c ON c.id = o.customer_id
                            WHERE o.order_date BETWEEN @startDate AND @endDate
                              AND o.status IN ('已审核', '已发货')
                            GROUP BY c.id, c.name
                            ORDER BY total_spent DESC
                            LIMIT @topN";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@topN", topN);
                
                int rank = 1;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CustomerRankingModel
                        {
                            Rank = rank++,
                            CustomerId = Convert.ToInt32(reader["id"]),
                            CustomerName = reader["customer_name"].ToString(),
                            TotalSpent = Convert.ToDecimal(reader["total_spent"]),
                            OrderCount = Convert.ToInt32(reader["order_count"])
                        });
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// 获取产品销售排名
        /// </summary>
        public List<ProductRankingModel> GetProductRankings(DateTime startDate, DateTime endDate, int topN = 10)
        {
            var result = new List<ProductRankingModel>();
            string sql = @"SELECT p.id AS product_id, p.name AS product_name, p.sku AS product_sku,
                                   SUM(oi.quantity) AS quantity_sold,
                                   SUM(oi.unit_price * oi.quantity) AS sales_amount
                            FROM sales_order_item oi
                            INNER JOIN sales_order o ON o.id = oi.order_id
                            INNER JOIN product p ON p.id = oi.product_id
                            WHERE o.order_date BETWEEN @startDate AND @endDate
                              AND o.status IN ('已审核', '已发货')
                            GROUP BY p.id, p.name, p.sku
                            ORDER BY quantity_sold DESC
                            LIMIT @topN";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@startDate", startDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@topN", topN);
                
                int rank = 1;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new ProductRankingModel
                        {
                            Rank = rank++,
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductName = reader["product_name"].ToString(),
                            ProductSku = reader["product_sku"].ToString(),
                            QuantitySold = Convert.ToInt32(reader["quantity_sold"]),
                            SalesAmount = Convert.ToDecimal(reader["sales_amount"])
                        });
                    }
                }
            }
            return result;
        }
        
        /// <summary>
        /// 获取月度销售数据
        /// </summary>
        public List<MonthlySalesData> GetMonthlySalesData(int year)
        {
            var result = new List<MonthlySalesData>();
            string sql = @"SELECT DATE_FORMAT(order_date, '%Y-%m') AS month,
                                   SUM(total_amount) AS sales_amount,
                                   COUNT(*) AS order_count
                            FROM sales_order
                            WHERE YEAR(order_date) = @year
                              AND status IN ('已审核', '已发货')
                            GROUP BY DATE_FORMAT(order_date, '%Y-%m')
                            ORDER BY month";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@year", year);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new MonthlySalesData
                        {
                            Month = reader["month"].ToString(),
                            SalesAmount = Convert.ToDecimal(reader["sales_amount"]),
                            OrderCount = Convert.ToInt32(reader["order_count"])
                        });
                    }
                }
            }
            return result;
        }
    }
}