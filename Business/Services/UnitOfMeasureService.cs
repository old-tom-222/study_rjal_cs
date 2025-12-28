using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class UnitOfMeasureService
    {
        private readonly UnitOfMeasureRepository _unitOfMeasureRepository;

        public UnitOfMeasureService()
        {
            _unitOfMeasureRepository = new UnitOfMeasureRepository();
        }

        /// <summary>
        /// 获取所有计量单位
        /// </summary>
        public List<UnitOfMeasure> GetAllUnitOfMeasures(string code = null, string name = null, bool? isActive = null)
        {
            try
            {
                return _unitOfMeasureRepository.GetUnitOfMeasures(code, name, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取计量单位列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取计量单位
        /// </summary>
        public UnitOfMeasure GetUnitOfMeasureById(int id)
        {
            try
            {
                var unitOfMeasure = _unitOfMeasureRepository.GetUnitOfMeasureById(id);
                if (unitOfMeasure == null)
                {
                    throw new Exception($"ID为 {id} 的计量单位不存在");
                }
                return unitOfMeasure;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取计量单位失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建计量单位
        /// </summary>
        public int CreateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            try
            {
                // 验证必要字段
                ValidateUnitOfMeasureRequiredFields(unitOfMeasure);

                // 验证计量单位代码是否唯一
                ValidateUnitOfMeasureCodeUnique(unitOfMeasure.Code, null);

                // 设置默认值
                if (unitOfMeasure.CreatedAt == DateTime.MinValue)
                {
                    unitOfMeasure.CreatedAt = DateTime.Now;
                }

                return _unitOfMeasureRepository.CreateUnitOfMeasure(unitOfMeasure);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建计量单位失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新计量单位
        /// </summary>
        public bool UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            try
            {
                // 验证计量单位是否存在
                var existingUnitOfMeasure = _unitOfMeasureRepository.GetUnitOfMeasureById(unitOfMeasure.Id);
                if (existingUnitOfMeasure == null)
                {
                    throw new Exception($"ID为 {unitOfMeasure.Id} 的计量单位不存在");
                }

                // 验证必要字段
                ValidateUnitOfMeasureRequiredFields(unitOfMeasure);

                // 验证计量单位代码是否唯一（排除当前计量单位）
                ValidateUnitOfMeasureCodeUnique(unitOfMeasure.Code, unitOfMeasure.Id);

                // 设置更新时间
                unitOfMeasure.UpdatedAt = DateTime.Now;

                return _unitOfMeasureRepository.UpdateUnitOfMeasure(unitOfMeasure);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新计量单位失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除计量单位
        /// </summary>
        public bool DeleteUnitOfMeasure(int id)
        {
            try
            {
                // 验证计量单位是否存在
                var existingUnitOfMeasure = _unitOfMeasureRepository.GetUnitOfMeasureById(id);
                if (existingUnitOfMeasure == null)
                {
                    throw new Exception($"ID为 {id} 的计量单位不存在");
                }

                return _unitOfMeasureRepository.DeleteUnitOfMeasure(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除计量单位失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证计量单位必要字段
        /// </summary>
        private void ValidateUnitOfMeasureRequiredFields(UnitOfMeasure unitOfMeasure)
        {
            if (string.IsNullOrEmpty(unitOfMeasure.Code))
            {
                throw new Exception("计量单位代码不能为空");
            }

            if (string.IsNullOrEmpty(unitOfMeasure.Name))
            {
                throw new Exception("计量单位名称不能为空");
            }

            if (string.IsNullOrEmpty(unitOfMeasure.Symbol))
            {
                throw new Exception("计量单位符号不能为空");
            }
        }

        /// <summary>
        /// 验证计量单位代码是否唯一
        /// </summary>
        private void ValidateUnitOfMeasureCodeUnique(string code, int? excludeId)
        {
            var unitsOfMeasure = _unitOfMeasureRepository.GetUnitOfMeasures(code: code);
            if (unitsOfMeasure.Exists(u => u.Code == code && u.Id != excludeId))
            {
                throw new Exception($"计量单位代码 '{code}' 已存在");
            }
        }
    }
}