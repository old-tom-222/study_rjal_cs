using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 部门类
    /// </summary>
    public class Department
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称不能为空")]
        [StringLength(100, ErrorMessage = "部门名称长度不能超过100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [Required(ErrorMessage = "部门编码不能为空")]
        [StringLength(20, ErrorMessage = "部门编码长度不能超过20个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 父部门ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 部门经理ID
        /// </summary>
        public int? ManagerId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 父部门
        /// </summary>
        public Department Parent { get; set; }

        /// <summary>
        /// 部门经理
        /// </summary>
        public Employee Manager { get; set; }
    }
}