-- 基础数据相关表结构
-- 开始事务
START TRANSACTION;

-- 部门表
CREATE TABLE department (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL, -- 部门名称
    code VARCHAR(20) NOT NULL UNIQUE, -- 部门编码
    parent_id INT DEFAULT NULL, -- 父部门ID，用于部门层级
    manager_id INT DEFAULT NULL, -- 部门经理ID
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 员工表
CREATE TABLE employee (
    id INT AUTO_INCREMENT PRIMARY KEY,
    employee_no VARCHAR(50) NOT NULL UNIQUE, -- 员工编号
    name VARCHAR(50) NOT NULL, -- 员工姓名
    department_id INT NOT NULL, -- 所属部门ID
    position VARCHAR(100), -- 职位
    gender VARCHAR(10), -- 性别
    birth_date DATE, -- 出生日期
    hire_date DATE, -- 入职日期
    phone VARCHAR(20), -- 联系电话
    email VARCHAR(100), -- 电子邮箱
    address VARCHAR(200), -- 地址
    status TINYINT(1) DEFAULT 1, -- 状态：1-在职，0-离职
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (department_id) REFERENCES department(id)
);

-- 更新部门表的外键约束，指向员工表
ALTER TABLE department
ADD CONSTRAINT fk_department_manager FOREIGN KEY (manager_id) REFERENCES employee(id);

-- 仓库类型表
CREATE TABLE warehouse_type (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL, -- 仓库类型名称
    description TEXT, -- 类型描述
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 付款方式表
CREATE TABLE payment_method (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL, -- 付款方式名称
    code VARCHAR(20) NOT NULL UNIQUE, -- 付款方式编码
    description TEXT, -- 方式描述
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 计量单位表
CREATE TABLE unit_of_measure (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL, -- 计量单位名称
    code VARCHAR(20) NOT NULL UNIQUE, -- 计量单位编码
    base_unit_id INT DEFAULT NULL, -- 基准单位ID（用于单位换算）
    conversion_rate DECIMAL(10,4) DEFAULT 1, -- 换算率
    status TINYINT(1) DEFAULT 1, -- 状态：1-启用，0-禁用
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- 添加索引
CREATE INDEX idx_department_name ON department(name);
CREATE INDEX idx_department_code ON department(code);
CREATE INDEX idx_employee_no ON employee(employee_no);
CREATE INDEX idx_employee_name ON employee(name);
CREATE INDEX idx_employee_department ON employee(department_id);
CREATE INDEX idx_warehouse_type_name ON warehouse_type(name);
CREATE INDEX idx_payment_method_name ON payment_method(name);
CREATE INDEX idx_payment_method_code ON payment_method(code);
CREATE INDEX idx_uom_name ON unit_of_measure(name);
CREATE INDEX idx_uom_code ON unit_of_measure(code);

-- 提交事务
COMMIT;