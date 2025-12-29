using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CSproject.Data.Helpers;
using CSproject.Business.Models;

namespace CSproject.Data.Repositories
{
    public class SalesOrderRepository
    {
        public List<SalesOrder> GetSalesOrders(string orderNumber = null, int? customerId = null, string status = null)
        {
            var result = new List<SalesOrder>();
            string sql = @"SELECT o.id, o.order_no, o.created_at, o.customer_id, 
                                   c.name AS customer_name,
                                   o.total_amount, o.status, o.created_by
                            FROM sales_order o
                            INNER JOIN customer c ON c.id = o.customer_id
                            WHERE (@orderNumber IS NULL OR o.order_no LIKE @orderNumber)
                              AND (@customerId IS NULL OR o.customer_id = @customerId)
                              AND (@status IS NULL OR o.status = @status)
                            ORDER BY o.created_at DESC, o.order_no";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                if (orderNumber != null)
                {
                    cmd.Parameters.AddWithValue("@orderNumber", $"%{orderNumber}%");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@orderNumber", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@customerId", (object)customerId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", (object)status ?? DBNull.Value);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SalesOrder
                    {
                        OrderId = Convert.ToInt32(reader["id"]),
                        OrderNumber = reader["order_no"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["created_at"]),
                        CustomerId = Convert.ToInt32(reader["customer_id"]),
                        CustomerName = reader["customer_name"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["total_amount"]),
                        OrderStatus = reader["status"].ToString(),
                        CreatedBy = reader["created_by"].ToString(),
                        CreatedDate = Convert.ToDateTime(reader["created_at"]),
                        Notes = null // 设置为null，因为数据库中没有notes字段
                    });
                    }
                }
            }
            return result;
        }
        
        public SalesOrder GetSalesOrderById(int orderId)
        {
            SalesOrder order = null;
            string sql = @"SELECT o.id, o.order_no, o.created_at, o.customer_id, 
                                   c.name AS customer_name,
                                   o.total_amount, o.status, o.created_by
                            FROM sales_order o
                            INNER JOIN customer c ON c.id = o.customer_id
                            WHERE o.id = @orderId";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@orderId", orderId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new SalesOrder
                        {
                            OrderId = Convert.ToInt32(reader["id"]),
                            OrderNumber = reader["order_no"].ToString(),
                            OrderDate = Convert.ToDateTime(reader["created_at"]),
                            CustomerId = Convert.ToInt32(reader["customer_id"]),
                            CustomerName = reader["customer_name"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["total_amount"]),
                            OrderStatus = reader["status"].ToString(),
                            CreatedBy = reader["created_by"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["created_at"]),
                            Notes = null, // 设置为null，因为数据库中没有notes字段
                            OrderItems = new List<SalesOrderItem>()
                        };
                    }
                }
            }
            
            if (order != null)
            {
                order.OrderItems = GetSalesOrderItems(orderId);
            }
            
            return order;
        }
        
        public List<SalesOrderItem> GetSalesOrderItems(int orderId)
        {
            var result = new List<SalesOrderItem>();
            string sql = @"SELECT oi.id AS order_item_id, oi.order_id, oi.product_id, 
                                   p.name AS product_name,
                                   oi.unit_price, oi.quantity
                            FROM sales_order_item oi
                            INNER JOIN product p ON p.id = oi.product_id
                            WHERE oi.order_id = @orderId
                            ORDER BY oi.id";
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@orderId", orderId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SalesOrderItem
                        {
                            OrderItemId = Convert.ToInt32(reader["order_item_id"]),
                            OrderId = Convert.ToInt32(reader["order_id"]),
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductSku = string.Empty, // product表中不存在sku字段
                            ProductName = reader["product_name"].ToString(),
                            UnitPrice = Convert.ToDecimal(reader["unit_price"]),
                            Quantity = Convert.ToInt32(reader["quantity"]),

                        });
                    }
                }
            }
            return result;
        }
        
        public int CreateSalesOrder(SalesOrder order)
        {
            int orderId;
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string sql = @"INSERT INTO sales_order (order_no, customer_id, warehouse_id, total_amount, 
                                                status, created_by, created_at)
                                        VALUES (@orderNumber, @customerId, 1, @totalAmount, 
                                                '待审核', @createdBy, NOW());
                                        SELECT LAST_INSERT_ID();";
                        
                        using (var cmd = new MySqlCommand(sql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@orderNumber", order.OrderNumber);
                            cmd.Parameters.AddWithValue("@customerId", order.CustomerId);
                            cmd.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
                            cmd.Parameters.AddWithValue("@createdBy", order.CreatedBy);

                            
                            orderId = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        
                        foreach (var item in order.OrderItems)
                        {
                            sql = @"INSERT INTO sales_order_item (order_id, product_id, unit_price, quantity)
                                    VALUES (@orderId, @productId, @unitPrice, @quantity);";
                            
                            using (var cmd = new MySqlCommand(sql, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@orderId", orderId);
                                cmd.Parameters.AddWithValue("@productId", item.ProductId);
                                cmd.Parameters.AddWithValue("@unitPrice", item.UnitPrice);
                                cmd.Parameters.AddWithValue("@quantity", item.Quantity);

                                
                                cmd.ExecuteNonQuery();
                            }
                        }
                        
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            return orderId;
        }
        
        public bool UpdateSalesOrderStatus(int orderId, string status, string userId)
        {
            string sql = "";
            switch (status)
            {
                case "已审核":
                    sql = @"UPDATE sales_order SET status = '已审核' 
                           WHERE id = @orderId AND status = '待审核'";
                    break;
                case "已驳回":
                    sql = @"UPDATE sales_order SET status = '已驳回' 
                           WHERE id = @orderId AND status = '待审核'";
                    break;
                case "已发货":
                    sql = @"UPDATE sales_order SET status = '已发货' 
                           WHERE id = @orderId AND status = '已审核'";
                    break;
                default:
                    return false;
            }
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@orderId", orderId);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        
        public string GenerateOrderNumber()
        {
            string prefix = "SO" + DateTime.Now.ToString("yyyyMMdd");
            string sql = "SELECT MAX(order_no) FROM sales_order WHERE order_no LIKE @prefix";
            
            using (var conn = new MySqlConnection(DbHelper.GetConnectionString()))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@prefix", prefix + "%");
                
                var lastNumber = cmd.ExecuteScalar() as string;
                if (lastNumber == null)
                {
                    return prefix + "001";
                }
                
                int sequence = int.Parse(lastNumber.Substring(prefix.Length)) + 1;
                return prefix + sequence.ToString("000");
            }
        }
    }
}