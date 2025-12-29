-- 创建应收账款表
CREATE TABLE account_receivable(
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_no VARCHAR(50) NOT NULL,
    customer_id INT NOT NULL,
    total_amount DECIMAL(10,2) DEFAULT 0,
    paid_amount DECIMAL(10,2) DEFAULT 0,
    outstanding_amount DECIMAL(10,2) DEFAULT 0,
    status VARCHAR(20) DEFAULT 'pending',
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    due_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY(customer_id) REFERENCES customer(id)
);

-- 创建应付账款表
CREATE TABLE account_payable(
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_no VARCHAR(50) NOT NULL,
    supplier_id INT NOT NULL,
    total_amount DECIMAL(10,2) DEFAULT 0,
    paid_amount DECIMAL(10,2) DEFAULT 0,
    outstanding_amount DECIMAL(10,2) DEFAULT 0,
    status VARCHAR(20) DEFAULT 'pending',
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    due_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY(supplier_id) REFERENCES supplier(id)
);

-- 添加索引以提高查询性能
CREATE INDEX idx_account_receivable_order_no ON account_receivable(order_no);
CREATE INDEX idx_account_receivable_customer_id ON account_receivable(customer_id);
CREATE INDEX idx_account_receivable_status ON account_receivable(status);
CREATE INDEX idx_account_receivable_due_date ON account_receivable(due_date);

CREATE INDEX idx_account_payable_order_no ON account_payable(order_no);
CREATE INDEX idx_account_payable_supplier_id ON account_payable(supplier_id);
CREATE INDEX idx_account_payable_status ON account_payable(status);
CREATE INDEX idx_account_payable_due_date ON account_payable(due_date);