using System;
using System.Collections.Generic;
using System.Data.MySqlClient;
using CSproject.Business.Models;
using CSproject.Data.Helpers;

namespace CSproject.Data.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository()
        {
            _connectionString = DbHelper.GetConnectionString();
        }

        /// <summary>
        /// 获取所有员工
        /// </summary>
        public List<Employee> GetEmployees(string code = null, string name = null, int? departmentId = null, bool? isActive = null)
        {
            var employees = new List<Employee>();
            string query = @"
                SELECT e.id, e.code, e.name, e.department_id AS DepartmentId, e.position, e.gender,
                       e.birth_date AS BirthDate, e.hire_date AS HireDate, e.phone, e.email, e.address,
                       e.is_active AS IsActive, e.created_at AS CreatedAt
                FROM employee e
                WHERE 1=1
            ";

            // 根据条件构建查询
            if (!string.IsNullOrEmpty(code))
            {
                query += " AND e.code LIKE @Code";
            }

            if (!string.IsNullOrEmpty(name))
            {
                query += " AND e.name LIKE @Name";
            }

            if (departmentId.HasValue)
            {
                query += " AND e.department_id = @DepartmentId";
            }

            if (isActive.HasValue)
            {
                query += " AND e.is_active = @IsActive";
            }

            query += " ORDER BY e.created_at DESC";

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

                    if (departmentId.HasValue)
                    {
                        command.Parameters.AddWithValue("@DepartmentId", departmentId.Value);
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
                            employees.Add(MapToEmployee(reader));
                        }
                    }
                }
            }

            return employees;
        }

        /// <summary>
        /// 根据ID获取员工
        /// </summary>
        public Employee GetEmployeeById(int id)
        {
            string query = @"
                SELECT e.id, e.code, e.name, e.department_id AS DepartmentId, e.position, e.gender,
                       e.birth_date AS BirthDate, e.hire_date AS HireDate, e.phone, e.email, e.address,
                       e.is_active AS IsActive, e.created_at AS CreatedAt
                FROM employee e
                WHERE e.id = @Id
            ";

            Employee employee = null;

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
                            employee = MapToEmployee(reader);
                        }
                    }
                }
            }

            return employee;
        }

        /// <summary>
        /// 创建员工
        /// </summary>
        public int CreateEmployee(Employee employee)
        {
            string query = @"
                INSERT INTO employee (code, name, department_id, position, gender, birth_date, hire_date, phone, email, address, is_active, created_at)
                VALUES (@Code, @Name, @DepartmentId, @Position, @Gender, @BirthDate, @HireDate, @Phone, @Email, @Address, @IsActive, @CreatedAt);
                SELECT LAST_INSERT_ID();
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", employee.Code);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@IsActive", employee.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@CreatedAt", employee.CreatedAt);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        /// <summary>
        /// 更新员工
        /// </summary>
        public bool UpdateEmployee(Employee employee)
        {
            string query = @"
                UPDATE employee
                SET code = @Code,
                    name = @Name,
                    department_id = @DepartmentId,
                    position = @Position,
                    gender = @Gender,
                    birth_date = @BirthDate,
                    hire_date = @HireDate,
                    phone = @Phone,
                    email = @Email,
                    address = @Address,
                    is_active = @IsActive
                WHERE id = @Id;
            ";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // 添加参数
                    command.Parameters.AddWithValue("@Code", employee.Code);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Email", employee.Email);
                    command.Parameters.AddWithValue("@Address", employee.Address);
                    command.Parameters.AddWithValue("@IsActive", employee.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@Id", employee.Id);

                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        public bool DeleteEmployee(int id)
        {
            string query = "DELETE FROM employee WHERE id = @Id;";

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
        /// 将DataReader映射到Employee对象
        /// </summary>
        private Employee MapToEmployee(MySqlDataReader reader)
        {
            return new Employee
            {
                Id = Convert.ToInt32(reader["id"]),
                Code = reader["code"].ToString(),
                Name = reader["name"].ToString(),
                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                Position = reader["position"].ToString(),
                Gender = Convert.ToInt32(reader["gender"]),
                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                HireDate = Convert.ToDateTime(reader["HireDate"]),
                Phone = reader["phone"].ToString(),
                Email = reader["email"].ToString(),
                Address = reader["address"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }
    }
}