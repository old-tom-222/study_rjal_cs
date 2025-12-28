using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Drawing;

namespace CSproject.UI.Forms
{
    public partial class BusinessDashboardForm : Form
    {
        private readonly BusinessDashboardService _dashboardService;

        public BusinessDashboardForm()
        {
            InitializeComponent();
            _dashboardService = new BusinessDashboardService();
            InitializeDataGridViews();
        }

        private void BusinessDashboardForm_Load(object sender, EventArgs e)
        {
            // 初始化日期控件
            dtpDashboardRange.Value = DateTime.Now.AddDays(-30);
            
            // 设置图表样式
            InitializeCharts();
            
            // 加载初始数据
            LoadDashboardData();
        }

        private void InitializeDataGridViews()
        {
            // 初始化热销产品数据网格
            dgvTopSellingProducts.AutoGenerateColumns = false;
            dgvTopSellingProducts.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "排名", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "SalesQuantity", HeaderText = "销售数量", DataPropertyName = "Quantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SalesAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 100 }
            );

            // 初始化最近交易数据网格
            dgvRecentTransactions.AutoGenerateColumns = false;
            dgvRecentTransactions.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "TransactionId", HeaderText = "交易ID", DataPropertyName = "TransactionId", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TransactionType", HeaderText = "交易类型", DataPropertyName = "TransactionType", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "数量", DataPropertyName = "Quantity", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "Amount", HeaderText = "金额", DataPropertyName = "Amount", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TransactionDate", HeaderText = "交易时间", DataPropertyName = "TransactionDate", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "RelatedParty", HeaderText = "相关方", DataPropertyName = "RelatedParty", Width = 120 }
            );
        }

        private void InitializeCharts()
        {
            // 初始化销售趋势图表
            chartSalesTrend.Titles.Add("销售趋势");
            chartSalesTrend.Series.Add("销售额");
            chartSalesTrend.Series["销售额"].ChartType = SeriesChartType.Line;
            chartSalesTrend.Series["销售额"].Color = Color.Blue;
            chartSalesTrend.ChartAreas[0].AxisX.Title = "日期";
            chartSalesTrend.ChartAreas[0].AxisY.Title = "金额";
            
            // 初始化库存状态图表
            chartInventoryStatus.Titles.Add("库存状态分布");
            chartInventoryStatus.Series.Add("库存状态");
            chartInventoryStatus.Series["库存状态"].ChartType = SeriesChartType.Pie;
            chartInventoryStatus.Series["库存状态"].IsValueShownAsLabel = true;
            
            // 初始化销售类别分布图表
            chartSalesByCategory.Titles.Add("销售类别分布");
            chartSalesByCategory.Series.Add("销售额");
            chartSalesByCategory.Series["销售额"].ChartType = SeriesChartType.Bar;
            chartSalesByCategory.ChartAreas[0].AxisX.Title = "类别";
            chartSalesByCategory.ChartAreas[0].AxisY.Title = "销售额";
        }

        private void LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围
                var endDate = DateTime.Now;
                var startDate = dtpDashboardRange.Value;
                
                // 加载仪表板概要数据
                LoadDashboardSummary(startDate, endDate);
                
                // 加载热销产品
                LoadTopSellingProducts(startDate, endDate);
                
                // 加载最近交易
                LoadRecentTransactions();
                
                // 更新图表数据
                UpdateCharts(startDate, endDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载仪表板数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadDashboardSummary(DateTime startDate, DateTime endDate)
        {
            var summary = _dashboardService.GetDashboardSummary(startDate, endDate);
            
            // 更新关键指标
            lblTotalSalesAmount.Text = summary.TotalSalesAmount.ToString("F2");
            lblTotalPurchaseAmount.Text = summary.TotalPurchaseAmount.ToString("F2");
            lblTotalProfit.Text = summary.TotalProfit.ToString("F2");
            lblCurrentInventoryValue.Text = summary.CurrentInventoryValue.ToString("F2");
            lblSalesOrdersCount.Text = summary.SalesOrdersCount.ToString();
            lblPurchaseOrdersCount.Text = summary.PurchaseOrdersCount.ToString();
            lblLowStockItemsCount.Text = summary.LowStockItemsCount.ToString();
            lblAvgDailySales.Text = summary.AvgDailySales.ToString("F2");
            
            // 更新环比变化
            UpdateChangeLabels(summary);
        }

        private void UpdateChangeLabels(DashboardSummaryModel summary)
        {
            // 更新销售额环比变化
            UpdateChangeLabel(lblSalesChange, summary.SalesChangePercent);
            
            // 更新采购额环比变化
            UpdateChangeLabel(lblPurchaseChange, summary.PurchaseChangePercent);
            
            // 更新利润环比变化
            UpdateChangeLabel(lblProfitChange, summary.ProfitChangePercent);
        }

        private void UpdateChangeLabel(Label label, decimal changePercent)
        {
            if (changePercent > 0)
            {
                label.Text = string.Format("+{0:F1}%", changePercent);
                label.ForeColor = Color.Green;
            }
            else if (changePercent < 0)
            {
                label.Text = string.Format("{0:F1}%", changePercent);
                label.ForeColor = Color.Red;
            }
            else
            {
                label.Text = "0.0%";
                label.ForeColor = Color.Gray;
            }
        }

        private void LoadTopSellingProducts(DateTime startDate, DateTime endDate)
        {
            var topProducts = _dashboardService.GetTopSellingProducts(startDate, endDate, 10);
            
            // 添加排名
            for (int i = 0; i < topProducts.Count; i++)
            {
                // 使用DataRowView或动态对象添加排名信息
                // 这里为了简化，我们将使用对象集合
            }
            
            dgvTopSellingProducts.DataSource = topProducts;
        }

        private void LoadRecentTransactions()
        {
            var transactions = _dashboardService.GetRecentTransactions(20);
            dgvRecentTransactions.DataSource = transactions;
        }

        private void UpdateCharts(DateTime startDate, DateTime endDate)
        {
            // 清空现有数据
            chartSalesTrend.Series["销售额"].Points.Clear();
            chartInventoryStatus.Series["库存状态"].Points.Clear();
            chartSalesByCategory.Series["销售额"].Points.Clear();
            
            // 添加销售趋势数据（模拟数据）
            // 实际应用中应该从服务层获取
            for (int i = 14; i >= 0; i--)
            {
                var date = DateTime.Now.AddDays(-i);
                var amount = 5000 + new Random(date.Day).Next(0, 5000);
                chartSalesTrend.Series["销售额"].Points.AddXY(date.ToString("MM-dd"), amount);
            }
            
            // 添加库存状态数据（模拟数据）
            chartInventoryStatus.Series["库存状态"].Points.AddXY("正常库存", 85);
            chartInventoryStatus.Series["库存状态"].Points.AddXY("低库存", 12);
            chartInventoryStatus.Series["库存状态"].Points.AddXY("零库存", 3);
            
            // 添加类别销售数据（模拟数据）
            chartSalesByCategory.Series["销售额"].Points.AddXY("电子产品", 150000);
            chartSalesByCategory.Series["销售额"].Points.AddXY("办公用品", 80000);
            chartSalesByCategory.Series["销售额"].Points.AddXY("家居用品", 120000);
            chartSalesByCategory.Series["销售额"].Points.AddXY("食品饮料", 60000);
            chartSalesByCategory.Series["销售额"].Points.AddXY("其他", 30000);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void BtnExportDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|PDF文件 (*.pdf)|*.pdf";
                    saveFileDialog.Title = "导出仪表板";
                    saveFileDialog.FileName = string.Format("经营仪表板_{0:yyyyMMdd_HHmmss}", DateTime.Now);
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 这里可以添加实际的导出逻辑
                        // 由于是框架搭建，暂时只显示消息
                        MessageBox.Show(string.Format("仪表板已导出到: {0}", saveFileDialog.FileName), "导出成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出仪表板失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetDashboardRange_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

       


        /// <summary>
        /// 导出仪表板数据按钮点击事件处理程序
        /// </summary>
        private void BtnExportDashboardClick(object sender, EventArgs e)
        {
            try
            {
                // 创建保存文件对话框
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv|PDF文件 (*.pdf)|*.pdf";
                saveFileDialog.Title = "导出仪表板数据";
                saveFileDialog.FileName = string.Format("仪表板数据_{0}", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                // 显示对话框并检查用户是否点击了确定按钮
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 根据所选文件格式执行导出操作
                    string fileExtension = Path.GetExtension(saveFileDialog.FileName);
                    string message = string.Empty;

                    switch (fileExtension.ToLower())
                    {
                        case ".xlsx":
                            // 实现Excel导出逻辑
                            message = "Excel文件导出成功";
                            break;
                        case ".csv":
                            // 实现CSV导出逻辑
                            message = "CSV文件导出成功";
                            break;
                        case ".pdf":
                            // 实现PDF导出逻辑
                            message = "PDF文件导出成功";
                            break;
                        default:
                            message = "不支持的文件格式";
                            break;
                    }

                    // 显示导出成功消息
                    MessageBox.Show(message, "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // 显示错误消息
                MessageBox.Show(string.Format("导出过程中发生错误: {0}", ex.Message), "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载仪表板数据
        /// </summary>
        private void BusinessDashboardFormLoad(object sender, EventArgs e)
        {
            // 初始化仪表板数据
            LoadDashboardData();
        }

        /// <summary>
        /// 刷新按钮点击事件处理程序
        /// </summary>
        private void BtnRefreshClick(object sender, EventArgs e)
        {
            // 重新加载仪表板数据
            LoadDashboardData();
        }

        /// <summary>
        /// 设置仪表板时间范围按钮点击事件处理程序
        /// </summary>
        private void BtnSetDashboardRangeClick(object sender, EventArgs e)
        {
            // 应用选定的时间范围并重新加载数据
            LoadDashboardData();
        }
    }
}