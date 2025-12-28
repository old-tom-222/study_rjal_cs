using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService()
        {
            _departmentRepository = new DepartmentRepository();
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        public List<Department> GetAllDepartments(string code = null, string name = null, bool? isActive = null)
        {
            try
            {
                return _departmentRepository.GetDepartments(code, name, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取部门列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取部门
        /// </summary>
        public Department GetDepartmentById(int id)
        {
            try
            {
                var department = _departmentRepository.GetDepartmentById(id);
                if (department == null)
                {
                    throw new Exception($"ID为 {id} 的部门不存在");
                }
                return department;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取部门失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public int CreateDepartment(Department department)
        {
            try
            {
                // 验证必要字段
                ValidateDepartmentRequiredFields(department);

                // 验证部门代码是否唯一
                ValidateDepartmentCodeUnique(department.Code, null);

                // 设置默认值
                if (department.CreatedAt == DateTime.MinValue)
                {
                    department.CreatedAt = DateTime.Now;
                }

                return _departmentRepository.CreateDepartment(department);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建部门失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public bool UpdateDepartment(Department department)
        {
            try
            {
                // 验证部门是否存在
                var existingDepartment = _departmentRepository.GetDepartmentById(department.Id);
                if (existingDepartment == null)
                {
                    throw new Exception($"ID为 {department.Id} 的部门不存在");
                }

                // 验证必要字段
                ValidateDepartmentRequiredFields(department);

                // 验证部门代码是否唯一（排除当前部门）
                ValidateDepartmentCodeUnique(department.Code, department.Id);

                // 设置更新时间
                department.UpdatedAt = DateTime.Now;

                return _departmentRepository.UpdateDepartment(department);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新部门失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public bool DeleteDepartment(int id)
        {
            try
            {
                // 验证部门是否存在
                var existingDepartment = _departmentRepository.GetDepartmentById(id);
                if (existingDepartment == null)
                {
                    throw new Exception($"ID为 {id} 的部门不存在");
                }

                return _departmentRepository.DeleteDepartment(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除部门失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证部门必要字段
        /// </summary>
        private void ValidateDepartmentRequiredFields(Department department)
        {
            if (string.IsNullOrEmpty(department.Code))
            {
                throw new Exception("部门代码不能为空");
            }

            if (string.IsNullOrEmpty(department.Name))
            {
                throw new Exception("部门名称不能为空");
            }
        }

        /// <summary>
        /// 验证部门代码是否唯一
        /// </summary>
        private void ValidateDepartmentCodeUnique(string code, int? excludeId)
        {
            var departments = _departmentRepository.GetDepartments(code: code);
            if (departments.Exists(d => d.Code == code && d.Id != excludeId))
            {
                throw new Exception($"部门代码 '{code}' 已存在");
            }
        }
    }
}