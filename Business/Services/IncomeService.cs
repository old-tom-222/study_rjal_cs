using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class IncomeService
    {
        private readonly IncomeRepository _incomeRepository;
        private readonly AccountingSubjectRepository _accountingSubjectRepository;

        public IncomeService()
        {
            _incomeRepository = new IncomeRepository();
            _accountingSubjectRepository = new AccountingSubjectRepository();
        }

        /// <summary>
        /// 获取所有收入记录
        /// </summary>
        public List<Income> GetAllIncomes(string code = null, int? subjectId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return _incomeRepository.GetIncomes(code, subjectId, startDate, endDate);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取收入记录
        /// </summary>
        public Income GetIncomeById(int id)
        {
            try
            {
                var income = _incomeRepository.GetIncomeById(id);
                if (income == null)
                {
                    throw new Exception($"ID为 {id} 的收入记录不存在");
                }
                return income;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建收入记录
        /// </summary>
        public int CreateIncome(Income income)
        {
            try
            {
                // 验证会计科目是否存在
                ValidateAccountingSubjectExists(income.SubjectId);

                // 验证金额是否有效
                ValidateAmount(income.Amount);

                // 生成收入编号（示例：IN+年月日+5位流水号）
                if (string.IsNullOrEmpty(income.IncomeNo))
                {
                    income.IncomeNo = GenerateIncomeNumber();
                }

                // 设置创建时间
                if (income.CreatedAt == DateTime.MinValue)
                {
                    income.CreatedAt = DateTime.Now;
                }

                // 设置创建人（这里可以根据实际登录用户设置，目前示例使用固定值1）
                if (income.CreatedBy <= 0)
                {
                    income.CreatedBy = 1; // 默认创建人ID为1
                }

                return _incomeRepository.CreateIncome(income);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新收入记录
        /// </summary>
        public bool UpdateIncome(Income income)
        {
            try
            {
                // 检查收入记录是否存在
                var existingIncome = _incomeRepository.GetIncomeById(income.Id);
                if (existingIncome == null)
                {
                    throw new Exception($"ID为 {income.Id} 的收入记录不存在");
                }

                // 验证会计科目是否存在
                ValidateAccountingSubjectExists(income.SubjectId);

                // 验证金额是否有效
                ValidateAmount(income.Amount);

                return _incomeRepository.UpdateIncome(income);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除收入记录
        /// </summary>
        public bool DeleteIncome(int id)
        {
            try
            {
                // 检查收入记录是否存在
                var existingIncome = _incomeRepository.GetIncomeById(id);
                if (existingIncome == null)
                {
                    throw new Exception($"ID为 {id} 的收入记录不存在");
                }

                return _incomeRepository.DeleteIncome(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据日期范围获取总收入
        /// </summary>
        public decimal GetTotalIncomeByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _incomeRepository.GetTotalIncomeByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"计算总收入失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 生成收入编号
        /// </summary>
        private string GenerateIncomeNumber()
        {
            // 示例：IN+年月日+5位流水号，如IN2024052000001
            string prefix = "IN";
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            string serialNumber = "00001";

            // 获取当天最后一条收入记录
            var todayIncomes = _incomeRepository.GetIncomes(startDate: DateTime.Today, endDate: DateTime.Today.AddDays(1));
            if (todayIncomes.Count > 0)
            {
                // 提取最后一条记录的编号，生成新的流水号
                string lastCode = todayIncomes[^1].IncomeNo;
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