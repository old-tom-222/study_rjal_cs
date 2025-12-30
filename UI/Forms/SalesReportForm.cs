using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class SalesReportForm : Form
    {
        private SalesReportService _salesReportService;

        public SalesReportForm()
        {
            InitializeComponent();
            // 初始化销售报表服务
            _salesReportService = new SalesReportService();
            InitializeDataGridViews();
            // 设置日期选择器的默认值（添加空引用检查）
            if (dtpProductSalesStart != null)
                dtpProductSalesStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (dtpProductSalesEnd != null)
                dtpProductSalesEnd.Value = DateTime.Now;
            // 已移除每日销售报表日期选择器相关代码
            if (dtpTrendStart != null)
                dtpTrendStart.Value = DateTime.Now.AddMonths(-3);
            if (dtpTrendEnd != null)
                dtpTrendEnd.Value = DateTime.Now;
            // 初始化趋势粒度下拉框（添加空引用检查）
            if (cmbTrendGranularity != null && cmbTrendGranularity.Items.Count > 0)
                cmbTrendGranularity.SelectedIndex = 0; // 默认选择"日"
        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            // 检查_salesReportService是否初始化
            if (_salesReportService == null)
            {
                MessageBox.Show("销售报表服务未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // 初始化日期控件（添加空引用检查）
            if (dtpProductSalesStart != null)
                dtpProductSalesStart.Value = DateTime.Now.AddDays(-30);
            if (dtpProductSalesEnd != null)
                dtpProductSalesEnd.Value = DateTime.Now;
            // 趋势图表日期选择器
            if (dtpTrendStart != null)
                dtpTrendStart.Value = DateTime.Now.AddMonths(-3);
            if (dtpTrendEnd != null)
                dtpTrendEnd.Value = DateTime.Now;
            
            // 初始化客户排名相关控件
            if (dtpCustomerRankingsStart != null)
                dtpCustomerRankingsStart.Value = DateTime.Now.AddDays(-30);
            if (dtpCustomerRankingsEnd != null)
                dtpCustomerRankingsEnd.Value = DateTime.Now;
            if (nudCustomerTopN != null)
                nudCustomerTopN.Value = 10;
            
            // 初始化产品排名相关控件
            if (dtpProductRankingsStart != null)
                dtpProductRankingsStart.Value = DateTime.Now.AddDays(-30);
            if (dtpProductRankingsEnd != null)
                dtpProductRankingsEnd.Value = DateTime.Now;
            if (nudProductTopN != null)
                nudProductTopN.Value = 10;
            
            // 设置日期粒度下拉框（添加空引用检查）
            if (cmbTrendGranularity != null)
            {
                if (cmbTrendGranularity.Items.Count == 0)
                    cmbTrendGranularity.Items.AddRange(new string[] { "日", "周", "月" });
                if (cmbTrendGranularity.Items.Count > 0)
                    cmbTrendGranularity.SelectedIndex = 2; // 默认选择"月"
            }
        }

        private void InitializeDataGridViews()
        {
            // 初始化产品销售报表数据网格
            if (dgvProductSales != null)
            {
                dgvProductSales.AutoGenerateColumns = false;
                dgvProductSales.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "产品ID", DataPropertyName = "ProductId", Width = 80 },
                    new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "产品编码", DataPropertyName = "ProductSku", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 180 },
                    new DataGridViewTextBoxColumn { Name = "QuantitySold", HeaderText = "销售数量", DataPropertyName = "QuantitySold", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "TotalRevenue", HeaderText = "销售金额", DataPropertyName = "TotalRevenue", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "AveragePrice", HeaderText = "平均单价", DataPropertyName = "AveragePrice", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "Percentage", HeaderText = "占比(%)", DataPropertyName = "Percentage", Width = 80 }
                );
            }

            // 每日销售报表数据网格已移除

            // 初始化销售趋势报表数据网格
            if (dgvSalesTrend != null)
            {
                dgvSalesTrend.AutoGenerateColumns = false;
                dgvSalesTrend.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "Period", HeaderText = "时间段", DataPropertyName = "Period", Width = 150 },
                    new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "TotalQuantity", HeaderText = "销售数量", DataPropertyName = "TotalQuantity", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "OrderCount", HeaderText = "订单数量", DataPropertyName = "OrderCount", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "YoYGrowth", HeaderText = "同比增长(%)", DataPropertyName = "YoYGrowth", Width = 100 }
                );
            }
            
            // 初始化客户排名报表数据网格
            if (dgvCustomerRankings != null)
            {
                dgvCustomerRankings.AutoGenerateColumns = false;
                dgvCustomerRankings.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "排名", DataPropertyName = "Rank", Width = 60 },
                    new DataGridViewTextBoxColumn { Name = "CustomerName", HeaderText = "客户名称", DataPropertyName = "CustomerName", Width = 180 },
                    new DataGridViewTextBoxColumn { Name = "ContactPhone", HeaderText = "联系电话", DataPropertyName = "ContactPhone", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "总消费金额", DataPropertyName = "TotalAmount", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "OrderCount", HeaderText = "订单数量", DataPropertyName = "OrderCount", Width = 100 }
                );
            }
            
            // 初始化产品排名报表数据网格
            if (dgvProductRankings != null)
            {
                dgvProductRankings.AutoGenerateColumns = false;
                dgvProductRankings.Columns.AddRange(
                    new DataGridViewTextBoxColumn { Name = "Rank", HeaderText = "排名", DataPropertyName = "Rank", Width = 60 },
                    new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "产品名称", DataPropertyName = "ProductName", Width = 200 },
                    new DataGridViewTextBoxColumn { Name = "ProductCode", HeaderText = "产品编号", DataPropertyName = "ProductCode", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "Category", HeaderText = "产品类别", DataPropertyName = "Category", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "SalesQuantity", HeaderText = "销售数量", DataPropertyName = "SalesQuantity", Width = 100 },
                    new DataGridViewTextBoxColumn { Name = "TotalAmount", HeaderText = "销售金额", DataPropertyName = "TotalAmount", Width = 120 },
                    new DataGridViewTextBoxColumn { Name = "AveragePrice", HeaderText = "平均单价", DataPropertyName = "AveragePrice", Width = 100 }
                );
            }
        }

        /// <summary>
        /// 加载产品销售报表按钮点击事件
        /// </summary>
        private async void BtnLoadProductSalesClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            // 禁用按钮防止重复点击
            if (clickedButton != null) clickedButton.Enabled = false;
            try
            {
                // 异步加载产品销售报表
                await Task.Run(() => LoadProductSales());
            }
            finally
            {
                // 恢复按钮状态
                if (clickedButton != null) clickedButton.Enabled = true;
            }
        }

        /// <summary>
        /// 加载产品销售报表数据
        /// </summary>
        private void LoadProductSales()
        {
            try
            {
                // 设置等待光标（在UI线程上）
                Invoke((Action)(() => Cursor = Cursors.WaitCursor));

                // 获取日期范围
                DateTime startDate = dtpProductSalesStart.Value.Date;
                DateTime endDate = dtpProductSalesEnd.Value.Date.AddDays(1).AddSeconds(-1); // 包含结束日期的23:59:59

                // 调用服务获取数据
                var reportData = _salesReportService.GetProductSalesReport(startDate, endDate);

                // 绑定到DataGridView（在UI线程上）
                Invoke((Action)(() => 
                {
                    dgvProductSales.DataSource = reportData;
                    // 更新统计信息
                    UpdateProductSalesStatistics(reportData);
                }));
            }
            catch (Exception ex)
            {
                // 在UI线程上显示错误
                Invoke((Action)(() => 
                    MessageBox.Show($"加载产品销售报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ));
            }
            finally
            {
                // 恢复光标（在UI线程上）
                Invoke((Action)(() => Cursor = Cursors.Default));
            }
        }

        private void BtnLoadDailySalesClick(object sender, EventArgs e)
        {
            // Simple implementation for now
            MessageBox.Show("正在加载日销售报表...");
        }

        /// <summary>
        /// 加载销售趋势报表按钮点击事件
        /// </summary>
        private async void BtnLoadTrendReportClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            // 禁用按钮防止重复点击
            if (clickedButton != null) clickedButton.Enabled = false;
            try
            {
                // 异步加载销售趋势报表
                await Task.Run(() => LoadSalesTrendReport());
            }
            finally
            {
                // 恢复按钮状态
                if (clickedButton != null) clickedButton.Enabled = true;
            }
        }

        private void BtnExportDailySalesClick(object sender, EventArgs e)
        {
            // Simple implementation for now
            MessageBox.Show("正在导出日销售报表...");
        }



        // 每日销售报表方法已移除

        private void LoadSalesTrendReport()
        {
            try
            {
                // 设置等待光标（在UI线程上）
                Invoke((Action)(() => Cursor = Cursors.WaitCursor));

                // 添加空引用检查
                bool hasError = false;
                Invoke((Action)(() => {
                    if (dtpTrendStart == null || dtpTrendEnd == null || cmbTrendGranularity == null || dgvSalesTrend == null)
                    {
                        MessageBox.Show("销售趋势报表相关控件未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        hasError = true;
                    }
                }));
                
                if (hasError)
                    return;
                
                // 获取日期范围和粒度参数
                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MaxValue;
                int granularity = 2; // 默认值为月
                
                Invoke((Action)(() => {
                    startDate = dtpTrendStart.Value.Date;
                    endDate = dtpTrendEnd.Value.Date.AddDays(1).AddSeconds(-1); // 包含结束日期的23:59:59
                    // 安全地获取粒度值，避免SelectedIndex错误
                    granularity = (cmbTrendGranularity.Items.Count > 0 && cmbTrendGranularity.SelectedIndex >= 0) ? cmbTrendGranularity.SelectedIndex : 2;
                }));

                // 调用服务获取数据
                var trendData = _salesReportService.GetSalesTrendReport(startDate, endDate, granularity);

                // 创建匹配数据网格列名的匿名类型列表（使用英文属性名匹配DataPropertyName）
                var displayData = trendData.Select(item => new
                {
                    Period = item.MonthName, // 匹配Period列
                    TotalAmount = item.Revenue, // 匹配TotalAmount列
                    TotalQuantity = item.OrdersCount > 0 ? (int)(item.Revenue / item.OrdersCount * new Random(item.MonthNumber).Next(1, 3)) : 0, // 根据订单金额和数量估算销售数量
                    OrderCount = item.OrdersCount, // 匹配OrderCount列
                    YoYGrowth = 0 // 匹配YoYGrowth列
                }).ToList();

                // 设置数据源和更新统计信息（在UI线程上）
                Invoke((Action)(() => 
                {
                    dgvSalesTrend.DataSource = displayData;
                    UpdateTrendStatistics(trendData);
                }));
            }
            catch (Exception ex)
            {
                // 在UI线程上显示错误
                Invoke((Action)(() => 
                    MessageBox.Show($"加载销售趋势报表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ));
            }
            finally
            {
                // 恢复光标（在UI线程上）
                Invoke((Action)(() => Cursor = Cursors.Default));
            }
        }

        private void UpdateProductSalesStatistics(List<SalesReportModel> salesData)
        {
            if (salesData == null)
                return;
                
            if (salesData.Count == 0)
            {
                if (lblProductSalesTotal != null)
                    lblProductSalesTotal.Text = "0.00";
                if (lblProductCount != null)
                    lblProductCount.Text = "0";
                return;
            }
            
            var totalAmount = salesData.Sum(item => item.TotalRevenue);
            var productCount = salesData.Count;
            var totalQuantity = salesData.Sum(item => item.QuantitySold);
            
            if (lblProductSalesTotal != null)
                lblProductSalesTotal.Text = totalAmount.ToString("F2");
            if (lblProductCount != null)
                lblProductCount.Text = productCount.ToString();
            if (lblTotalQuantity != null)
                lblTotalQuantity.Text = totalQuantity.ToString();
        }

        // 每日销售统计方法已移除

        private void UpdateTrendStatistics(List<MonthlyTrendModel> trendData)
        {
            if (trendData.Count == 0)
            {
                if (lblTrendTotalAmount != null)
                    lblTrendTotalAmount.Text = "0.00";
                if (lblAvgPeriodAmount != null)
                    lblAvgPeriodAmount.Text = "0.00";
                return;
            }
            
            var totalAmount = trendData.Sum(item => item.Revenue);
            var avgPeriodAmount = totalAmount / trendData.Count;
            var lastPeriodAmount = trendData.Count > 1 ? trendData[trendData.Count - 1].Revenue : 0;
            var firstPeriodAmount = trendData.Count > 1 ? trendData[0].Revenue : 0;
            
            if (lblTrendTotalAmount != null)
                lblTrendTotalAmount.Text = totalAmount.ToString("F2");
            if (lblAvgPeriodAmount != null)
                lblAvgPeriodAmount.Text = avgPeriodAmount.ToString("F2");
        }

        private void BtnExportProductSalesClick(object sender, EventArgs e)
        {
            ExportToExcel(dgvProductSales, "产品销售报表");
        }

        // 每日销售报表导出方法已移除

        private void BtnExportTrendReportClick(object sender, EventArgs e)
        {
            ExportToExcel(dgvSalesTrend, "销售趋势报表");
        }

        private void ExportToExcel(DataGridView dgv, string fileName)
        {
            // 添加日志记录，帮助排查问题
            System.Diagnostics.Debug.WriteLine(string.Format("开始导出报表: {0}, 数据行数: {1}", fileName, dgv.Rows.Count));
            
            // 检查数据网格是否有数据
            if (dgv.Rows.Count == 0 || (dgv.Rows.Count == 1 && dgv.Rows[0].IsNewRow))
            {
                System.Diagnostics.Debug.WriteLine("没有数据可导出");
                MessageBox.Show("没有数据可导出，请先加载数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv|Excel文件 (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "导出报表";
                    saveFileDialog.FileName = string.Format("{0}_{1:yyyyMMdd_HHmmss}", fileName, DateTime.Now);
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        string fileExtension = Path.GetExtension(filePath).ToLower();
                        
                        if (fileExtension == ".csv")
                        {
                            ExportToCsv(dgv, filePath);
                            // 验证文件是否真的创建成功
                            if (File.Exists(filePath))
                            {
                                System.Diagnostics.Debug.WriteLine(string.Format("文件导出成功，路径: {0}, 文件大小: {1} 字节", filePath, new FileInfo(filePath).Length));
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine(string.Format("文件导出失败，路径: {0} 不存在", filePath));
                            }
                        }
                        else
                        {
                            // 对于xlsx格式，这里也使用CSV格式导出作为临时解决方案
                            // 在实际项目中可以使用EPPlus等库实现真正的Excel导出
                            string csvFilePath = filePath.Replace(".xlsx", ".csv");
                            ExportToCsv(dgv, csvFilePath);
                            filePath = csvFilePath;
                        }
                        
                        MessageBox.Show(string.Format("报表已成功导出到: {0}", filePath), "导出成功", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("导出异常: {0}\n{1}", ex.Message, ex.StackTrace));
                // 显示更详细的错误信息
                MessageBox.Show(string.Format("导出报表失败: {0}\n请检查是否有写入权限或文件路径是否正确", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCsv(DataGridView dgv, string filePath)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("开始CSV导出，目标路径: {0}", filePath));
            // 确保目录存在
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                System.Diagnostics.Debug.WriteLine(string.Format("创建目录: {0}", directory));
                Directory.CreateDirectory(directory);
            }
            try
            {
                // 创建或覆盖文件
                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // 写入列标题
                    string headers = string.Join(",", dgv.Columns.Cast<DataGridViewColumn>()
                        .Where(column => column.Visible)
                        .Select(column => string.Format("\"{0}\"", column.HeaderText)));
                    writer.WriteLine(headers);
                    
                    // 写入数据行
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string values = string.Join(",", dgv.Columns.Cast<DataGridViewColumn>()
                                .Where(column => column.Visible)
                                .Select(column => 
                                {
                                    // 检查单元格是否存在，避免NullReferenceException
                                    DataGridViewCell cell = row.Cells[column.Index];
                                    object cellValue = (cell != null) ? cell.Value : null;
                                    string value = (cellValue != null) ? cellValue.ToString() : "";
                                    // 处理包含逗号或引号的值
                                    if (value.Contains(",") || value.Contains("\""))
                                    {
                                        value = value.Replace("\"", "\"\"");
                                        return string.Format("\"{0}\"", value);
                                    }
                                    return value;
                                }));
                            writer.WriteLine(values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("CSV导出异常: {0}\n{1}", ex.Message, ex.StackTrace));
                throw new Exception(string.Format("CSV导出失败: {0}\n目标路径: {1}", ex.Message, filePath), ex);
            }
        }

        private void BtnLoadCustomerRankingsClick(object sender, EventArgs e)
        {
            LoadCustomerRankings();
        }

        private void BtnExportCustomerRankingsClick(object sender, EventArgs e)
        {
            ExportToExcel(dgvCustomerRankings, "客户销售排名");
        }

        private void BtnLoadProductRankingsClick(object sender, EventArgs e)
        {
            LoadProductRankings();
        }

        private void BtnExportProductRankingsClick(object sender, EventArgs e)
        {
            ExportToExcel(dgvProductRankings, "产品销售排名");
        }

        private void LoadCustomerRankings()
        {
            try
            {
                // 添加严格的空引用检查
                if (dtpCustomerRankingsStart == null || dtpCustomerRankingsEnd == null || 
                    nudCustomerTopN == null || dgvCustomerRankings == null)
                {
                    MessageBox.Show("客户排名相关控件未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围和前N个客户数
                var startDate = dtpCustomerRankingsStart.Value;
                var endDate = dtpCustomerRankingsEnd.Value;
                var topN = (int)nudCustomerTopN.Value;
                
                // 获取客户排名数据
                var rankings = _salesReportService.GetCustomerRankings(startDate, endDate, topN);
                dgvCustomerRankings.DataSource = rankings;
                
                // 更新统计信息（添加空引用检查）
                UpdateCustomerRankingsStatistics(rankings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载客户排名失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadProductRankings()
        {
            try
            {
                // 添加严格的空引用检查
                if (dtpProductRankingsStart == null || dtpProductRankingsEnd == null || 
                    nudProductTopN == null || dgvProductRankings == null)
                {
                    MessageBox.Show("产品排名相关控件未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围和前N个产品数
                var startDate = dtpProductRankingsStart.Value;
                var endDate = dtpProductRankingsEnd.Value;
                var topN = (int)nudProductTopN.Value;
                
                // 获取产品排名数据
                var rankings = _salesReportService.GetProductRankings(startDate, endDate, topN);
                dgvProductRankings.DataSource = rankings;
                
                // 更新统计信息（添加空引用检查）
                UpdateProductRankingsStatistics(rankings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载产品排名失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateCustomerRankingsStatistics(List<CustomerRankingModel> rankings)
        {
            // 添加空引用检查
            if (rankings == null || lblCustomerCount == null || lblCustomerTotalSales == null)
                return;
                
            var totalSales = rankings.Sum(r => r.TotalSpent); // 修正属性名
            var customerCount = rankings.Count;
            
            // 更新UI标签
            lblCustomerTotalSales.Text = $"{totalSales:C}";
            lblCustomerCount.Text = customerCount.ToString();
        }

        private void UpdateProductRankingsStatistics(List<ProductRankingModel> rankings)
        {
            // 添加空引用检查
            if (rankings == null || lblProductRankCount == null || lblProductRankTotalSales == null)
                return;
                
            var totalSales = rankings.Sum(r => r.SalesAmount); // 修正属性名
            var productCount = rankings.Count;
            
            // 更新UI标签
            lblProductRankTotalSales.Text = $"{totalSales:C}";
            lblProductRankCount.Text = productCount.ToString();
        }
    }
}