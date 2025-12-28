using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 仓库类型类
    /// </summary>
    public class WarehouseType
    {
        /// <summary>
        /// 类型ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required(ErrorMessage = "类型名称不能为空")]
        [StringLength(100, ErrorMessage = "类型名称长度不能超过100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 类型编码
        /// </summary>
        [Required(ErrorMessage = "类型编码不能为空")]
        [StringLength(20, ErrorMessage = "类型编码长度不能超过20个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

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