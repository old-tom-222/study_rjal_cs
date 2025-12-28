using System;
using System.Collections.Generic;
using System.Data.MySqlClient;
using CSproject.Business.Models;
using CSproject.Data.Helpers;

namespace CSproject.Data.Repositories
{
    public class PaymentMethodRepository
    {
        private readonly string _connectionString;

        public PaymentMethodRepository()
        {
            _connectionString = DbHelper.GetConnectionString();
        }

        /// <summary>
        /// 获取所有付款方式
        /// </summary>
        public List<PaymentMethod> GetPaymentMethods(string code = null, string name = null, bool? isActive = null)
        {
            var paymentMethods = new List<PaymentMethod>();
            string query = @"
                SELECT pm.id, pm.code, pm.name, pm.description,
                       pm.is_active AS IsActive, pm.created_at AS CreatedAt
                FROM payment_method pm
                WHERE 1=1
            ";

            // 根据条件构建查询
            if (!string.IsNullOrEmpty(code))
            {
                query += " AND pm.code LIKE @Code";
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND pm.name LIKE @Name";
            }

            if (isActive.HasValue)
            {
                query += " AND pm.is_active = @IsActive";
            }

            query += " ORDER BY pm.created_at DESC";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    if (!string.IsNullOrEmpty(code))
                    {
                        command.Parameters.AddWithValue("@Code", $"%{code}%");
                    }

                    if (!string.IsNullOrEmpty(name))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{name}%");
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
                            paymentMethods.Add(MapToPaymentMethod(reader));
                        }
                    }
                }
            }

            return paymentMethods;
        }

        /// <summary>
        /// 根据ID获取付款方式
        /// </summary>
        public PaymentMethod GetPaymentMethodById(int id)
        {
            string query = @"
                SELECT pm.id, pm.code, pm.name, pm.description,
                       pm.is_active AS IsActive, pm.created_at AS CreatedAt
                FROM payment_method pm
                WHERE pm.id = @Id
            ";

            PaymentMethod paymentMethod = null;

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
                            paymentMethod = MapToPaymentMethod(reader);
                        }
                    }
                }
            }

            return paymentMethod;
        }

        /// <summary>
        /// 创建付款方式
        /// </summary>
        public int CreatePaymentMethod(PaymentMethod paymentMethod)
        {
            string query = @"
                INSERT INTO payment_method (code, name, description, is_active, created_at)
                VALUES (@Code, @Name, @Description, @IsActive, @CreatedAt);
                SELECT LAST_INSERT_ID();
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", paymentMethod.Code);
                    command.Parameters.AddWithValue("@Name", paymentMethod.Name);
                    command.Parameters.AddWithValue("@Description", paymentMethod.Description);
                    command.Parameters.AddWithValue("@IsActive", paymentMethod.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@CreatedAt", paymentMethod.CreatedAt);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// 更新付款方式
        /// </summary>
        public bool UpdatePaymentMethod(PaymentMethod paymentMethod)
        {
            string query = @"
                UPDATE payment_method
                SET code = @Code,
                    name = @Name,
                    description = @Description,
                    is_active = @IsActive
                WHERE id = @Id;
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", paymentMethod.Code);
                    command.Parameters.AddWithValue("@Name", paymentMethod.Name);
                    command.Parameters.AddWithValue("@Description", paymentMethod.Description);
                    command.Parameters.AddWithValue("@IsActive", paymentMethod.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", paymentMethod.Id);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 删除付款方式
        /// </summary>
        public bool DeletePaymentMethod(int id)
        {
            string query = "DELETE FROM payment_method WHERE id = @Id;";

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
        /// 将DataReader映射到PaymentMethod对象
        /// </summary>
        private PaymentMethod MapToPaymentMethod(MySqlDataReader reader)
        {
            return new PaymentMethod
            {
                Id = Convert.ToInt32(reader["id"]),
                Code = reader["code"].ToString(),
                Name = reader["name"].ToString(),
                Description = reader["description"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}