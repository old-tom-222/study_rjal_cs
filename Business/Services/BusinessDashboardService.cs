using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;


namespace CSproject.Business.Services
{
    public class BusinessDashboardService
    {
        private readonly InventoryRepository _inventoryRepo;
        private readonly InventoryTransactionRepository _transactionRepo;
        private readonly SalesReportService _salesReportService;
        private readonly PurchaseReportService _purchaseReportService;

        public BusinessDashboardService()
        {
            _inventoryRepo = new InventoryRepository();
            _transactionRepo = new InventoryTransactionRepository();
            _salesReportService = new SalesReportService();
            _purchaseReportService = new PurchaseReportService();
        }

        /// <summary>
        /// 获取经营看板概要信息 - 带日期范围参数
        /// </summary>
        public DashboardSummaryModel GetDashboardSummary(DateTime startDate, DateTime endDate)
        {
            // 实际应用中需要从各个模块获取真实数据
            // 这里模拟生成一些测试数据用于演示
            
            var today = DateTime.Today;
            var previousPeriodStart = startDate.AddDays(-(endDate - startDate).Days - 1);
            var previousPeriodEnd = startDate.AddDays(-1);

            // 销售额统计（模拟数据）
            decimal salesCurrentPeriod = new Random().Next(100000, 500000);
            decimal salesPreviousPeriod = new Random().Next(80000, 450000);
            decimal salesChangePercent = salesPreviousPeriod > 0 ? 
                ((salesCurrentPeriod - salesPreviousPeriod) / salesPreviousPeriod) * 100 : 0;

            // 采购额统计（模拟数据）
            decimal purchaseCurrentPeriod = new Random().Next(70000, 300000);
            decimal purchasePreviousPeriod = new Random().Next(60000, 280000);
            decimal purchaseChangePercent = purchasePreviousPeriod > 0 ? 
                ((purchaseCurrentPeriod - purchasePreviousPeriod) / purchasePreviousPeriod) * 100 : 0;

            // 利润统计（模拟数据）
            decimal profitCurrentPeriod = salesCurrentPeriod - purchaseCurrentPeriod;
            decimal profitPreviousPeriod = salesPreviousPeriod - purchasePreviousPeriod;
            decimal profitChangePercent = profitPreviousPeriod > 0 ? 
                ((profitCurrentPeriod - profitPreviousPeriod) / profitPreviousPeriod) * 100 : 0;

            int daysInPeriod = (endDate - startDate).Days + 1;

            return new DashboardSummaryModel
            {
                // 销售指标
                TotalSalesAmount = salesCurrentPeriod,
                AvgDailySales = daysInPeriod > 0 ? salesCurrentPeriod / daysInPeriod : 0,
                SalesOrdersCount = new Random().Next(50, 200),
                SalesChangePercent = Math.Round(salesChangePercent, 2),

                // 采购指标
                TotalPurchaseAmount = purchaseCurrentPeriod,
                PurchaseOrdersCount = new Random().Next(30, 150),
                PurchaseChangePercent = Math.Round(purchaseChangePercent, 2),

                // 利润指标
                TotalProfit = profitCurrentPeriod,
                ProfitChangePercent = Math.Round(profitChangePercent, 2),

                // 库存指标
                CurrentInventoryValue = new Random().Next(200000, 1000000),
                LowStockItemsCount = new Random().Next(5, 30),
                OutOfStockItemsCount = new Random().Next(1, 10)
            };
        }

