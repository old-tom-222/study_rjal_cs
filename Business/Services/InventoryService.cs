using System;
using System.Collections.Generic;
using System.Linq;
using CSproject.Business.Models;
using CSproject.Data.Repositories;

namespace CSproject.Business.Services
{
    public class InventoryService
    {
        private readonly InventoryRepository _inventoryRepo = new InventoryRepository();
        private readonly InventoryTransactionRepository _txnRepo = new InventoryTransactionRepository();

        // 库存查询
        public List<InventoryItem> QueryInventory(int? productId = null, int? warehouseId = null)
        {
            return _inventoryRepo.GetInventoryList(productId, warehouseId);
        }
        
        // 为销售订单界面提供库存模型数据
        public List<InventoryModel> GetInventoryModels(int? productId = null, int? warehouseId = null, int? categoryId = null)
        {
            var inventoryItems = _inventoryRepo.GetInventoryList(productId, warehouseId);
            return inventoryItems.Select(item => new InventoryModel
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                WarehouseId = item.WarehouseId,
                WarehouseName = item.WarehouseName
            }).ToList();
        }



        // 库存流水查询
        public List<InventoryTransaction> QueryTransactions(int? productId = null, int? warehouseId = null, DateTime? from = null, DateTime? to = null)
        {
            return _txnRepo.GetTransactions(productId, warehouseId, from, to);
        }

        // 库存预警：返回低于安全库存的条目
        public List<InventoryItem> GetLowStockWarnings()
        {
            return _inventoryRepo.GetLowStockWarnings();
        }
        
        /// <summary>
        /// 减少库存数量并记录交易流水
        /// </summary>
        public bool ReduceInventory(int productId, int warehouseId, int quantity, string reference, string remark = null)
        {
            // 先检查库存是否充足
            var inventory = _inventoryRepo.GetInventory(productId, warehouseId);
            if (inventory == null || inventory.Quantity < quantity)
            {
                return false;
            }
            
            // 减少库存
            bool reduceSuccess = _inventoryRepo.ReduceInventory(productId, warehouseId, quantity);
            if (reduceSuccess)
            {
                // 记录交易流水（销售出库，数量为负数）
                _txnRepo.AddTransaction(productId, warehouseId, -quantity, "sales", reference, remark ?? "销售出库");
            }
            
            return reduceSuccess;
        }

    }
}