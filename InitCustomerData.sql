-- 初始客户数据插入脚本
-- 适用于调试和测试

-- 清空现有客户数据
DELETE FROM customer;

-- 重置自增ID
ALTER TABLE customer AUTO_INCREMENT = 1;

-- 插入测试客户数据
INSERT INTO customer (name, contact_person, phone, credit_limit, status) VALUES
('阿里巴巴集团', '张三', '13800138001', 1000000.00, 1),
('腾讯科技', '李四', '13900139001', 800000.00, 1),
('百度公司', '王五', '13700137001', 600000.00, 1),
('京东集团', '赵六', '13600136001', 900000.00, 1);

-- 查询插入的客户数据，验证插入是否成功
SELECT * FROM customer;