using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Data.Repositories;

namespace CSproject.Data.Repositories
{
    public class PurchaseOrderRepository
    {
        private readonly InventoryRepository _inventoryRepo;
        
        public PurchaseOrderRepository()
        {
            _inventoryRepo = new InventoryRepository();
        }
        /// <summary>
        /// 获取所有采购订单
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllPurchaseOrders()
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT po.id, po.order_no, s.name as supplier_name, w.name as warehouse_name, 
                                 po.total_amount, po.status, po.created_at, u.name as purchaser_name
                                 FROM purchase_order po
                                 JOIN supplier s ON po.supplier_id = s.id
                                 JOIN warehouse w ON po.warehouse_id = w.id
                                 JOIN user u ON po.created_by = u.id
                                 ORDER BY po.created_at DESC";
                
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 根据ID获取采购订单
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataTable GetPurchaseOrderById(int orderId)
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT po.id, po.order_no, s.name as supplier_name, w.name as warehouse_name, 
                                 po.total_amount, po.status, po.created_at, u.name as purchaser_name
                                 FROM purchase_order po
                                 JOIN supplier s ON po.supplier_id = s.id
                                 JOIN warehouse w ON po.warehouse_id = w.id
                                 JOIN user u ON po.created_by = u.id
                                 WHERE po.id = @orderId";
                
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@orderId", orderId);
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 获取采购订单详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable GetPurchaseOrderItems(int orderId)
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"SELECT poi.id, p.name as product_name, poi.quantity, poi.unit_price, 
                                 (poi.quantity * poi.unit_price) as total_price 
                                 FROM purchase_order_item poi
                                 JOIN product p ON poi.product_id = p.id
                                 WHERE poi.order_id = @orderId";
                
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@orderId", orderId);
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 创建采购订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="supplierId"></param>
        /// <param name="warehouseId"></param>
        /// <param name="createdById"></param>
        /// <returns></returns>
        public int CreatePurchaseOrder(string orderNo, int supplierId, int warehouseId, int purchaserId)
        {
            // 调用重载方法，传入空字符串作为名称（将从Form2中传入正确的参数）
            return CreatePurchaseOrder(orderNo, supplierId, "", warehouseId, "", purchaserId);
        }
        
        /// <summary>
        /// 创建采购订单（支持用户输入的供应商和仓库名称）
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierName"></param>
        /// <param name="warehouseId"></param>
        /// <param name="warehouseName"></param>
        /// <param name="createdById"></param>
        /// <returns></returns>
        public int CreatePurchaseOrder(string orderNo, int supplierId, string supplierName, int warehouseId, string warehouseName, int purchaserId)
        {
            string connectionString = DbHelper.GetConnectionString();
            int orderId = 0;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 如果supplierId为-1且提供了supplierName，查找或创建供应商
                        if (supplierId == -1 && !string.IsNullOrEmpty(supplierName))
                        {
                            supplierId = FindOrCreateSupplier(connection, transaction, supplierName);
                        }
                        
                        // 如果warehouseId为-1且提供了warehouseName，查找或创建仓库
                        if (warehouseId == -1 && !string.IsNullOrEmpty(warehouseName))
                        {
                            warehouseId = FindOrCreateWarehouse(connection, transaction, warehouseName);
                        }
                        
                        string query = "INSERT INTO purchase_order (order_no, supplier_id, warehouse_id, created_by) VALUES (@orderNo, @supplierId, @warehouseId, @createdBy)";
                        using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderNo", orderNo);
                            command.Parameters.AddWithValue("@supplierId", supplierId);
                            command.Parameters.AddWithValue("@warehouseId", warehouseId);
                            command.Parameters.AddWithValue("@createdBy", purchaserId);
                            
                            command.ExecuteNonQuery();
                            orderId = Convert.ToInt32(command.LastInsertedId);
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            return orderId;
        }
        