        /// <summary>
        /// 获取经营看板概要信息 - 默认方法
        /// </summary>
        public BusinessDashboardSummaryModel GetDashboardSummary()
        {
            // 实际应用中需要从各个模块获取真实数据
            // 这里模拟生成一些测试数据用于演示
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var endOfLastMonth = startOfMonth.AddDays(-1);

            // 销售额统计（模拟数据）
            decimal revenueThisMonth = new Random().Next(100000, 500000);
            decimal revenueLastMonth = new Random().Next(80000, 450000);
            decimal revenueGrowthRate = revenueLastMonth > 0 ? 
                ((revenueThisMonth - revenueLastMonth) / revenueLastMonth) * 100 : 0;

            return new BusinessDashboardSummaryModel
            {
                // 销售额统计
                TotalRevenue = revenueThisMonth + revenueLastMonth,
                RevenueToday = new Random(today.Day).Next(5000, 20000),
                RevenueThisWeek = new Random((int)today.DayOfWeek).Next(30000, 100000),
                RevenueThisMonth = revenueThisMonth,
                RevenueLastMonth = revenueLastMonth,
                RevenueGrowthRate = Math.Round(revenueGrowthRate, 2),

                // 销售数量统计
                TotalItemsSold = new Random().Next(1000, 5000),
                ItemsSoldToday = new Random(today.Day + 1).Next(50, 200),

                // 订单统计
                TotalOrders = new Random().Next(500, 2000),
                OrdersToday = new Random(today.Day + 2).Next(20, 100),
                AverageOrderValue = new Random().Next(200, 500),

                // 库存统计
                TotalInventoryValue = new Random().Next(200000, 1000000),
                LowStockItemsCount = new Random().Next(5, 30),
                OutOfStockItemsCount = new Random().Next(1, 10),

                // 采购统计
                TotalPurchaseCost = new Random().Next(150000, 800000),
                PurchaseCostThisMonth = new Random().Next(70000, 300000),

                // 利润统计
                TotalProfit = new Random().Next(50000, 200000),
                ProfitMargin = 30 // 默认利润率30%
            };
        }

        /// <summary>
        /// 获取销售排行前N的产品
        /// </summary>
        public List<TopSellingProductModel> GetTopSellingProducts(DateTime startDate, DateTime endDate, int count = 10)
        {
            // 获取指定日期范围内的销售数据
            var salesReport = _salesReportService.GetProductSalesReport(startDate, endDate);
            
            // 转换为TopSellingProductModel格式
            return salesReport.Take(count).Select((r, index) => new TopSellingProductModel
            {
                ProductId = r.ProductId,
                ProductName = r.ProductName,
                ProductSku = r.ProductSku,
                QuantitySold = r.QuantitySold,
                TotalRevenue = r.TotalRevenue,
                RevenuePercentage = 0 // 实际应用中需要计算占比
            }).ToList();
        }
        
        /// <summary>
        /// 获取销售排行前N的产品（重载方法，默认获取本月数据）
        /// </summary>
        public List<TopSellingProductModel> GetTopSellingProducts(int count = 10)
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);
            
            return GetTopSellingProducts(startOfMonth, today, count);
        }

        /// <summary>
        /// 获取最近的交易记录
        /// </summary>
        public List<InventoryTransaction> GetRecentTransactions(int count = 20)
        {
            // 获取最近的交易记录
            var today = DateTime.Today;
            var startDate = today.AddDays(-30); // 获取最近30天的数据
            
            // 获取交易记录并按日期倒序排序
            // 正确传递参数：productId(null), warehouseId(null), from(startDate), to(today)
            var transactions = _transactionRepo.GetTransactions(null, null, startDate, today)
                .OrderByDescending(t => t.CreatedAt) // 使用正确的属性名CreatedAt
                .Take(count)
                .ToList();
                
            return transactions;
        }
        
        /// <summary>
        /// 获取经营趋势数据
        /// </summary>
        public List<MonthlyTrendModel> GetBusinessTrendData(int year)
        {
            // 获取销售趋势
            var salesTrends = _salesReportService.GetSalesTrendReport(year);
            // 获取采购趋势
            var purchaseTrends = _purchaseReportService.GetPurchaseTrendReport(year);
            
            // 合并数据
            var trendData = new List<MonthlyTrendModel>();
            
            for (int month = 1; month <= 12; month++)
            {
                var salesTrend = salesTrends.FirstOrDefault(t => t.MonthNumber == month);
                var purchaseTrend = purchaseTrends.FirstOrDefault(t => t.MonthNumber == month);
                
                trendData.Add(new MonthlyTrendModel
                {
                    MonthName = salesTrend?.MonthName ?? new DateTime(year, month, 1).ToString("yyyy年MM月"),
                    MonthNumber = month,
                    Revenue = salesTrend?.Revenue ?? 0,
                    Cost = purchaseTrend?.Cost ?? 0,
                    Profit = (salesTrend?.Profit ?? 0) - (purchaseTrend?.Cost ?? 0),
                    OrdersCount = salesTrend?.OrdersCount ?? 0
                });
            }
            
            return trendData;
        }
    }
}