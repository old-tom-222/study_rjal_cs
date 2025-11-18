using System;
using System.Windows.Forms;

namespace CSproject.UI.Forms
{
    public partial class ReportsMainForm : Form
    {
        public ReportsMainForm()
        {
            InitializeComponent();
            // 默认显示经营看板
            BtnBusinessDashboardClick(null, null);
        }

        private void ReportsMainForm_Load(object sender, EventArgs e)
        {
            // 初始化报表界面
            UpdateStatusLabel("报表分析系统已就绪");
        }

        private void BtnInventoryReportsClick(object sender, EventArgs e)
        {
            // 清除现有内容
            panelReportContent.Controls.Clear();
            
            // 创建并显示实际的库存报表表单
            var inventoryReportForm = new InventoryReportForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            
            // 添加到内容面板
            panelReportContent.Controls.Add(inventoryReportForm);
            inventoryReportForm.Show();
            
            // 更新状态
            UpdateStatusLabel("库存报表模块已加载");
        }

        private void BtnSalesReportsClick(object sender, EventArgs e)
        {
            // 清除现有内容
            panelReportContent.Controls.Clear();
            
            // 创建销售报表占位表单
            var placeholderForm = new Form
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            
            // 添加标签说明
            var label = new Label
            {
                Text = "销售报表功能开发中...",
                Font = new System.Drawing.Font("Microsoft YaHei UI", 12F),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };
            placeholderForm.Controls.Add(label);
            
            // 添加到内容面板
            panelReportContent.Controls.Add(placeholderForm);
            placeholderForm.Show();
            
            // 更新状态
            UpdateStatusLabel("销售报表模块已加载");
        }

        private void BtnPurchaseReportsClick(object sender, EventArgs e)
        {
            // 清除现有内容
            panelReportContent.Controls.Clear();
            
            // 创建采购报表占位表单
            var placeholderForm = new Form
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            
            // 添加标签说明
            var label = new Label
            {
                Text = "采购报表功能开发中...",
                Font = new System.Drawing.Font("Microsoft YaHei UI", 12F),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };
            placeholderForm.Controls.Add(label);
            
            // 添加到内容面板
            panelReportContent.Controls.Add(placeholderForm);
            placeholderForm.Show();
            
            // 更新状态
            UpdateStatusLabel("采购报表模块已加载");
        }

        private void BtnBusinessDashboardClick(object sender, EventArgs e)
        {
            // 清除现有内容
            panelReportContent.Controls.Clear();
            
            // 创建经营看板占位表单
            var placeholderForm = new Form
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            
            // 添加标签说明
            var label = new Label
            {
                Text = "经营看板功能开发中...",
                Font = new System.Drawing.Font("Microsoft YaHei UI", 12F),
                AutoSize = true,
                Location = new System.Drawing.Point(20, 20)
            };
            placeholderForm.Controls.Add(label);
            
            // 添加到内容面板
            panelReportContent.Controls.Add(placeholderForm);
            placeholderForm.Show();
            
            // 更新状态
            UpdateStatusLabel("经营看板模块已加载");
        }

        private void UpdateStatusLabel(string message)
        {
            lblStatus.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss} - {1}", DateTime.Now, message);
        }

        private void BtnRefreshDataClick(object sender, EventArgs e)
        {
            // 刷新数据功能
            UpdateStatusLabel("正在刷新数据...");
            // 模拟数据刷新
            System.Threading.Thread.Sleep(1000);
            UpdateStatusLabel("数据刷新完成");
        }

        private void BtnExportReportClick(object sender, EventArgs e)
        {
            // 导出报表功能
            UpdateStatusLabel("准备导出报表...");
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel文件 (*.xlsx)|*.xlsx|CSV文件 (*.csv)|*.csv|PDF文件 (*.pdf)|*.pdf";
                saveFileDialog.Title = "导出报表";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatusLabel(string.Format("报表已导出到: {0}", saveFileDialog.FileName));
                }
                else
                {
                    UpdateStatusLabel("导出操作已取消");
                }
            }
        }

        


    }
}