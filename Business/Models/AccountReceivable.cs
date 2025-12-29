using System;

namespace CSproject.Business.Models
{
    public class AccountReceivable
    {
        public int ReceivableId { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}