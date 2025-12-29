-- 清除所有表中的数据，但保留表结构
SET FOREIGN_KEY_CHECKS = 0;

-- 清除每个表的数据
TRUNCATE TABLE account_payable;
TRUNCATE TABLE account_receivable;
TRUNCATE TABLE customer;
TRUNCATE TABLE inventory;
TRUNCATE TABLE inventory_transaction;
TRUNCATE TABLE product;
TRUNCATE TABLE product_category;
TRUNCATE TABLE purchase_order_item;
TRUNCATE TABLE purchase_order;
TRUNCATE TABLE sales_order_item;
TRUNCATE TABLE sales_order;
TRUNCATE TABLE supplier;
TRUNCATE TABLE user;
TRUNCATE TABLE warehouse;

SET FOREIGN_KEY_CHECKS = 1;

-- 验证表的字符集仍然是UTF-8
SELECT TABLE_NAME, TABLE_COLLATION
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'mycsproject';