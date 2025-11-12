using System;

namespace CSproject.Business.Models
{
    public class InventoryTransaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int ChangeQty { get; set; }
        public string Type { get; set; } // adjust, purchase, sale, transfer
        public string Reference { get; set; }
        public string Remark { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}