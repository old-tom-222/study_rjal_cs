using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class SalesReportService
    {
        private readonly InventoryTransactionRepository _transactionRepo;

        public SalesReportService()
        {
            _transactionRepo = new InventoryTransactionRepository();
        }

        /// <summary>
        /// 获取产品销售报表
        /// </summary>
        public List<SalesReportModel> GetProductSalesReport(DateTime startDate, DateTime endDate)
        {
            // 获取销售类型的交易记录
            // 获取时间范围内的所有交易记录
            var allTransactions = _transactionRepo.GetTransactions(from: startDate, to: endDate);
            // 筛选出销售类型的交易记录
            var salesTransactions = allTransactions.Where(t => t.Type == "sale").ToList();
            
            // 按产品分组统计
            var productGroups = salesTransactions.GroupBy(t => t.ProductId);
            var reportItems = new List<SalesReportModel>();

            foreach (var group in productGroups)
            {
                int totalQuantity = group.Sum(t => Math.Abs(t.ChangeQty)); // 销售是负数，取绝对值
                decimal averagePrice = 0; // 实际应用中需要从销售订单获取价格信息
                decimal totalRevenue = averagePrice * totalQuantity;

                var reportItem = new SalesReportModel
                {
                    ProductId = group.Key,
                    ProductSku = group.First().ProductSku,
                    ProductName = group.First().ProductName,
                    QuantitySold = totalQuantity,
                    TotalRevenue = totalRevenue,
                    AveragePrice = averagePrice,
                    StartDate = startDate,
                    EndDate = endDate,
                    ProfitMargin = 30, // 默认利润率30%，实际应用中需要计算
                    // 其他属性可以从其他数据源获取
                };

                reportItems.Add(reportItem);
            }

            return reportItems.OrderByDescending(r => r.QuantitySold).ToList();
        }

        /// <summary>
        /// 获取每日销售报表
        /// </summary>
        public List<DailySalesReportModel> GetDailySalesReport(DateTime startDate, DateTime endDate)
        {
            var reportItems = new List<DailySalesReportModel>();
            
            // 实际应用中需要根据销售订单数据生成
            // 这里模拟生成一些数据用于演示
            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // 随机生成一些测试数据
                int totalOrders = new Random(date.Day).Next(10, 50);
                int totalItemsSold = totalOrders * new Random(date.Day + 10).Next(1, 5);
                decimal averageOrderValue = new Random(date.Day + 20).Next(100, 500);
                decimal totalRevenue = totalOrders * averageOrderValue;

                reportItems.Add(new DailySalesReportModel
                {
                    Date = date,
                    TotalOrders = totalOrders,
                    TotalItemsSold = totalItemsSold,
                    TotalRevenue = totalRevenue,
                    AverageOrderValue = averageOrderValue
                });
            }

            return reportItems;
        }

        /// <summary>
        /// 获取销售趋势报表
        /// </summary>
        public List<MonthlyTrendModel> GetSalesTrendReport(int year)
        {
            var reportItems = new List<MonthlyTrendModel>();

            // 生成全年12个月的报表数据
            for (int month = 1; month <= 12; month++)
            {
                var monthName = new DateTime(year, month, 1).ToString("yyyy年MM月");
                decimal revenue = new Random(month).Next(50000, 200000);
                decimal cost = revenue * 0.7m; // 假设成本是收入的70%
                decimal profit = revenue - cost;

                reportItems.Add(new MonthlyTrendModel
                {
                    MonthName = monthName,
                    MonthNumber = month,
                    Revenue = revenue,
                    Cost = cost,
                    Profit = profit,
                    OrdersCount = new Random(month + 12).Next(100, 500)
                });
            }

            return reportItems;
        }
    }
}