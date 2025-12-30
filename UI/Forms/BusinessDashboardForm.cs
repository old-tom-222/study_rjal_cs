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
                new DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "排名", DataPropertyName = "Rank", Width = 60 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "SalesQuantity", HeaderText = "销售数量", DataPropertyName = "QuantitySold", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SalesAmount", HeaderText = "销售金额", DataPropertyName = "TotalRevenue", Width = 100 }
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
            chartSalesTrend.Series.Clear(); // 清除默认Series
            chartSalesTrend.Series.Add("销售额");
            chartSalesTrend.Series["销售额"].ChartType = SeriesChartType.Line;
            chartSalesTrend.Series["销售额"].Color = Color.Blue;
            chartSalesTrend.ChartAreas[0].AxisX.Title = "日期";
            chartSalesTrend.ChartAreas[0].AxisY.Title = "金额";
            
            // 初始化库存状态图表
            chartInventoryStatus.Titles.Add("库存状态分布");
            chartInventoryStatus.Series.Clear(); // 清除默认Series
            chartInventoryStatus.Series.Add("库存状态");
            chartInventoryStatus.Series["库存状态"].ChartType = SeriesChartType.Pie;
            chartInventoryStatus.Series["库存状态"].IsValueShownAsLabel = true;
            
            // 初始化销售类别分布图表
            chartSalesByCategory.Titles.Add("销售类别分布");
            chartSalesByCategory.Series.Clear(); // 清除默认Series
            chartSalesByCategory.Series.Add("销售额");
            chartSalesByCategory.Series["销售额"].ChartType = SeriesChartType.Bar;
            chartSalesByCategory.ChartAreas[0].AxisX.Title = "类别";
            chartSalesByCategory.ChartAreas[0].AxisY.Title = "金额";
        }

        private void UpdateCharts(DateTime startDate, DateTime endDate)
        {
            // 清空现有数据
            chartSalesTrend.Series["销售额"].Points.Clear();
            chartInventoryStatus.Series["库存状态"].Points.Clear();
            chartSalesByCategory.Series["销售额"].Points.Clear();
            
            try
            {
                // 获取销售趋势数据
                var trendData = _dashboardService.GetBusinessTrendData(startDate, endDate);
                
                // 添加销售趋势数据
                foreach (var item in trendData)
                {
                    // 格式化日期显示：从MonthName中提取年份
                    int year = int.Parse(item.MonthName.Substring(0, 4)); // 假设MonthName格式为"2023年1月"
                    var dateLabel = new DateTime(year, item.MonthNumber, 1).ToString("MM-dd");
                    chartSalesTrend.Series["销售额"].Points.AddXY(dateLabel, item.Revenue);
                }
                
                // 获取库存状态数据
                var dashboardSummary = _dashboardService.GetDashboardSummary(startDate, endDate);
                
                // 添加库存状态数据
                int totalItems = dashboardSummary.LowStockItemsCount + dashboardSummary.OutOfStockItemsCount + 100; // 假设总库存为100+低库存+零库存
                chartInventoryStatus.Series["库存状态"].Points.AddXY("正常库存", totalItems);
                chartInventoryStatus.Series["库存状态"].Points.AddXY("低库存", dashboardSummary.LowStockItemsCount);
                chartInventoryStatus.Series["库存状态"].Points.AddXY("零库存", dashboardSummary.OutOfStockItemsCount);
                
                // 获取类别销售数据（暂时使用模拟数据，后续可以从数据库获取）
                // 这里先使用模拟数据，因为目前没有从数据库获取类别销售数据的方法
                chartSalesByCategory.Series["销售额"].Points.AddXY("电子产品", 150000);
                chartSalesByCategory.Series["销售额"].Points.AddXY("办公用品", 80000);
                chartSalesByCategory.Series["销售额"].Points.AddXY("家居用品", 120000);
                chartSalesByCategory.Series["销售额"].Points.AddXY("食品饮料", 60000);
                chartSalesByCategory.Series["销售额"].Points.AddXY("其他", 30000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("更新图表数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDashboardData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围 - 确保日期范围包含完整的时间
                var endDate = DateTime.Now.Date.AddDays(1); // 结束日期设为明天开始，包含今天
                var startDate = dtpDashboardRange.Value.Date; // 开始日期设为当天开始
                
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
            try
            {
                var summary = _dashboardService.GetDashboardSummary(startDate, endDate);
                
                // 更新关键指标
                lblTotalSalesAmount.Text = summary.TotalSalesAmount.ToString("C2");
                lblTotalPurchaseAmount.Text = summary.TotalPurchaseAmount.ToString("C2");
                lblTotalProfit.Text = summary.TotalProfit.ToString("C2");
                lblCurrentInventoryValue.Text = summary.CurrentInventoryValue.ToString("C2");
                lblSalesOrdersCount.Text = summary.SalesOrdersCount.ToString();
                lblPurchaseOrdersCount.Text = summary.PurchaseOrdersCount.ToString();
                lblLowStockItemsCount.Text = summary.LowStockItemsCount.ToString();
                // lblOutOfStockItemsCount.Text = summary.OutOfStockItemsCount.ToString(); // 移除对不存在的标签的引用
                
                // 更新变化百分比
                UpdateChangeLabels(summary);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载仪表板概要数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChangeLabels(DashboardSummaryModel summary)
        {
            // 计算并更新销售额变化百分比
            decimal salesChangePercent = summary.SalesChangePercent;
            lblSalesChange.Text = string.Format("{0:0.00}%", salesChangePercent);
            lblSalesChange.ForeColor = salesChangePercent >= 0 ? Color.Green : Color.Red;

            // 计算并更新采购额变化百分比
            decimal purchaseChangePercent = summary.PurchaseChangePercent;
            lblPurchaseChange.Text = string.Format("{0:0.00}%", purchaseChangePercent);
            lblPurchaseChange.ForeColor = purchaseChangePercent >= 0 ? Color.Green : Color.Red;

            // 计算并更新利润变化百分比
            decimal profitChangePercent = summary.ProfitChangePercent;
            lblProfitChange.Text = string.Format("{0:0.00}%", profitChangePercent);
            lblProfitChange.ForeColor = profitChangePercent >= 0 ? Color.Green : Color.Red;
        }

        private void LoadTopSellingProducts(DateTime startDate, DateTime endDate)
        {
            try
            {
                var topSellingProducts = _dashboardService.GetTopSellingProducts(startDate, endDate, 10);
                
                // 添加排名
                for (int i = 0; i < topSellingProducts.Count; i++)
                {
                    topSellingProducts[i].Rank = i + 1;
                }
                
                dgvTopSellingProducts.DataSource = topSellingProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载热销产品数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRecentTransactions()
        {
            try
            {
                var recentTransactions = _dashboardService.GetRecentTransactions(10);
                dgvRecentTransactions.DataSource = recentTransactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载最近交易数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void BtnExportDashboard_Click(object sender, EventArgs e)
        {
            try
            {
                // 创建一个保存文件对话框
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx|所有文件 (*.*)|*.*";
                saveFileDialog.Title = "导出仪表板数据";
                saveFileDialog.FileName = string.Format("经营看板_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 这里可以实现导出逻辑
                    MessageBox.Show("导出功能开发中...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出仪表板数据失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetDashboardRange_Click(object sender, EventArgs e)
        {
            // 实现设置仪表板范围的逻辑
            LoadDashboardData();
        }

        private void DtpDashboardRange_ValueChanged(object sender, EventArgs e)
        {
            LoadDashboardData();
        }
    }
}