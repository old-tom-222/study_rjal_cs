using CSproject.Business.Models;
using CSproject.Data.Repositories;
using System;
using System.Collections.Generic;

namespace CSproject.Business.Services
{
    public class AccountReceivableService
    {
        private readonly AccountReceivableRepository _receivableRepo;

        public AccountReceivableService()
        {
            _receivableRepo = new AccountReceivableRepository();
        }

        public List<AccountReceivable> GetAccountReceivables(string orderNo = null, int? customerId = null, string status = null)
        {
            return _receivableRepo.GetAccountReceivables(orderNo, customerId, status);
        }

        public AccountReceivable GetAccountReceivableById(int receivableId)
        {
            return _receivableRepo.GetAccountReceivableById(receivableId);
        }

        public int CreateAccountReceivable(AccountReceivable receivable)
        {
            if (string.IsNullOrWhiteSpace(receivable.OrderNo))
                throw new ArgumentException("订单号不能为空");
            if (receivable.CustomerId <= 0)
                throw new ArgumentException("客户ID必须大于0");
            if (receivable.TotalAmount < 0)
                throw new ArgumentException("总金额不能为负数");
            if (receivable.PaidAmount < 0)
                throw new ArgumentException("已付金额不能为负数");
            if (receivable.PaidAmount > receivable.TotalAmount)
                throw new ArgumentException("已付金额不能大于总金额");

            // 计算未付金额
            receivable.OutstandingAmount = receivable.TotalAmount - receivable.PaidAmount;

            // 设置默认状态
            if (string.IsNullOrWhiteSpace(receivable.Status))
            {
                receivable.Status = receivable.OutstandingAmount == 0 ? "paid" : "pending";
            }

            // 设置默认日期
            if (receivable.OrderDate == DateTime.MinValue)
                receivable.OrderDate = DateTime.Now;
            if (receivable.DueDate == DateTime.MinValue)
                receivable.DueDate = DateTime.Now.AddDays(30); // 默认30天付款期限

            return _receivableRepo.CreateAccountReceivable(receivable);
        }

        public bool UpdateAccountReceivable(AccountReceivable receivable)
        {
            if (receivable.ReceivableId <= 0)
                throw new ArgumentException("应收账款ID必须大于0");
            if (string.IsNullOrWhiteSpace(receivable.OrderNo))
                throw new ArgumentException("订单号不能为空");
            if (receivable.CustomerId <= 0)
                throw new ArgumentException("客户ID必须大于0");
            if (receivable.TotalAmount < 0)
                throw new ArgumentException("总金额不能为负数");
            if (receivable.PaidAmount < 0)
                throw new ArgumentException("已付金额不能为负数");
            if (receivable.PaidAmount > receivable.TotalAmount)
                throw new ArgumentException("已付金额不能大于总金额");

            // 计算未付金额
            receivable.OutstandingAmount = receivable.TotalAmount - receivable.PaidAmount;

            // 更新状态
            if (receivable.OutstandingAmount == 0)
                receivable.Status = "paid";
            else if (receivable.Status == "paid")
                receivable.Status = "partially_paid";

            return _receivableRepo.UpdateAccountReceivable(receivable);
        }

        public bool DeleteAccountReceivable(int receivableId)
        {
            if (receivableId <= 0)
                throw new ArgumentException("应收账款ID必须大于0");

            return _receivableRepo.DeleteAccountReceivable(receivableId);
        }

        public bool RecordPayment(int receivableId, decimal amount)
        {
            if (receivableId <= 0)
                throw new ArgumentException("应收账款ID必须大于0");
            if (amount <= 0)
                throw new ArgumentException("付款金额必须大于0");

            var receivable = _receivableRepo.GetAccountReceivableById(receivableId);
            if (receivable == null)
                throw new ArgumentException("未找到指定的应收账款");

            // 更新已付金额
            receivable.PaidAmount += amount;
            // 确保已付金额不超过总金额
            if (receivable.PaidAmount > receivable.TotalAmount)
                receivable.PaidAmount = receivable.TotalAmount;

            // 计算未付金额
            receivable.OutstandingAmount = receivable.TotalAmount - receivable.PaidAmount;

            // 更新状态
            if (receivable.OutstandingAmount == 0)
                receivable.Status = "paid";
            else if (receivable.Status == "pending")
                receivable.Status = "partially_paid";

            return _receivableRepo.UpdateAccountReceivable(receivable);
        }
    }
}