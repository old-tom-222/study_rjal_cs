using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CSproject.Data.Helpers
{
    public static class DbHelper
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool UserExists(string account)
        {
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM user WHERE account = @account";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool ValidateUser(string account, string password)
        {
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM user WHERE account = @account AND password = @password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    command.Parameters.AddWithValue("@password", password);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static bool AddUser(string account, string password, string name, string role)
        {
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO user (account, password, name, role) VALUES (@account, @password, @name, @role)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@role", role);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}