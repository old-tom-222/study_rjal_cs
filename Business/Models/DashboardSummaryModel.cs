using System;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 仪表板概要数据模型
    /// </summary>
    public class DashboardSummaryModel
    {
        // 销售指标
        public decimal TotalSalesAmount { get; set; }
        public decimal AvgDailySales { get; set; }
        public int SalesOrdersCount { get; set; }
        public decimal SalesChangePercent { get; set; }
        public int TotalItemsSold { get; set; }
        public int ItemsSoldToday { get; set; }

        // 采购指标
        public decimal TotalPurchaseAmount { get; set; }
        public int PurchaseOrdersCount { get; set; }
        public decimal PurchaseChangePercent { get; set; }

        // 利润指标
        public decimal TotalProfit { get; set; }
        public decimal ProfitChangePercent { get; set; }

        // 库存指标
        public decimal CurrentInventoryValue { get; set; }
        public int LowStockItemsCount { get; set; }
        public int OutOfStockItemsCount { get; set; }
    }
}