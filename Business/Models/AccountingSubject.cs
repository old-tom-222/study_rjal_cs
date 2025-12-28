using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 会计科目类
    /// </summary>
    public class AccountingSubject
    {
        /// <summary>
        /// 科目ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 科目编码
        /// </summary>
        [Required(ErrorMessage = "科目编码不能为空")]
        [StringLength(20, ErrorMessage = "科目编码长度不能超过20个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 科目名称
        /// </summary>
        [Required(ErrorMessage = "科目名称不能为空")]
        [StringLength(100, ErrorMessage = "科目名称长度不能超过100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 科目类型
        /// </summary>
        [Required(ErrorMessage = "科目类型不能为空")]
        [StringLength(20, ErrorMessage = "科目类型长度不能超过20个字符")]
        public string Type { get; set; }

        /// <summary>
        /// 父科目ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 父科目
        /// </summary>
        public AccountingSubject Parent { get; set; }
    }
}