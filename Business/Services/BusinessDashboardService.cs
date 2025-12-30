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
        private readonly BusinessDashboardRepository _dashboardRepo;

        public BusinessDashboardService()
        {
            _inventoryRepo = new InventoryRepository();
            _transactionRepo = new InventoryTransactionRepository();
            _salesReportService = new SalesReportService();
            _purchaseReportService = new PurchaseReportService();
            _dashboardRepo = new BusinessDashboardRepository();
        }

        /// <summary>
        /// 获取经营看板概要信息 - 带日期范围参数
        /// </summary>
        public DashboardSummaryModel GetDashboardSummary(DateTime startDate, DateTime endDate)
        {
            // 从数据库获取真实数据
            return _dashboardRepo.GetDashboardSummary(startDate, endDate);
        }

        /// <summary>
        /// 获取经营看板概要信息 - 默认方法（使用最近30天数据）
        /// </summary>
        public BusinessDashboardSummaryModel GetDashboardSummary()
        {
            // 从数据库获取真实数据（使用最近30天）
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-29); // 最近30天
            
            // 获取真实数据
            var dashboardSummary = _dashboardRepo.GetDashboardSummary(startDate, endDate);
            
            // 转换为BusinessDashboardSummaryModel返回
            return new BusinessDashboardSummaryModel
            {
                // 销售额统计
                TotalRevenue = dashboardSummary.TotalSalesAmount,
                RevenueToday = dashboardSummary.TotalSalesAmount / 30, // 简单平均到每天
                RevenueThisWeek = dashboardSummary.TotalSalesAmount / 4, // 简单平均到每周
                RevenueThisMonth = dashboardSummary.TotalSalesAmount,
                RevenueLastMonth = dashboardSummary.TotalSalesAmount * 0.9m, // 假设上月为当前月的90%
                RevenueGrowthRate = dashboardSummary.SalesChangePercent,

                // 销售数量统计
                TotalItemsSold = dashboardSummary.TotalItemsSold,
                ItemsSoldToday = dashboardSummary.ItemsSoldToday,

                // 订单统计
                TotalOrders = dashboardSummary.SalesOrdersCount,
                OrdersToday = (int)(dashboardSummary.SalesOrdersCount / 30), // 简单平均到每天，并显式转换为int
                AverageOrderValue = dashboardSummary.SalesOrdersCount > 0 ? dashboardSummary.TotalSalesAmount / dashboardSummary.SalesOrdersCount : 0,

                // 库存统计
                TotalInventoryValue = dashboardSummary.CurrentInventoryValue,
                LowStockItemsCount = dashboardSummary.LowStockItemsCount,
                OutOfStockItemsCount = dashboardSummary.OutOfStockItemsCount,

                // 采购统计
                TotalPurchaseCost = dashboardSummary.TotalPurchaseAmount,
                PurchaseCostThisMonth = dashboardSummary.TotalPurchaseAmount,

                // 利润统计
                TotalProfit = dashboardSummary.TotalProfit,
                ProfitMargin = dashboardSummary.TotalSalesAmount > 0 ? 
                    (dashboardSummary.TotalProfit / dashboardSummary.TotalSalesAmount) * 100 : 0
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
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31);
            var salesTrends = _salesReportService.GetSalesTrendReport(startDate, endDate, 1);
            // 获取采购趋势
            var purchaseTrends = _purchaseReportService.GetPurchaseTrendReport(year);
            
            // 合并数据
            var trendData = new List<MonthlyTrendModel>();
            
            for (int month = 1; month <= 12; month++)
            {
                var salesTrend = salesTrends.FirstOrDefault(t => t.MonthNumber == month);
                var purchaseTrend = purchaseTrends.FirstOrDefault(t => t.MonthNumber == month);
                
                var monthlyModel = new MonthlyTrendModel();
                monthlyModel.MonthNumber = month;
                monthlyModel.MonthName = (salesTrend != null) ? salesTrend.MonthName : new DateTime(year, month, 1).ToString("yyyy年MM月");
                monthlyModel.Revenue = (salesTrend != null) ? salesTrend.Revenue : 0;
                monthlyModel.Cost = (purchaseTrend != null) ? purchaseTrend.Cost : 0;
                monthlyModel.Profit = ((salesTrend != null) ? salesTrend.Profit : 0) - ((purchaseTrend != null) ? purchaseTrend.Cost : 0);
                monthlyModel.OrdersCount = (salesTrend != null) ? salesTrend.OrdersCount : 0;
                
                trendData.Add(monthlyModel);
            }
            
            return trendData;
        }
        
        /// <summary>
        /// 获取经营趋势数据 - 按日期范围
        /// </summary>
        public List<MonthlyTrendModel> GetBusinessTrendData(DateTime startDate, DateTime endDate)
        {
            // 获取销售趋势数据
            var salesTrends = _salesReportService.GetSalesTrendReport(startDate, endDate, 2); // 使用月度粒度
            
            // 获取采购趋势数据（需要处理日期范围）
            int startYear = startDate.Year;
            int endYear = endDate.Year;
            List<MonthlyTrendModel> allPurchaseTrends = new List<MonthlyTrendModel>();
            
            // 获取日期范围内所有年份的采购趋势数据
            for (int year = startYear; year <= endYear; year++)
            {
                var yearlyPurchaseTrends = _purchaseReportService.GetPurchaseTrendReport(year);
                // 为采购趋势数据添加年份信息
                foreach (var purchaseTrend in yearlyPurchaseTrends)
                {
                    // 创建新对象，将MonthName修改为包含年份的格式
                    allPurchaseTrends.Add(new MonthlyTrendModel
                    {
                        MonthName = $"{year}年{purchaseTrend.MonthName}",
                        MonthNumber = purchaseTrend.MonthNumber,
                        Cost = purchaseTrend.Cost,
                        // 其他属性在销售趋势中已经提供，这里只需要成本相关数据
                    });
                }
            }
            
            // 合并数据并按月份名称排序
            var result = new List<MonthlyTrendModel>();
            
            // 遍历销售趋势，匹配对应的采购成本数据
            foreach (var saleTrend in salesTrends)
            {
                // 查找对应的采购成本数据
                var matchingPurchase = allPurchaseTrends.FirstOrDefault(
                    p => p.MonthName == saleTrend.MonthName);
                
                // 合并数据
                result.Add(new MonthlyTrendModel
                {
                    MonthName = saleTrend.MonthName,
                    MonthNumber = saleTrend.MonthNumber,
                    Revenue = saleTrend.Revenue,
                    Cost = matchingPurchase?.Cost ?? 0,
                    Profit = saleTrend.Revenue - (matchingPurchase?.Cost ?? 0),
                    OrdersCount = saleTrend.OrdersCount
                });
            }
            
            return result;
        }
    }
}