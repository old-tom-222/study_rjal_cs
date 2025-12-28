using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class AccountingSubjectService
    {
        private readonly AccountingSubjectRepository _accountingSubjectRepository;

        public AccountingSubjectService()
        {
            _accountingSubjectRepository = new AccountingSubjectRepository();
        }

        /// <summary>
        /// 获取所有会计科目
        /// </summary>
        public List<AccountingSubject> GetAllSubjects(string code = null, string name = null, bool? isActive = null)
        {
            try
            {
                return _accountingSubjectRepository.GetAccountingSubjects(code, name, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取会计科目失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取会计科目
        /// </summary>
        public AccountingSubject GetSubjectById(int id)
        {
            try
            {
                var subject = _accountingSubjectRepository.GetAccountingSubjectById(id);
                if (subject == null)
                {
                    throw new Exception($"ID为 {id} 的会计科目不存在");
                }
                return subject;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取会计科目失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建会计科目
        /// </summary>
        public int CreateSubject(AccountingSubject subject)
        {
            try
            {
                // 验证会计科目代码是否唯一
                ValidateSubjectCode(subject.Code, null);

                // 设置创建时间
                if (subject.CreatedAt == DateTime.MinValue)
                {
                    subject.CreatedAt = DateTime.Now;
                }

                return _accountingSubjectRepository.CreateAccountingSubject(subject);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建会计科目失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新会计科目
        /// </summary>
        public bool UpdateSubject(AccountingSubject subject)
        {
            try
            {
                // 检查会计科目是否存在
                var existingSubject = _accountingSubjectRepository.GetAccountingSubjectById(subject.Id);
                if (existingSubject == null)
                {
                    throw new Exception($"ID为 {subject.Id} 的会计科目不存在");
                }

                // 验证会计科目代码是否唯一（排除当前科目）
                ValidateSubjectCode(subject.Code, subject.Id);

                return _accountingSubjectRepository.UpdateAccountingSubject(subject);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新会计科目失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除会计科目
        /// </summary>
        public bool DeleteSubject(int id)
        {
            try
            {
                // 检查会计科目是否存在
                var existingSubject = _accountingSubjectRepository.GetAccountingSubjectById(id);
                if (existingSubject == null)
                {
                    throw new Exception($"ID为 {id} 的会计科目不存在");
                }

                // 检查会计科目是否被使用（可以在后面扩展实现）
                // ValidateSubjectIsNotUsed(id);

                return _accountingSubjectRepository.DeleteAccountingSubject(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除会计科目失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证会计科目代码是否唯一
        /// </summary>
        private void ValidateSubjectCode(string code, int? excludeId)
        {
            var subjects = _accountingSubjectRepository.GetAccountingSubjects(code: code);
            if (subjects.Exists(s => s.Code == code && s.Id != excludeId))
            {
                throw new Exception($"会计科目代码 '{code}' 已存在");
            }
        }
    }
}