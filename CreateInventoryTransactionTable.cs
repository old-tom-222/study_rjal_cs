using System;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace CSproject
{
    class CreateInventoryTransactionTable
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("开始创建inventory_transaction表...");
                
                // 获取连接字符串
                string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
                
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("数据库连接成功！");
                    
                    // 创建表
                    string createTableSql = @"
                        CREATE TABLE IF NOT EXISTS inventory_transaction (
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
                    ";
                    
                    using (MySqlCommand command = new MySqlCommand(createTableSql, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("inventory_transaction表创建成功！");
                    }
                    
                    // 添加索引
                    string[] indexStatements = new string[]
                    {
                        "CREATE INDEX idx_product_id ON inventory_transaction(product_id)",
                        "CREATE INDEX idx_warehouse_id ON inventory_transaction(warehouse_id)",
                        "CREATE INDEX idx_type ON inventory_transaction(type)",
                        "CREATE INDEX idx_created_at ON inventory_transaction(created_at)",
                        "CREATE INDEX idx_reference ON inventory_transaction(reference)"
                    };
                    
                    foreach (string indexSql in indexStatements)
                    {
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand(indexSql, connection))
                            {
                                command.ExecuteNonQuery();
                                Console.WriteLine($"索引创建成功: {indexSql.Split(' ')[2]}");
                            }
                        }
                        catch (MySqlException ex) when (ex.Number == 1061) // 索引已存在
                        {
                            Console.WriteLine($"索引已存在: {indexSql.Split(' ')[2]}");
                        }
                    }
                    
                    Console.WriteLine("\n所有操作完成！inventory_transaction表已成功创建。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"内部错误: {ex.InnerException.Message}");
                }
            }
            finally
            {
                Console.WriteLine("\n按任意键退出...");
                Console.ReadKey();
            }
        }
    }
}