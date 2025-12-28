using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 银行账户类
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        [Required(ErrorMessage = "银行名称不能为空")]
        [StringLength(100, ErrorMessage = "银行名称长度不能超过100个字符")]
        public string BankName { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        [Required(ErrorMessage = "账户名称不能为空")]
        [StringLength(100, ErrorMessage = "账户名称长度不能超过100个字符")]
        public string AccountName { get; set; }

        /// <summary>
        /// 账户号码
        /// </summary>
        [Required(ErrorMessage = "账户号码不能为空")]
        [StringLength(50, ErrorMessage = "账户号码长度不能超过50个字符")]
        public string AccountNumber { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}