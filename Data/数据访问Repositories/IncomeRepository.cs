using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class IncomeRepository
    {
        /// <summary>
        /// 获取所有收入记录
        /// </summary>
        public List<Income> GetIncomes(string incomeNo = null, int? subjectId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var result = new List<Income>();
            string sql = @"SELECT i.id, i.income_no, i.subject_id, i.amount, i.income_date, i.source, i.description, 
                               i.created_by, i.created_at, 
                               a.code as subject_code, a.name as subject_name,
                               u.name as user_name
                            FROM income i
                            LEFT JOIN accounting_subject a ON i.subject_id = a.id
                            LEFT JOIN user u ON i.created_by = u.id
                            WHERE (@incomeNo IS NULL OR i.income_no LIKE @incomeNo)
                              AND (@subjectId IS NULL OR i.subject_id = @subjectId)
                              AND (@fromDate IS NULL OR i.income_date >= @fromDate)
                              AND (@toDate IS NULL OR i.income_date <= @toDate)
                            ORDER BY i.income_date DESC, i.id DESC";

            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (incomeNo != null)
                        {
                            cmd.Parameters.AddWithValue("@incomeNo", $"%{incomeNo}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@incomeNo", DBNull.Value);
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
                                result.Add(new Income
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    IncomeNo = reader["income_no"].ToString(),
                                    SubjectId = Convert.ToInt32(reader["subject_id"]),
                                    Amount = Convert.ToDecimal(reader["amount"]),
                                    IncomeDate = Convert.ToDateTime(reader["income_date"]),
                                    Source = reader["source"].ToString(),
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
                throw new Exception($"获取收入记录失败: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取收入记录失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取收入记录
        /// </summary>
        public Income GetIncomeById(int id)
        {
            Income income = null;
            string sql = @"SELECT i.id, i.income_no, i.subject_id, i.amount, i.income_date, i.source, i.description, 
                               i.created_by, i.created_at, 
                               a.code as subject_code, a.name as subject_name,
                               u.name as user_name
                            FROM income i
                            LEFT JOIN accounting_subject a ON i.subject_id = a.id
                            LEFT JOIN user u ON i.created_by = u.id
                            WHERE i.id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        income = new Income
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IncomeNo = reader["income_no"].ToString(),
                            SubjectId = Convert.ToInt32(reader["subject_id"]),
                            Amount = Convert.ToDecimal(reader["amount"]),
                            IncomeDate = Convert.ToDateTime(reader["income_date"]),
                            Source = reader["source"].ToString(),
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
            return income;
        }

        /// <summary>
        /// 创建收入记录
        /// </summary>
        public int CreateIncome(Income income)
        {
            string sql = @"INSERT INTO income (income_no, subject_id, amount, income_date, source, description, reference, created_by, created_at)
                            VALUES (@incomeNo, @subjectId, @amount, @incomeDate, @source, @description, @reference, @createdBy, @createdAt);
                            SELECT LAST_INSERT_ID();";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@incomeNo", income.IncomeNo);
                cmd.Parameters.AddWithValue("@subjectId", income.SubjectId);
                cmd.Parameters.AddWithValue("@amount", income.Amount);
                cmd.Parameters.AddWithValue("@incomeDate", income.IncomeDate);
                cmd.Parameters.AddWithValue("@source", income.Source);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(income.Description) ? DBNull.Value : (object)income.Description);
                cmd.Parameters.AddWithValue("@reference", string.IsNullOrEmpty(income.Reference) ? DBNull.Value : (object)income.Reference);
                cmd.Parameters.AddWithValue("@createdBy", income.CreatedBy);
                cmd.Parameters.AddWithValue("@createdAt", income.CreatedAt == DateTime.MinValue ? DateTime.Now : income.CreatedAt);

                // 执行并返回新插入的收入记录ID
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// 更新收入记录
        /// </summary>
        public bool UpdateIncome(Income income)
        {
            string sql = @"UPDATE income
                            SET income_no = @incomeNo, subject_id = @subjectId, amount = @amount, income_date = @incomeDate,
                                source = @source, description = @description, reference = @reference, created_by = @createdBy
                            WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@incomeNo", income.IncomeNo);
                cmd.Parameters.AddWithValue("@subjectId", income.SubjectId);
                cmd.Parameters.AddWithValue("@amount", income.Amount);
                cmd.Parameters.AddWithValue("@incomeDate", income.IncomeDate);
                cmd.Parameters.AddWithValue("@source", income.Source);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(income.Description) ? DBNull.Value : (object)income.Description);
                cmd.Parameters.AddWithValue("@reference", string.IsNullOrEmpty(income.Reference) ? DBNull.Value : (object)income.Reference);
                cmd.Parameters.AddWithValue("@createdBy", income.CreatedBy);
                cmd.Parameters.AddWithValue("@id", income.Id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 根据ID删除收入记录
        /// </summary>
        public bool DeleteIncome(int id)
        {
            string sql = @"DELETE FROM income WHERE id = @id";

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
        /// 获取指定日期范围内的收入总额
        /// </summary>
        public decimal GetTotalIncomeByDateRange(DateTime fromDate, DateTime toDate)
        {
            string sql = @"SELECT COALESCE(SUM(amount), 0) FROM income 
                            WHERE income_date >= @fromDate AND income_date <= @toDate";

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