using System;

namespace CSproject.Business.Models
{
    public class AccountPayable
    {
        public int PayableId { get; set; }
        public string OrderNo { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}