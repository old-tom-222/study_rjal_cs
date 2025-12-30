using System;

namespace CSproject.Business.Models
{
    public class BusinessDashboardSummaryModel
    {
        // 销售额统计
        public decimal TotalRevenue { get; set; }
        public decimal RevenueToday { get; set; }
        public decimal RevenueThisWeek { get; set; }
        public decimal RevenueThisMonth { get; set; }
        public decimal RevenueLastMonth { get; set; }
        public decimal RevenueGrowthRate { get; set; } // 增长率

        // 销售数量统计
        public int TotalItemsSold { get; set; }
        public int ItemsSoldToday { get; set; }

        // 订单统计
        public int TotalOrders { get; set; }
        public int OrdersToday { get; set; }
        public decimal AverageOrderValue { get; set; }

        // 库存统计
        public decimal TotalInventoryValue { get; set; }
        public int LowStockItemsCount { get; set; }
        public int OutOfStockItemsCount { get; set; }

        // 采购统计
        public decimal TotalPurchaseCost { get; set; }
        public decimal PurchaseCostThisMonth { get; set; }

        // 利润统计
        public decimal TotalProfit { get; set; }
        public decimal ProfitMargin { get; set; }
    }

    public class TopSellingProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal RevenuePercentage { get; set; }
        public int Rank { get; set; }
    }

    public class MonthlyTrendModel
    {
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
        public decimal Revenue { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit { get; set; }
        public int OrdersCount { get; set; }
    }
}