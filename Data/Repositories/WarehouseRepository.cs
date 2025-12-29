using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class WarehouseRepository
    {
        public List<Warehouse> GetWarehouses(string warehouseName = null, string status = null)
        {
            var result = new List<Warehouse>();
            string sql = @"SELECT id AS WarehouseId, name AS WarehouseName, address AS Address, 
                                 status AS Status
                            FROM warehouse
                            WHERE (@warehouseName IS NULL OR name LIKE @warehouseName)
                              AND (@status IS NULL OR status = @status)
                            ORDER BY name";
            
            try
            {
                using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
                {
                    conn.Open();
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        if (warehouseName != null)
                        {
                            cmd.Parameters.AddWithValue("@warehouseName", $"%{warehouseName}%");
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@warehouseName", DBNull.Value);
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
                                result.Add(new Warehouse
                                {
                                    WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                                    WarehouseName = reader["WarehouseName"].ToString(),
                                    Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
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
                throw new Exception($"获取仓库列表失败: {ex.Message}", ex);
            }
        }
        
        public Warehouse GetWarehouseById(int warehouseId)
        {
            Warehouse warehouse = null;
            string sql = @"SELECT id AS WarehouseId, name AS WarehouseName, address AS Address, 
                                 status AS Status
                            FROM warehouse
                            WHERE id = @warehouseId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        warehouse = new Warehouse
                        {
                            WarehouseId = Convert.ToInt32(reader["WarehouseId"]),
                            WarehouseName = reader["WarehouseName"].ToString(),
                            Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader["Address"].ToString(),
                            Status = reader["Status"].ToString(),
                            CreatedDate = DateTime.MinValue,
                            LastUpdated = DateTime.MinValue
                        };
                    }
                }
            }
            return warehouse;
        }
        
        public List<Warehouse> GetAllWarehouses()
        {
            return GetWarehouses();
        }
        
        public List<Warehouse> GetActiveWarehouses()
        {
            return GetWarehouses(status: "1");
        }
        
        public int CreateWarehouse(Warehouse warehouse)
        {
            string sql = @"INSERT INTO warehouse (name, address, status)
                            VALUES (@warehouseName, @address, @status);
                            SELECT 573654898;";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@warehouseName", warehouse.WarehouseName);
                cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(warehouse.Address) ? DBNull.Value : (object)warehouse.Address);
                cmd.Parameters.AddWithValue("@status", warehouse.Status ?? "1");
                
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        
        public bool UpdateWarehouse(Warehouse warehouse)
        {
            string sql = @"UPDATE warehouse 
                            SET name = @warehouseName, 
                                address = @address,
                                status = @status
                            WHERE id = @warehouseId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@warehouseName", warehouse.WarehouseName);
                cmd.Parameters.AddWithValue("@address", string.IsNullOrEmpty(warehouse.Address) ? DBNull.Value : (object)warehouse.Address);
                cmd.Parameters.AddWithValue("@status", warehouse.Status);
                cmd.Parameters.AddWithValue("@warehouseId", warehouse.WarehouseId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public bool DeleteWarehouse(int warehouseId)
        {
            string sql = "DELETE FROM warehouse WHERE id = @warehouseId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}