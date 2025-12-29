using System;

namespace CSproject.Business.Models
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public string Status { get; set; } // 活跃、禁用
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Notes { get; set; }
    }
}