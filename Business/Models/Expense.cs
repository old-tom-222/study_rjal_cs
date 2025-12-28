using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 支出类
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// 支出ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 支出编号
        /// </summary>
        [Required(ErrorMessage = "支出编号不能为空")]
        [StringLength(50, ErrorMessage = "支出编号长度不能超过50个字符")]
        public string ExpenseNo { get; set; }

        /// <summary>
        /// 科目ID
        /// </summary>
        [Required(ErrorMessage = "科目ID不能为空")]
        public int SubjectId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Required(ErrorMessage = "金额不能为空")]
        [Range(0.01, 999999999.99, ErrorMessage = "金额必须大于0")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 支出日期
        /// </summary>
        [Required(ErrorMessage = "支出日期不能为空")]
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// 支出类别
        /// </summary>
        [Required(ErrorMessage = "支出类别不能为空")]
        [StringLength(100, ErrorMessage = "支出类别长度不能超过100个字符")]
        public string Category { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参考
        /// </summary>
        [StringLength(100, ErrorMessage = "参考长度不能超过100个字符")]
        public string Reference { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        [Required(ErrorMessage = "创建人ID不能为空")]
        public int CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        public AccountingSubject Subject { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public User Creator { get; set; }
    }
}