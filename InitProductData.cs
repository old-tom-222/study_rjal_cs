using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CSproject
{
    class InitProductData
    {
        static void Main(string[] args)
        {
            try
            {
                // 获取数据库连接字符串
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
                
                // 初始化产品分类和产品数据的SQL语句
                string[] sqlStatements = new string[]
                {
                    // 清空已有的产品分类和产品数据（如果存在）
                    "DELETE FROM product WHERE 1=1;",
                    "DELETE FROM product_category WHERE 1=1;",
                    "ALTER TABLE product AUTO_INCREMENT = 1;",
                    "ALTER TABLE product_category AUTO_INCREMENT = 1;",
                    
                    // 插入产品分类数据
                    "INSERT INTO product_category (name, parent_id) VALUES ('电子产品', NULL);",
                    "INSERT INTO product_category (name, parent_id) VALUES ('办公用品', NULL);",
                    "INSERT INTO product_category (name, parent_id) VALUES ('电脑设备', 1);",
                    "INSERT INTO product_category (name, parent_id) VALUES ('手机配件', 1);",
                    
                    // 插入产品数据
                    "INSERT INTO product (sku, name, category_id, cost_price, sale_price, safe_stock, status) VALUES ('P001', '笔记本电脑', 3, 4500.00, 5999.00, 5, 1);",
                    "INSERT INTO product (sku, name, category_id, cost_price, sale_price, safe_stock, status) VALUES ('P002', '无线鼠标', 3, 50.00, 99.00, 20, 1);",
                    "INSERT INTO product (sku, name, category_id, cost_price, sale_price, safe_stock, status) VALUES ('P003', '智能手机', 4, 1200.00, 1999.00, 10, 1);",
                    "INSERT INTO product (sku, name, category_id, cost_price, sale_price, safe_stock, status) VALUES ('P004', 'A4打印纸', 2, 20.00, 25.00, 50, 1);"
                };
                
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("数据库连接成功！");
                    
                    // 开始事务
                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (string sql in sqlStatements)
                            {
                                using (MySqlCommand command = new MySqlCommand(sql, connection, transaction))
                                {
                                    int rowsAffected = command.ExecuteNonQuery();
                                    Console.WriteLine($"执行SQL: {sql.Substring(0, Math.Min(50, sql.Length))}... 影响行数: {rowsAffected}");
                                }
                            }
                            
                            // 提交事务
                            transaction.Commit();
                            Console.WriteLine("\n初始数据插入成功！");
                            Console.WriteLine("已添加产品分类: 电子产品、办公用品、电脑设备、手机配件");
                            Console.WriteLine("已添加产品: 笔记本电脑、无线鼠标、智能手机、A4打印纸");
                        }
                        catch (Exception ex)
                        {
                            // 回滚事务
                            transaction.Rollback();
                            Console.WriteLine($"\n数据插入失败，已回滚事务: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
            
            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
    }
}