using System;
using System.Collections.Generic;
using System.Data.MySqlClient;
using CSproject.Business.Models;
using CSproject.Data.Helpers;

namespace CSproject.Data.Repositories
{
    public class UnitOfMeasureRepository
    {
        private readonly string _connectionString;

        public UnitOfMeasureRepository()
        {
            _connectionString = DbHelper.GetConnectionString();
        }

        /// <summary>
        /// 获取所有计量单位
        /// </summary>
        public List<UnitOfMeasure> GetUnitOfMeasures(string code = null, string name = null, bool? isActive = null)
        {
            var unitOfMeasures = new List<UnitOfMeasure>();
            string query = @"
                SELECT u.id, u.code, u.name, u.base_unit_id AS BaseUnitId, u.conversion_rate AS ConversionRate,
                       u.is_active AS IsActive, u.created_at AS CreatedAt
                FROM unit_of_measure u
                WHERE 1=1
            ";

            // 根据条件构建查询
            if (!string.IsNullOrEmpty(code))
            {
                query += " AND u.code LIKE @Code";
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND u.name LIKE @Name";
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
                            unitOfMeasures.Add(MapToUnitOfMeasure(reader));
                        }
                    }
                }
            }

            return unitOfMeasures;
        }

        /// <summary>
        /// 根据ID获取计量单位
        /// </summary>
        public UnitOfMeasure GetUnitOfMeasureById(int id)
        {
            string query = @"
                SELECT u.id, u.code, u.name, u.base_unit_id AS BaseUnitId, u.conversion_rate AS ConversionRate,
                       u.is_active AS IsActive, u.created_at AS CreatedAt
                FROM unit_of_measure u
                WHERE u.id = @Id
            ";

            UnitOfMeasure unitOfMeasure = null;

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
                            unitOfMeasure = MapToUnitOfMeasure(reader);
                        }
                    }
                }
            }

            return unitOfMeasure;
        }

        /// <summary>
        /// 创建计量单位
        /// </summary>
        public int CreateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            string query = @"
                INSERT INTO unit_of_measure (code, name, base_unit_id, conversion_rate, is_active, created_at)
                VALUES (@Code, @Name, @BaseUnitId, @ConversionRate, @IsActive, @CreatedAt);
                SELECT LAST_INSERT_ID();
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", unitOfMeasure.Code);
                    command.Parameters.AddWithValue("@Name", unitOfMeasure.Name);
                    command.Parameters.AddWithValue("@BaseUnitId", (object)unitOfMeasure.BaseUnitId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ConversionRate", unitOfMeasure.ConversionRate);
                    command.Parameters.AddWithValue("@IsActive", unitOfMeasure.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@CreatedAt", unitOfMeasure.CreatedAt);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// 更新计量单位
        /// </summary>
        public bool UpdateUnitOfMeasure(UnitOfMeasure unitOfMeasure)
        {
            string query = @"
                UPDATE unit_of_measure
                SET code = @Code,
                    name = @Name,
                    base_unit_id = @BaseUnitId,
                    conversion_rate = @ConversionRate,
                    is_active = @IsActive
                WHERE id = @Id;
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", unitOfMeasure.Code);
                    command.Parameters.AddWithValue("@Name", unitOfMeasure.Name);
                    command.Parameters.AddWithValue("@BaseUnitId", (object)unitOfMeasure.BaseUnitId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ConversionRate", unitOfMeasure.ConversionRate);
                    command.Parameters.AddWithValue("@IsActive", unitOfMeasure.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", unitOfMeasure.Id);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 删除计量单位
        /// </summary>
        public bool DeleteUnitOfMeasure(int id)
        {
            string query = "DELETE FROM unit_of_measure WHERE id = @Id;";

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
        /// 将DataReader映射到UnitOfMeasure对象
        /// </summary>
        private UnitOfMeasure MapToUnitOfMeasure(MySqlDataReader reader)
        {
            return new UnitOfMeasure
            {
                Id = Convert.ToInt32(reader["id"]),
                Code = reader["code"].ToString(),
                Name = reader["name"].ToString(),
                BaseUnitId = reader.IsDBNull(reader.GetOrdinal("BaseUnitId")) ? (int?)null : Convert.ToInt32(reader["BaseUnitId"]),
                ConversionRate = Convert.ToDecimal(reader["ConversionRate"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}