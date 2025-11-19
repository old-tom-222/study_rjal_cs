using System;

namespace CSproject.Business.Models
{
    public class InventoryModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string WarehouseName { get; set; }
        public int WarehouseId { get; set; }
        
        // 为了在下拉框中显示正确的产品名称
        public override string ToString()
        {
            return ProductName;
        }
    }
}