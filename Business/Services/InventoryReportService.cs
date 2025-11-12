using System;
using System.Collections.Generic;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class InventoryReportService
    {
        private readonly InventoryRepository _inventoryRepo;
        private readonly InventoryTransactionRepository _transactionRepo;

        public InventoryReportService()
        {
            _inventoryRepo = new InventoryRepository();
            _transactionRepo = new InventoryTransactionRepository();
        }

        /// <summary>
        /// 获取库存概览报表
        /// </summary>
        public List<InventoryReportModel> GetInventoryOverview(int? warehouseId = null)
        {
            var inventoryItems = _inventoryRepo.GetInventoryList(warehouseId: warehouseId);
            var reportItems = new List<InventoryReportModel>();

            foreach (var item in inventoryItems)
            {
                var reportItem = new InventoryReportModel
                {
                    ProductId = item.ProductId,
                    ProductSku = item.ProductSku,
                    ProductName = item.ProductName,
                    CurrentQuantity = item.Quantity,
                    SafeStock = item.SafeStock,
                    ReorderQuantity = (int)(item.SafeStock * 1.5), // 默认重订点为安全库存的1.5倍
                    WarehouseId = item.WarehouseId,
                    WarehouseName = item.WarehouseName,
                    // 其他属性可以从其他数据源获取或计算
                    MinQuantity = 0, // 实际应用中可以查询历史最低库存
                    MaxQuantity = item.Quantity * 2, // 实际应用中可以查询历史最高库存
                    AverageCost = 0, // 实际应用中需要从成本数据获取
                    LastStockMovementDate = DateTime.Now, // 实际应用中需要查询最近交易日期
                };
                reportItem.TotalValue = reportItem.AverageCost * reportItem.CurrentQuantity;

                reportItems.Add(reportItem);
            }

            return reportItems;
        }

        /// <summary>
        /// 获取低库存预警报表
        /// </summary>
        public List<InventoryReportModel> GetLowStockReport(int? warehouseId = null)
        {
            var overview = GetInventoryOverview(warehouseId);
            return overview.FindAll(item => item.IsLowStock);
        }

        /// <summary>
        /// 获取低库存警告列表
        /// </summary>
        public List<InventoryReportModel> GetLowStockWarnings(int? warehouseId = null)
        {
            // 与低库存报表逻辑类似，用于获取需要警告的低库存商品
            var overview = GetInventoryOverview(warehouseId);
            return overview.FindAll(item => item.IsLowStock);
        }

        /// <summary>
        /// 获取库存周转率报表（按时间段）
        /// </summary>
        public List<InventoryReportModel> GetInventoryTurnoverReport(DateTime startDate, DateTime endDate, int? warehouseId = null)
        {
            // 这里可以实现更复杂的库存周转率计算逻辑
            // 目前返回库存概览，实际应用中需要计算周转次数、周转天数等
            return GetInventoryOverview(warehouseId);
        }
    }
}