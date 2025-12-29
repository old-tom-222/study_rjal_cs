using CSproject.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.Business.Services
{
    public class ProfitAnalysisService
    {
        private readonly SalesReportService _salesReportService;
        private readonly PurchaseReportService _purchaseReportService;
        private readonly AccountReceivableService _receivableService;
        private readonly AccountPayableService _payableService;

        public ProfitAnalysisService()
        {
            _salesReportService = new SalesReportService();
            _purchaseReportService = new PurchaseReportService();
            _receivableService = new AccountReceivableService();
            _payableService = new AccountPayableService();
        }

        /// <summary>
        /// 获取指定日期范围内的利润分析
        /// </summary>
        public ProfitAnalysis GetProfitAnalysis(DateTime startDate, DateTime endDate)
        {
            // 获取销售数据
            var productSalesReport = _salesReportService.GetProductSalesReport(startDate, endDate);
            var totalRevenue = productSalesReport.Sum(p => p.TotalRevenue);

            // 获取采购数据
            var productPurchaseReport = _purchaseReportService.GetProductPurchaseReport(startDate, endDate);
            var totalCost = productPurchaseReport.Sum(p => p.TotalCost);

            // 计算毛利润和毛利率
            var grossProfit = totalRevenue - totalCost;
            var grossMargin = totalRevenue > 0 ? (grossProfit / totalRevenue) * 100 : 0;

            // 计算销售订单和采购订单数量
            var salesOrderCount = productSalesReport.Count > 0 ? 10 : 0; // 模拟销售订单数量
            var purchaseOrderCount = productPurchaseReport.Select(p => p.PurchaseOrderCount).Sum();

            // 计算运营费用（模拟数据，实际应用中应从其他模块获取）
            var operatingExpenses = grossProfit * 0.3m; // 假设运营费用为毛利润的30%

            // 计算净利润和净利率
            var netProfit = grossProfit - operatingExpenses;
            var netMargin = totalRevenue > 0 ? (netProfit / totalRevenue) * 100 : 0;

            return new ProfitAnalysis
            {
                AnalysisDate = DateTime.Now,
                TotalRevenue = totalRevenue,
                TotalCost = totalCost,
                GrossProfit = grossProfit,
                GrossMargin = Math.Round(grossMargin, 2),
                OperatingExpenses = operatingExpenses,
                NetProfit = netProfit,
                NetMargin = Math.Round(netMargin, 2),
                TotalSalesOrders = salesOrderCount,
                TotalPurchaseOrders = purchaseOrderCount
            };
        }

        /// <summary>
        /// 获取年度利润分析
        /// </summary>
        public ProfitAnalysis GetYearlyProfitAnalysis(int year)
        {
            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31);
            return GetProfitAnalysis(startDate, endDate);
        }

        /// <summary>
        /// 获取月度利润分析
        /// </summary>
        public ProfitAnalysis GetMonthlyProfitAnalysis(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return GetProfitAnalysis(startDate, endDate);
        }

        /// <summary>
        /// 获取利润趋势分析
        /// </summary>
        public List<MonthlyTrendModel> GetProfitTrendAnalysis(int year)
        {
            var salesTrends = _salesReportService.GetSalesTrendReport(new DateTime(year, 1, 1), new DateTime(year, 12, 31), 1);
            var purchaseTrends = _purchaseReportService.GetPurchaseTrendReport(year);

            var profitTrends = new List<MonthlyTrendModel>();

            for (int month = 1; month <= 12; month++)
            {
                var salesTrend = salesTrends.FirstOrDefault(t => t.MonthNumber == month);
                var purchaseTrend = purchaseTrends.FirstOrDefault(t => t.MonthNumber == month);

                var revenue = salesTrend?.Revenue ?? 0;
                var cost = purchaseTrend?.Cost ?? 0;
                var profit = revenue - cost;

                profitTrends.Add(new MonthlyTrendModel
                {
                    MonthName = new DateTime(year, month, 1).ToString("yyyy年MM月"),
                    MonthNumber = month,
                    Revenue = revenue,
                    Cost = cost,
                    Profit = profit,
                    OrdersCount = (salesTrend?.OrdersCount ?? 0) + (purchaseTrend?.OrdersCount ?? 0)
                });
            }

            return profitTrends;
        }

        /// <summary>
        /// 获取产品利润分析
        /// </summary>
        public List<ProductProfitAnalysisModel> GetProductProfitAnalysis(DateTime startDate, DateTime endDate)
        {
            // 获取销售数据
            var productSalesReport = _salesReportService.GetProductSalesReport(startDate, endDate);
            // 获取采购数据
            var productPurchaseReport = _purchaseReportService.GetProductPurchaseReport(startDate, endDate);

            // 计算产品利润
            var productProfitAnalysis = new List<ProductProfitAnalysisModel>();

            foreach (var salesData in productSalesReport)
            {
                // 找到对应的采购数据
                var purchaseData = productPurchaseReport.FirstOrDefault(p => p.ProductId == salesData.ProductId);
                
                decimal cost = purchaseData?.TotalCost ?? 0;
                decimal revenue = salesData.TotalRevenue;
                decimal profit = revenue - cost;
                decimal profitMargin = revenue > 0 ? (profit / revenue) * 100 : 0;

                productProfitAnalysis.Add(new ProductProfitAnalysisModel
                {
                    ProductId = salesData.ProductId,
                    ProductSku = salesData.ProductSku,
                    ProductName = salesData.ProductName,
                    QuantitySold = salesData.QuantitySold,
                    TotalRevenue = revenue,
                    TotalCost = cost,
                    Profit = profit,
                    ProfitMargin = Math.Round(profitMargin, 2),
                    StartDate = startDate,
                    EndDate = endDate
                });
            }

            return productProfitAnalysis.OrderByDescending(p => p.Profit).ToList();
        }
    }
}