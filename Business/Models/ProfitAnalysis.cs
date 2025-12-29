using System;

namespace CSproject.Business.Models
{
    public class ProfitAnalysis
    {
        public int AnalysisId { get; set; }
        public DateTime AnalysisDate { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal GrossProfit { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal OperatingExpenses { get; set; }
        public decimal NetProfit { get; set; }
        public decimal NetMargin { get; set; }
        public int TotalSalesOrders { get; set; }
        public int TotalPurchaseOrders { get; set; }
    }
}