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
            // 获取库存概览数据作为基础
            var inventoryOverview = GetInventoryOverview(warehouseId);
            var result = new List<InventoryReportModel>();
            
            // 获取指定时间段内的所有库存交易
            var allTransactions = _transactionRepo.GetTransactions(null, warehouseId, startDate, endDate);
            
            // 获取时间段之前的所有库存交易（用于计算期初库存）
            var beforeTransactions = _transactionRepo.GetTransactions(null, warehouseId, null, startDate.AddDays(-1));
            
            // 对每个库存项计算周转率相关数据
            foreach (var item in inventoryOverview)
            {
                var turnoverItem = new InventoryReportModel
                {
                    ProductId = item.ProductId,
                    ProductSku = item.ProductSku,
                    ProductName = item.ProductName,
                    WarehouseId = item.WarehouseId,
                    WarehouseName = item.WarehouseName,
                    CurrentQuantity = item.CurrentQuantity,
                    SafeStock = item.SafeStock,
                    ReorderQuantity = item.ReorderQuantity,
                    AverageCost = item.AverageCost,
                    TotalValue = item.TotalValue
                };
                
                // 计算期初库存：当前库存 - 时间段内的净变动
                int periodNetChange = allTransactions
                    .Where(t => t.ProductId == item.ProductId && t.WarehouseId == item.WarehouseId)
                    .Sum(t => t.ChangeQty);
                turnoverItem.OpeningStock = item.CurrentQuantity - periodNetChange;
                
                // 期末库存即为当前库存
                turnoverItem.ClosingStock = item.CurrentQuantity;
                
                // 计算平均库存
                turnoverItem.AverageStock = (turnoverItem.OpeningStock + turnoverItem.ClosingStock) / 2m;
                
                // 计算销售数量（筛选销售类型的交易，取绝对值之和）
                turnoverItem.SalesQuantity = Math.Abs(allTransactions
                    .Where(t => t.ProductId == item.ProductId && t.WarehouseId == item.WarehouseId && t.Type == "sale")
                    .Sum(t => t.ChangeQty));
                
                // 计算周转率
                if (turnoverItem.AverageStock > 0)
                {
                    turnoverItem.TurnoverRate = turnoverItem.SalesQuantity / turnoverItem.AverageStock;
                }
                else
                {
                    turnoverItem.TurnoverRate = 0;
                }
                
                result.Add(turnoverItem);
            }
            
            return result;
        }
        
        /// <summary>
        /// 获取库存流水报表
        /// </summary>
        public List<InventoryTransaction> GetInventoryTransactions(int? productId = null, int? warehouseId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _transactionRepo.GetTransactions(productId, warehouseId, startDate, endDate);
        }
    }
}