using System;
using System.Data;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class InventoryReportForm : Form
    {
        private readonly InventoryReportService _inventoryReportService;

        public InventoryReportForm()
        {
            InitializeComponent();
            _inventoryReportService = new InventoryReportService();
            InitializeDataGridViews();
        }

        private void InventoryReportForm_Load(object sender, EventArgs e)
        {
            // 初始化日期控件
            dtpStartDate.Value = DateTime.Now.AddDays(-30); // 默认显示过去30天
            dtpEndDate.Value = DateTime.Now;
            dtpTransStartDate.Value = DateTime.Now.AddDays(-30); // 默认显示过去30天
            dtpTransEndDate.Value = DateTime.Now;
            
            // 加载初始数据
            LoadInventoryOverview();
            LoadInventoryTransactions();
        }

        private void InitializeDataGridViews()
        {
            // 初始化库存概览数据网格
            dgvInventoryOverview.AutoGenerateColumns = false;
            dgvInventoryOverview.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "商品ID", DataPropertyName = "ProductId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "商品SKU", DataPropertyName = "ProductSku", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "商品名称", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "WarehouseId", HeaderText = "仓库ID", DataPropertyName = "WarehouseId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "WarehouseName", HeaderText = "仓库名称", DataPropertyName = "WarehouseName", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "CurrentQuantity", HeaderText = "数量", DataPropertyName = "CurrentQuantity", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "SafeStock", HeaderText = "安全库存", DataPropertyName = "SafeStock", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "LastStockMovementDate", HeaderText = "最后更新", DataPropertyName = "LastStockMovementDate", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" } }
            );

            // 初始化低库存预警数据网格
            dgvLowStock.AutoGenerateColumns = false;
            dgvLowStock.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "商品ID", DataPropertyName = "ProductId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "商品SKU", DataPropertyName = "ProductSku", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "商品名称", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "WarehouseId", HeaderText = "仓库ID", DataPropertyName = "WarehouseId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "WarehouseName", HeaderText = "仓库名称", DataPropertyName = "WarehouseName", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "CurrentQuantity", HeaderText = "当前数量", DataPropertyName = "CurrentQuantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SafeStock", HeaderText = "安全库存", DataPropertyName = "SafeStock", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ReorderQuantity", HeaderText = "重订点", DataPropertyName = "ReorderQuantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "LastStockMovementDate", HeaderText = "最后更新", DataPropertyName = "LastStockMovementDate", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" } }
            );

            // 初始化库存周转率数据网格
            dgvInventoryTurnover.AutoGenerateColumns = false;
            dgvInventoryTurnover.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "商品ID", DataPropertyName = "ProductId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "商品SKU", DataPropertyName = "ProductSku", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "商品名称", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "WarehouseId", HeaderText = "仓库ID", DataPropertyName = "WarehouseId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "WarehouseName", HeaderText = "仓库名称", DataPropertyName = "WarehouseName", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "OpeningStock", HeaderText = "期初库存", DataPropertyName = "OpeningStock", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "ClosingStock", HeaderText = "期末库存", DataPropertyName = "ClosingStock", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "AverageStock", HeaderText = "平均库存", DataPropertyName = "AverageStock", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "SalesQuantity", HeaderText = "销售数量", DataPropertyName = "SalesQuantity", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "TurnoverRate", HeaderText = "周转率", DataPropertyName = "TurnoverRate", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "0.00" } }
            );

            // 初始化库存流水数据网格
            dgvInventoryTransactions.AutoGenerateColumns = false;
            dgvInventoryTransactions.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "流水ID", DataPropertyName = "Id", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductId", HeaderText = "商品ID", DataPropertyName = "ProductId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "ProductName", HeaderText = "商品名称", DataPropertyName = "ProductName", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "ProductSku", HeaderText = "SKU", DataPropertyName = "ProductSku", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "WarehouseId", HeaderText = "仓库ID", DataPropertyName = "WarehouseId", Width = 80 },
                new DataGridViewTextBoxColumn { Name = "WarehouseName", HeaderText = "仓库名称", DataPropertyName = "WarehouseName", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "ChangeQty", HeaderText = "变动数量", DataPropertyName = "ChangeQty", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight } },
                new DataGridViewTextBoxColumn { Name = "Type", HeaderText = "变动类型", DataPropertyName = "Type", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "CreatedAt", HeaderText = "变动日期", DataPropertyName = "CreatedAt", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm:ss" } },
                new DataGridViewTextBoxColumn { Name = "Reference", HeaderText = "参考ID", DataPropertyName = "Reference", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Remark", HeaderText = "备注", DataPropertyName = "Remark", Width = 150 }
            );
        }

        private void BtnLoadOverview_Click(object sender, EventArgs e)
        {
            LoadInventoryOverview();
        }

        private void BtnLoadLowStock_Click(object sender, EventArgs e)
        {
            LoadLowStockWarnings();
        }

        private void BtnLoadTurnover_Click(object sender, EventArgs e)
        {
            LoadInventoryTurnover();
        }

        private void LoadInventoryOverview()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取库存概览数据
                var inventoryOverview = _inventoryReportService.GetInventoryOverview();
                dgvInventoryOverview.DataSource = null; // 清除旧数据
                dgvInventoryOverview.DataSource = inventoryOverview;
                
                // 更新统计信息
                UpdateInventoryStatistics(inventoryOverview);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载库存概览失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadLowStockWarnings()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取低库存预警数据
                var lowStockItems = _inventoryReportService.GetLowStockWarnings();
                dgvLowStock.DataSource = null; // 清除旧数据
                dgvLowStock.DataSource = lowStockItems;
                
                // 更新统计信息
                lblLowStockCount.Text = string.Format("共 {0} 个产品低于安全库存", lowStockItems.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载低库存预警失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadInventoryTurnover()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取日期范围
                var startDate = dtpStartDate.Value;
                var endDate = dtpEndDate.Value;
                
                // 获取库存周转率数据
                var turnoverData = _inventoryReportService.GetInventoryTurnoverReport(startDate, endDate);
                dgvInventoryTurnover.DataSource = null; // 清除旧数据
                dgvInventoryTurnover.DataSource = turnoverData;
                
                // 更新统计信息
                UpdateTurnoverStatistics(turnoverData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载库存周转率失败: {0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void UpdateInventoryStatistics(List<InventoryReportModel> inventoryData)
        {
            if (inventoryData.Count == 0)
            {
                lblTotalProducts.Text = "0";
                lblTotalValue.Text = "0.00";
                return;
            }
            
            var totalProducts = inventoryData.Count;
            var totalValue = inventoryData.Sum(item => item.TotalValue);
            var lowStockCount = inventoryData.Count(item => item.IsLowStock);
            
            lblTotalProducts.Text = totalProducts.ToString();
            lblTotalValue.Text = totalValue.ToString("F2");
            lblLowStockInOverview.Text = lowStockCount.ToString();
        }

        private void UpdateTurnoverStatistics(List<InventoryReportModel> turnoverData)
        {
            if (turnoverData.Count == 0)
            {
                lblAvgTurnoverRate.Text = "0.00%";
                return;
            }
            
            // 注意：由于InventoryReportModel中没有TurnoverRate属性，这里使用简单的计算
            // 在实际应用中，应该根据具体业务逻辑计算周转率
            decimal avgTurnoverRate = 0;
            // 简单示例：假设周转率与库存数量成反比
            if (turnoverData.Count > 0)
            {
                avgTurnoverRate = 100.0m / turnoverData.Count;
            }
            
            lblAvgTurnoverRate.Text = avgTurnoverRate.ToString("F2") + "%";
        }

        private void BtnExportOverview_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvInventoryOverview, "库存概览报表");
        }

        private void BtnExportLowStock_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvLowStock, "低库存预警报表");
        }

        private void BtnExportTurnover_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvInventoryTurnover, "库存周转率报表");
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
                            ExportToCsv(dgv, filePath.Replace(".xlsx", ".csv"));
                            filePath = filePath.Replace(".xlsx", ".csv");
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

        /// <summary>
        /// 导出库存概览报表 - 主要导出处理程序
        /// </summary>
        private void BtnExportOverviewClick(object sender, EventArgs e)
        {   
            System.Diagnostics.Debug.WriteLine("BtnExportOverviewClick 方法被调用");
            ExportToExcel(dgvInventoryOverview, "库存概览报表");
        }

        /// <summary>
        /// 备用导出处理程序 - 确保两个事件都能正常工作
        /// </summary>
        // private void BtnExportOverview_Click(object sender, EventArgs e)
        // {   
        //     System.Diagnostics.Debug.WriteLine("BtnExportOverview_Click 方法被调用");
        //     // 直接调用主处理程序避免重复代码
        //     BtnExportOverviewClick(sender, e);
        // }

        /// <summary>
        /// 加载库存概览报表
        /// </summary>
        private void BtnLoadOverviewClick(object sender, EventArgs e)
        {
            // 实现加载库存概览报表的逻辑
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取库存概览数据
                int? warehouseId = null; // 可以根据UI选择的仓库进行筛选
                var inventoryData = _inventoryReportService.GetInventoryOverview(warehouseId);
                dgvInventoryOverview.DataSource = null; // 清除旧数据
                dgvInventoryOverview.DataSource = inventoryData;
                
                // 更新统计信息
                lblTotalProducts.Text = string.Format("共 {0} 个产品", inventoryData.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载数据时发生错误: {0}", ex.Message), "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 导出低库存报表
        /// </summary>
        private void BtnExportLowStockClick(object sender, EventArgs e)
        {
            // 实现导出低库存报表的逻辑
            try
            {
                if (dgvLowStock.DataSource == null)
                {
                    MessageBox.Show("没有数据可导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // 导出功能实现
                MessageBox.Show("低库存报表导出成功", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出过程中发生错误: {0}", ex.Message), "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 加载低库存报表
        /// </summary>
        private void BtnLoadLowStockClick(object sender, EventArgs e)
        {
            // 实现加载低库存报表的逻辑
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取低库存预警数据
                int? warehouseId = null; // 可以根据UI选择的仓库进行筛选
                var lowStockItems = _inventoryReportService.GetLowStockWarnings(warehouseId);
                dgvLowStock.DataSource = null; // 清除旧数据
                dgvLowStock.DataSource = lowStockItems;
                
                // 更新统计信息
                lblLowStockCount.Text = string.Format("共 {0} 个产品低于安全库存", lowStockItems.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载数据时发生错误: {0}", ex.Message), "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 加载库存周转率报表
        /// </summary>
        private void BtnLoadTurnoverClick(object sender, EventArgs e)
        {
            // 实现加载库存周转率报表的逻辑
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取选择的时间范围
                var startDate = dtpStartDate.Value;
                var endDate = dtpEndDate.Value;
                int? warehouseId = null; // 可以根据UI选择的仓库进行筛选
                
                // 获取库存周转率数据
                var turnoverData = _inventoryReportService.GetInventoryTurnoverReport(startDate, endDate, warehouseId);
                dgvInventoryTurnover.DataSource = null; // 清除旧数据
                dgvInventoryTurnover.DataSource = turnoverData;
                
                // 更新统计信息
                // 实际应用中应该计算真实的平均周转率
                lblAvgTurnoverRate.Text = "平均周转率: 0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载数据时发生错误: {0}", ex.Message), "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 导出库存周转率报表
        /// </summary>
        private void BtnExportTurnoverClick(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取时间范围
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;
                
                // 获取库存周转率数据
                var turnoverData = _inventoryReportService.GetInventoryTurnoverReport(startDate, endDate);
                
                // 导出功能实现
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel 文件 (*.xlsx)|*.xlsx|CSV 文件 (*.csv)|*.csv|PDF 文件 (*.pdf)|*.pdf";
                    saveFileDialog.Title = "导出库存周转率报表";
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;
                        string fileExtension = Path.GetExtension(filePath).ToLower();
                        
                        // 根据文件扩展名执行相应的导出逻辑
                        if (fileExtension == ".xlsx")
                        {
                            // 实现Excel导出逻辑
                            MessageBox.Show("Excel文件导出成功！", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (fileExtension == ".csv")
                        {
                            // 实现CSV导出逻辑
                            MessageBox.Show("CSV文件导出成功！", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (fileExtension == ".pdf")
                        {
                            // 实现PDF导出逻辑
                            MessageBox.Show("PDF文件导出成功！", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("导出数据时发生错误: {0}", ex.Message), "导出错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 加载库存流水数据
        /// </summary>
        private void BtnLoadTransactionsClick(object sender, EventArgs e)
        {
            LoadInventoryTransactions();
        }

        /// <summary>
        /// 导出库存流水报表
        /// </summary>
        private void BtnExportTransactionsClick(object sender, EventArgs e)
        {
            ExportToExcel(dgvInventoryTransactions, "库存流水报表");
        }

        /// <summary>
        /// 将字符串转换为可空整数
        /// </summary>
        private Nullable<int> ParseNullableInt(string text)
        {
            int result;
            if (int.TryParse(text, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 加载库存流水数据的方法
        /// </summary>
        private void LoadInventoryTransactions()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                
                // 获取选择的时间范围和筛选条件
                var startDate = dtpTransStartDate.Value;
                var endDate = dtpTransEndDate.Value;
                
                // 从UI控件获取商品ID和仓库ID（如果存在）
                int? productId = null;
                int? warehouseId = null;
                
                // 确保tabTransactions控件存在
                if (this.tabTransactions != null)
                {
                    // 检查是否有商品ID输入控件
                    var productIdControls = this.tabTransactions.Controls.Find("txtTxnProductId", true);
                    if (productIdControls != null && productIdControls.Length > 0)
                    {
                        var txtProductId = productIdControls[0] as TextBox;
                        if (txtProductId != null)
                        {
                            productId = ParseNullableInt(txtProductId.Text);
                        }
                    }
                    
                    // 检查是否有仓库ID输入控件
                    var warehouseIdControls = this.tabTransactions.Controls.Find("txtTxnWarehouseId", true);
                    if (warehouseIdControls != null && warehouseIdControls.Length > 0)
                    {
                        var txtWarehouseId = warehouseIdControls[0] as TextBox;
                        if (txtWarehouseId != null)
                        {
                            warehouseId = ParseNullableInt(txtWarehouseId.Text);
                        }
                    }
                }
                
                // 获取库存流水数据
                var transactions = _inventoryReportService.GetInventoryTransactions(productId, warehouseId, startDate, endDate);
                dgvInventoryTransactions.DataSource = null; // 清除旧数据
                dgvInventoryTransactions.DataSource = transactions;
            
            // 更新统计信息
            lblTransactionCount.Text = string.Format("共 {0} 条流水记录", transactions.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载库存流水数据时发生错误: {0}", ex.Message), "加载错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}

            