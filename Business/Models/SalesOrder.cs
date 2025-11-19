using System;
using System.Collections.Generic;

namespace CSproject.Business.Models
{
    public class SalesOrder
    {
        // 构造函数，初始化OrderItems集合
        public SalesOrder()
        {
            OrderItems = new List<SalesOrderItem>();
        }

        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } // 待审核、已审核、已发货、已完成、已取消
        public string PaymentStatus { get; set; } // 未付款、部分付款、已付款
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShippingAddress { get; set; }
        public string ContactPhone { get; set; }
        public string Notes { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        
        // 兼容旧版本的Status属性
        public string Status { get { return OrderStatus; } set { OrderStatus = value; } }
        
        // 关联的订单明细
        public List<SalesOrderItem> OrderItems { get; set; }
        
        // 添加SalesOrderDetails属性，作为OrderItems的别名，以保持向后兼容性
        public List<SalesOrderItem> SalesOrderDetails { get { return OrderItems; } set { OrderItems = value; } }
        
        // 计算属性
        public bool IsApproved => OrderStatus == "已审核";
        public bool IsShipped => OrderStatus == "已发货" || OrderStatus == "已完成";
    }
}