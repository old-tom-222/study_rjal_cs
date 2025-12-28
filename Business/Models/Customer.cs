using System;

namespace CSproject.Business.Models
{
    public class Customer
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