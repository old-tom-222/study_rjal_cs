using System;
using System.Collections.Generic;
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


    }
}