using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class AccountingSubjectRepository
    {
        /// <summary>
        /// 获取所有会计科目
        /// </summary>
        public List<AccountingSubject> GetAccountingSubjects(string code = null, string name = null, bool? status = null)
        {
            var result = new List<AccountingSubject>();
            string sql = @"SELECT id, code, name, type, parent_id, status, created_at
                            FROM accounting_subject
                            WHERE (@code IS NULL OR code LIKE @code)
                              AND (@name IS NULL OR name LIKE @name)
                              AND (@status IS NULL OR status = @status)
                            ORDER BY code";

            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (code != null)
                        {
                            cmd.Parameters.AddWithValue("@code", $"%{code}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@code", DBNull.Value);
                        }
                        if (name != null)
                        {
                            cmd.Parameters.AddWithValue("@name", $"%{name}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@name", DBNull.Value);
                        }
                        if (status != null)
                        {
                            cmd.Parameters.AddWithValue("@status", status.Value ? 1 : 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@status", DBNull.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new AccountingSubject
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Code = reader["code"].ToString(),
                                    Name = reader["name"].ToString(),
                                    Type = reader["type"].ToString(),
                                    ParentId = reader.IsDBNull(reader.GetOrdinal("parent_id")) ? (int?)null : Convert.ToInt32(reader["parent_id"]),
                                    Status = Convert.ToBoolean(reader["status"]),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"])
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取会计科目列表失败: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取会计科目列表失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取会计科目
        /// </summary>
        public AccountingSubject GetAccountingSubjectById(int id)
        {
            AccountingSubject subject = null;
            string sql = @"SELECT id, code, name, type, parent_id, status, created_at
                            FROM accounting_subject
                            WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        subject = new AccountingSubject
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Code = reader["code"].ToString(),
                            Name = reader["name"].ToString(),
                            Type = reader["type"].ToString(),
                            ParentId = reader.IsDBNull(reader.GetOrdinal("parent_id")) ? (int?)null : Convert.ToInt32(reader["parent_id"]),
                            Status = Convert.ToBoolean(reader["status"]),
                            CreatedAt = Convert.ToDateTime(reader["created_at"])
                        };
                    }
                }
            }
            return subject;
        }

        /// <summary>
        /// 创建会计科目
        /// </summary>
        public int CreateAccountingSubject(AccountingSubject subject)
        {
            string sql = @"INSERT INTO accounting_subject (code, name, type, parent_id, status, created_at)
                            VALUES (@code, @name, @type, @parentId, @status, @createdAt);
                            SELECT LAST_INSERT_ID();";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@code", subject.Code);
                cmd.Parameters.AddWithValue("@name", subject.Name);
                cmd.Parameters.AddWithValue("@type", subject.Type);
                cmd.Parameters.AddWithValue("@parentId", subject.ParentId.HasValue ? (object)subject.ParentId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", subject.Status ? 1 : 0);
                cmd.Parameters.AddWithValue("@createdAt", subject.CreatedAt == DateTime.MinValue ? DateTime.Now : subject.CreatedAt);

                // 执行并返回新插入的会计科目ID
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// 更新会计科目
        /// </summary>
        public bool UpdateAccountingSubject(AccountingSubject subject)
        {
            string sql = @"UPDATE accounting_subject
                            SET code = @code, name = @name, type = @type, parent_id = @parentId, status = @status
                            WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@code", subject.Code);
                cmd.Parameters.AddWithValue("@name", subject.Name);
                cmd.Parameters.AddWithValue("@type", subject.Type);
                cmd.Parameters.AddWithValue("@parentId", subject.ParentId.HasValue ? (object)subject.ParentId.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@status", subject.Status ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", subject.Id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 根据ID删除会计科目
        /// </summary>
        public bool DeleteAccountingSubject(int id)
        {
            string sql = @"DELETE FROM accounting_subject WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@id", id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}