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
        public bool IsLowStock
        {
            get { return CurrentQuantity <= ReorderQuantity; }
        }
        public DateTime LastStockMovementDate { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        
        // 库存周转率相关属性
        public int OpeningStock { get; set; } // 期初库存
        public int ClosingStock { get; set; } // 期末库存
        public decimal AverageStock { get; set; } // 平均库存
        public int SalesQuantity { get; set; } // 销售数量
        public decimal TurnoverRate { get; set; } // 周转率
    }
}