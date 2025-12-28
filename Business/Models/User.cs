using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        [StringLength(50, ErrorMessage = "账号长度不能超过50个字符")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        [StringLength(50, ErrorMessage = "姓名长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required(ErrorMessage = "角色不能为空")]
        [StringLength(20, ErrorMessage = "角色长度不能超过20个字符")]
        public string Role { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}