        /// <summary>
        /// 查找或创建供应商
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="supplierName"></param>
        /// <returns></returns>
        private int FindOrCreateSupplier(MySqlConnection connection, MySqlTransaction transaction, string supplierName)
        {
            // 先尝试查找供应商
            string findQuery = "SELECT id FROM supplier WHERE name = @name";
            using (MySqlCommand command = new MySqlCommand(findQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", supplierName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }
            
            // 如果不存在，创建新供应商（只使用name和status列）
            string createQuery = "INSERT INTO supplier (name, status) VALUES (@name, 1)";
            using (MySqlCommand command = new MySqlCommand(createQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", supplierName);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        /// <summary>
        /// 查找或创建产品
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        private int FindOrCreateProduct(MySqlConnection connection, MySqlTransaction transaction, string productName, decimal unitPrice)
        {
            // 先尝试查找产品
            string findQuery = "SELECT id FROM product WHERE name = @name";
            using (MySqlCommand command = new MySqlCommand(findQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", productName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    // 产品已存在，更新其成本价和售价
                    int productId = Convert.ToInt32(result);
                    string updateQuery = "UPDATE product SET cost_price = @unitPrice, sale_price = @unitPrice WHERE id = @productId";
                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection, transaction))
                    {
                        updateCmd.Parameters.AddWithValue("@unitPrice", unitPrice);
                        updateCmd.Parameters.AddWithValue("@productId", productId);
                        updateCmd.ExecuteNonQuery();
                    }
                    return productId;
                }
            }
            
            // 获取或创建默认分类
            int categoryId = FindOrCreateDefaultCategory(connection, transaction);
            
            // 生成唯一的SKU
            string sku = GenerateUniqueSKU(productName);
            
            // 如果不存在，创建新产品（填写所有必填字段，并使用传入的价格）
            string createQuery = "INSERT INTO product (sku, name, category_id, cost_price, sale_price, safe_stock, status) " +
                                 "VALUES (@sku, @name, @categoryId, @unitPrice, @unitPrice, @safeStock, @status)";
            using (MySqlCommand command = new MySqlCommand(createQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@sku", sku);
                command.Parameters.AddWithValue("@name", productName);
                command.Parameters.AddWithValue("@categoryId", categoryId);
                command.Parameters.AddWithValue("@unitPrice", unitPrice); // 使用传入的价格作为成本价和售价
                command.Parameters.AddWithValue("@safeStock", 10); // 默认安全库存
                command.Parameters.AddWithValue("@status", 1); // 默认启用状态
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }
        
        /// <summary>
        /// 查找或创建默认分类
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private int FindOrCreateDefaultCategory(MySqlConnection connection, MySqlTransaction transaction)
        {
            // 先尝试查找默认分类
            string findQuery = "SELECT id FROM product_category WHERE name = '默认分类'";
            using (MySqlCommand command = new MySqlCommand(findQuery, connection, transaction))
            {
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }
            
            // 如果不存在，创建默认分类，确保提供了所有必需的字段
            string createQuery = "INSERT INTO product_category (name, parent_id) VALUES ('默认分类', NULL)";
            using (MySqlCommand command = new MySqlCommand(createQuery, connection, transaction))
            {
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }
        
        /// <summary>
        /// 生成唯一的产品SKU
        /// </summary>
        /// <param name="productName">产品名称</param>
        /// <returns>唯一的SKU</returns>
        private string GenerateUniqueSKU(string productName)
        {
            // 取产品名称的前4个字母（去除空格并转为大写）
            string prefix = new string(productName
                .Where(c => !char.IsWhiteSpace(c))
                .Take(4)
                .ToArray())
                .ToUpper();
            
            // 如果前缀不足4个字符，用X补充
            if (prefix.Length < 4)
            {
                prefix = prefix.PadRight(4, 'X');
            }
            
            // 生成时间戳和随机数组合的唯一部分
            string timestamp = DateTime.Now.ToString("yyMMddHHmmss");
            Random random = new Random();
            string randomPart = random.Next(1000, 9999).ToString();
            
            // 组合成最终的SKU
            return string.Format("{0}-{1}-{2}", prefix, timestamp, randomPart);
        }

        /// <summary>
        /// 添加订单产品明细
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productName"></param>
        /// <param name="quantity"></param>
        /// <param name="unitPrice"></param>
        public void AddPurchaseOrderItem(int orderId, string productName, int quantity, decimal unitPrice)
        {
            string connectionString = DbHelper.GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 查找或创建产品（传入unitPrice参数）
                        int productId = FindOrCreateProduct(connection, transaction, productName, unitPrice);

                        // 添加订单产品明细
                        string query = "INSERT INTO purchase_order_item (order_id, product_id, quantity, unit_price) VALUES (@orderId, @productId, @quantity, @unitPrice)";
                        using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
                            command.Parameters.AddWithValue("@productId", productId);
                            command.Parameters.AddWithValue("@quantity", quantity);
                            command.Parameters.AddWithValue("@unitPrice", unitPrice);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("添加订单产品明细失败：" + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 删除订单产品明细
        /// </summary>
        /// <param name="orderId"></param>
        public void DeletePurchaseOrderItems(int orderId)
        {
            string connectionString = DbHelper.GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "DELETE FROM purchase_order_item WHERE order_id = @orderId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 更新订单总金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="totalAmount"></param>
        public void UpdatePurchaseOrderTotalAmount(int orderId, decimal totalAmount)
        {
            string connectionString = DbHelper.GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "UPDATE purchase_order SET total_amount = @totalAmount WHERE id = @orderId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@totalAmount", totalAmount);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        
        /// <summary>
        /// 查找或创建仓库
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="warehouseName"></param>
        /// <returns></returns>
        private int FindOrCreateWarehouse(MySqlConnection connection, MySqlTransaction transaction, string warehouseName)
        {
            // 先尝试查找仓库
            string findQuery = "SELECT id FROM warehouse WHERE name = @name";
            using (MySqlCommand command = new MySqlCommand(findQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", warehouseName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }
            
            // 如果不存在，创建新仓库（只使用name和status列）
            string createQuery = "INSERT INTO warehouse (name, status) VALUES (@name, 1)";
            using (MySqlCommand command = new MySqlCommand(createQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", warehouseName);
                command.ExecuteNonQuery();
                return Convert.ToInt32(command.LastInsertedId);
            }
        }
        
        /// <summary>
        /// 更新采购订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderNo"></param>
        /// <param name="supplierId"></param>
        /// <param name="supplierName"></param>
        /// <param name="warehouseId"></param>
        /// <param name="warehouseName"></param>
        /// <param name="status"></param>
        /// <param name="purchaserId"></param>
        /// <param name="updatedById"></param>
        public void UpdatePurchaseOrder(int orderId, string orderNo, int supplierId, string supplierName, int warehouseId, string warehouseName, string status, int purchaserId, int updatedById)
        {
            string connectionString = DbHelper.GetConnectionString();
            string previousStatus = string.Empty;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. 获取订单的当前状态
                        string getStatusQuery = "SELECT status FROM purchase_order WHERE id = @orderId";
                        using (MySqlCommand statusCmd = new MySqlCommand(getStatusQuery, connection, transaction))
                        {
                            statusCmd.Parameters.AddWithValue("@orderId", orderId);
                            object statusResult = statusCmd.ExecuteScalar();
                            previousStatus = statusResult != null ? statusResult.ToString() : string.Empty;
                        }
                        
                        // 2. 如果supplierId为-1且提供了supplierName，查找或创建供应商
                        if (supplierId == -1 && !string.IsNullOrEmpty(supplierName))
                        {
                            supplierId = FindOrCreateSupplier(connection, transaction, supplierName);
                        }
                        
                        // 3. 如果warehouseId为-1且提供了warehouseName，查找或创建仓库
                        if (warehouseId == -1 && !string.IsNullOrEmpty(warehouseName))
                        {
                            warehouseId = FindOrCreateWarehouse(connection, transaction, warehouseName);
                        }
                        
                        // 4. 更新订单信息
                        string query = "UPDATE purchase_order SET order_no = @orderNo, supplier_id = @supplierId, warehouse_id = @warehouseId, status = @status, created_by = @createdBy WHERE id = @orderId";
                        using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
                            command.Parameters.AddWithValue("@orderNo", orderNo);
                            command.Parameters.AddWithValue("@supplierId", supplierId);
                            command.Parameters.AddWithValue("@warehouseId", warehouseId);
                            command.Parameters.AddWithValue("@status", status);
                            command.Parameters.AddWithValue("@createdBy", purchaserId);
                            
                            command.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            
            // 5. 根据订单状态更新库存
            if (status == "已完成")
            {
                UpdateInventoryForCompletedOrder(orderId, warehouseId);
            }
            else if (status == "已取消")
            {
                // 订单取消，需要撤销该订单对库存的历史贡献
                string cancelConnectionString = DbHelper.GetConnectionString();
                using (MySqlConnection connection = new MySqlConnection(cancelConnectionString))
                {
                    connection.Open();
                    
                    // 获取该订单对库存的历史贡献
                    Dictionary<int, int> historicalContribution = new Dictionary<int, int>();
                    string getHistoricalContributionQuery = @"SELECT product_id, SUM(change_qty) as total_change 
                                                           FROM inventory_transaction 
                                                           WHERE type = '采购入库' 
                                                           AND reference LIKE CONCAT('%', @orderId, '%') 
                                                           GROUP BY product_id";
                    
                    using (MySqlCommand historicalCmd = new MySqlCommand(getHistoricalContributionQuery, connection))
                    {
                        historicalCmd.Parameters.AddWithValue("@orderId", orderId);
                        
                        using (MySqlDataReader historicalReader = historicalCmd.ExecuteReader())
                        {
                            while (historicalReader.Read())
                            {
                                int productId = Convert.ToInt32(historicalReader["product_id"]);
                                int totalChange = Convert.ToInt32(historicalReader["total_change"]);
                                historicalContribution.Add(productId, totalChange);
                            }
                        }
                    }
                    
                    // 撤销该订单对库存的历史贡献
                    foreach (var contribution in historicalContribution)
                    {
                        _inventoryRepo.UpdateInventory(contribution.Key, warehouseId, -contribution.Value);
                    }
                }
            }
        }
        
        /// <summary>
        /// 为已完成的采购订单更新库存
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="warehouseId">仓库ID</param>
        private void UpdateInventoryForCompletedOrder(int orderId, int warehouseId)
        {
            string connectionString = DbHelper.GetConnectionString();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                
                // 1. 获取订单的所有产品明细
                string getItemsQuery = @"SELECT product_id, quantity 
                                        FROM purchase_order_item 
                                        WHERE order_id = @orderId";
                
                using (MySqlCommand itemsCmd = new MySqlCommand(getItemsQuery, connection))
                {
                    itemsCmd.Parameters.AddWithValue("@orderId", orderId);
                    
                    using (MySqlDataReader reader = itemsCmd.ExecuteReader())
                    {
                        // 临时存储产品ID和数量，用于后续处理
                        List<Tuple<int, int>> orderItems = new List<Tuple<int, int>>();
                        
                        while (reader.Read())
                        {
                            int productId = Convert.ToInt32(reader["product_id"]);
                            int quantity = Convert.ToInt32(reader["quantity"]);
                            orderItems.Add(new Tuple<int, int>(productId, quantity));
                        }
                        
                        reader.Close();
                        
                        // 2. 获取该订单对库存的历史贡献（用于撤销）
                        Dictionary<int, int> historicalContribution = new Dictionary<int, int>();
                        string getHistoricalContributionQuery = @"SELECT product_id, SUM(change_qty) as total_change 
                                                               FROM inventory_transaction 
                                                               WHERE type = '采购入库' 
                                                               AND reference LIKE CONCAT('%', @orderId, '%') 
                                                               GROUP BY product_id";
                        
                        using (MySqlCommand historicalCmd = new MySqlCommand(getHistoricalContributionQuery, connection))
                        {
                            historicalCmd.Parameters.AddWithValue("@orderId", orderId);
                            
                            using (MySqlDataReader historicalReader = historicalCmd.ExecuteReader())
                            {
                                while (historicalReader.Read())
                                {
                                    int productId = Convert.ToInt32(historicalReader["product_id"]);
                                    int totalChange = Convert.ToInt32(historicalReader["total_change"]);
                                    historicalContribution.Add(productId, totalChange);
                                }
                            }
                        }
                        
                        // 3. 撤销该订单对库存的历史贡献
                        foreach (var contribution in historicalContribution)
                        {
                            _inventoryRepo.UpdateInventory(contribution.Key, warehouseId, -contribution.Value);
                        }
                        
                        // 4. 应用当前订单明细对库存的新贡献
                        foreach (var item in orderItems)
                        {
                            _inventoryRepo.UpdateInventory(item.Item1, warehouseId, item.Item2);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 删除采购订单
        /// </summary>
        /// <param name="orderId"></param>
        public void DeletePurchaseOrder(int orderId)
        {
            string connectionString = DbHelper.GetConnectionString();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // 1. 获取订单信息，包括仓库ID和状态
                        int warehouseId = 0;
                        string status = string.Empty;
                        string getOrderQuery = "SELECT warehouse_id, status FROM purchase_order WHERE id = @orderId";
                        using (MySqlCommand orderCmd = new MySqlCommand(getOrderQuery, connection, transaction))
                        {
                            orderCmd.Parameters.AddWithValue("@orderId", orderId);
                            using (MySqlDataReader reader = orderCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    warehouseId = Convert.ToInt32(reader["warehouse_id"]);
                                    status = reader["status"].ToString();
                                }
                            }
                        }
                        
                        // 2. 获取订单的产品明细
                        List<Tuple<int, int>> orderItems = new List<Tuple<int, int>>();
                        string getItemsQuery = "SELECT product_id, quantity FROM purchase_order_item WHERE order_id = @orderId";
                        using (MySqlCommand itemsCmd = new MySqlCommand(getItemsQuery, connection, transaction))
                        {
                            itemsCmd.Parameters.AddWithValue("@orderId", orderId);
                            using (MySqlDataReader reader = itemsCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int productId = Convert.ToInt32(reader["product_id"]);
                                    int quantity = Convert.ToInt32(reader["quantity"]);
                                    orderItems.Add(new Tuple<int, int>(productId, quantity));
                                }
                            }
                        }
                        
                        // 3. 如果订单状态为已完成，先更新库存（减去该订单的采购数量）
                        if (status == "已完成" && warehouseId > 0)
                        {
                            foreach (var item in orderItems)
                            {
                                // 减去库存
                                _inventoryRepo.UpdateInventory(item.Item1, warehouseId, -item.Item2, connection, transaction);
                                
                                // 检查库存是否为0
                                int currentStock = _inventoryRepo.GetCurrentStock(item.Item1, warehouseId, connection, transaction);
                                if (currentStock <= 0)
                                {
                                    // 如果库存为0，删除产品
                                    string deleteProductQuery = "DELETE FROM product WHERE id = @productId";
                                    using (MySqlCommand deleteCmd = new MySqlCommand(deleteProductQuery, connection, transaction))
                                    {
                                        deleteCmd.Parameters.AddWithValue("@productId", item.Item1);
                                        deleteCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        
                        // 4. 先删除订单详情
                        string deleteItemsQuery = "DELETE FROM purchase_order_item WHERE order_id = @orderId";
                        using (MySqlCommand command = new MySqlCommand(deleteItemsQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
                            command.ExecuteNonQuery();
                        }
                        
                        // 5. 再删除订单
                        string deleteOrderQuery = "DELETE FROM purchase_order WHERE id = @orderId";
                        using (MySqlCommand command = new MySqlCommand(deleteOrderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@orderId", orderId);
                            command.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public string GenerateOrderNo()
        {
            string connectionString = DbHelper.GetConnectionString();
            string orderNoPrefix = DateTime.Now.ToString("yyyyMMdd");
            int sequence = 1;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                
                // 查询当天最大的订单号
                string query = "SELECT MAX(order_no) FROM purchase_order WHERE order_no LIKE @prefix";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@prefix", orderNoPrefix + "%");
                    object result = command.ExecuteScalar();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        string maxOrderNo = result.ToString();
                        // 提取序号部分并加1
                        sequence = int.Parse(maxOrderNo.Substring(8)) + 1;
                    }
                }
            }
            
            // 格式化为：年月日+4位序号
            return orderNoPrefix + sequence.ToString("D4");
        }
        
        /// <summary>
        /// 获取所有供应商
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllSuppliers()
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name FROM supplier WHERE status = 1 ORDER BY name";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 获取所有仓库
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllWarehouses()
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name FROM warehouse WHERE status = 1 ORDER BY name";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllUsers()
        {
            string connectionString = DbHelper.GetConnectionString();
            DataTable dt = new DataTable();
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT id, name FROM user WHERE status = 1 ORDER BY name";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection))
                {
                    adapter.Fill(dt);
                }
            }
            
            return dt;
        }
        
        /// <summary>
        /// 根据名称获取或创建用户ID
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetOrCreateUserIdByName(string userName)
        {
            string connectionString = DbHelper.GetConnectionString();
            int userId = 0;
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                
                // 先查找用户
                string findQuery = "SELECT id FROM user WHERE name = @name";
                using (MySqlCommand command = new MySqlCommand(findQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", userName);
                    object result = command.ExecuteScalar();
                    
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                    else
                    {
                        // 创建新用户，使用默认值
                        string createQuery = "INSERT INTO user (account, password, name, role, status) VALUES (@account, @password, @name, @role, 1)";
                        using (MySqlCommand createCommand = new MySqlCommand(createQuery, connection))
                        {
                            createCommand.Parameters.AddWithValue("@account", userName + "_" + DateTime.Now.Ticks);
                            createCommand.Parameters.AddWithValue("@password", "123456"); // 默认密码
                            createCommand.Parameters.AddWithValue("@name", userName);
                            createCommand.Parameters.AddWithValue("@role", "采购员"); // 默认角色
                            
                            createCommand.ExecuteNonQuery();
                            userId = Convert.ToInt32(createCommand.LastInsertedId);
                        }
                    }
                }
            }
            
            return userId;
        }
    }
}