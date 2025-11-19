using System;
using System.Collections.Generic;
using System.Linq;
using Customer = CSproject.Business.Models.Customer;  // Explicitly reference Customer type
using CSproject.Business.Models;
using CSproject.Data.Repositories;  // Explicitly including repository namespace for CustomerRepository

namespace CSproject.Business.Services
{
    public class SalesOrderService
    {
        private readonly SalesOrderRepository _orderRepo = new SalesOrderRepository();
        private readonly CustomerRepository _customerRepo = new CustomerRepository();
        private readonly InventoryRepository _inventoryRepo = new InventoryRepository();
        private readonly InventoryService _inventoryService = new InventoryService();

        /// <summary>
        /// 获取销售订单列表
        /// </summary>
        public List<SalesOrder> GetSalesOrders(string orderNumber = null, int? customerId = null, string status = null)
        {
            return _orderRepo.GetSalesOrders(orderNumber, customerId, status);
        }

        /// <summary>
        /// 获取销售订单详情
        /// </summary>
        public SalesOrder GetSalesOrderById(int orderId)
        {
            return _orderRepo.GetSalesOrderById(orderId);
        }

        /// <summary>
        /// 创建新的销售订单
        /// </summary>
        public int CreateSalesOrder(SalesOrder order, string userId = null)
        {
            // 验证客户是否存在且活跃
            var customer = _customerRepo.GetCustomerById(order.CustomerId);
            if (customer == null)
            {
                throw new Exception($"客户不存在或状态不活跃 - 未找到ID为{order.CustomerId}的客户");
            }
            
            if (customer.Status != null)
            {
                // 使用Trim去除可能的空格，并进行不区分大小写的比较
                string trimmedStatus = customer.Status.Trim();
                
                // 同时支持"1"和"True"作为有效的活跃状态值
                if (trimmedStatus != "1" && trimmedStatus.ToLower() != "true")
                {
                    throw new Exception($"客户不存在或状态不活跃 - 客户ID:{order.CustomerId}的状态为'{trimmedStatus}'，不是'1'或'True'");
                }
            }
            else
            {
                throw new Exception($"客户不存在或状态不活跃 - 客户ID:{order.CustomerId}的状态为null");
            }

            // 验证产品ID是否存在且有效
            foreach (var item in order.OrderItems)
            {
                // 首先验证产品ID必须大于0
                if (item.ProductId <= 0)
                {
                    throw new Exception($"无效的产品ID - 产品ID必须大于0，当前值: {item.ProductId}");
                }
                
                // 检查产品ID是否有效（通过检查库存是否大于等于0来间接验证产品存在）
                var totalInventory = _inventoryRepo.GetTotalInventoryByProductId(item.ProductId);
                
                // 如果库存查询返回-1，表示产品不存在
                if (totalInventory == -1)
                {
                    throw new Exception($"产品不存在 - 产品ID:{item.ProductId}在系统中不存在");
                }
            }

            // 验证库存是否充足
            foreach (var item in order.OrderItems)
            {
                // 获取所有仓库的该产品库存总和
                var totalInventory = _inventoryRepo.GetTotalInventoryByProductId(item.ProductId);
                
                if (totalInventory < item.Quantity)
                {
                    throw new Exception($"产品 {item.ProductName} 库存不足 - 需求: {item.Quantity}, 可用: {totalInventory}");
                }
            }

            // 设置创建人 - 如果userId是字符串且不能转换为整数，则设置为默认值1
            int creatorId = 1; // 默认用户ID
            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int parsedUserId))
            {
                creatorId = parsedUserId;
            }
            order.CreatedBy = creatorId.ToString(); // 保持与模型一致，但确保值是有效的整数字符串
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                order.OrderNumber = _orderRepo.GenerateOrderNumber();
            }

            // 计算订单总金额
            order.TotalAmount = 0;
            foreach (var item in order.OrderItems)
            {
                order.TotalAmount += item.UnitPrice * item.Quantity;
            }

            // 创建订单
            int orderId = _orderRepo.CreateSalesOrder(order);
            
            // 注意：库存减少操作已移至发货环节（ShipSalesOrder方法）
            
            return orderId;
        }

        /// <summary>
        /// 审核销售订单
        /// </summary>
        public bool ApproveSalesOrder(int orderId, string userId)
        {
            return _orderRepo.UpdateSalesOrderStatus(orderId, "已审核", userId);
        }

        /// <summary>
        /// 驳回销售订单
        /// </summary>
        public bool RejectSalesOrder(int orderId, string userId)
        {
            return _orderRepo.UpdateSalesOrderStatus(orderId, "已驳回", userId);
        }

        /// <summary>
        /// 发货处理
        /// </summary>
        public bool ShipSalesOrder(int orderId, string userId)
        {
            // 先获取订单信息进行验证
            var order = _orderRepo.GetSalesOrderById(orderId);
            if (order == null || order.OrderStatus != "已审核")
            {
                return false;
            }
            
            // 再次检查库存是否充足（防止订单创建后库存被其他操作减少）
            foreach (var item in order.OrderItems)
            {
                var totalInventory = _inventoryRepo.GetTotalInventoryByProductId(item.ProductId);
                if (totalInventory < item.Quantity)
                {
                    throw new Exception($"产品 {item.ProductName} 库存不足 - 需求: {item.Quantity}, 可用: {totalInventory}");
                }
            }
            
            // 减少库存
            foreach (var item in order.OrderItems)
            {
                // 从第一个有库存的仓库减少库存
                var inventoryModels = _inventoryService.GetInventoryModels(productId: item.ProductId);
                if (inventoryModels != null && inventoryModels.Count > 0)
                {
                    var selectedWarehouse = inventoryModels.First();
                    _inventoryService.ReduceInventory(
                        item.ProductId, 
                        selectedWarehouse.WarehouseId, 
                        item.Quantity, 
                        order.OrderNumber,
                        $"销售订单{order.OrderNumber}出库");
                }
            }
            
            // 执行发货操作
            return _orderRepo.UpdateSalesOrderStatus(orderId, "已发货", userId);
        }
        
        /// <summary>
        /// 获取订单详情（兼容旧版本调用）
        /// </summary>
        public SalesOrder GetOrderById(int orderId)
        {
            return GetSalesOrderById(orderId);
        }
        
        /// <summary>
        /// 根据状态获取订单列表（兼容旧版本调用）
        /// </summary>
        public List<SalesOrder> GetOrdersByStatus(string status)
        {
            return _orderRepo.GetSalesOrders(status: status);
        }
        
        /// <summary>
        /// 根据状态获取订单列表
        /// </summary>
        public List<SalesOrder> GetSalesOrdersByStatus(string status)
        {
            return _orderRepo.GetSalesOrders(status: status);
        }
        
        /// <summary>
        /// 审核订单（兼容旧版本调用）
        /// </summary>
        public bool ApproveOrder(int orderId, string userId)
        {
            return ApproveSalesOrder(orderId, userId);
        }
        
        /// <summary>
        /// 驳回订单（兼容旧版本调用）
        /// </summary>
        public bool RejectOrder(int orderId, string userId)
        {
            return RejectSalesOrder(orderId, userId);
        }
        
        /// <summary>
        /// 发货订单（兼容旧版本调用）
        /// </summary>
        public bool ShipOrder(int orderId, string userId)
        {
            return ShipSalesOrder(orderId, userId);
        }

        /// <summary>
        /// 获取待审核的订单列表
        /// </summary>
        public List<SalesOrder> GetPendingOrders()
        {
            return _orderRepo.GetSalesOrders(status: "待审核");
        }

        /// <summary>
        /// 获取待发货的订单列表
        /// </summary>
        public List<SalesOrder> GetApprovedOrders()
        {
            return _orderRepo.GetSalesOrders(status: "已审核");
        }

        /// <summary>
        /// 生成订单编号
        /// </summary>
        public string GenerateOrderNumber()
        {
            return _orderRepo.GenerateOrderNumber();
        }
        
        /// <summary>
        /// 创建销售订单（兼容旧版本调用）
        /// </summary>
        public int CreateOrder(SalesOrder order, string userId)
        {
            return CreateSalesOrder(order, userId);
        }
    }
}