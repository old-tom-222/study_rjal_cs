using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class CustomerRepository
    {
        public List<Customer> GetCustomers(string customerCode = null, string customerName = null, string status = null)
        {
            var result = new List<Customer>();
            string sql = @"SELECT id, name, contact_person, phone AS ContactPhone, credit_limit, status
                            FROM customer
                            WHERE (@customerName IS NULL OR name LIKE @customerName)
                              AND (@status IS NULL OR status = @status)
                            ORDER BY name";
            

            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (customerName != null)
                        {
                            cmd.Parameters.AddWithValue("@customerName", $"%{customerName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@customerName", DBNull.Value);
                        }
                        if (status != null)
                        {
                            cmd.Parameters.AddWithValue("@status", status);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@status", DBNull.Value);
                        }
                        
                        using (var reader = cmd.ExecuteReader())
                        {
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                rowCount++;
                                
                                result.Add(new Customer
                                {
                                    CustomerId = Convert.ToInt32(reader["id"]),
                                    CustomerName = reader["name"].ToString(),
                                    ContactPerson = reader.IsDBNull(reader.GetOrdinal("contact_person")) ? null : reader["contact_person"].ToString(),
                                    ContactPhone = reader.IsDBNull(reader.GetOrdinal("ContactPhone")) ? null : reader["ContactPhone"].ToString(),
                                    Status = reader.IsDBNull(reader.GetOrdinal("status")) ? "" : reader["status"].ToString(),
                                    // 设置数据库中不存在的字段为null或默认值
                                    CustomerCode = null,
                                    Email = null,
                                    Address = null,
                                    City = null,
                                    Province = null,
                                    PostalCode = null,
                                    CustomerType = null,
                                    CreatedDate = DateTime.MinValue,
                                    LastUpdated = DateTime.MinValue,
                                    Notes = null
                                });
                            }

                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取客户列表失败: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取客户列表失败: {ex.Message}", ex);
            }
        }
        
        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;
            string sql = @"SELECT id, name, contact_person, phone AS ContactPhone, credit_limit, status
                            FROM customer
                            WHERE id = @customerId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@customerId", customerId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        customer = new Customer
                        {
                            CustomerId = Convert.ToInt32(reader["id"]),
                            CustomerName = reader["name"].ToString(),
                            ContactPerson = reader.IsDBNull(reader.GetOrdinal("contact_person")) ? null : reader["contact_person"].ToString(),
                            ContactPhone = reader.IsDBNull(reader.GetOrdinal("ContactPhone")) ? null : reader["ContactPhone"].ToString(),
                            Status = reader.IsDBNull(reader.GetOrdinal("status")) ? "" : reader["status"].ToString(),
                            // 设置数据库中不存在的字段为null或默认值
                            CustomerCode = null,
                            Email = null,
                            Address = null,
                            City = null,
                            Province = null,
                            PostalCode = null,
                            CustomerType = null,
                            CreatedDate = DateTime.MinValue,
                            LastUpdated = DateTime.MinValue,
                            Notes = null
                        };
                    }
                }
            }
            return customer;
        }
        
        public List<Customer> GetActiveCustomers()
        {
            return GetCustomers(status: "1");
        }

        /// <summary>
        /// 创建新客户
        /// </summary>
        public int CreateCustomer(Customer customer)
        {
            string sql = @"INSERT INTO customer (name, contact_person, phone, credit_limit, status)
                            VALUES (@name, @contactPerson, @phone, @creditLimit, @status);
                            SELECT LAST_INSERT_ID();";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@name", string.IsNullOrEmpty(customer.CustomerName) ? DBNull.Value : (object)customer.CustomerName);
                cmd.Parameters.AddWithValue("@contactPerson", string.IsNullOrEmpty(customer.ContactPerson) ? DBNull.Value : (object)customer.ContactPerson);
                cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(customer.ContactPhone) ? DBNull.Value : (object)customer.ContactPhone);
                // 使用默认信用额度值
                cmd.Parameters.AddWithValue("@creditLimit", 10000); // 默认信用额度为10000
                // 使用默认状态值为1（假设1表示活跃）
                cmd.Parameters.AddWithValue("@status", string.IsNullOrEmpty(customer.Status) ? "1" : customer.Status);

                // 执行并返回新插入的客户ID
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
    }
}