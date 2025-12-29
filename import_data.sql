-- 填充测试数据SQL脚本
-- 按照表之间的依赖关系顺序插入数据

-- 1. 用户表数据（无status字段）
INSERT INTO user (account, password, name, role) VALUES
('admin', 'admin123', '系统管理员', 'admin'),
('user1', 'user123', '张三', 'staff'),
('user2', 'user123', '李四', 'staff'),
('user3', 'user123', '王五', 'manager');

-- 2. 商品分类表数据
INSERT INTO product_category (name, parent_id) VALUES
('电子产品', NULL),
('手机配件', 1),
('电脑配件', 1),
('办公用品', NULL),
('文具', 4),
('办公设备', 4);

-- 3. 仓库表数据
INSERT INTO warehouse (name, address, status) VALUES
('主仓库', '北京市朝阳区一号仓库', 1),
('华东仓库', '上海市浦东新区二号仓库', 1),
('华南仓库', '广州市天河区三号仓库', 1),
('西南仓库', '成都市武侯区四号仓库', 1);

-- 4. 供应商表数据
INSERT INTO supplier (name, contact_person, phone, status) VALUES
('联想供应商', '李明', '13800138001', 1),
('华为供应商', '王芳', '13900139002', 1),
('苹果供应商', '张强', '13700137003', 1),
('得力文具供应商', '赵丽', '13600136004', 1);

-- 5. 客户表数据
INSERT INTO customer (name, contact_person, phone, credit_limit, status) VALUES
('北京科技有限公司', '陈总', '13500135001', 50000.00, 1),
('上海贸易公司', '吴经理', '13400134002', 30000.00, 1),
('广州电子科技', '郑主任', '13300133003', 40000.00, 1),
('成都教育机构', '刘校长', '13200132004', 20000.00, 1);

-- 6. 商品信息表数据（调整字段以匹配实际表结构）
INSERT INTO product (name, category_id, warehouse_id, unit, price, stock_qty, status) VALUES
('华为Mate 60 Pro', 1, 1, '台', 7999.00, 1, 1),
('iPhone 15 Pro', 1, 1, '台', 8999.00, 0, 1),
('AirPods Pro 2', 2, 2, '个', 1899.00, 1, 1),
('华为原装充电器', 2, 1, '个', 299.00, 10, 1),
('联想ThinkPad笔记本', 3, 1, '台', 6999.00, 3, 1),
('机械键盘', 3, 2, '个', 399.00, 12, 1),
('得力中性笔', 5, 3, '支', 2.00, 50, 1),
('A4打印纸', 5, 3, '包', 25.00, 60, 1),
('HP激光打印机', 6, 1, '台', 1599.00, 3, 1),
('办公椅', 6, 4, '把', 1299.00, 5, 1);

-- 7. 采购订单主表数据
INSERT INTO purchase_order (order_no, supplier_id, warehouse_id, total_amount, status, created_by) VALUES
('PO202401001', 1, 1, 17997.00, 'completed', 1),
('PO202401002', 2, 1, 15996.00, 'completed', 2),
('PO202401003', 3, 2, 9495.00, 'pending', 3),
('PO202401004', 4, 3, 430.00, 'completed', 2);

-- 8. 销售订单主表数据
INSERT INTO sales_order (order_no, customer_id, warehouse_id, total_amount, status, created_by) VALUES
('SO202401001', 1, 1, 9898.00, 'completed', 1),
('SO202401002', 2, 2, 2199.00, 'pending', 2),
('SO202401003', 3, 1, 7999.00, 'completed', 3),
('SO202401004', 4, 3, 1525.00, 'completed', 1);

-- 9. 采购订单明细表数据
INSERT INTO purchase_order_item (order_id, product_id, quantity, unit_price) VALUES
(1, 5, 3, 5999.00),
(2, 1, 2, 6999.00),
(2, 4, 10, 199.00),
(3, 2, 1, 7999.00),
(3, 3, 1, 1499.00),
(4, 7, 100, 1.50),
(4, 8, 10, 18.00);

-- 10. 销售订单明细表数据
INSERT INTO sales_order_item (order_id, product_id, quantity, unit_price) VALUES
(1, 2, 1, 8999.00),
(1, 3, 1, 899.00),
(2, 6, 3, 399.00),
(2, 9, 1, 999.00),
(3, 1, 1, 7999.00),
(4, 7, 50, 2.00),
(4, 8, 50, 25.00);

