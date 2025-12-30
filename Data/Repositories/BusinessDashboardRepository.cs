using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class BusinessDashboardRepository
    {
        /// <summary>
        /// 获取经营看板概要数据 - 默认方法
        /// </summary>
        public BusinessDashboardSummaryModel GetDashboardSummary()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var endOfLastMonth = startOfMonth.AddDays(-1);

            // 销售统计查询
            string salesSql = @"
                SELECT 
                    COALESCE(SUM(CASE WHEN DATE(t.created_at) = @today THEN sd.unit_price * ABS(t.change_qty) ELSE 0 END), 0) AS revenue_today,
                    COALESCE(SUM(CASE WHEN t.created_at >= @startOfWeek THEN sd.unit_price * ABS(t.change_qty) ELSE 0 END), 0) AS revenue_this_week,
                    COALESCE(SUM(CASE WHEN t.created_at >= @startOfMonth THEN sd.unit_price * ABS(t.change_qty) ELSE 0 END), 0) AS revenue_this_month,
                    COALESCE(SUM(CASE WHEN t.created_at BETWEEN @startOfLastMonth AND @endOfLastMonth THEN sd.unit_price * ABS(t.change_qty) ELSE 0 END), 0) AS revenue_last_month,
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_revenue,
                    COUNT(DISTINCT t.reference) AS total_orders,
                    COUNT(DISTINCT CASE WHEN DATE(t.created_at) = @today THEN t.reference END) AS orders_today,
                    SUM(ABS(t.change_qty)) AS total_items_sold,
                    SUM(CASE WHEN DATE(t.created_at) = @today THEN ABS(t.change_qty) ELSE 0 END) AS items_sold_today
                FROM inventory_transaction t
                LEFT JOIN sales_order_item sd ON sd.product_id = t.product_id
                LEFT JOIN sales_order o ON o.id = sd.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales'";

            // 库存统计查询
            string inventorySql = @"
                SELECT 
                    COUNT(*) AS total_products,
                    COUNT(CASE WHEN i.quantity <= p.safe_stock THEN 1 END) AS low_stock_items_count,
                    COUNT(CASE WHEN i.quantity = 0 THEN 1 END) AS out_of_stock_items_count,
                    COALESCE(SUM(i.quantity * COALESCE(p.cost_price, 0)), 0) AS total_inventory_value
                FROM inventory i
                INNER JOIN product p ON p.id = i.product_id";

            // 采购统计查询
            string purchaseSql = @"
                SELECT 
                    COALESCE(SUM(pi.unit_price * t.change_qty), 0) AS total_purchase_cost,
                    COALESCE(SUM(CASE WHEN t.created_at >= @startOfMonth THEN pi.unit_price * t.change_qty ELSE 0 END), 0) AS purchase_cost_this_month
                FROM inventory_transaction t
                LEFT JOIN purchase_order po ON po.order_no = t.reference
                LEFT JOIN purchase_order_item pi ON pi.order_id = po.id AND pi.product_id = t.product_id
                WHERE t.type = 'purchase'";

            var summary = new BusinessDashboardSummaryModel();

            // 获取销售统计数据
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(salesSql, conn))
                {
                    cmd.Parameters.AddWithValue("@today", today);
                    cmd.Parameters.AddWithValue("@startOfWeek", startOfWeek);
                    cmd.Parameters.AddWithValue("@startOfMonth", startOfMonth);
                    cmd.Parameters.AddWithValue("@startOfLastMonth", startOfLastMonth);
                    cmd.Parameters.AddWithValue("@endOfLastMonth", endOfLastMonth);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.RevenueToday = reader["revenue_today"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue_today"]);
                            summary.RevenueThisWeek = reader["revenue_this_week"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue_this_week"]);
                            summary.RevenueThisMonth = reader["revenue_this_month"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue_this_month"]);
                            summary.RevenueLastMonth = reader["revenue_last_month"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["revenue_last_month"]);
                            summary.TotalRevenue = reader["total_revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_revenue"]);
                            summary.TotalOrders = reader["total_orders"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_orders"]);
                            summary.OrdersToday = reader["orders_today"] == DBNull.Value ? 0 : Convert.ToInt32(reader["orders_today"]);
                            summary.TotalItemsSold = reader["total_items_sold"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_items_sold"]);
                            summary.ItemsSoldToday = reader["items_sold_today"] == DBNull.Value ? 0 : Convert.ToInt32(reader["items_sold_today"]);
                        }
                    }
                }

                // 获取库存统计数据
                using (var cmd = new MySqlCommand(inventorySql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.LowStockItemsCount = reader["low_stock_items_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["low_stock_items_count"]);
                            summary.OutOfStockItemsCount = reader["out_of_stock_items_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["out_of_stock_items_count"]);
                            summary.TotalInventoryValue = reader["total_inventory_value"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_inventory_value"]);
                        }
                    }
                }

                // 获取采购统计数据
                using (var cmd = new MySqlCommand(purchaseSql, conn))
                {
                    cmd.Parameters.AddWithValue("@startOfMonth", startOfMonth);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.TotalPurchaseCost = reader["total_purchase_cost"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_purchase_cost"]);
                            summary.PurchaseCostThisMonth = reader["purchase_cost_this_month"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["purchase_cost_this_month"]);
                        }
                    }
                }
            }

            // 计算其他指标
            summary.AverageOrderValue = summary.TotalOrders > 0 ? summary.TotalRevenue / summary.TotalOrders : 0;
            summary.RevenueGrowthRate = summary.RevenueLastMonth > 0 ? 
                ((summary.RevenueThisMonth - summary.RevenueLastMonth) / summary.RevenueLastMonth) * 100 : 0;
            summary.TotalProfit = summary.TotalRevenue - summary.TotalPurchaseCost; // 简化计算
            summary.ProfitMargin = summary.TotalRevenue > 0 ? (summary.TotalProfit / summary.TotalRevenue) * 100 : 0;

            return summary;
        }

        /// <summary>
        /// 获取经营看板概要数据 - 带日期范围参数
        /// </summary>
        public DashboardSummaryModel GetDashboardSummary(DateTime startDate, DateTime endDate)
        {
            // 计算上一个周期的日期范围
            var previousPeriodStart = startDate.AddDays(-(endDate - startDate).Days - 1);
            var previousPeriodEnd = startDate.AddDays(-1);

            // 销售统计查询（当前周期）
            string currentSalesSql = @"
                SELECT 
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_sales_amount,
                    COUNT(DISTINCT t.reference) AS sales_orders_count,
                    SUM(ABS(t.change_qty)) AS total_items_sold,
                    SUM(CASE WHEN DATE(t.created_at) = CURDATE() THEN ABS(t.change_qty) ELSE 0 END) AS items_sold_today
                FROM inventory_transaction t
                LEFT JOIN sales_order_item sd ON sd.product_id = t.product_id
                LEFT JOIN sales_order o ON o.id = sd.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales' AND t.created_at BETWEEN @startDate AND @endDate";

            // 销售统计查询（上一个周期）
            string previousSalesSql = @"
                SELECT 
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_sales_amount
                FROM inventory_transaction t
                LEFT JOIN sales_order_item sd ON sd.product_id = t.product_id
                LEFT JOIN sales_order o ON o.id = sd.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales' AND t.created_at BETWEEN @previousStartDate AND @previousEndDate";

            // 采购统计查询（当前周期）
            string currentPurchaseSql = @"
                SELECT 
                    COALESCE(SUM(pi.unit_price * t.change_qty), 0) AS total_purchase_amount,
                    COUNT(DISTINCT t.reference) AS purchase_orders_count
                FROM inventory_transaction t
                LEFT JOIN purchase_order po ON po.order_no = t.reference
                LEFT JOIN purchase_order_item pi ON pi.order_id = po.id AND pi.product_id = t.product_id
                WHERE t.type = 'purchase' AND t.created_at BETWEEN @startDate AND @endDate";

            // 采购统计查询（上一个周期）
            string previousPurchaseSql = @"
                SELECT 
                    COALESCE(SUM(pi.unit_price * t.change_qty), 0) AS total_purchase_amount
                FROM inventory_transaction t
                LEFT JOIN purchase_order po ON po.order_no = t.reference
                LEFT JOIN purchase_order_item pi ON pi.order_id = po.id AND pi.product_id = t.product_id
                WHERE t.type = 'purchase' AND t.created_at BETWEEN @previousStartDate AND @previousEndDate";

            // 库存统计查询
            string inventorySql = @"
                SELECT 
                    COUNT(CASE WHEN i.quantity <= p.safe_stock THEN 1 END) AS low_stock_items_count,
                    COUNT(CASE WHEN i.quantity = 0 THEN 1 END) AS out_of_stock_items_count,
                    COALESCE(SUM(i.quantity * COALESCE(p.cost_price, 0)), 0) AS current_inventory_value
                FROM inventory i
                INNER JOIN product p ON p.id = i.product_id";

            var summary = new DashboardSummaryModel();

            // 获取销售统计数据
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            {
                conn.Open();

                // 当前周期销售统计
                using (var cmd = new MySqlCommand(currentSalesSql, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.TotalSalesAmount = reader["total_sales_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_sales_amount"]);
                            summary.SalesOrdersCount = reader["sales_orders_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["sales_orders_count"]);
                            summary.TotalItemsSold = reader["total_items_sold"] == DBNull.Value ? 0 : Convert.ToInt32(reader["total_items_sold"]);
                            summary.ItemsSoldToday = reader["items_sold_today"] == DBNull.Value ? 0 : Convert.ToInt32(reader["items_sold_today"]);
                        }
                    }
                }

                // 上一个周期销售统计
                using (var cmd = new MySqlCommand(previousSalesSql, conn))
                {
                    cmd.Parameters.AddWithValue("@previousStartDate", previousPeriodStart);
                    cmd.Parameters.AddWithValue("@previousEndDate", previousPeriodEnd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal previousSalesAmount = reader["total_sales_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_sales_amount"]);
                            summary.SalesChangePercent = previousSalesAmount > 0 ? 
                                Math.Round(((summary.TotalSalesAmount - previousSalesAmount) / previousSalesAmount) * 100, 2) : 0;
                        }
                    }
                }

                // 当前周期采购统计
                using (var cmd = new MySqlCommand(currentPurchaseSql, conn))
                {
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.TotalPurchaseAmount = reader["total_purchase_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_purchase_amount"]);
                            summary.PurchaseOrdersCount = reader["purchase_orders_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["purchase_orders_count"]);
                        }
                    }
                }

                // 上一个周期采购统计
                using (var cmd = new MySqlCommand(previousPurchaseSql, conn))
                {
                    cmd.Parameters.AddWithValue("@previousStartDate", previousPeriodStart);
                    cmd.Parameters.AddWithValue("@previousEndDate", previousPeriodEnd);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal previousPurchaseAmount = reader["total_purchase_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_purchase_amount"]);
                            summary.PurchaseChangePercent = previousPurchaseAmount > 0 ? 
                                Math.Round(((summary.TotalPurchaseAmount - previousPurchaseAmount) / previousPurchaseAmount) * 100, 2) : 0;
                        }
                    }
                }

                // 库存统计
                using (var cmd = new MySqlCommand(inventorySql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            summary.CurrentInventoryValue = reader["current_inventory_value"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["current_inventory_value"]);
                            summary.LowStockItemsCount = reader["low_stock_items_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["low_stock_items_count"]);
                            summary.OutOfStockItemsCount = reader["out_of_stock_items_count"] == DBNull.Value ? 0 : Convert.ToInt32(reader["out_of_stock_items_count"]);
                        }
                    }
                }
            }

            // 计算其他指标
            int daysInPeriod = (endDate - startDate).Days + 1;
            summary.AvgDailySales = daysInPeriod > 0 ? summary.TotalSalesAmount / daysInPeriod : 0;
            summary.TotalProfit = summary.TotalSalesAmount - summary.TotalPurchaseAmount;

            // 计算上一个周期利润
            decimal previousProfitSalesAmount = 0;
            decimal previousProfitPurchaseAmount = 0;

            // 获取上一个周期利润所需数据
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            {
                conn.Open();

                // 上一个周期销售
                using (var cmd = new MySqlCommand(previousSalesSql, conn))
                {
                    cmd.Parameters.AddWithValue("@previousStartDate", previousPeriodStart);
                    cmd.Parameters.AddWithValue("@previousEndDate", previousPeriodEnd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            previousProfitSalesAmount = reader["total_sales_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_sales_amount"]);
                        }
                    }
                }

                // 上一个周期采购
                using (var cmd = new MySqlCommand(previousPurchaseSql, conn))
                {
                    cmd.Parameters.AddWithValue("@previousStartDate", previousPeriodStart);
                    cmd.Parameters.AddWithValue("@previousEndDate", previousPeriodEnd);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            previousProfitPurchaseAmount = reader["total_purchase_amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_purchase_amount"]);
                        }
                    }
                }
            }

            decimal previousProfit = previousProfitSalesAmount - previousProfitPurchaseAmount;
            summary.ProfitChangePercent = previousProfit > 0 ? 
                Math.Round(((summary.TotalProfit - previousProfit) / previousProfit) * 100, 2) : 0;

            return summary;
        }

        /// <summary>
        /// 获取销售排行前N的产品
        /// </summary>
        public List<TopSellingProductModel> GetTopSellingProducts(int count = 10)
        {
            var result = new List<TopSellingProductModel>();
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            string sql = @"
                SELECT 
                    t.product_id,
                    p.name AS product_name,
                    SUM(ABS(t.change_qty)) AS quantity_sold,
                    COALESCE(SUM(sd.unit_price * ABS(t.change_qty)), 0) AS total_revenue
                FROM inventory_transaction t
                INNER JOIN product p ON p.id = t.product_id
                LEFT JOIN sales_order_item sd ON sd.product_id = t.product_id
                LEFT JOIN sales_order o ON o.id = sd.order_id AND o.order_no = t.reference
                WHERE t.type = 'sales'
                  AND t.created_at >= @startOfMonth
                GROUP BY t.product_id, p.name
                ORDER BY total_revenue DESC
                LIMIT @count";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@startOfMonth", startOfMonth);
                cmd.Parameters.AddWithValue("@count", count);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new TopSellingProductModel
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = string.Empty, // product表中不存在sku字段
                            ProductName = reader["product_name"].ToString(),
                            QuantitySold = Convert.ToInt32(reader["quantity_sold"]),
                            TotalRevenue = reader["total_revenue"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["total_revenue"]),
                            RevenuePercentage = 0 // 后续可以计算占比
                        });
                    }
                }
            }

            // 计算占比
            if (result.Count > 0)
            {
                decimal total = result.Sum(r => r.TotalRevenue);
                if (total > 0)
                {
                    foreach (var item in result)
                    {
                        item.RevenuePercentage = Math.Round((item.TotalRevenue / total) * 100, 2);
                    }
                }
            }

            return result;
        }
    }
}