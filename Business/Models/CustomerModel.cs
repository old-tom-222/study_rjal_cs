using System;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 客户模型（用于界面展示和操作）
    /// </summary>
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string CustomerType { get; set; }
        public string Status { get; set; } // 活跃、禁用
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Notes { get; set; }
    }
}