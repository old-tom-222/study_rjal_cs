using System;

namespace CSproject.Business.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int SafeStock { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}