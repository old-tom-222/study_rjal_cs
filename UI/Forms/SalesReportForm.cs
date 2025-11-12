using System;
using System.Data;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class SalesReportForm : Form
    {
        private readonly SalesReportService _salesReportService;

        public SalesReportForm()
        {
            InitializeComponent();
            _salesReportService = new SalesReportService();
            InitializeDataGridViews();
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            // 初始化日期控件
            dtpProductSalesStart.Value = DateTime.Now.AddDays(-30);
            dtpProductSalesEnd.Value = DateTime.Now;
            dtpDailySalesStart.Value = DateTime.Now.AddDays(-30);
            dtpDailySalesEnd.Value = DateTime.Now;
            dtpTrendStart.Value = DateTime.Now.AddMonths(-3);
            dtpTrendEnd.Value = DateTime.Now;
            
            // 设置日期粒度下拉框
            cmbTrendGranularity.Items.AddRange(new string[] { "日", "周", "月" });
            cmbTrendGranularity.SelectedIndex = 2; // 默认选择"月"
        }

        private void InitializeDataGridViews()
        {
            // 初始化产品销售报表数据网格
            dgvProductSales.AutoGenerateColumns = false;
            dgvProductSales.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "产品ID", DataPropertyName = "ProductId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "产品编码", DataPropertyName = "ProductSku", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "销售数量", DataPropertyName = "Quantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "AveragePrice", HeaderText = "平均单价", DataPropertyName = "AveragePrice", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Percentage", HeaderText = "占比(%)", DataPropertyName = "Percentage", Width = 80 }
            );

            // 初始化每日销售报表数据网格
            dgvDailySales.AutoGenerateColumns = false;
            dgvDailySales.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Date", HeaderText = "日期", DataPropertyName = "Date", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "OrderCount", HeaderText = "订单数量", DataPropertyName = "OrderCount", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TotalQuantity", HeaderText = "销售数量", DataPropertyName = "TotalQuantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "AverageOrderAmount", HeaderText = "平均订单金额", DataPropertyName = "AverageOrderAmount", Width = 120 }
            );

            // 初始化销售趋势报表数据网格
            dgvSalesTrend.AutoGenerateColumns = false;
            dgvSalesTrend.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Period", HeaderText = "时间段", DataPropertyName = "Period", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "TotalQuantity", HeaderText = "销售数量", DataPropertyName = "TotalQuantity", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "OrderCount", HeaderText = "订单数量", DataPropertyName = "OrderCount", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "YoYGrowth", HeaderText = "同比增长(%)", DataPropertyName = "YoYGrowth", Width = 100 }
            );
        }

        private void BtnLoadProductSalesClick(object sender, EventArgs e)
        {            
            LoadProductSalesReport();
        }

        private void BtnLoadDailySalesClick(object sender, EventArgs e)
        {            
            LoadDailySalesReport();
        }

        private void BtnLoadTrendReportClick(object sender, EventArgs e)
        {            
            LoadSalesTrendReport();
        }

        private void BtnExportProductSalesClick(object sender, EventArgs e)
        {            
            if (dgvProductSales.DataSource == null || !(dgvProductSales.DataSource is List<SalesReportModel> data))
            {
                MessageBox.Show("没有可导出的数据，请先加载报表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv";
                saveFileDialog.Title = "导出产品销售报表";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        // 实际应用中需要实现导出逻辑
                        MessageBox.Show("导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void BtnExportDailySalesClick(object sender, EventArgs e)
        {            
            if (dgvDailySales.DataSource == null || !(dgvDailySales.DataSource is List<DailySalesReportModel> data))
            {
                MessageBox.Show("没有可导出的数据，请先加载报表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv";
                saveFileDialog.Title = "导出每日销售报表";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        // 实际应用中需要实现导出逻辑
                        MessageBox.Show("导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void BtnExportTrendReportClick(object sender, EventArgs e)
        {            
            if (dgvSalesTrend.DataSource == null || !(dgvSalesTrend.DataSource is List<MonthlyTrendModel> data))
            {
                MessageBox.Show("没有可导出的数据，请先加载报表。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv";
                saveFileDialog.Title = "导出销售趋势报表";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        // 实际应用中需要实现导出逻辑
                        MessageBox.Show("导出成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void LoadProductSalesReport()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围
                var startDate = dtpProductSalesStart.Value;
                var endDate = dtpProductSalesEnd.Value;
                
                // 获取产品销售数据
                var productSales = _salesReportService.GetProductSalesReport(startDate, endDate);
                dgvProductSales.DataSource = productSales;
                
                // 更新统计信息
                UpdateProductSalesStatistics(productSales);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载产品销售报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadDailySalesReport()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围
                var startDate = dtpDailySalesStart.Value;
                var endDate = dtpDailySalesEnd.Value;
                
                // 获取每日销售数据
                var dailySales = _salesReportService.GetDailySalesReport(startDate, endDate);
                dgvDailySales.DataSource = dailySales;
                
                // 更新统计信息
                UpdateDailySalesStatistics(dailySales);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载每日销售报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadSalesTrendReport()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围和粒度
                var startDate = dtpTrendStart.Value;
                var endDate = dtpTrendEnd.Value;
                var granularity = cmbTrendGranularity.SelectedIndex; // 0=日, 1=周, 2=月
                
                // 获取销售趋势数据 (只传入年份参数)
                var trendData = _salesReportService.GetSalesTrendReport(startDate.Year);
                dgvSalesTrend.DataSource = trendData;
                
                // 更新统计信息
                UpdateTrendStatistics(trendData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载销售趋势报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateProductSalesStatistics(List<SalesReportModel> salesData)
        {
            if (salesData.Count == 0)
            {
                lblProductSalesTotal.Text = "0.00";
                lblProductCount.Text = "0";
                return;
            }
            
            var totalAmount = salesData.Sum(item => item.TotalRevenue);
            var productCount = salesData.Count;
            var totalQuantity = salesData.Sum(item => item.QuantitySold);
            
            lblProductSalesTotal.Text = totalAmount.ToString("F2");
            lblProductCount.Text = productCount.ToString();
            lblTotalQuantity.Text = totalQuantity.ToString();
        }

        private void UpdateDailySalesStatistics(List<DailySalesReportModel> dailyData)
        {
            if (dailyData.Count == 0)
            {
                lblDailySalesTotal.Text = "0.00";
                lblTotalOrders.Text = "0";
                return;
            }
            
            var totalAmount = dailyData.Sum(item => item.TotalRevenue);
            var totalOrders = dailyData.Sum(item => item.TotalOrders);
            var avgDailySales = totalAmount / dailyData.Count;
            
            lblDailySalesTotal.Text = totalAmount.ToString("F2");
            lblTotalOrders.Text = totalOrders.ToString();
            lblAvgDailySales.Text = avgDailySales.ToString("F2");
        }

        private void UpdateTrendStatistics(List<MonthlyTrendModel> trendData)
        {
            if (trendData.Count == 0)
            {
                lblTrendTotalAmount.Text = "0.00";
                return;
            }
            
            var totalAmount = trendData.Sum(item => item.Revenue);
            var avgPeriodAmount = totalAmount / trendData.Count;
            var lastPeriodAmount = trendData.Count > 1 ? trendData[trendData.Count - 1].Revenue : 0;
            var firstPeriodAmount = trendData.Count > 1 ? trendData[0].Revenue : 0;
            
            lblTrendTotalAmount.Text = totalAmount.ToString("F2");
            lblAvgPeriodAmount.Text = avgPeriodAmount.ToString("F2");
        }

        private void BtnExportProductSales_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvProductSales, "产品销售报表");
        }

        private void BtnExportDailySales_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvDailySales, "每日销售报表");
        }

        private void BtnExportTrendReport_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvSalesTrend, "销售趋势报表");
        }

        private void ExportToExcel(DataGridView dgv, string fileName)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv";
                    saveFileDialog.Title = "导出报表";
                    saveFileDialog.FileName = $"{fileName}_{DateTime.Now:yyyyMMdd_HHmmss}";
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // 这里可以添加实际的导出逻辑
                        // 由于是框架搭建，暂时只显示消息
                        MessageBox.Show($"报表已导出到: {saveFileDialog.FileName}", "导出成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        
    }
}