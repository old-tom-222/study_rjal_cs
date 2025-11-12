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

        // 库存调整（增减）并记录流水
        public InventoryItem AdjustInventory(int productId, int warehouseId, int deltaQty, string type = "adjust", string reference = null, string remark = null)
        {
            var newQty = _inventoryRepo.AdjustInventory(productId, warehouseId, deltaQty);
            _txnRepo.AddTransaction(productId, warehouseId, deltaQty, type, reference, remark);
            return _inventoryRepo.GetInventory(productId, warehouseId);
        }

        // 设置库存（覆盖为指定数量）并记录流水（记录差值）
        public InventoryItem SetInventory(int productId, int warehouseId, int newQty, string reference = null, string remark = null)
        {
            var current = _inventoryRepo.GetInventory(productId, warehouseId);
            var delta = (current == null ? newQty : (newQty - current.Quantity));
            _inventoryRepo.SetInventory(productId, warehouseId, newQty);
            _txnRepo.AddTransaction(productId, warehouseId, delta, "set", reference, remark);
            return _inventoryRepo.GetInventory(productId, warehouseId);
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

        // 批次/串号管理：占位方法（基于现有表结构后续扩展）
        public void ReceiveByBatch(int productId, int warehouseId, string batchNo, int qty, DateTime? mfgDate = null, DateTime? expDate = null, string reference = null)
        {
            // TODO: 结合批次表结构扩展具体实现。
            AdjustInventory(productId, warehouseId, qty, type: "batch_receive", reference: reference, remark: $"batch:{batchNo}");
        }

        public void IssueBySerial(int productId, int warehouseId, string serialNo, string reference = null)
        {
            // TODO: 结合串号表结构扩展具体实现。
            AdjustInventory(productId, warehouseId, -1, type: "serial_issue", reference: reference, remark: $"serial:{serialNo}");
        }
    }
}