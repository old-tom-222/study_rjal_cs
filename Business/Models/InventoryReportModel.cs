using System;

namespace CSproject.Business.Models
{
    public class InventoryReportModel
    {
        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public int CurrentQuantity { get; set; }
        public int SafeStock { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public decimal AverageCost { get; set; }
        public decimal TotalValue { get; set; }
        public int ReorderQuantity { get; set; }
        public bool IsLowStock => CurrentQuantity <= ReorderQuantity;
        public DateTime LastStockMovementDate { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
    }
}