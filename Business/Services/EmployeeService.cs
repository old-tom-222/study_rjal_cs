using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _departmentRepository = new DepartmentRepository();
        }

        /// <summary>
        /// 获取所有员工
        /// </summary>
        public List<Employee> GetAllEmployees(string code = null, string name = null, int? departmentId = null, bool? isActive = null)
        {
            try
            {
                return _employeeRepository.GetEmployees(code, name, departmentId, isActive);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取员工列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取员工
        /// </summary>
        public Employee GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                if (employee == null)
                {
                    throw new Exception($"ID为 {id} 的员工不存在");
                }
                return employee;
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"获取员工失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 创建员工
        /// </summary>
        public int CreateEmployee(Employee employee)
        {
            try
            {
                // 验证必要字段
                ValidateEmployeeRequiredFields(employee);

                // 验证员工编号是否唯一
                ValidateEmployeeCodeUnique(employee.Code, null);

                // 验证部门是否存在
                ValidateDepartmentExists(employee.DepartmentId);

                // 设置默认值
                if (employee.CreatedAt == DateTime.MinValue)
                {
                    employee.CreatedAt = DateTime.Now;
                }

                return _employeeRepository.CreateEmployee(employee);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"创建员工失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 更新员工
        /// </summary>
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                // 验证员工是否存在
                var existingEmployee = _employeeRepository.GetEmployeeById(employee.Id);
                if (existingEmployee == null)
                {
                    throw new Exception($"ID为 {employee.Id} 的员工不存在");
                }

                // 验证必要字段
                ValidateEmployeeRequiredFields(employee);

                // 验证员工编号是否唯一（排除当前员工）
                ValidateEmployeeCodeUnique(employee.Code, employee.Id);

                // 验证部门是否存在
                ValidateDepartmentExists(employee.DepartmentId);

                // 设置更新时间
                employee.UpdatedAt = DateTime.Now;

                return _employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"更新员工失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        public bool DeleteEmployee(int id)
        {
            try
            {
                // 验证员工是否存在
                var existingEmployee = _employeeRepository.GetEmployeeById(id);
                if (existingEmployee == null)
                {
                    throw new Exception($"ID为 {id} 的员工不存在");
                }

                return _employeeRepository.DeleteEmployee(id);
            }
            catch (Exception ex)
            {
                // 可以在这里添加日志记录
                throw new Exception($"删除员工失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 验证员工必要字段
        /// </summary>
        private void ValidateEmployeeRequiredFields(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Code))
            {
                throw new Exception("员工编号不能为空");
            }

            if (string.IsNullOrEmpty(employee.Name))
            {
                throw new Exception("员工姓名不能为空");
            }

            if (employee.DepartmentId <= 0)
            {
                throw new Exception("部门ID不能为空");
            }
        }

        /// <summary>
        /// 验证员工编号是否唯一
        /// </summary>
        private void ValidateEmployeeCodeUnique(string code, int? excludeId)
        {
            var employees = _employeeRepository.GetEmployees(code: code);
            if (employees.Exists(e => e.Code == code && e.Id != excludeId))
            {
                throw new Exception($"员工编号 '{code}' 已存在");
            }
        }

        /// <summary>
        /// 验证部门是否存在
        /// </summary>
        private void ValidateDepartmentExists(int departmentId)
        {
            var department = _departmentRepository.GetDepartmentById(departmentId);
            if (department == null)
            {
                throw new Exception($"ID为 {departmentId} 的部门不存在");
            }
        }
    }
}