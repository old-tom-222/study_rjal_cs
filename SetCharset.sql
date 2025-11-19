-- 修改数据库和表的字符集设置为UTF-8
ALTER DATABASE mycsproject CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 修改customer表的字符集
ALTER TABLE customer CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

-- 确认字符集设置
SHOW CREATE TABLE customer;