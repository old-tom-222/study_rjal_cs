using System;

namespace CSproject.Business.Models
{
    public class PurchaseReportModel
    {
        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AverageCost { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int PurchaseOrderCount { get; set; }
        public string Category { get; set; }
    }

    public class SupplierPerformanceReportModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSpent { get; set; }
        public int TotalItemsReceived { get; set; }
        public decimal AverageDeliveryTimeDays { get; set; }
        public decimal ComplianceRate { get; set; } // 交货及时率
    }
}