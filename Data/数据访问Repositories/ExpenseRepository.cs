using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class ExpenseRepository
    {
        /// <summary>
        /// 获取所有支出记录
        /// </summary>
        public List<Expense> GetExpenses(string expenseNo = null, int? subjectId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var result = new List<Expense>();
            string sql = @"SELECT e.id, e.expense_no, e.subject_id, e.amount, e.expense_date, e.category, e.description, e.reference, 
                               e.created_by, e.created_at, 
                               a.code as subject_code, a.name as subject_name,
                               u.name as user_name
                            FROM expense e
                            LEFT JOIN accounting_subject a ON e.subject_id = a.id
                            LEFT JOIN user u ON e.created_by = u.id
                            WHERE (@expenseNo IS NULL OR e.expense_no LIKE @expenseNo)
                              AND (@subjectId IS NULL OR e.subject_id = @subjectId)
                              AND (@fromDate IS NULL OR e.expense_date >= @fromDate)
                              AND (@toDate IS NULL OR e.expense_date <= @toDate)
                            ORDER BY e.expense_date DESC, e.id DESC";

            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (expenseNo != null)
                        {
                            cmd.Parameters.AddWithValue("@expenseNo", $"%{expenseNo}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@expenseNo", DBNull.Value);
                        }
                        if (subjectId != null)
                        {
                            cmd.Parameters.AddWithValue("@subjectId", subjectId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@subjectId", DBNull.Value);
                        }
                        if (fromDate != null)
                        {
                            cmd.Parameters.AddWithValue("@fromDate", fromDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fromDate", DBNull.Value);
                        }
                        if (toDate != null)
                        {
                            cmd.Parameters.AddWithValue("@toDate", toDate.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@toDate", DBNull.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new Expense
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    ExpenseNo = reader["expense_no"].ToString(),
                                    SubjectId = Convert.ToInt32(reader["subject_id"]),
                                    Amount = Convert.ToDecimal(reader["amount"]),
                                    ExpenseDate = Convert.ToDateTime(reader["expense_date"]),
                                    Category = reader["category"].ToString(),
                                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader["description"].ToString(),
                                    Reference = reader.IsDBNull(reader.GetOrdinal("reference")) ? null : reader["reference"].ToString(),
                                    CreatedBy = Convert.ToInt32(reader["created_by"]),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                    
                                    // 关联对象
                                    Subject = new AccountingSubject
                                    {
                                        Id = Convert.ToInt32(reader["subject_id"]),
                                        Code = reader["subject_code"].ToString(),
                                        Name = reader["subject_name"].ToString()
                                    },
                                    
                                    Creator = new User
                                    {
                                        Id = Convert.ToInt32(reader["created_by"]),
                                        Name = reader["user_name"].ToString()
                                    }
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取支出记录失败: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取支出记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取支出记录
        /// </summary>
        public Expense GetExpenseById(int id)
        {
            Expense expense = null;
            string sql = @"SELECT e.id, e.expense_no, e.subject_id, e.amount, e.expense_date, e.category, e.description, e.reference, 
                               e.created_by, e.created_at, 
                               a.code as subject_code, a.name as subject_name,
                               u.name as user_name
                            FROM expense e
                            LEFT JOIN accounting_subject a ON e.subject_id = a.id
                            LEFT JOIN user u ON e.created_by = u.id
                            WHERE e.id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        expense = new Expense
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ExpenseNo = reader["expense_no"].ToString(),
                            SubjectId = Convert.ToInt32(reader["subject_id"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            ExpenseDate = Convert.ToDateTime(reader["expense_date"]),
                            Category = reader["category"].ToString(),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader["description"].ToString(),
                            Reference = reader.IsDBNull(reader.GetOrdinal("reference")) ? null : reader["reference"].ToString(),
                            CreatedBy = Convert.ToInt32(reader["created_by"]),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            
                            // 关联对象
                            Subject = new AccountingSubject
                            {
                                Id = Convert.ToInt32(reader["subject_id"]),
                                Code = reader["subject_code"].ToString(),
                                Name = reader["subject_name"].ToString()
                            },
                            
                            Creator = new User
                            {
                                Id = Convert.ToInt32(reader["created_by"]),
                                Name = reader["user_name"].ToString()
                            }
                        };
                    }
                }
            }
            return expense;
        }

        /// <summary>
        /// 创建支出记录
        /// </summary>
        public int CreateExpense(Expense expense)
        {
            string sql = @"INSERT INTO expense (expense_no, subject_id, amount, expense_date, category, description, reference, created_by, created_at)
                            VALUES (@expenseNo, @subjectId, @amount, @expenseDate, @category, @description, @reference, @createdBy, @createdAt);
                            SELECT LAST_INSERT_ID();";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@expenseNo", expense.ExpenseNo);
                cmd.Parameters.AddWithValue("@subjectId", expense.SubjectId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@expenseDate", expense.ExpenseDate);
                cmd.Parameters.AddWithValue("@category", expense.Category);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(expense.Description) ? DBNull.Value : (object)expense.Description);
                cmd.Parameters.AddWithValue("@reference", string.IsNullOrEmpty(expense.Reference) ? DBNull.Value : (object)expense.Reference);
                cmd.Parameters.AddWithValue("@createdBy", expense.CreatedBy);
                cmd.Parameters.AddWithValue("@createdAt", expense.CreatedAt == DateTime.MinValue ? DateTime.Now : expense.CreatedAt);

                // 执行并返回新插入的支出记录ID
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// 更新支出记录
        /// </summary>
        public bool UpdateExpense(Expense expense)
        {
            string sql = @"UPDATE expense
                            SET expense_no = @expenseNo, subject_id = @subjectId, amount = @amount, expense_date = @expenseDate,
                                category = @category, description = @description, reference = @reference, created_by = @createdBy
                            WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@expenseNo", expense.ExpenseNo);
                cmd.Parameters.AddWithValue("@subjectId", expense.SubjectId);
                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@expenseDate", expense.ExpenseDate);
                cmd.Parameters.AddWithValue("@category", expense.Category);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(expense.Description) ? DBNull.Value : (object)expense.Description);
                cmd.Parameters.AddWithValue("@reference", string.IsNullOrEmpty(expense.Reference) ? DBNull.Value : (object)expense.Reference);
                cmd.Parameters.AddWithValue("@createdBy", expense.CreatedBy);
                cmd.Parameters.AddWithValue("@id", expense.Id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 根据ID删除支出记录
        /// </summary>
        public bool DeleteExpense(int id)
        {
            string sql = @"DELETE FROM expense WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 获取指定日期范围内的支出总额
        /// </summary>
        public decimal GetTotalExpenseByDateRange(DateTime fromDate, DateTime toDate)
        {
            string sql = @"SELECT COALESCE(SUM(amount), 0) FROM expense 
                            WHERE expense_date >= @fromDate AND expense_date <= @toDate";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
    }
}