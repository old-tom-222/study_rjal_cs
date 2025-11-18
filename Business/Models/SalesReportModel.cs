using System;

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
        public decimal TotalProfit
        {
            get { return TotalRevenue * ProfitMargin / 100; }
        }
    }

    public class DailySalesReportModel
    {
        public DateTime Date { get; set; }
        public int TotalOrders { get; set; }
        public int TotalItemsSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
}