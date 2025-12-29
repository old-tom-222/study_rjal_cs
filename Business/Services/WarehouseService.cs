using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class WarehouseService
    {
        private readonly WarehouseRepository _warehouseRepo = new WarehouseRepository();

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        public List<Warehouse> GetWarehouses(string warehouseName = null, string status = null)
        {
            return _warehouseRepo.GetWarehouses(warehouseName, status);
        }

        /// <summary>
        /// 获取所有仓库列表（用于下拉框）
        /// </summary>
        public List<Warehouse> GetAllWarehouses()
        {
            return _warehouseRepo.GetAllWarehouses();
        }

        /// <summary>
        /// 获取仓库详情
        /// </summary>
        public Warehouse GetWarehouseById(int warehouseId)
        {
            return _warehouseRepo.GetWarehouseById(warehouseId);
        }

        /// <summary>
        /// 获取活跃仓库列表
        /// </summary>
        public List<Warehouse> GetActiveWarehouses()
        {
            return _warehouseRepo.GetActiveWarehouses();
        }

        /// <summary>
        /// 创建仓库
        /// </summary>
        public int CreateWarehouse(Warehouse warehouse)
        {
            try
            {
                // 业务逻辑验证
                if (string.IsNullOrWhiteSpace(warehouse.WarehouseName))
                {
                    throw new ArgumentException("仓库名称不能为空");
                }

                // 设置默认状态
                if (string.IsNullOrWhiteSpace(warehouse.Status))
                {
                    warehouse.Status = "1"; // 活跃
                }

                // 设置日期
                warehouse.CreatedDate = DateTime.Now;
                warehouse.LastUpdated = DateTime.Now;

                // 调用数据访问层创建仓库
                return _warehouseRepo.CreateWarehouse(warehouse);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("创建仓库失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 更新仓库
        /// </summary>
        public bool UpdateWarehouse(Warehouse warehouse)
        {
            try
            {
                // 业务逻辑验证
                if (warehouse.WarehouseId <= 0)
                {
                    throw new ArgumentException("仓库ID无效");
                }

                if (string.IsNullOrWhiteSpace(warehouse.WarehouseName))
                {
                    throw new ArgumentException("仓库名称不能为空");
                }

                // 检查仓库是否存在
                var existingWarehouse = _warehouseRepo.GetWarehouseById(warehouse.WarehouseId);
                if (existingWarehouse == null)
                {
                    throw new ArgumentException("仓库不存在");
                }

                // 更新最后修改日期
                warehouse.LastUpdated = DateTime.Now;

                // 调用数据访问层更新仓库
                return _warehouseRepo.UpdateWarehouse(warehouse);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("更新仓库失败: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        public bool DeleteWarehouse(int warehouseId)
        {
            try
            {
                // 业务逻辑验证
                if (warehouseId <= 0)
                {
                    throw new ArgumentException("仓库ID无效");
                }

                // 检查仓库是否存在
                var warehouse = _warehouseRepo.GetWarehouseById(warehouseId);
                if (warehouse == null)
                {
                    throw new ArgumentException("仓库不存在");
                }

                // 这里可以添加检查仓库是否已被库存或订单使用的逻辑

                // 调用数据访问层删除仓库
                return _warehouseRepo.DeleteWarehouse(warehouseId);
            }
            catch (Exception ex)
            {
                // 记录错误并重新抛出
                throw new Exception("删除仓库失败: " + ex.Message, ex);
            }
        }
    }
}