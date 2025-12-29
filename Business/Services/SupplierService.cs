using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class SupplierService
    {
        private readonly SupplierRepository _supplierRepo = new SupplierRepository();

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        public List<Supplier> GetSuppliers(string supplierName = null, string contactPerson = null, string status = null)
        {
            return _supplierRepo.GetSuppliers(supplierName, contactPerson, status);
        }

        /// <summary>
        /// 获取所有供应商列表（用于下拉框）
        /// </summary>
        public List<Supplier> GetAllSuppliers()
        {
            return _supplierRepo.GetAllSuppliers();
        }

        /// <summary>
        /// 获取供应商详情
        /// </summary>
        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepo.GetSupplierById(supplierId);
        }

        /// <summary>
        /// 获取活跃供应商列表
        /// </summary>
        public List<Supplier> GetActiveSuppliers()
        {
            return _supplierRepo.GetActiveSuppliers();
        }

        /// <summary>
        /// 创建供应商
        /// </summary>
        public int CreateSupplier(Supplier supplier)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                {
                    throw new ArgumentException("供应商名称不能为空");
                }

                // 设置默认状态
                if (string.IsNullOrWhiteSpace(supplier.Status))
                {
                    supplier.Status = "1"; // 活跃
                }

                // 设置日期
                supplier.CreatedDate = DateTime.Now;
                supplier.LastUpdated = DateTime.Now;

                // 调用数据访问层创建供应商
                return _supplierRepo.CreateSupplier(supplier);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("创建供应商失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新供应商
        /// </summary>
        public bool UpdateSupplier(Supplier supplier)
        {
            try
            {
                // 业务逻辑验证
                if (supplier.SupplierId <= 0)
                {
                    throw new ArgumentException("供应商ID无效");
                }

                if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                {
                    throw new ArgumentException("供应商名称不能为空");
                }

                // 检查供应商是否存在
                var existingSupplier = _supplierRepo.GetSupplierById(supplier.SupplierId);
                if (existingSupplier == null)
                {
                    throw new ArgumentException("供应商不存在");
                }

                // 更新最后修改日期
                supplier.LastUpdated = DateTime.Now;

                // 调用数据访问层更新供应商
                return _supplierRepo.UpdateSupplier(supplier);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("更新供应商失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除供应商
        /// </summary>
        public bool DeleteSupplier(int supplierId)
        {
            try
            {
                // 业务逻辑验证
                if (supplierId <= 0)
                {
                    throw new ArgumentException("供应商ID无效");
                }

                // 检查供应商是否存在
                var supplier = _supplierRepo.GetSupplierById(supplierId);
                if (supplier == null)
                {
                    throw new ArgumentException("供应商不存在");
                }

                // 这里可以添加检查供应商是否已被采购订单使用的逻辑

                // 调用数据访问层删除供应商
                return _supplierRepo.DeleteSupplier(supplierId);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("删除供应商失败: " + ex.Message, ex);
            }
        }
    }
}