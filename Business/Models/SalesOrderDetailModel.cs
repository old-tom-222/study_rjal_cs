using System;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 销售订单明细模型（用于创建订单界面）
    /// </summary>
    public class SalesOrderDetailModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }
}