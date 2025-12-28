using System;
using System.Collections.Generic;
using System.Data;
using System.Data.MySqlClient;
using CSproject.Business.Models;
using CSproject.Data.Helpers;

namespace CSproject.Data.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DbHelper.GetConnectionString();
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        public List<User> GetUsers(string account = null, string name = null, int? role = null, bool? isActive = null)
        {
            var users = new List<User>();
            string query = @"
                SELECT u.id, u.account, u.password, u.name, u.role, u.is_active AS IsActive, u.created_at AS CreatedAt
                FROM user u
                WHERE 1=1
            ";

            // 根据条件构建查询
            if (!string.IsNullOrEmpty(account))
            {
                query += " AND u.account LIKE @Account";
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND u.name LIKE @Name";
            }

            if (role.HasValue)
            {
                query += " AND u.role = @Role";
            }

            if (isActive.HasValue)
            {
                query += " AND u.is_active = @IsActive";
            }

            query += " ORDER BY u.created_at DESC";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    if (!string.IsNullOrEmpty(account))
                    {
                        command.Parameters.AddWithValue("@Account", $"%{account}%");
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{name}%");
                    }

                    if (role.HasValue)
                    {
                        command.Parameters.AddWithValue("@Role", role.Value);
                    }

                    if (isActive.HasValue)
                    {
                        command.Parameters.AddWithValue("@IsActive", isActive.Value ? 1 : 0);
                    }

                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(MapToUser(reader));
                        }
                    }
                }
            }

            return users;
        }

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        public User GetUserById(int id)
        {
            string query = @"
                SELECT u.id, u.account, u.password, u.name, u.role, u.is_active AS IsActive, u.created_at AS CreatedAt
                FROM user u
                WHERE u.id = @Id
            ";

            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = MapToUser(reader);
                        }
                    }
                }
            }

            return user;
        }

        /// <summary>
        /// 根据账户名获取用户
        /// </summary>
        public User GetUserByAccount(string account)
        {
            string query = @"
                SELECT u.id, u.account, u.password, u.name, u.role, u.is_active AS IsActive, u.created_at AS CreatedAt
                FROM user u
                WHERE u.account = @Account
            ";

            User user = null;

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Account", account);
                    connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = MapToUser(reader);
                        }
                    }
                }
            }

            return user;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreateUser(User user)
        {
            string query = @"
                INSERT INTO user (account, password, name, role, is_active, created_at)
                VALUES (@Account, @Password, @Name, @Role, @IsActive, @CreatedAt);
                SELECT LAST_INSERT_ID();
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Account", user.Account);
                    command.Parameters.AddWithValue("@Password", user.Password); // 注意：在实际应用中应该对密码进行加密
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@CreatedAt", user.CreatedAt);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public bool UpdateUser(User user)
        {
            string query = @"
                UPDATE user
                SET account = @Account,
                    password = @Password,
                    name = @Name,
                    role = @Role,
                    is_active = @IsActive
                WHERE id = @Id;
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Account", user.Account);
                    command.Parameters.AddWithValue("@Password", user.Password); // 注意：在实际应用中应该对密码进行加密
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@IsActive", user.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", user.Id);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public bool DeleteUser(int id)
        {
            string query = "DELETE FROM user WHERE id = @Id;";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 将DataReader映射到User对象
        /// </summary>
        private User MapToUser(MySqlDataReader reader)
        {
            return new User
            {
                Id = Convert.ToInt32(reader["id"]),
                Account = reader["account"].ToString(),
                Password = reader["password"].ToString(),
                Name = reader["name"].ToString(),
                Role = Convert.ToInt32(reader["role"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}