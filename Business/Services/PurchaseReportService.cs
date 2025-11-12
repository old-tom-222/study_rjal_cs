using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class PurchaseReportService
    {
        private readonly InventoryTransactionRepository _transactionRepo;

        public PurchaseReportService()
        {
            _transactionRepo = new InventoryTransactionRepository();
        }

        /// <summary>
        /// 获取产品采购报表
        /// </summary>
        public List<PurchaseReportModel> GetProductPurchaseReport(DateTime startDate, DateTime endDate)
        {
            // 获取时间范围内的所有交易记录
            var allTransactions = _transactionRepo.GetTransactions(from: startDate, to: endDate);
            // 筛选出采购类型的交易记录
            var purchaseTransactions = allTransactions.Where(t => t.Type == "purchase").ToList();
            
            // 按产品分组统计
            var productGroups = purchaseTransactions.GroupBy(t => t.ProductId);
            var reportItems = new List<PurchaseReportModel>();

            foreach (var group in productGroups)
            {
                int totalQuantity = group.Sum(t => t.ChangeQty); // 采购是正数
                decimal averageCost = 0; // 实际应用中需要从采购订单获取价格信息
                decimal totalCost = averageCost * totalQuantity;

                var reportItem = new PurchaseReportModel
                {
                    ProductId = group.Key,
                    ProductSku = group.First().ProductSku,
                    ProductName = group.First().ProductName,
                    QuantityPurchased = totalQuantity,
                    TotalCost = totalCost,
                    AverageCost = averageCost,
                    StartDate = startDate,
                    EndDate = endDate,
                    PurchaseOrderCount = group.Select(t => t.Reference).Distinct().Count(),
                    // 其他属性可以从其他数据源获取
                };

                reportItems.Add(reportItem);
            }

            return reportItems.OrderByDescending(r => r.QuantityPurchased).ToList();
        }

        /// <summary>
        /// 获取供应商表现报表
        /// </summary>
        public List<SupplierPerformanceReportModel> GetSupplierPerformanceReport(DateTime startDate, DateTime endDate)
        {
            // 实际应用中需要从采购订单和收货记录获取供应商表现数据
            // 这里模拟生成一些测试数据
            var reportItems = new List<SupplierPerformanceReportModel>();
            
            // 模拟几个供应商的数据
            var supplierNames = new[] { "供应商A", "供应商B", "供应商C", "供应商D", "供应商E" };
            
            for (int i = 1; i <= supplierNames.Length; i++)
            {
                int totalOrders = new Random(i).Next(5, 20);
                decimal totalSpent = new Random(i + 10).Next(10000, 100000);
                int totalItemsReceived = totalOrders * new Random(i + 20).Next(10, 50);
                decimal avgDeliveryDays = new Random(i + 30).Next(1, 10) + (decimal)new Random(i + 40).NextDouble();
                decimal complianceRate = new Random(i + 50).Next(70, 100) + (decimal)new Random(i + 60).NextDouble();

                reportItems.Add(new SupplierPerformanceReportModel
                {
                    SupplierId = i,
                    SupplierName = supplierNames[i - 1],
                    TotalOrders = totalOrders,
                    TotalSpent = totalSpent,
                    TotalItemsReceived = totalItemsReceived,
                    AverageDeliveryTimeDays = Math.Round(avgDeliveryDays, 2),
                    ComplianceRate = Math.Round(complianceRate, 2)
                });
            }

            return reportItems.OrderByDescending(r => r.ComplianceRate).ToList();
        }

        /// <summary>
        /// 获取采购趋势报表
        /// </summary>
        public List<MonthlyTrendModel> GetPurchaseTrendReport(int year)
        {
            var reportItems = new List<MonthlyTrendModel>();

            // 生成全年12个月的采购趋势数据
            for (int month = 1; month <= 12; month++)
            {
                var monthName = new DateTime(year, month, 1).ToString("yyyy年MM月");
                decimal cost = new Random(month + 100).Next(30000, 150000);
                
                reportItems.Add(new MonthlyTrendModel
                {
                    MonthName = monthName,
                    MonthNumber = month,
                    Revenue = 0,
                    Cost = cost,
                    Profit = 0,
                    OrdersCount = new Random(month + 200).Next(50, 200)
                });
            }

            return reportItems;
        }
    }
}