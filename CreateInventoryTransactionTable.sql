-- 创建库存交易记录表
CREATE TABLE IF NOT EXISTS inventory_transaction (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    warehouse_id INT NOT NULL,
    change_qty INT NOT NULL,
    type VARCHAR(50) NOT NULL DEFAULT 'adjust',
    reference VARCHAR(100) NULL,
    remark TEXT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    -- 添加外键约束
    FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE CASCADE,
    FOREIGN KEY (warehouse_id) REFERENCES warehouse(id) ON DELETE CASCADE
);

-- 添加索引以提高查询性能
CREATE INDEX idx_product_id ON inventory_transaction(product_id);
CREATE INDEX idx_warehouse_id ON inventory_transaction(warehouse_id);
CREATE INDEX idx_type ON inventory_transaction(type);
CREATE INDEX idx_created_at ON inventory_transaction(created_at);
CREATE INDEX idx_reference ON inventory_transaction(reference);

SELECT 'inventory_transaction表创建成功' AS message;