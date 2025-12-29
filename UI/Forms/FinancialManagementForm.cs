using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class FinancialManagementForm : Form
    {
        private readonly AccountReceivableService _receivableService = new AccountReceivableService();
        private readonly AccountPayableService _payableService = new AccountPayableService();
        private readonly ProfitAnalysisService _profitService = new ProfitAnalysisService();
        private TabControl tabControl1;

        public FinancialManagementForm()
        {
            // 初始化基本窗体设置 - 与库存管理模块保持一致
            this.Size = new System.Drawing.Size(900, 600);
            this.Text = "财务管理";
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // 创建tabControl1
            tabControl1 = new TabControl();
            tabControl1.Dock = DockStyle.Fill;
            this.Controls.Add(tabControl1);
            
            this.Load += FinancialManagementForm_Load;
        }

        private void FinancialManagementForm_Load(object sender, EventArgs e)
        {
            // 设置选项卡
            tabControl1.TabPages.Add("应收账款");
            tabControl1.TabPages.Add("应付账款");
            tabControl1.TabPages.Add("利润分析");

            // 初始化应收账款界面
            InitializeReceivableTab();
            // 初始化应付账款界面
            InitializePayableTab();
            // 初始化利润分析界面
            InitializeProfitTab();

            // 加载所有标签页的数据
            LoadReceivables();
            LoadPayables();
            LoadProfitAnalysis();
        }

        #region 应收账款管理
        private DataGridView gridReceivables;
        private TextBox txtReceivableOrderNo;
        private TextBox txtReceivableCustomerId;
        private ComboBox cboReceivableStatus;
        private Button btnReceivableQuery;
        private Button btnRecordPayment;

        private void InitializeReceivableTab()
        {
            var tabPage = tabControl1.TabPages[0];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "订单号:", Location = new Point(20, 32) });
            txtReceivableOrderNo = new TextBox { Location = new Point(90, 30), Width = 150 };
            panelQuery.Controls.Add(txtReceivableOrderNo);

            panelQuery.Controls.Add(new Label { Text = "客户ID:", Location = new Point(260, 32) });
            txtReceivableCustomerId = new TextBox { Location = new Point(320, 30), Width = 100 };
            panelQuery.Controls.Add(txtReceivableCustomerId);

            panelQuery.Controls.Add(new Label { Text = "状态:", Location = new Point(440, 32) });
            cboReceivableStatus = new ComboBox { Location = new Point(490, 30), Width = 100 };
            cboReceivableStatus.Items.AddRange(new string[] { "", "pending", "partially_paid", "paid", "overdue" });
            panelQuery.Controls.Add(cboReceivableStatus);

            btnReceivableQuery = new Button { Text = "查询", Location = new Point(610, 30), Size = new Size(75, 23) };
            btnReceivableQuery.Click += BtnReceivableQuery_Click;
            panelQuery.Controls.Add(btnReceivableQuery);

            btnRecordPayment = new Button { Text = "记录付款", Location = new Point(700, 30), Size = new Size(75, 23) };
            btnRecordPayment.Click += BtnRecordPayment_Click;
            panelQuery.Controls.Add(btnRecordPayment);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridReceivables = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridReceivables);

            // 设置数据网格列
            SetupReceivableGrid();
        }

        private void SetupReceivableGrid()
        {
            gridReceivables.AutoGenerateColumns = false;
            gridReceivables.Columns.Clear();
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ReceivableId", HeaderText = "ID" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderNo", HeaderText = "订单号" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerId", HeaderText = "客户ID" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalAmount", HeaderText = "总金额" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaidAmount", HeaderText = "已付金额" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OutstandingAmount", HeaderText = "未付金额" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderDate", HeaderText = "订单日期" });
            gridReceivables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DueDate", HeaderText = "到期日期" });
            gridReceivables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridReceivables.ScrollBars = ScrollBars.Both;
        }

        private void LoadReceivables()
        {
            var orderNo = txtReceivableOrderNo?.Text?.Trim();
            int? customerId = ParseNullableInt(txtReceivableCustomerId?.Text);
            var status = cboReceivableStatus?.SelectedItem?.ToString();

            var list = _receivableService.GetAccountReceivables(orderNo, customerId, status);
            gridReceivables.DataSource = list;
        }

        private void BtnReceivableQuery_Click(object sender, EventArgs e)
        {
            LoadReceivables();
        }

        private void BtnRecordPayment_Click(object sender, EventArgs e)
        {
            if (gridReceivables.SelectedRows.Count > 0)
            {
                var receivable = (AccountReceivable)gridReceivables.SelectedRows[0].DataBoundItem;
                RecordPaymentForm form = new RecordPaymentForm(receivable);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadReceivables();
                }
            }
            else
            {
                MessageBox.Show("请选择要记录付款的应收账款", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 应付账款管理
        private DataGridView gridPayables;
        private TextBox txtPayableOrderNo;
        private TextBox txtPayableSupplierId;
        private ComboBox cboPayableStatus;
        private Button btnPayableQuery;
        private Button btnPayPayment;

        private void InitializePayableTab()
        {
            var tabPage = tabControl1.TabPages[1];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "订单号:", Location = new Point(20, 32) });
            txtPayableOrderNo = new TextBox { Location = new Point(90, 30), Width = 150 };
            panelQuery.Controls.Add(txtPayableOrderNo);

            panelQuery.Controls.Add(new Label { Text = "供应商ID:", Location = new Point(260, 32) });
            txtPayableSupplierId = new TextBox { Location = new Point(330, 30), Width = 100 };
            panelQuery.Controls.Add(txtPayableSupplierId);

            panelQuery.Controls.Add(new Label { Text = "状态:", Location = new Point(450, 32) });
            cboPayableStatus = new ComboBox { Location = new Point(500, 30), Width = 100 };
            cboPayableStatus.Items.AddRange(new string[] { "", "pending", "partially_paid", "paid", "overdue" });
            panelQuery.Controls.Add(cboPayableStatus);

            btnPayableQuery = new Button { Text = "查询", Location = new Point(620, 30), Size = new Size(75, 23) };
            btnPayableQuery.Click += BtnPayableQuery_Click;
            panelQuery.Controls.Add(btnPayableQuery);

            btnPayPayment = new Button { Text = "支付", Location = new Point(710, 30), Size = new Size(75, 23) };
            btnPayPayment.Click += BtnPayPayment_Click;
            panelQuery.Controls.Add(btnPayPayment);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridPayables = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridPayables);

            // 设置数据网格列
            SetupPayableGrid();
        }

        private void SetupPayableGrid()
        {
            gridPayables.AutoGenerateColumns = false;
            gridPayables.Columns.Clear();
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PayableId", HeaderText = "ID" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderNo", HeaderText = "订单号" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SupplierId", HeaderText = "供应商ID" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalAmount", HeaderText = "总金额" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PaidAmount", HeaderText = "已付金额" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OutstandingAmount", HeaderText = "未付金额" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "状态" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OrderDate", HeaderText = "订单日期" });
            gridPayables.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DueDate", HeaderText = "到期日期" });
            gridPayables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPayables.ScrollBars = ScrollBars.Both;
        }

        private void LoadPayables()
        {
            var orderNo = txtPayableOrderNo?.Text?.Trim();
            int? supplierId = ParseNullableInt(txtPayableSupplierId?.Text);
            var status = cboPayableStatus?.SelectedItem?.ToString();

            var list = _payableService.GetAccountPayables(orderNo, supplierId, status);
            gridPayables.DataSource = list;
        }

        private void BtnPayableQuery_Click(object sender, EventArgs e)
        {
            LoadPayables();
        }

        private void BtnPayPayment_Click(object sender, EventArgs e)
        {
            if (gridPayables.SelectedRows.Count > 0)
            {
                var payable = (AccountPayable)gridPayables.SelectedRows[0].DataBoundItem;
                PayPaymentForm form = new PayPaymentForm(payable);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPayables();
                }
            }
            else
            {
                MessageBox.Show("请选择要支付的应付账款", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 利润分析
        private DataGridView gridProfit;
        private DateTimePicker dtProfitFrom;
        private DateTimePicker dtProfitTo;
        private ComboBox cboProfitType;
        private Button btnProfitQuery;

        private void InitializeProfitTab()
        {
            var tabPage = tabControl1.TabPages[2];
            tabPage.Controls.Clear();

            // 创建查询控件 - 与库存管理模块保持一致的布局
            var panelQuery = new Panel { Dock = DockStyle.Top, Height = 80 };
            tabPage.Controls.Add(panelQuery);

            panelQuery.Controls.Add(new Label { Text = "起始日期:", Location = new Point(20, 32) });
            dtProfitFrom = new DateTimePicker { Location = new Point(90, 30), Width = 150, Value = DateTime.Now.AddMonths(-1) };
            panelQuery.Controls.Add(dtProfitFrom);

            panelQuery.Controls.Add(new Label { Text = "结束日期:", Location = new Point(260, 32) });
            dtProfitTo = new DateTimePicker { Location = new Point(330, 30), Width = 150, Value = DateTime.Now };
            panelQuery.Controls.Add(dtProfitTo);

            panelQuery.Controls.Add(new Label { Text = "分析类型:", Location = new Point(490, 32) });
            cboProfitType = new ComboBox { Location = new Point(560, 30), Width = 100 };
            cboProfitType.Items.AddRange(new string[] { "按日期", "按产品" });
            cboProfitType.SelectedIndex = 0;
            panelQuery.Controls.Add(cboProfitType);

            btnProfitQuery = new Button { Text = "查询", Location = new Point(680, 30), Size = new Size(75, 23) };
            btnProfitQuery.Click += BtnProfitQuery_Click;
            panelQuery.Controls.Add(btnProfitQuery);

            // 创建数据网格 - 与库存管理模块保持一致的定位
            gridProfit = new DataGridView { 
                Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                            | System.Windows.Forms.AnchorStyles.Left) 
                            | System.Windows.Forms.AnchorStyles.Right))),
                Location = new Point(20, 80),
                Size = new Size(850, 470),
                ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            };
            tabPage.Controls.Add(gridProfit);

            // 设置数据网格列
            SetupProfitGrid();
        }

        private void SetupProfitGrid()
        {
            gridProfit.AutoGenerateColumns = false;
            gridProfit.Columns.Clear();
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AnalysisDate", HeaderText = "日期" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductId", HeaderText = "产品ID" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "产品名称" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalRevenue", HeaderText = "总收入" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalCost", HeaderText = "总成本" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Profit", HeaderText = "利润" });
            gridProfit.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ProfitMargin", HeaderText = "利润率" });
            gridProfit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridProfit.ScrollBars = ScrollBars.Both;
        }

        private void LoadProfitAnalysis()
        {
            var fromDate = dtProfitFrom.Value;
            var toDate = dtProfitTo.Value;
            var analysisType = cboProfitType.SelectedItem.ToString();

            if (analysisType == "按日期")
            {
                var analysis = _profitService.GetProfitAnalysis(fromDate, toDate);
                // 创建一个包含单个分析对象的列表，以便DataGridView可以显示
                gridProfit.DataSource = new List<ProfitAnalysis> { analysis };
            }
            else if (analysisType == "按产品")
            {
                var list = _profitService.GetProductProfitAnalysis(fromDate, toDate);
                gridProfit.DataSource = list;
            }
        }

        private void BtnProfitQuery_Click(object sender, EventArgs e)
        {
            LoadProfitAnalysis();
        }
        #endregion

        #region 通用方法
        private int? ParseNullableInt(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            int val;
            if (int.TryParse(text, out val)) return val;
            return null;
        }
        #endregion
    }
}
