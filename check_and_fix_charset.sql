-- 检查数据库的字符集设置
SHOW VARIABLES LIKE 'character_set_database';
SHOW VARIABLES LIKE 'collation_database';

-- 检查customer表的字符集设置
SHOW CREATE TABLE customer;

-- 如果需要，修改customer表的字符集为utf8mb4
ALTER TABLE customer CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 验证修改是否成功
SHOW CREATE TABLE customer;
