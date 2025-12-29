using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class AccountReceivableRepository
    {
        public List<AccountReceivable> GetAccountReceivables(string orderNo = null, int? customerId = null, string status = null)
        {
            var result = new List<AccountReceivable>();
            string sql = @"SELECT ar.id AS ReceivableId, ar.order_no AS OrderNo, 
                                 ar.customer_id AS CustomerId, c.name AS CustomerName,
                                 ar.total_amount AS TotalAmount, ar.paid_amount AS PaidAmount,
                                 ar.outstanding_amount AS OutstandingAmount, ar.status AS Status,
                                 ar.order_date AS OrderDate, ar.due_date AS DueDate
                            FROM account_receivable ar
                            INNER JOIN customer c ON ar.customer_id = c.id
                            WHERE (@orderNo IS NULL OR ar.order_no LIKE @orderNo)
                              AND (@customerId IS NULL OR ar.customer_id = @customerId)
                              AND (@status IS NULL OR ar.status = @status)
                            ORDER BY ar.due_date";
            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (orderNo != null)
                        {
                            cmd.Parameters.AddWithValue("@orderNo", $"%{orderNo}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@orderNo", DBNull.Value);
                        }
                        if (customerId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@customerId", customerId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@customerId", DBNull.Value);
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
                            while (reader.Read())
                            {
                                result.Add(new AccountReceivable
                                {
                                    ReceivableId = Convert.ToInt32(reader["ReceivableId"]),
                                    OrderNo = reader["OrderNo"].ToString(),
                                    CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                    CustomerName = reader["CustomerName"].ToString(),
                                    TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                    PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                                    OutstandingAmount = Convert.ToDecimal(reader["OutstandingAmount"]),
                                    Status = reader["Status"].ToString(),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    DueDate = Convert.ToDateTime(reader["DueDate"])
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取应收账款列表失败: {ex.Message}", ex);
            }
        }
        
        public AccountReceivable GetAccountReceivableById(int receivableId)
        {
            AccountReceivable receivable = null;
            string sql = @"SELECT ar.id AS ReceivableId, ar.order_no AS OrderNo, 
                                 ar.customer_id AS CustomerId, c.name AS CustomerName,
                                 ar.total_amount AS TotalAmount, ar.paid_amount AS PaidAmount,
                                 ar.outstanding_amount AS OutstandingAmount, ar.status AS Status,
                                 ar.order_date AS OrderDate, ar.due_date AS DueDate
                            FROM account_receivable ar
                            INNER JOIN customer c ON ar.customer_id = c.id
                            WHERE ar.id = @receivableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@receivableId", receivableId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        receivable = new AccountReceivable
                        {
                            ReceivableId = Convert.ToInt32(reader["ReceivableId"]),
                            OrderNo = reader["OrderNo"].ToString(),
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            CustomerName = reader["CustomerName"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                            OutstandingAmount = Convert.ToDecimal(reader["OutstandingAmount"]),
                            Status = reader["Status"].ToString(),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"])
                        };
                    }
                }
            }
            return receivable;
        }
        
        public int CreateAccountReceivable(AccountReceivable receivable)
        {
            string sql = @"INSERT INTO account_receivable (order_no, customer_id, total_amount, paid_amount, outstanding_amount, status, order_date, due_date)
                            VALUES (@orderNo, @customerId, @totalAmount, @paidAmount, @outstandingAmount, @status, @orderDate, @dueDate);
                            SELECT LAST_INSERT_ID();";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@orderNo", receivable.OrderNo);
                cmd.Parameters.AddWithValue("@customerId", receivable.CustomerId);
                cmd.Parameters.AddWithValue("@totalAmount", receivable.TotalAmount);
                cmd.Parameters.AddWithValue("@paidAmount", receivable.PaidAmount);
                cmd.Parameters.AddWithValue("@outstandingAmount", receivable.OutstandingAmount);
                cmd.Parameters.AddWithValue("@status", receivable.Status ?? "pending");
                cmd.Parameters.AddWithValue("@orderDate", receivable.OrderDate);
                cmd.Parameters.AddWithValue("@dueDate", receivable.DueDate);
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateAccountReceivable(AccountReceivable receivable)
        {
            string sql = @"UPDATE account_receivable 
                            SET order_no = @orderNo, 
                                customer_id = @customerId,
                                total_amount = @totalAmount,
                                paid_amount = @paidAmount,
                                outstanding_amount = @outstandingAmount,
                                status = @status,
                                order_date = @orderDate,
                                due_date = @dueDate
                            WHERE id = @receivableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@orderNo", receivable.OrderNo);
                cmd.Parameters.AddWithValue("@customerId", receivable.CustomerId);
                cmd.Parameters.AddWithValue("@totalAmount", receivable.TotalAmount);
                cmd.Parameters.AddWithValue("@paidAmount", receivable.PaidAmount);
                cmd.Parameters.AddWithValue("@outstandingAmount", receivable.OutstandingAmount);
                cmd.Parameters.AddWithValue("@status", receivable.Status);
                cmd.Parameters.AddWithValue("@orderDate", receivable.OrderDate);
                cmd.Parameters.AddWithValue("@dueDate", receivable.DueDate);
                cmd.Parameters.AddWithValue("@receivableId", receivable.ReceivableId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteAccountReceivable(int receivableId)
        {
            string sql = "DELETE FROM account_receivable WHERE id = @receivableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@receivableId", receivableId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}