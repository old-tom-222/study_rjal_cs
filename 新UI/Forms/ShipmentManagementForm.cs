using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class ShipmentManagementForm : Form
    {
        private SalesOrderService _salesOrderService;

        public ShipmentManagementForm()
        {
            InitializeComponent();
            _salesOrderService = new SalesOrderService();
            InitializeDataGridView();
            LoadPendingShipmentOrders();
        }

        // 初始化DataGridView列
        private void InitializeDataGridView()
        {
            // 设置DataGridView属性
            dgvPendingShipments.AutoGenerateColumns = false;
            dgvPendingShipments.AllowUserToAddRows = false;
            dgvPendingShipments.ReadOnly = true;
            dgvPendingShipments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 添加列
            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderId",
                HeaderText = "订单ID",
                Width = 80
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderNumber",
                HeaderText = "订单编号",
                Width = 120
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",
                HeaderText = "客户名称",
                Width = 150
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "OrderDate",
                HeaderText = "订单日期",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "订单金额",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductCount",
                HeaderText = "商品种类",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvPendingShipments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CreatedAt",
                HeaderText = "创建时间",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            });

            // 添加操作列
            DataGridViewButtonColumn viewDetailColumn = new DataGridViewButtonColumn
            {
                Name = "ViewDetail",
                HeaderText = "操作",
                Text = "查看详情",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvPendingShipments.Columns.Add(viewDetailColumn);

            DataGridViewButtonColumn shipColumn = new DataGridViewButtonColumn
            {
                Name = "Ship",
                HeaderText = "发货",
                Text = "发货",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvPendingShipments.Columns.Add(shipColumn);

            // 绑定按钮点击事件
            dgvPendingShipments.CellContentClick += dgvPendingShipments_CellContentClick;
        }

        /// <summary>
        /// 加载待发货订单数据
        /// </summary>
        private async void LoadPendingShipmentOrders()
        {
            try
            {
                // 禁用刷新按钮防止重复点击
                btnRefresh.Enabled = false;
                // 显示等待光标
                Cursor = Cursors.WaitCursor;
                
                // 异步加载数据，避免阻塞UI
                var displayData = await Task.Run(() => {
                        // 获取所有已审核但未发货的订单
                        var orders = _salesOrderService.GetSalesOrdersByStatus("已审核");
                        
                        // 创建显示数据的列表
                        return orders.Select(o => new
                        {
                            OrderId = o.OrderId,
                            OrderNumber = o.OrderNumber,
                            CustomerName = o.CustomerName ?? "未知客户",
                            OrderDate = o.OrderDate,
                            TotalAmount = o.TotalAmount,
                            ProductCount = o.OrderItems.Count,
                            CreatedAt = o.CreatedDate
                        }).ToList();
                    });
                
                // 在UI线程上更新DataGridView
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => dgvPendingShipments.DataSource = displayData));
                }
                else
                {
                    dgvPendingShipments.DataSource = displayData;
                }
                
                // 更新统计信息
                UpdateStatistics(displayData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载订单失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 确保统计信息显示为0
                ResetStatistics();
            }
            finally
            {
                // 恢复按钮状态和光标
                btnRefresh.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 更新统计信息
        /// </summary>
        private void UpdateStatistics(object displayDataList)
        {
            try
            {
                // 确保在UI线程上更新控件
                if (this.InvokeRequired)
                {
                    // 传递数据给Invoke方法时使用对象数组
                    this.Invoke(new Action<object>(UpdateStatisticsWorker), displayDataList);
                }
                else
                {
                    UpdateStatisticsWorker(displayDataList);
                }
            }
            catch (Exception ex)
            {
                // 发生异常时重置统计信息
                ResetStatistics();
                
                // 记录异常
                Console.WriteLine($"更新待发货订单统计信息失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 统计信息更新工作方法
        /// </summary>
        private void UpdateStatisticsWorker(object displayDataList)
        {
            // 直接在UI线程上更新统计信息
            UpdateStatisticsOnUI(displayDataList);
        }
        
        /// <summary>
        /// 在UI线程上更新统计信息
        /// </summary>
        private void UpdateStatisticsOnUI(object dataSource)
        {
            try
            {
                // 检查数据源是否有效
                if (dataSource == null)
                {
                    ResetStatistics();
                    return;
                }
                
                // 尝试将数据源转换为列表
                var displayData = dataSource as System.Collections.IEnumerable;
                if (displayData == null)
                {
                    ResetStatistics();
                    return;
                }
                
                // 转换为列表并计算计数
                var items = displayData.Cast<object>().ToList();
                if (items.Count == 0)
                {
                    ResetStatistics();
                    return;
                }
                
                // 更新订单总数
                lblTotalOrders.Text = "待发货订单总数: " + items.Count;
                
                // 计算总金额和产品数量
                decimal totalAmount = 0;
                int productCount = 0;
                
                // 使用反射来访问属性值，避免dynamic类型问题
                foreach (var item in items)
                {
                    try
                    {
                        // 获取TotalAmount属性
                        var amountProperty = item.GetType().GetProperty("TotalAmount");
                        if (amountProperty != null)
                        {
                            var amountValue = amountProperty.GetValue(item);
                            if (amountValue != null && amountValue is decimal)
                            {
                                totalAmount += (decimal)amountValue;
                            }
                        }
                        
                        // 获取ProductCount属性
                        var countProperty = item.GetType().GetProperty("ProductCount");
                        if (countProperty != null)
                        {
                            var countValue = countProperty.GetValue(item);
                            if (countValue != null && countValue is int)
                            {
                                productCount += (int)countValue;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // 单个项目处理失败时继续处理其他项目
                        Console.WriteLine($"处理订单项统计信息时出错: {ex.Message}");
                    }
                }
                
                // 更新显示
                lblTotalAmount.Text = "待发货总金额: " + totalAmount.ToString("F2");
                lblTotalProducts.Text = "待发货商品种类: " + productCount;
            }
            catch (Exception ex)
            {
                // 发生异常时重置统计信息
                ResetStatistics();
                Console.WriteLine($"更新统计显示时出错: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 重置统计信息为0
        /// </summary>
        private void ResetStatistics()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(() => {
                    lblTotalOrders.Text = "待发货订单总数: 0";
                    lblTotalAmount.Text = "待发货总金额: 0.00";
                    lblTotalProducts.Text = "待发货商品种类: 0";
                }));
            }
            else
            {
                lblTotalOrders.Text = "待发货订单总数: 0";
                lblTotalAmount.Text = "待发货总金额: 0.00";
                lblTotalProducts.Text = "待发货商品种类: 0";
            }
        }

        // 搜索按钮点击事件
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchOrders();
        }

        // 搜索订单
        private void SearchOrders()
        {
            try
            {
                string orderNumber = txtOrderNumber.Text.Trim();
                string customerName = txtCustomerName.Text.Trim();
                DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value.Date : (DateTime?)null;
                DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1) : (DateTime?)null;

                // 获取所有已审核订单
                var orders = _salesOrderService.GetSalesOrdersByStatus("已审核");

                // 应用搜索条件 - 订单编号搜索（支持部分匹配）
                if (!string.IsNullOrEmpty(orderNumber))
                {
                    orders = orders.Where(o => o.OrderNumber != null && o.OrderNumber.IndexOf(orderNumber, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // 应用搜索条件 - 客户名称搜索（支持部分匹配，不区分大小写）
                if (!string.IsNullOrEmpty(customerName))
                {
                    orders = orders.Where(o => o.CustomerName != null && o.CustomerName.IndexOf(customerName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }

                // 应用搜索条件 - 日期范围
                if (startDate.HasValue)
                {
                    orders = orders.Where(o => o.OrderDate >= startDate.Value).ToList();
                }

                if (endDate.HasValue)
                {
                    orders = orders.Where(o => o.OrderDate <= endDate.Value).ToList();
                }

                // 创建显示数据的列表
                var displayData = orders.Select(o => new
                {
                    OrderId = o.OrderId,
                    OrderNumber = o.OrderNumber,
                    CustomerName = o.CustomerName ?? "未知客户",
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    ProductCount = o.OrderItems.Count,
                    CreatedAt = o.CreatedDate
                }).ToList();

                // 绑定数据源 - 使用空列表而不是null，确保统计计算正确
                dgvPendingShipments.DataSource = displayData;
                UpdateStatistics(displayData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("搜索订单失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 重置搜索条件
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtOrderNumber.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            LoadPendingShipmentOrders();
        }

        // 刷新按钮点击事件
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingShipmentOrders();
        }

        // DataGridView按钮点击事件
        private void dgvPendingShipments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保点击的是有效行
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    // 直接从DataGridView行获取OrderId值，避免类型转换问题
                    DataGridViewRow row = dgvPendingShipments.Rows[e.RowIndex];
                    int orderId = 0;
                    
                    // 尝试从第一列（OrderId列）获取值
                    if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out orderId))
                    {
                        // 处理查看详情按钮
                        if (e.ColumnIndex == dgvPendingShipments.Columns["ViewDetail"].Index)
                        {
                            ShowOrderDetail(orderId);
                        }
                        // 处理发货按钮
                        else if (e.ColumnIndex == dgvPendingShipments.Columns["Ship"].Index)
                        {
                            ProcessShipment(orderId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("处理按钮点击失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 显示订单详情
        private void ShowOrderDetail(int orderId)
        {
            SalesOrderDetailForm detailForm = new SalesOrderDetailForm(orderId);
            detailForm.ShowDialog();
            // 刷新列表，因为详情页可能进行了状态更改
            LoadPendingShipmentOrders();
        }

        // 处理发货
        private void ProcessShipment(int orderId)
        {
            try
            {
                // 显示发货确认对话框
                string shipmentRemark = PromptForShipmentRemark();
                if (shipmentRemark == null) // 用户取消
                    return;

                // 执行发货操作
                bool success = _salesOrderService.ShipSalesOrder(orderId, shipmentRemark);
                if (success)
                {
                    MessageBox.Show("发货成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPendingShipmentOrders(); // 刷新列表
                }
                else
                {
                    MessageBox.Show("发货失败，请检查订单状态。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发货处理失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 提示用户输入发货备注
        private string PromptForShipmentRemark()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 300;
                prompt.Height = 200;
                prompt.Text = "发货确认";
                prompt.StartPosition = FormStartPosition.CenterScreen;

                Label textLabel = new Label() { Left = 20, Top = 20, Width = 260, Text = "请输入发货备注:" };
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 260, Multiline = true, Height = 60 };
                Button confirmation = new Button() { Text = "确认", Left = 60, Width = 80, Top = 120 };
                Button cancel = new Button() { Text = "取消", Left = 160, Width = 80, Top = 120 };

                confirmation.Click += (sender, e) => { prompt.Close(); };
                cancel.Click += (sender, e) => { inputBox.Text = null; prompt.Close(); };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);

                prompt.ShowDialog();
                return inputBox.Text;
            }
        }

        // 导出数据按钮点击事件
        private void btnExportData_Click(object sender, EventArgs e)
        {
            ExportDataToText();
        }

        // 导出数据为文本文件
        private void ExportDataToText()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "文本文件|*.txt|CSV文件|*.csv|所有文件|*.*";
                saveFileDialog.Title = "导出待发货订单数据";
                saveFileDialog.FileName = $"待发货订单_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    
                    // 添加标题行
                    sb.AppendLine("订单ID,订单编号,客户名称,订单日期,订单金额,商品种类,创建时间");

                    // 添加数据行
                    var displayData = dgvPendingShipments.DataSource as List<dynamic>;
                    if (displayData != null)
                    {
                        foreach (var item in displayData)
                        {
                            sb.AppendLine($"{item.OrderId},{item.OrderNumber},{item.CustomerName},{item.OrderDate:yyyy-MM-dd},{item.TotalAmount},{item.ProductCount},{item.CreatedAt:yyyy-MM-dd HH:mm}");
                        }
                    }

                    // 写入文件
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("数据导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}