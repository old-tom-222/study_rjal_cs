using System;
using System.ComponentModel.DataAnnotations;

namespace CSproject.Business.Models
{
    /// <summary>
    /// 计量单位类
    /// </summary>
    public class UnitOfMeasure
    {
        /// <summary>
        /// 计量单位ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 计量单位名称
        /// </summary>
        [Required(ErrorMessage = "计量单位名称不能为空")]
        [StringLength(50, ErrorMessage = "计量单位名称长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 计量单位编码
        /// </summary>
        [Required(ErrorMessage = "计量单位编码不能为空")]
        [StringLength(20, ErrorMessage = "计量单位编码长度不能超过20个字符")]
        public string Code { get; set; }

        /// <summary>
        /// 基本单位ID
        /// </summary>
        public int? BaseUnitId { get; set; }

        /// <summary>
        /// 转换率
        /// </summary>
        public decimal ConversionRate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public UnitOfMeasure BaseUnit { get; set; }
    }
}