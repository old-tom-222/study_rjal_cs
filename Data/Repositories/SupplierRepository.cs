using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class SupplierRepository
    {
        public List<Supplier> GetSuppliers(string supplierName = null, string contactPerson = null, string status = null)
        {
            var result = new List<Supplier>();
            string sql = @"SELECT id AS SupplierId, name AS SupplierName, contact_person AS ContactPerson, 
                                 phone AS Phone, status AS Status
                            FROM supplier
                            WHERE (@supplierName IS NULL OR name LIKE @supplierName)
                              AND (@contactPerson IS NULL OR contact_person LIKE @contactPerson)
                              AND (@status IS NULL OR status = @status)
                            ORDER BY name";
            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (supplierName != null)
                        {
                            cmd.Parameters.AddWithValue("@supplierName", $"%{supplierName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@supplierName", DBNull.Value);
                        }
                        if (contactPerson != null)
                        {
                            cmd.Parameters.AddWithValue("@contactPerson", $"%{contactPerson}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@contactPerson", DBNull.Value);
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
                                result.Add(new Supplier
                                {
                                    SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                    SupplierName = reader["SupplierName"].ToString(),
                                    ContactPerson = reader.IsDBNull(reader.GetOrdinal("ContactPerson")) ? null : reader["ContactPerson"].ToString(),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader["Phone"].ToString(),
                                    Status = reader["Status"].ToString(),
                                    CreatedDate = DateTime.MinValue,
                                    LastUpdated = DateTime.MinValue
                                });
                            }
                        }
                    }
                }
                return result;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"获取供应商列表失败: {ex.Message}", ex);
            }
        }
        
        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = null;
            string sql = @"SELECT id AS SupplierId, name AS SupplierName, contact_person AS ContactPerson, 
                                 phone AS Phone, status AS Status
                            FROM supplier
                            WHERE id = @supplierId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supplier = new Supplier
                        {
                            SupplierId = Convert.ToInt32(reader["SupplierId"]),
                            SupplierName = reader["SupplierName"].ToString(),
                            ContactPerson = reader.IsDBNull(reader.GetOrdinal("ContactPerson")) ? null : reader["ContactPerson"].ToString(),
                            Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? null : reader["Phone"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.MinValue,
                            LastUpdated = DateTime.MinValue
                        };
                    }
                }
            }
            return supplier;
        }
        
        public List<Supplier> GetAllSuppliers()
        {
            return GetSuppliers();
        }
        
        public List<Supplier> GetActiveSuppliers()
        {
            return GetSuppliers(status: "1");
        }
        
        public int CreateSupplier(Supplier supplier)
        {
            string sql = @"INSERT INTO supplier (name, contact_person, phone, status)
                            VALUES (@supplierName, @contactPerson, @phone, @status);
                            SELECT 573654896;";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@supplierName", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@contactPerson", string.IsNullOrEmpty(supplier.ContactPerson) ? DBNull.Value : (object)supplier.ContactPerson);
                cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : (object)supplier.Phone);
                cmd.Parameters.AddWithValue("@status", supplier.Status ?? "1");
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateSupplier(Supplier supplier)
        {
            string sql = @"UPDATE supplier 
                            SET name = @supplierName, 
                                contact_person = @contactPerson, 
                                phone = @phone,
                                status = @status
                            WHERE id = @supplierId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@supplierName", supplier.SupplierName);
                cmd.Parameters.AddWithValue("@contactPerson", string.IsNullOrEmpty(supplier.ContactPerson) ? DBNull.Value : (object)supplier.ContactPerson);
                cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(supplier.Phone) ? DBNull.Value : (object)supplier.Phone);
                cmd.Parameters.AddWithValue("@status", supplier.Status);
                cmd.Parameters.AddWithValue("@supplierId", supplier.SupplierId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteSupplier(int supplierId)
        {
            string sql = "DELETE FROM supplier WHERE id = @supplierId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@supplierId", supplierId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}