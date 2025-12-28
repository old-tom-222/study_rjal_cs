using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class BankAccountService
    {
        private readonly BankAccountRepository _bankAccountRepository;

        public BankAccountService()
        {
            _bankAccountRepository = new BankAccountRepository();
        }

        /// <summary>
        /// 获取所有银行账户
        /// </summary>
        public List<BankAccount> GetAllBankAccounts(string bankName = null, string accountName = null, bool? isActive = null)
        {
            try
            {
                return _bankAccountRepository.GetBankAccounts(bankName, accountName, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取银行账户列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取银行账户
        /// </summary>
        public BankAccount GetBankAccountById(int id)
        {
            try
            {
                var bankAccount = _bankAccountRepository.GetBankAccountById(id);
                if (bankAccount == null)
                {
                    throw new Exception($"ID为 {id} 的银行账户不存在");
                }
                return bankAccount;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取银行账户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建银行账户
        /// </summary>
        public int CreateBankAccount(BankAccount bankAccount)
        {
            try
            {
                // 验证必要字段
                ValidateBankAccountRequiredFields(bankAccount);

                // 设置默认值
                if (bankAccount.Balance == null)
                {
                    bankAccount.Balance = 0;
                }

                if (bankAccount.CreatedAt == DateTime.MinValue)
                {
                    bankAccount.CreatedAt = DateTime.Now;
                }

                return _bankAccountRepository.CreateBankAccount(bankAccount);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建银行账户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新银行账户
        /// </summary>
        public bool UpdateBankAccount(BankAccount bankAccount)
        {
            try
            {
                // 验证银行账户是否存在
                var existingBankAccount = _bankAccountRepository.GetBankAccountById(bankAccount.Id);
                if (existingBankAccount == null)
                {
                    throw new Exception($"ID为 {bankAccount.Id} 的银行账户不存在");
                }

                // 验证必要字段
                ValidateBankAccountRequiredFields(bankAccount);

                // 设置更新时间
                bankAccount.UpdatedAt = DateTime.Now;

                return _bankAccountRepository.UpdateBankAccount(bankAccount);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新银行账户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除银行账户
        /// </summary>
        public bool DeleteBankAccount(int id)
        {
            try
            {
                // 验证银行账户是否存在
                var existingBankAccount = _bankAccountRepository.GetBankAccountById(id);
                if (existingBankAccount == null)
                {
                    throw new Exception($"ID为 {id} 的银行账户不存在");
                }

                // 验证账户余额是否为0
                if (existingBankAccount.Balance > 0)
                {
                    throw new Exception("账户余额不为0，无法删除");
                }

                return _bankAccountRepository.DeleteBankAccount(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除银行账户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新银行账户余额
        /// </summary>
        public bool UpdateBankAccountBalance(int id, decimal amount)
        {
            try
            {
                // 验证银行账户是否存在
                var existingBankAccount = _bankAccountRepository.GetBankAccountById(id);
                if (existingBankAccount == null)
                {
                    throw new Exception($"ID为 {id} 的银行账户不存在");
                }

                // 计算新余额
                decimal newBalance = existingBankAccount.Balance + amount;
                if (newBalance < 0)
                {
                    throw new Exception("账户余额不足");
                }

                return _bankAccountRepository.UpdateBankAccountBalance(id, newBalance);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新银行账户余额失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证银行账户必要字段
        /// </summary>
        private void ValidateBankAccountRequiredFields(BankAccount bankAccount)
        {
            if (string.IsNullOrEmpty(bankAccount.BankName))
            {
                throw new Exception("银行名称不能为空");
            }

            if (string.IsNullOrEmpty(bankAccount.AccountName))
            {
                throw new Exception("账户名称不能为空");
            }

            if (string.IsNullOrEmpty(bankAccount.AccountNumber))
            {
                throw new Exception("账号不能为空");
            }
        }
    }
}