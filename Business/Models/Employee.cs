using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 员工类
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        [Required(ErrorMessage = "员工编号不能为空")]
        [StringLength(50, ErrorMessage = "员工编号长度不能超过50个字符")]
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Required(ErrorMessage = "员工姓名不能为空")]
        [StringLength(50, ErrorMessage = "员工姓名长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Required(ErrorMessage = "部门ID不能为空")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        [StringLength(100, ErrorMessage = "职位长度不能超过100个字符")]
        public string Position { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(10, ErrorMessage = "性别长度不能超过10个字符")]
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(200, ErrorMessage = "地址长度不能超过200个字符")]
        public string Address { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public Department Department { get; set; }
    }
}