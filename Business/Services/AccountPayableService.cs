using CSproject.Business.Models;
using CSproject.Data.Repositories;
using System;
using System.Collections.Generic;

namespace CSproject.Business.Services
{
    public class AccountPayableService
    {
        private readonly AccountPayableRepository _payableRepo;

        public AccountPayableService()
        {
            _payableRepo = new AccountPayableRepository();
        }

        public List<AccountPayable> GetAccountPayables(string orderNo = null, int? supplierId = null, string status = null)
        {
            return _payableRepo.GetAccountPayables(orderNo, supplierId, status);
        }

        public AccountPayable GetAccountPayableById(int payableId)
        {
            return _payableRepo.GetAccountPayableById(payableId);
        }

        public int CreateAccountPayable(AccountPayable payable)
        {
            if (string.IsNullOrWhiteSpace(payable.OrderNo))
                throw new ArgumentException("订单号不能为空");
            if (payable.SupplierId <= 0)
                throw new ArgumentException("供应商ID必须大于0");
            if (payable.TotalAmount < 0)
                throw new ArgumentException("总金额不能为负数");
            if (payable.PaidAmount < 0)
                throw new ArgumentException("已付金额不能为负数");
            if (payable.PaidAmount > payable.TotalAmount)
                throw new ArgumentException("已付金额不能大于总金额");

            // 计算未付金额
            payable.OutstandingAmount = payable.TotalAmount - payable.PaidAmount;

            // 设置默认状态
            if (string.IsNullOrWhiteSpace(payable.Status))
            {
                payable.Status = payable.OutstandingAmount == 0 ? "paid" : "pending";
            }

            // 设置默认日期
            if (payable.OrderDate == DateTime.MinValue)
                payable.OrderDate = DateTime.Now;
            if (payable.DueDate == DateTime.MinValue)
                payable.DueDate = DateTime.Now.AddDays(30); // 默认30天付款期限

            return _payableRepo.CreateAccountPayable(payable);
        }

        public bool UpdateAccountPayable(AccountPayable payable)
        {
            if (payable.PayableId <= 0)
                throw new ArgumentException("应付账款ID必须大于0");
            if (string.IsNullOrWhiteSpace(payable.OrderNo))
                throw new ArgumentException("订单号不能为空");
            if (payable.SupplierId <= 0)
                throw new ArgumentException("供应商ID必须大于0");
            if (payable.TotalAmount < 0)
                throw new ArgumentException("总金额不能为负数");
            if (payable.PaidAmount < 0)
                throw new ArgumentException("已付金额不能为负数");
            if (payable.PaidAmount > payable.TotalAmount)
                throw new ArgumentException("已付金额不能大于总金额");

            // 计算未付金额
            payable.OutstandingAmount = payable.TotalAmount - payable.PaidAmount;

            // 更新状态
            if (payable.OutstandingAmount == 0)
                payable.Status = "paid";
            else if (payable.Status == "paid")
                payable.Status = "partially_paid";

            return _payableRepo.UpdateAccountPayable(payable);
        }

        public bool DeleteAccountPayable(int payableId)
        {
            if (payableId <= 0)
                throw new ArgumentException("应付账款ID必须大于0");

            return _payableRepo.DeleteAccountPayable(payableId);
        }

        public bool RecordPayment(int payableId, decimal amount)
        {
            if (payableId <= 0)
                throw new ArgumentException("应付账款ID必须大于0");
            if (amount <= 0)
                throw new ArgumentException("付款金额必须大于0");

            var payable = _payableRepo.GetAccountPayableById(payableId);
            if (payable == null)
                throw new ArgumentException("未找到指定的应付账款");

            // 更新已付金额
            payable.PaidAmount += amount;
            // 确保已付金额不超过总金额
            if (payable.PaidAmount > payable.TotalAmount)
                payable.PaidAmount = payable.TotalAmount;

            // 计算未付金额
            payable.OutstandingAmount = payable.TotalAmount - payable.PaidAmount;

            // 更新状态
            if (payable.OutstandingAmount == 0)
                payable.Status = "paid";
            else if (payable.Status == "pending")
                payable.Status = "partially_paid";

            return _payableRepo.UpdateAccountPayable(payable);
        }
    }
}