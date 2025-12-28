using System;
using System.Collections.Generic;
using System.Data.MySqlClient;
using CSproject.Business.Models;
using CSproject.Data.Helpers;

namespace CSproject.Data.Repositories
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository()
        {
            _connectionString = DbHelper.GetConnectionString();
        }

        /// <summary>
        /// 获取所有部门
        /// </summary>
        public List<Department> GetDepartments(string code = null, string name = null, bool? isActive = null)
        {
            var departments = new List<Department>();
            string query = @"
                SELECT d.id, d.code, d.name, d.parent_id AS ParentId, d.manager_id AS ManagerId,
                       d.is_active AS IsActive, d.created_at AS CreatedAt
                FROM department d
                WHERE 1=1
            ";

            // 根据条件构建查询
            if (!string.IsNullOrEmpty(code))
            {
                query += " AND d.code LIKE @Code";
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND d.name LIKE @Name";
            }

            if (isActive.HasValue)
            {
                query += " AND d.is_active = @IsActive";
            }

            query += " ORDER BY d.code";

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
                            departments.Add(MapToDepartment(reader));
                        }
                    }
                }
            }

            return departments;
        }

        /// <summary>
        /// 根据ID获取部门
        /// </summary>
        public Department GetDepartmentById(int id)
        {
            string query = @"
                SELECT d.id, d.code, d.name, d.parent_id AS ParentId, d.manager_id AS ManagerId,
                       d.is_active AS IsActive, d.created_at AS CreatedAt
                FROM department d
                WHERE d.id = @Id
            ";

            Department department = null;

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
                            department = MapToDepartment(reader);
                        }
                    }
                }
            }

            return department;
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public int CreateDepartment(Department department)
        {
            string query = @"
                INSERT INTO department (code, name, parent_id, manager_id, is_active, created_at)
                VALUES (@Code, @Name, @ParentId, @ManagerId, @IsActive, @CreatedAt);
                SELECT LAST_INSERT_ID();
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", department.Code);
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@ParentId", (object)department.ParentId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ManagerId", (object)department.ManagerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", department.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@CreatedAt", department.CreatedAt);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public bool UpdateDepartment(Department department)
        {
            string query = @"
                UPDATE department
                SET code = @Code,
                    name = @Name,
                    parent_id = @ParentId,
                    manager_id = @ManagerId,
                    is_active = @IsActive
                WHERE id = @Id;
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", department.Code);
                    command.Parameters.AddWithValue("@Name", department.Name);
                    command.Parameters.AddWithValue("@ParentId", (object)department.ParentId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ManagerId", (object)department.ManagerId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@IsActive", department.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", department.Id);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public bool DeleteDepartment(int id)
        {
            string query = "DELETE FROM department WHERE id = @Id;";

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
        /// 将DataReader映射到Department对象
        /// </summary>
        private Department MapToDepartment(MySqlDataReader reader)
        {
            return new Department
            {
                Id = Convert.ToInt32(reader["id"]),
                Code = reader["code"].ToString(),
                Name = reader["name"].ToString(),
                ParentId = reader.IsDBNull(reader.GetOrdinal("ParentId")) ? (int?)null : Convert.ToInt32(reader["ParentId"]),
                ManagerId = reader.IsDBNull(reader.GetOrdinal("ManagerId")) ? (int?)null : Convert.ToInt32(reader["ManagerId"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}