using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class InventoryTransactionRepository
    {
        public int AddTransaction(int productId, int warehouseId, int changeQty, string type, string reference, string remark)
        {
            string sql = @"INSERT INTO inventory_transaction(product_id, warehouse_id, change_qty, type, reference, remark)
                           VALUES(@productId, @warehouseId, @changeQty, @type, @reference, @remark);";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@warehouseId", warehouseId);
                cmd.Parameters.AddWithValue("@changeQty", changeQty);
                cmd.Parameters.AddWithValue("@type", type ?? "adjust");
                cmd.Parameters.AddWithValue("@reference", reference ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@remark", remark ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
                using (var idCmd = new MySqlCommand("SELECT LAST_INSERT_ID();", conn))
                {
                    var idObj = idCmd.ExecuteScalar();
                    return Convert.ToInt32(idObj);
                }
            }
        }

        public List<InventoryTransaction> GetTransactions(int? productId = null, int? warehouseId = null, DateTime? from = null, DateTime? to = null)
        {
            var result = new List<InventoryTransaction>();
            string sql = @"SELECT t.id, t.product_id, p.sku, p.name AS product_name,
                                   t.warehouse_id, w.name AS warehouse_name,
                                   t.change_qty, t.type, t.reference, t.remark, t.created_at
                            FROM inventory_transaction t
                            INNER JOIN product p ON p.id = t.product_id
                            INNER JOIN warehouse w ON w.id = t.warehouse_id
                            WHERE (@productId IS NULL OR t.product_id = @productId)
                              AND (@warehouseId IS NULL OR t.warehouse_id = @warehouseId)
                              AND (@from IS NULL OR t.created_at >= @from)
                              AND (@to IS NULL OR t.created_at <= @to)
                            ORDER BY t.created_at DESC";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@productId", (object)productId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@warehouseId", (object)warehouseId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@from", (object)from ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@to", (object)to ?? DBNull.Value);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new InventoryTransaction
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = reader["sku"].ToString(),
                            ProductName = reader["product_name"].ToString(),
                            WarehouseId = Convert.ToInt32(reader["warehouse_id"]),
                            WarehouseName = reader["warehouse_name"].ToString(),
                            ChangeQty = Convert.ToInt32(reader["change_qty"]),
                            Type = reader["type"].ToString(),
                            Reference = reader["reference"] == DBNull.Value ? null : reader["reference"].ToString(),
                            Remark = reader["remark"] == DBNull.Value ? null : reader["remark"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"])
                        });
                    }
                }
            }
            return result;
        }
    }
}