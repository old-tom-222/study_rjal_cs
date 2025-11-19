using System;
using System.Collections.Generic;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 销售订单模型（用于创建订单界面）
    /// </summary>
    public class SalesOrderModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string ShippingAddress { get; set; }
        public string ContactPhone { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // 添加CreateTime属性，与CreatedDate同步
        public DateTime CreateTime { get { return CreatedDate; } set { CreatedDate = value; } }
        
        // 订单明细
        public List<SalesOrderDetailModel> OrderDetails { get; set; } = new List<SalesOrderDetailModel>();
        
        // 添加SalesOrderDetails属性，作为OrderDetails的别名
        public List<SalesOrderDetailModel> SalesOrderDetails { get { return OrderDetails; } set { OrderDetails = value; } }
    }
}