using System;

namespace CSproject.Business.Models
{
    public class InventoryItem
    {
        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int Quantity { get; set; }
        public int SafeStock { get; set; }
        public decimal UnitPrice { get; set; } // 添加单价属性
        public DateTime LastUpdated { get; set; }
        public bool IsBelowSafeStock
        {
            get { return Quantity < SafeStock; }
        }
    }
}