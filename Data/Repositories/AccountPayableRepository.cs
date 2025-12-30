using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class AccountPayableRepository
    {
        public List<AccountPayable> GetAccountPayables(string orderNo = null, int? supplierId = null, string status = null)
        {
            var result = new List<AccountPayable>();
            string sql = @"SELECT ap.id AS PayableId, ap.order_no AS OrderNo, 
                                 ap.supplier_id AS SupplierId, s.name AS SupplierName,
                                 ap.total_amount AS TotalAmount, ap.paid_amount AS PaidAmount,
                                 ap.outstanding_amount AS OutstandingAmount, ap.status AS Status,
                                 ap.order_date AS OrderDate, ap.due_date AS DueDate
                            FROM account_payable ap
                            INNER JOIN supplier s ON ap.supplier_id = s.id
                            WHERE (@orderNo IS NULL OR ap.order_no LIKE @orderNo)
                              AND (@supplierId IS NULL OR ap.supplier_id = @supplierId)
                              AND (@status IS NULL OR ap.status = @status)
                            ORDER BY ap.due_date";
            
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
                        if (supplierId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@supplierId", supplierId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@supplierId", DBNull.Value);
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
                                result.Add(new AccountPayable
                                {
                                    PayableId = Convert.ToInt32(reader["PayableId"]),
                                    OrderNo = reader["OrderNo"].ToString(),
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader["SupplierName"].ToString(),
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
                throw new Exception($"获取应付账款列表失败: {ex.Message}", ex);
            }
        }
        
        public AccountPayable GetAccountPayableById(int payableId)
        {
            AccountPayable payable = null;
            string sql = @"SELECT ap.id AS PayableId, ap.order_no AS OrderNo, 
                                 ap.supplier_id AS SupplierId, s.name AS SupplierName,
                                 ap.total_amount AS TotalAmount, ap.paid_amount AS PaidAmount,
                                 ap.outstanding_amount AS OutstandingAmount, ap.status AS Status,
                                 ap.order_date AS OrderDate, ap.due_date AS DueDate
                            FROM account_payable ap
                            INNER JOIN supplier s ON ap.supplier_id = s.id
                            WHERE ap.id = @payableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@payableId", payableId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payable = new AccountPayable
                        {
                            PayableId = Convert.ToInt32(reader["PayableId"]),
                            OrderNo = reader["OrderNo"].ToString(),
                            SupplierId = Convert.ToInt32(reader["SupplierId"]),
                            SupplierName = reader["SupplierName"].ToString(),
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
            return payable;
        }
        
        public int CreateAccountPayable(AccountPayable payable)
        {
            string sql = @"INSERT INTO account_payable (order_no, supplier_id, total_amount, paid_amount, outstanding_amount, status, order_date, due_date)
                            VALUES (@orderNo, @supplierId, @totalAmount, @paidAmount, @outstandingAmount, @status, @orderDate, @dueDate);
                            SELECT LAST_INSERT_ID();";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@orderNo", payable.OrderNo);
                cmd.Parameters.AddWithValue("@supplierId", payable.SupplierId);
                cmd.Parameters.AddWithValue("@totalAmount", payable.TotalAmount);
                cmd.Parameters.AddWithValue("@paidAmount", payable.PaidAmount);
                cmd.Parameters.AddWithValue("@outstandingAmount", payable.OutstandingAmount);
                cmd.Parameters.AddWithValue("@status", payable.Status ?? "pending");
                cmd.Parameters.AddWithValue("@orderDate", payable.OrderDate);
                cmd.Parameters.AddWithValue("@dueDate", payable.DueDate);
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateAccountPayable(AccountPayable payable)
        {
            string sql = @"UPDATE account_payable 
                            SET order_no = @orderNo, 
                                supplier_id = @supplierId,
                                total_amount = @totalAmount,
                                paid_amount = @paidAmount,
                                outstanding_amount = @outstandingAmount,
                                status = @status,
                                order_date = @orderDate,
                                due_date = @dueDate
                            WHERE id = @payableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@orderNo", payable.OrderNo);
                cmd.Parameters.AddWithValue("@supplierId", payable.SupplierId);
                cmd.Parameters.AddWithValue("@totalAmount", payable.TotalAmount);
                cmd.Parameters.AddWithValue("@paidAmount", payable.PaidAmount);
                cmd.Parameters.AddWithValue("@outstandingAmount", payable.OutstandingAmount);
                cmd.Parameters.AddWithValue("@status", payable.Status);
                cmd.Parameters.AddWithValue("@orderDate", payable.OrderDate);
                cmd.Parameters.AddWithValue("@dueDate", payable.DueDate);
                cmd.Parameters.AddWithValue("@payableId", payable.PayableId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteAccountPayable(int payableId)
        {
            string sql = "DELETE FROM account_payable WHERE id = @payableId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@payableId", payableId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}