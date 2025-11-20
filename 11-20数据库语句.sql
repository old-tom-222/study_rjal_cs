-- 开始事务
START TRANSACTION;

-- 删除现有表（注意顺序，先删除有外键依赖的表）
DROP TABLE IF EXISTS sales_order_item;
DROP TABLE IF EXISTS purchase_order_item;
DROP TABLE IF EXISTS sales_order;
DROP TABLE IF EXISTS purchase_order;
DROP TABLE IF EXISTS inventory_transaction;
DROP TABLE IF EXISTS inventory;
DROP TABLE IF EXISTS product;
DROP TABLE IF EXISTS product_category;
DROP TABLE IF EXISTS category; -- 额外的表
DROP TABLE IF EXISTS customer;
DROP TABLE IF EXISTS supplier;
DROP TABLE IF EXISTS warehouse;
DROP TABLE IF EXISTS user;

-- 用户表 
 CREATE TABLE user( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     account VARCHAR(50) NOT NULL UNIQUE, 
     password VARCHAR(100) NOT NULL, 
     name VARCHAR(50) NOT NULL, 
     role VARCHAR(20) DEFAULT 'staff', -- 直接存储角色，简化权限管理 
     status TINYINT(1) DEFAULT 1, 
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP 
 ); 
 
 -- 商品分类表 
 CREATE TABLE product_category( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     name VARCHAR(100) NOT NULL, 
     parent_id INT DEFAULT NULL 
 ); 
 
 -- 商品信息表 
 CREATE TABLE product( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     sku VARCHAR(50) NOT NULL UNIQUE, 
     name VARCHAR(200) NOT NULL, 
     category_id INT NOT NULL, 
     cost_price DECIMAL(10,2) DEFAULT 0, 
     sale_price DECIMAL(10,2) DEFAULT 0, 
     safe_stock INT DEFAULT 10, 
     status TINYINT(1) DEFAULT 1 
 ); 
 
 -- 仓库表 
 CREATE TABLE warehouse( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     name VARCHAR(100) NOT NULL, 
     address VARCHAR(200), 
     status TINYINT(1) DEFAULT 1 
 ); 
 
 -- 供应商表 
 CREATE TABLE supplier( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     name VARCHAR(200) NOT NULL, 
     contact_person VARCHAR(50), 
     phone VARCHAR(20), 
     status TINYINT(1) DEFAULT 1 
 ); 
 
 -- 客户表 
 CREATE TABLE customer( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     name VARCHAR(200) NOT NULL, 
     contact_person VARCHAR(50), 
     phone VARCHAR(20), 
     credit_limit DECIMAL(10,2) DEFAULT 10000, 
     status TINYINT(1) DEFAULT 1 
 ); 
 
 -- 采购订单主表（关联用户） 
 CREATE TABLE purchase_order( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     order_no VARCHAR(50) NOT NULL UNIQUE, 
     supplier_id INT NOT NULL, 
     warehouse_id INT NOT NULL, -- 直接关联仓库，简化流程 
     total_amount DECIMAL(10,2) DEFAULT 0, 
     status VARCHAR(20) DEFAULT 'pending', 
     created_by INT NOT NULL, -- 关联用户表 
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
     FOREIGN KEY(supplier_id) REFERENCES supplier(id), 
     FOREIGN KEY(warehouse_id) REFERENCES warehouse(id), 
     FOREIGN KEY(created_by) REFERENCES user(id) 
 ); 
 
 -- 采购订单明细表 
 CREATE TABLE purchase_order_item( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     order_id INT NOT NULL, 
     product_id INT NOT NULL, 
     quantity INT NOT NULL, 
     unit_price DECIMAL(10,2) NOT NULL, 
     FOREIGN KEY(order_id) REFERENCES purchase_order(id), 
     FOREIGN KEY(product_id) REFERENCES product(id) 
 ); 
 
 -- 销售订单主表（关联用户） 
 CREATE TABLE sales_order( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     order_no VARCHAR(50) NOT NULL UNIQUE, 
     customer_id INT NOT NULL, 
     warehouse_id INT NOT NULL, -- 直接关联仓库，简化流程 
     total_amount DECIMAL(10,2) DEFAULT 0, 
     status VARCHAR(20) DEFAULT 'pending', 
     created_by INT NOT NULL, -- 关联用户表 
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
     FOREIGN KEY(customer_id) REFERENCES customer(id), 
     FOREIGN KEY(warehouse_id) REFERENCES warehouse(id), 
     FOREIGN KEY(created_by) REFERENCES user(id) 
 ); 
 
 -- 销售订单明细表 
 CREATE TABLE sales_order_item( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     order_id INT NOT NULL, 
     product_id INT NOT NULL, 
     quantity INT NOT NULL, 
     unit_price DECIMAL(10,2) NOT NULL, 
     FOREIGN KEY(order_id) REFERENCES sales_order(id), 
     FOREIGN KEY(product_id) REFERENCES product(id) 
 ); 
 
 -- 库存表 
 CREATE TABLE inventory( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     product_id INT NOT NULL, 
     warehouse_id INT NOT NULL, 
     quantity INT NOT NULL DEFAULT 0, 
     last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP, 
     FOREIGN KEY(product_id) REFERENCES product(id), 
     FOREIGN KEY(warehouse_id) REFERENCES warehouse(id), 
     UNIQUE KEY unique_inventory(product_id, warehouse_id) 
 ); 
 
 -- 库存交易表 
 CREATE TABLE inventory_transaction( 
     id INT AUTO_INCREMENT PRIMARY KEY, 
     product_id INT NOT NULL, 
     warehouse_id INT NOT NULL, 
     change_qty INT NOT NULL, 
     type VARCHAR(50) NOT NULL DEFAULT 'adjust', 
     reference VARCHAR(100) NULL, 
     remark TEXT NULL, 
     created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
     FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE CASCADE, 
     FOREIGN KEY (warehouse_id) REFERENCES warehouse(id) ON DELETE CASCADE 
 ); 

 -- 添加索引以提高查询性能
 CREATE INDEX idx_product_id ON inventory_transaction(product_id);
 CREATE INDEX idx_warehouse_id ON inventory_transaction(warehouse_id);
 CREATE INDEX idx_type ON inventory_transaction(type);
 CREATE INDEX idx_created_at ON inventory_transaction(created_at);
 CREATE INDEX idx_reference ON inventory_transaction(reference);

-- 提交事务
COMMIT;