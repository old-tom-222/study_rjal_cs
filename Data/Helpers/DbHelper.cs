using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CSproject.Data.Helpers
{
    public static class DbHelper
    {
        /// <summary>
        /// 静态构造函数，确保数据库表已创建
        /// </summary>
        static DbHelper()
        {
            InitializeDatabase();
        }
        
        /// <summary>
        /// 初始化数据库，确保所有必需的表存在
        /// </summary>
        private static void InitializeDatabase()
        {
            try
            {
                string connectionString = GetConnectionString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    
                    // 检查并创建accounting_subject表
                    string createAccountingSubjectTable = @"
                        CREATE TABLE IF NOT EXISTS accounting_subject (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            code VARCHAR(20) NOT NULL UNIQUE,
                            name VARCHAR(100) NOT NULL,
                            type VARCHAR(20) NOT NULL,
                            parent_id INT DEFAULT NULL,
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createAccountingSubjectTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建income表
                    string createIncomeTable = @"
                        CREATE TABLE IF NOT EXISTS income (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            income_no VARCHAR(50) NOT NULL UNIQUE,
                            subject_id INT NOT NULL,
                            amount DECIMAL(12,2) NOT NULL,
                            income_date DATE NOT NULL,
                            source VARCHAR(200) NOT NULL,
                            description TEXT,
                            reference VARCHAR(100),
                            created_by INT NOT NULL,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (subject_id) REFERENCES accounting_subject(id),
                            FOREIGN KEY (created_by) REFERENCES user(id)
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createIncomeTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建expense表
                    string createExpenseTable = @"
                        CREATE TABLE IF NOT EXISTS expense (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            expense_no VARCHAR(50) NOT NULL UNIQUE,
                            subject_id INT NOT NULL,
                            amount DECIMAL(12,2) NOT NULL,
                            expense_date DATE NOT NULL,
                            category VARCHAR(100) NOT NULL,
                            description TEXT,
                            reference VARCHAR(100),
                            created_by INT NOT NULL,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (subject_id) REFERENCES accounting_subject(id),
                            FOREIGN KEY (created_by) REFERENCES user(id)
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createExpenseTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建bank_account表
                    string createBankAccountTable = @"
                        CREATE TABLE IF NOT EXISTS bank_account (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            account_name VARCHAR(100) NOT NULL,
                            bank_name VARCHAR(100) NOT NULL,
                            account_number VARCHAR(50) NOT NULL UNIQUE,
                            balance DECIMAL(12,2) DEFAULT 0,
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createBankAccountTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建department表
                    string createDepartmentTable = @"
                        CREATE TABLE IF NOT EXISTS department (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            name VARCHAR(100) NOT NULL,
                            code VARCHAR(20) NOT NULL UNIQUE,
                            parent_id INT DEFAULT NULL,
                            manager_id INT DEFAULT NULL,
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createDepartmentTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建employee表
                    string createEmployeeTable = @"
                        CREATE TABLE IF NOT EXISTS employee (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            employee_no VARCHAR(50) NOT NULL UNIQUE,
                            name VARCHAR(50) NOT NULL,
                            department_id INT NOT NULL,
                            position VARCHAR(100),
                            gender VARCHAR(10),
                            birth_date DATE,
                            hire_date DATE,
                            phone VARCHAR(20),
                            email VARCHAR(100),
                            address VARCHAR(200),
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                            FOREIGN KEY (department_id) REFERENCES department(id)
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createEmployeeTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 更新department表，添加manager_id外键
                    string updateDepartmentTable = @"
                        ALTER TABLE department
                        ADD CONSTRAINT IF NOT EXISTS fk_department_manager 
                        FOREIGN KEY (manager_id) REFERENCES employee(id)
                        ON DELETE SET NULL;
                    ";
                    using (MySqlCommand command = new MySqlCommand(updateDepartmentTable, connection))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (Exception)
                        {
                            // 忽略已存在的外键约束错误
                        }
                    }
                    
                    // 检查并创建payment_method表
                    string createPaymentMethodTable = @"
                        CREATE TABLE IF NOT EXISTS payment_method (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            name VARCHAR(100) NOT NULL,
                            code VARCHAR(20) NOT NULL UNIQUE,
                            description TEXT,
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createPaymentMethodTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建unit_of_measure表
                    string createUnitOfMeasureTable = @"
                        CREATE TABLE IF NOT EXISTS unit_of_measure (
                            id INT AUTO_INCREMENT PRIMARY KEY,
                            name VARCHAR(50) NOT NULL,
                            code VARCHAR(20) NOT NULL UNIQUE,
                            base_unit_id INT DEFAULT NULL,
                            conversion_rate DECIMAL(10,4) DEFAULT 1,
                            status TINYINT(1) DEFAULT 1,
                            created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        );
                    ";
                    using (MySqlCommand command = new MySqlCommand(createUnitOfMeasureTable, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    
                    // 检查并创建inventory_transaction表
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
                    }
                    
                    // 单独添加索引，避免SQL语法错误
                    string[] indexNames = { "idx_product_id", "idx_warehouse_id", "idx_type", "idx_created_at", "idx_reference" };
                    string[] indexColumns = { "product_id", "warehouse_id", "type", "created_at", "reference" };
                    
                    for (int i = 0; i < indexNames.Length; i++)
                    {
                        try
                        {
                            // 先检查索引是否存在
                            string checkIndexSql = $"SHOW INDEX FROM inventory_transaction WHERE Key_name = '{indexNames[i]}';";
                            using (MySqlCommand checkCmd = new MySqlCommand(checkIndexSql, connection))
                            {
                                using (MySqlDataReader reader = checkCmd.ExecuteReader())
                                {
                                    if (!reader.HasRows)
                                    {
                                        reader.Close();
                                        // 创建索引
                                        string createIndexSql = $"CREATE INDEX {indexNames[i]} ON inventory_transaction({indexColumns[i]});";
                                        using (MySqlCommand createIndexCmd = new MySqlCommand(createIndexSql, connection))
                                        {
                                            createIndexCmd.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // 忽略索引创建错误，继续执行
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录错误但不阻止应用程序启动
                Console.WriteLine($"数据库初始化警告: {ex.Message}");
            }
        }

        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionString()
        {
            // 检查配置是否存在
            var connectionStringSettings = ConfigurationManager.ConnectionStrings["MySqlConnectionString"];
            if (connectionStringSettings == null)
            {
                throw new ConfigurationErrorsException("数据库连接字符串配置缺失");
            }
            
            string connectionString = connectionStringSettings.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ConfigurationErrorsException("数据库连接字符串为空");
            }
            
            // 确保连接字符串包含连接超时设置
            if (!connectionString.Contains("Connection Timeout="))
            {
                connectionString += ";Connection Timeout=30";
            }
            
            return connectionString;
        }

        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        /// <returns></returns>
        public static bool TestConnection()
        {
            try
            {
                string connectionString = GetConnectionString();
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public static bool UserExists(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException(nameof(account), "账号不能为空");
            }
            
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
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
                catch (MySqlException ex)
                {
                    throw new Exception($"数据库操作错误: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"查询用户失败: {ex.Message}", ex);
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
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException(nameof(account), "账号不能为空");
            }
            
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "密码不能为空");
            }
            
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // 添加数据库连接对象的空引用检查
                    if (connection == null)
                    {
                        throw new Exception("无法创建数据库连接对象");
                    }
                    
                    // 连接超时已在构造函数中通过连接字符串设置
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
                catch (MySqlException ex)
                {
                    // 详细处理不同类型的MySQL异常
                    if (ex.Number == 1042) // 无法连接到服务器
                    {
                        throw new Exception("无法连接到数据库服务器，请检查网络连接和服务器状态", ex);
                    }
                    else if (ex.Number == 1045) // 访问被拒绝
                    {
                        throw new Exception("数据库访问被拒绝，请检查用户名和密码", ex);
                    }
                    else if (ex.Number == 1049) // 数据库不存在
                    {
                        throw new Exception("指定的数据库不存在，请检查数据库名称", ex);
                    }
                    else
                    {
                        throw new Exception($"数据库操作错误: {ex.Message}", ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"用户验证失败: {ex.Message}", ex);
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
            if (string.IsNullOrEmpty(account))
            {
                throw new ArgumentNullException(nameof(account), "账号不能为空");
            }
            
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password), "密码不能为空");
            }
            
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "姓名不能为空");
            }
            
            string connectionString = GetConnectionString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
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
                catch (MySqlException ex)
                {
                    if (ex.Number == 1062) // 重复键错误
                    {
                        throw new Exception("账号已存在", ex);
                    }
                    else
                    {
                        throw new Exception($"数据库操作错误: {ex.Message}", ex);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"添加用户失败: {ex.Message}", ex);
                }
            }
        }
    }
}