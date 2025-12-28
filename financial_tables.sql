-- 财务管理相关表结构
-- 开始事务
START TRANSACTION;

-- 会计科目表
CREATE TABLE accounting_subject (
    id INT AUTO_INCREMENT PRIMARY KEY,
    code VARCHAR(20) NOT NULL UNIQUE, -- 科目编码
    name VARCHAR(100) NOT NULL, -- 科目名称
    type VARCHAR(20) NOT NULL, -- 科目类型：资产、负债、所有者权益、成本、损益
    parent_id INT DEFAULT NULL, -- 父科目ID，用于科目层级
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 收入表
CREATE TABLE income (
    id INT AUTO_INCREMENT PRIMARY KEY,
    income_no VARCHAR(50) NOT NULL UNIQUE, -- 收入编号
    subject_id INT NOT NULL, -- 会计科目ID
    amount DECIMAL(12,2) NOT NULL, -- 收入金额
    income_date DATE NOT NULL, -- 收入日期
    source VARCHAR(200) NOT NULL, -- 收入来源
    description TEXT, -- 收入描述
    reference VARCHAR(100), -- 参考凭证号
    created_by INT NOT NULL, -- 创建人ID
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (subject_id) REFERENCES accounting_subject(id),
    FOREIGN KEY (created_by) REFERENCES user(id)
);

-- 支出表
CREATE TABLE expense (
    id INT AUTO_INCREMENT PRIMARY KEY,
    expense_no VARCHAR(50) NOT NULL UNIQUE, -- 支出编号
    subject_id INT NOT NULL, -- 会计科目ID
    amount DECIMAL(12,2) NOT NULL, -- 支出金额
    expense_date DATE NOT NULL, -- 支出日期
    category VARCHAR(100) NOT NULL, -- 支出类别
    description TEXT, -- 支出描述
    reference VARCHAR(100), -- 参考凭证号
    created_by INT NOT NULL, -- 创建人ID
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (subject_id) REFERENCES accounting_subject(id),
    FOREIGN KEY (created_by) REFERENCES user(id)
);

-- 银行账户表
CREATE TABLE bank_account (
    id INT AUTO_INCREMENT PRIMARY KEY,
    account_name VARCHAR(100) NOT NULL, -- 账户名称
    bank_name VARCHAR(100) NOT NULL, -- 银行名称
    account_number VARCHAR(50) NOT NULL UNIQUE, -- 银行账号
    balance DECIMAL(12,2) DEFAULT 0, -- 账户余额
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 财务报表表
CREATE TABLE financial_report (
    id INT AUTO_INCREMENT PRIMARY KEY,
    report_type VARCHAR(50) NOT NULL, -- 报表类型：资产负债表、利润表、现金流量表
    report_period VARCHAR(20) NOT NULL, -- 报表期间：如2024-01, 2024-Q1
    report_date DATE NOT NULL, -- 报表生成日期
    total_assets DECIMAL(12,2) DEFAULT 0, -- 总资产（资产负债表）
    total_liabilities DECIMAL(12,2) DEFAULT 0, -- 总负债（资产负债表）
    total_equity DECIMAL(12,2) DEFAULT 0, -- 所有者权益（资产负债表）
    total_income DECIMAL(12,2) DEFAULT 0, -- 总收入（利润表）
    total_expense DECIMAL(12,2) DEFAULT 0, -- 总支出（利润表）
    net_profit DECIMAL(12,2) DEFAULT 0, -- 净利润（利润表）
    created_by INT NOT NULL, -- 创建人ID
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (created_by) REFERENCES user(id)
);

-- 添加索引
CREATE INDEX idx_accounting_subject_code ON accounting_subject(code);
CREATE INDEX idx_accounting_subject_type ON accounting_subject(type);
CREATE INDEX idx_income_date ON income(income_date);
CREATE INDEX idx_income_subject ON income(subject_id);
CREATE INDEX idx_expense_date ON expense(expense_date);
CREATE INDEX idx_expense_subject ON expense(subject_id);
CREATE INDEX idx_bank_account_name ON bank_account(account_name);
CREATE INDEX idx_financial_report_type ON financial_report(report_type);
CREATE INDEX idx_financial_report_period ON financial_report(report_period);

-- 提交事务
COMMIT;