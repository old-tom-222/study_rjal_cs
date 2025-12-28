using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class BankAccountRepository
    {
        /// <summary>
        /// 获取所有银行账户
        /// </summary>
        public List<BankAccount> GetBankAccounts(string bankName = null, string accountName = null, bool? isActive = null)
        {
            var result = new List<BankAccount>();
            string sql = @"SELECT id, bank_name, account_name, account_number, current_balance, status, description, created_at, updated_at
                            FROM bank_account
                            WHERE (@bankName IS NULL OR bank_name LIKE @bankName)
                              AND (@accountName IS NULL OR account_name LIKE @accountName)
                              AND (@isActive IS NULL OR status = @isActive)
                            ORDER BY bank_name, account_name";

            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (bankName != null)
                        {
                            cmd.Parameters.AddWithValue("@bankName", $"%{bankName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@bankName", DBNull.Value);
                        }
                        if (accountName != null)
                        {
                            cmd.Parameters.AddWithValue("@accountName", $"%{accountName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@accountName", DBNull.Value);
                        }
                        if (isActive != null)
                        {
                            cmd.Parameters.AddWithValue("@isActive", isActive.Value ? 1 : 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@isActive", DBNull.Value);
                        }

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new BankAccount
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    BankName = reader["bank_name"].ToString(),
                                    AccountName = reader["account_name"].ToString(),
                                    AccountNumber = reader["account_number"].ToString(),
                                    CurrentBalance = Convert.ToDecimal(reader["current_balance"]),
                                    Status = Convert.ToBoolean(reader["status"]),
                                    Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader["description"].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                    UpdatedAt = reader["updated_at"] != DBNull.Value ? Convert.ToDateTime(reader["updated_at"]) : DateTime.MinValue
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取银行账户失败: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"获取银行账户失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 根据ID获取银行账户
        /// </summary>
        public BankAccount GetBankAccountById(int id)
        {
            BankAccount bankAccount = null;
            string sql = @"SELECT id, bank_name, account_name, account_number, current_balance, status, description, created_at, updated_at
                            FROM bank_account
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
                        bankAccount = new BankAccount
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            BankName = reader["bank_name"].ToString(),
                            AccountName = reader["account_name"].ToString(),
                            AccountNumber = reader["account_number"].ToString(),
                            CurrentBalance = Convert.ToDecimal(reader["current_balance"]),
                            Status = Convert.ToBoolean(reader["status"]),
                            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader["description"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            UpdatedAt = reader["updated_at"] != DBNull.Value ? Convert.ToDateTime(reader["updated_at"]) : DateTime.MinValue
                        };
                    }
                }
            }
            return bankAccount;
        }

        /// <summary>
        /// 创建银行账户
        /// </summary>
        public int CreateBankAccount(BankAccount bankAccount)
        {
            string sql = @"INSERT INTO bank_account (bank_name, account_name, account_number, current_balance, status, description, created_at, updated_at)
                            VALUES (@bankName, @accountName, @accountNumber, @currentBalance, @status, @description, @createdAt, @updatedAt);
                            SELECT LAST_INSERT_ID();";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@bankName", bankAccount.BankName);
                cmd.Parameters.AddWithValue("@accountName", bankAccount.AccountName);
                cmd.Parameters.AddWithValue("@accountNumber", bankAccount.AccountNumber);
                cmd.Parameters.AddWithValue("@currentBalance", bankAccount.CurrentBalance);
                cmd.Parameters.AddWithValue("@status", bankAccount.Status ? 1 : 0);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(bankAccount.Description) ? DBNull.Value : (object)bankAccount.Description);
                cmd.Parameters.AddWithValue("@createdAt", bankAccount.CreatedAt == DateTime.MinValue ? DateTime.Now : bankAccount.CreatedAt);
                cmd.Parameters.AddWithValue("@updatedAt", bankAccount.UpdatedAt == DateTime.MinValue ? DateTime.Now : bankAccount.UpdatedAt);

                // 执行并返回新插入的银行账户ID
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>
        /// 更新银行账户
        /// </summary>
        public bool UpdateBankAccount(BankAccount bankAccount)
        {
            string sql = @"UPDATE bank_account
                            SET bank_name = @bankName, account_name = @accountName, account_number = @accountNumber,
                                current_balance = @currentBalance, status = @status, description = @description, updated_at = @updatedAt
                            WHERE id = @id";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();

                cmd.Parameters.AddWithValue("@bankName", bankAccount.BankName);
                cmd.Parameters.AddWithValue("@accountName", bankAccount.AccountName);
                cmd.Parameters.AddWithValue("@accountNumber", bankAccount.AccountNumber);
                cmd.Parameters.AddWithValue("@currentBalance", bankAccount.CurrentBalance);
                cmd.Parameters.AddWithValue("@status", bankAccount.Status ? 1 : 0);
                cmd.Parameters.AddWithValue("@description", string.IsNullOrEmpty(bankAccount.Description) ? DBNull.Value : (object)bankAccount.Description);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", bankAccount.Id);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// 根据ID删除银行账户
        /// </summary>
        public bool DeleteBankAccount(int id)
        {
            string sql = @"DELETE FROM bank_account WHERE id = @id";

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
        /// 更新银行账户余额
        /// </summary>
        public bool UpdateBankAccountBalance(int accountId, decimal amount)
        {
            string sql = @"UPDATE bank_account
                            SET current_balance = current_balance + @amount, updated_at = @updatedAt
                            WHERE id = @accountId";

            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@accountId", accountId);

                // 执行并返回受影响的行数
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}