using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("测试MySQL数据库连接...");
                
                // 获取连接字符串
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["MySqlConnectionString"];
                if (connectionStringSettings == null)
                {
                    Console.WriteLine("错误: 连接字符串配置缺失");
                    return;
                }
                
                string connectionString = connectionStringSettings.ConnectionString;
                Console.WriteLine($"连接字符串: {connectionString}");
                
                // 测试连接
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine($"连接成功! 连接状态: {connection.State}");
                    
                    // 测试查询
                    using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM user", connection))
                    {
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        Console.WriteLine($"用户表记录数: {count}");
                    }
                }
                
                Console.WriteLine("测试完成，按任意键退出...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误: {ex.Message}");
                Console.WriteLine($"错误类型: {ex.GetType().Name}");
                Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
                Console.WriteLine("按任意键退出...");
                Console.ReadKey();
            }
        }
    }
}