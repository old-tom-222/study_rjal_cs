using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepo = new CustomerRepository();
        private readonly SalesOrderRepository _orderRepo = new SalesOrderRepository();

        /// <summary>
        /// 获取客户列表
        /// </summary>
        public List<Customer> GetCustomers(string customerCode = null, string customerName = null, string status = null)
        {
            return _customerRepo.GetCustomers(customerCode, customerName, status);
        }

        /// <summary>
        /// 获取所有客户列表（用于下拉框）
        /// </summary>
        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetCustomers();
        }

        /// <summary>
        /// 获取客户详情
        /// </summary>
        public Customer GetCustomerById(int customerId)
        {
            return _customerRepo.GetCustomerById(customerId);
        }

        /// <summary>
        /// 获取活跃客户列表
        /// </summary>
        public List<Customer> GetActiveCustomers()
        {
            return _customerRepo.GetActiveCustomers();
        }

        /// <summary>
        /// 获取客户的销售订单历史
        /// </summary>
        public List<SalesOrder> GetCustomerOrderHistory(int customerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var orders = _orderRepo.GetSalesOrders(customerId: customerId);
            
            // 如果指定了日期范围，则进行筛选
            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.OrderDate >= startDate.Value).ToList();
            }
            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.OrderDate <= endDate.Value).ToList();
            }
            
            return orders;
        }

        /// <summary>
        /// 创建新客户
        /// </summary>
        public int CreateCustomer(Customer customer)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(customer.CustomerName))
                {
                    throw new ArgumentException("客户名称不能为空");
                }

                // 设置默认状态
                if (string.IsNullOrWhiteSpace(customer.Status))
                {
                    customer.Status = "1"; // 使用数字状态值
                }

                // 设置日期
                customer.CreatedDate = DateTime.Now;
                customer.LastUpdated = DateTime.Now;

                // 调用数据访问层创建客户
                return _customerRepo.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("创建客户失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 计算客户的总销售额
        /// </summary>
        public decimal GetCustomerTotalSpent(int customerId, DateTime? startDate = null, DateTime? endDate = null)
        {
            var orders = GetCustomerOrderHistory(customerId, startDate, endDate);
            decimal total = 0;
            
            foreach (var order in orders)
            {
                if (order.Status == "APPROVED" || order.Status == "SHIPPED")
                {
                    total += order.TotalAmount;
                }
            }
            
            return total;
        }
    }
}