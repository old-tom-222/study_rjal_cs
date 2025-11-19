using System;
using System.Collections.Generic;

namespace CSproject.Business.Models
{
    public class SalesReportModel
    {
        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public int QuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AveragePrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal TotalProfit => TotalRevenue * ProfitMargin / 100;
        public decimal Percentage { get; set; } // 销售占比(%)
    }

    public class DailySalesReportModel
    {
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public int TotalItemsSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
    
    public class CustomerRankingModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalSpent { get; set; }
        public int OrderCount { get; set; }
        public int Rank { get; set; }
    }
    
    public class ProductRankingModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSku { get; set; }
        public int QuantitySold { get; set; }
        public decimal SalesAmount { get; set; }
        public int Rank { get; set; }
    }
    
    public class MonthlySalesData
    {
        public string Month { get; set; }
        public decimal SalesAmount { get; set; }
        public int OrderCount { get; set; }
    }
}