-- 11. 库存表数据
INSERT INTO inventory (product_id, warehouse_id, quantity) VALUES
(1, 1, 1),  -- 华为Mate 60 Pro在主仓库
(2, 1, 0),  -- iPhone 15 Pro在主仓库
(2, 2, 1),  -- iPhone 15 Pro在华东仓库
(3, 2, 1),  -- AirPods Pro 2在华东仓库
(4, 1, 10), -- 华为原装充电器在主仓库
(5, 1, 3),  -- 联想ThinkPad笔记本在主仓库
(6, 2, 12), -- 机械键盘在华东仓库
(7, 3, 50), -- 得力中性笔在华南仓库
(8, 3, 60), -- A4打印纸在华南仓库
(9, 1, 3),  -- HP激光打印机在主仓库
(10, 4, 5), -- 办公椅在西南仓库
(3, 1, 0),  -- AirPods Pro 2在主仓库
(6, 4, 8),  -- 机械键盘在西南仓库
(7, 4, 30); -- 得力中性笔在西南仓库

-- 12. 库存交易表数据
INSERT INTO inventory_transaction (product_id, warehouse_id, change_qty, type, reference, remark) VALUES
(1, 1, 2, 'purchase', 'PO202401002', '采购入库'),
(1, 1, -1, 'sales', 'SO202401003', '销售出库'),
(2, 2, 1, 'purchase', 'PO202401003', '采购入库'),
(2, 1, 1, 'purchase', 'PO202401001', '采购入库'),
(2, 1, -1, 'sales', 'SO202401001', '销售出库'),
(3, 2, 1, 'purchase', 'PO202401003', '采购入库'),
(3, 1, 1, 'purchase', 'PO202401002', '采购入库'),
(3, 1, -1, 'sales', 'SO202401001', '销售出库'),
(4, 1, 10, 'purchase', 'PO202401002', '采购入库'),
(5, 1, 3, 'purchase', 'PO202401001', '采购入库'),
(6, 2, 15, 'purchase', 'PO202401001', '采购入库'),
(6, 2, -3, 'sales', 'SO202401002', '销售出库'),
(7, 3, 100, 'purchase', 'PO202401004', '采购入库'),
(7, 3, -50, 'sales', 'SO202401004', '销售出库'),
(7, 4, 30, 'purchase', 'PO202401004', '采购入库'),
(8, 3, 70, 'purchase', 'PO202401004', '采购入库'),
(8, 3, -10, 'sales', 'SO202401004', '销售出库'),
(9, 1, 3, 'purchase', 'PO202401001', '采购入库'),
(10, 4, 5, 'purchase', 'PO202401001', '采购入库'),
(6, 4, 8, 'purchase', 'PO202401002', '采购入库');

-- 更新采购订单的总金额（确保数据一致性）
UPDATE purchase_order po 
SET total_amount = (
    SELECT SUM(quantity * unit_price)
    FROM purchase_order_item
    WHERE order_id = po.id
)
WHERE id > 0; -- 添加WHERE条件使用主键列

-- 更新销售订单的总金额（确保数据一致性）
UPDATE sales_order so 
SET total_amount = (
    SELECT SUM(quantity * unit_price)
    FROM sales_order_item
    WHERE order_id = so.id
)
WHERE id > 0; -- 添加WHERE条件使用主键列

-- 13. 应收账款表数据
INSERT INTO account_receivable (order_no, customer_id, total_amount, paid_amount, outstanding_amount, status, order_date, due_date) VALUES
('SO202401001', 1, 9898.00, 5000.00, 4898.00, 'partial', '2024-01-10', '2024-02-10'),
('SO202401002', 2, 2199.00, 0.00, 2199.00, 'unpaid', '2024-01-15', '2024-02-15'),
('SO202401003', 3, 7999.00, 7999.00, 0.00, 'paid', '2024-01-20', '2024-02-20'),
('SO202401004', 4, 1525.00, 1000.00, 525.00, 'partial', '2024-01-25', '2024-02-25');

-- 14. 应付账款表数据
INSERT INTO account_payable (order_no, supplier_id, total_amount, paid_amount, outstanding_amount, status, order_date, due_date) VALUES
('PO202401001', 1, 17997.00, 10000.00, 7997.00, 'partial', '2024-01-05', '2024-02-05'),
('PO202401002', 2, 15996.00, 15996.00, 0.00, 'paid', '2024-01-12', '2024-02-12'),
('PO202401003', 3, 9495.00, 0.00, 9495.00, 'unpaid', '2024-01-18', '2024-02-18'),
('PO202401004', 4, 430.00, 430.00, 0.00, 'paid', '2024-01-22', '2024-02-22');