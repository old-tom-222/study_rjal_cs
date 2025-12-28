using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository;
        private readonly AccountingSubjectRepository _accountingSubjectRepository;

        public ExpenseService()
        {
            _expenseRepository = new ExpenseRepository();
            _accountingSubjectRepository = new AccountingSubjectRepository();
        }

        /// <summary>
        /// 获取所有支出记录
        /// </summary>
        public List<Expense> GetAllExpenses(string code = null, int? subjectId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return _expenseRepository.GetExpenses(code, subjectId, startDate, endDate);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取支出记录
        /// </summary>
        public Expense GetExpenseById(int id)
        {
            try
            {
                var expense = _expenseRepository.GetExpenseById(id);
                if (expense == null)
                {
                    throw new Exception($"ID为 {id} 的支出记录不存在");
                }
                return expense;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建支出记录
        /// </summary>
        public int CreateExpense(Expense expense)
        {
            try
            {
                // 验证会计科目是否存在
                ValidateAccountingSubjectExists(expense.SubjectId);

                // 验证金额是否有效
                ValidateAmount(expense.Amount);

                // 生成支出编号（示例：EX+年月日+5位流水号）
                if (string.IsNullOrEmpty(expense.ExpenseNo))
                {
                    expense.ExpenseNo = GenerateExpenseNumber();
                }

                // 设置创建时间
                if (expense.CreatedAt == DateTime.MinValue)
                {
                    expense.CreatedAt = DateTime.Now;
                }

                // 设置创建人（这里可以根据实际登录用户设置，目前示例使用固定值1）
                if (expense.CreatedBy <= 0)
                {
                    expense.CreatedBy = 1; // 默认创建人ID为1
                }

                return _expenseRepository.CreateExpense(expense);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新支出记录
        /// </summary>
        public bool UpdateExpense(Expense expense)
        {
            try
            {
                // 检查支出记录是否存在
                var existingExpense = _expenseRepository.GetExpenseById(expense.Id);
                if (existingExpense == null)
                {
                    throw new Exception($"ID为 {expense.Id} 的支出记录不存在");
                }

                // 验证会计科目是否存在
                ValidateAccountingSubjectExists(expense.SubjectId);

                // 验证金额是否有效
                ValidateAmount(expense.Amount);

                return _expenseRepository.UpdateExpense(expense);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除支出记录
        /// </summary>
        public bool DeleteExpense(int id)
        {
            try
            {
                // 检查支出记录是否存在
                var existingExpense = _expenseRepository.GetExpenseById(id);
                if (existingExpense == null)
                {
                    throw new Exception($"ID为 {id} 的支出记录不存在");
                }

                return _expenseRepository.DeleteExpense(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据日期范围获取总支出
        /// </summary>
        public decimal GetTotalExpenseByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _expenseRepository.GetTotalExpenseByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"计算总支出失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 生成支出编号
        /// </summary>
        private string GenerateExpenseNumber()
        {
            // 示例：EX+年月日+5位流水号，如EX2024052000001
            string prefix = "EX";
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string serialNumber = "00001";

            // 获取当天最后一条支出记录
            var todayExpenses = _expenseRepository.GetExpenses(startDate: DateTime.Today, endDate: DateTime.Today.AddDays(1));
            if (todayExpenses.Count > 0)
            {
                // 提取最后一条记录的编号，生成新的流水号
                string lastCode = todayExpenses[^1].ExpenseNo;
                if (lastCode.Length >= prefix.Length + datePart.Length + serialNumber.Length)
                {
                    string lastSerial = lastCode.Substring(prefix.Length + datePart.Length);
                    if (int.TryParse(lastSerial, out int serial))
                    {
                        serialNumber = (serial + 1).ToString("D5");
                    }
                }
            }

            return $"{prefix}{datePart}{serialNumber}";
        }

        /// <summary>
        /// 验证会计科目是否存在
        /// </summary>
        private void ValidateAccountingSubjectExists(int subjectId)
        {
            var subject = _accountingSubjectRepository.GetAccountingSubjectById(subjectId);
            if (subject == null)
            {
                throw new Exception($"会计科目不存在");
            }
        }

        /// <summary>
        /// 验证金额是否有效
        /// </summary>
        private void ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception($"金额必须大于0");
            }
        }
    }
}