using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class SalesOrderListForm : Form
    {
        private SalesOrderService _orderService = new SalesOrderService();

        public SalesOrderListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void SalesOrderListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有订单
            LoadSalesOrders();
            // 设置日期范围为最近一个月
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void InitializeDataGridView()
        {
            // 配置DataGridView的列
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();

            // 订单ID列（隐藏）
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderId",
                DataPropertyName = "OrderId",
                Visible = false
            });

            // 订单号列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderNumber",
                HeaderText = "订单号",
                DataPropertyName = "OrderNumber",
                Width = 120
            });

            // 客户名列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerName",
                HeaderText = "客户",
                DataPropertyName = "CustomerName",
                Width = 150
            });

            // 订单日期列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderDate",
                HeaderText = "订单日期",
                DataPropertyName = "OrderDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            // 订单金额列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                HeaderText = "订单金额",
                DataPropertyName = "TotalAmount",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });

            // 订单状态列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "订单状态",
                DataPropertyName = "OrderStatus",
                Width = 100
            });

            // 创建人列
            dgvOrders.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedBy",
                HeaderText = "创建人",
                DataPropertyName = "CreatedBy",
                Width = 100
            });

            // 操作列
            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "ViewDetails",
                HeaderText = "操作",
                Text = "查看详情",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvOrders.Columns.Add(actionColumn);

            // 设置行高
            dgvOrders.RowTemplate.Height = 30;
        }

        private void LoadSalesOrders()
        {;
            try
            {
                // 显示加载中
                Cursor.Current = Cursors.WaitCursor;
                
                // 首先测试数据库连接
                bool connectionOk = CSproject.Data.Helpers.DbHelper.TestConnection();
                
                // 获取搜索条件
                string orderNumber = txtOrderNumber.Text?.Trim();
                string customerName = txtCustomerName.Text?.Trim();
                string status = cmbStatus.SelectedItem != null && !string.IsNullOrEmpty(cmbStatus.SelectedItem.ToString()) && cmbStatus.SelectedItem.ToString() != "全部" 
                    ? cmbStatus.SelectedItem.ToString() 
                    : null;
                
                // 获取销售订单列表（使用服务的过滤功能）
                List<SalesOrder> orders = _orderService.GetSalesOrders(orderNumber, null, status);
                
                // 移除诊断信息弹窗
                
                // 根据日期范围筛选（修改为包含结束日期的完整24小时）
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1); // 包含结束日期的23:59:59
                orders = orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
                
                // 增强客户名称搜索（不区分大小写）
                if (!string.IsNullOrEmpty(customerName))
                {
                    orders = orders.Where(o => o.CustomerName != null && 
                                              o.CustomerName.IndexOf(customerName, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
                }
                
                // 绑定到DataGridView
                dgvOrders.DataSource = orders;
                
                // 更新统计信息
                UpdateStatistics(orders);
            }
            catch (Exception ex)
            {
                // 详细记录错误信息
                string errorMsg = $"加载订单失败: {ex.Message}\n\n堆栈跟踪:\n{ex.StackTrace}";
                MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void UpdateStatistics(List<SalesOrder> orders)
        {
            // 计算统计信息
            decimal totalAmount = orders.Sum(o => o.TotalAmount);
            int orderCount = orders.Count;
            
            // 更新标签显示
            lblTotalOrders.Text = "订单总数: " + orderCount;
            lblTotalAmount.Text = "总金额: " + totalAmount.ToString("N2");
        }

        private void BtnSearchClick(object sender, EventArgs e)
        {
            // 搜索订单
            LoadSalesOrders();
        }

        private void BtnCreateOrderClick(object sender, EventArgs e)
        {
            // 打开新建订单页面
            using (var createOrderForm = new SalesOrderCreateForm())
            {
                if (createOrderForm.ShowDialog() == DialogResult.OK)
                {
                    // 刷新订单列表
                    LoadSalesOrders();
                }
            }
        }

        private void BtnRefreshClick(object sender, EventArgs e)
        {
            // 刷新订单列表
            LoadSalesOrders();
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理查看详情按钮点击
            if (e.ColumnIndex == dgvOrders.Columns["ViewDetails"].Index && e.RowIndex >= 0)
            {
                // 获取选中的订单ID
                int orderId = (int)dgvOrders.Rows[e.RowIndex].Cells["OrderId"].Value;
                
                // 打开订单详情页面
                var detailForm = new SalesOrderDetailForm(orderId);
                detailForm.ShowDialog();
            }
        }

        private void BtnExportClick(object sender, EventArgs e)
        {
            // 导出功能（简化实现）
            MessageBox.Show("导出功能开发中...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}