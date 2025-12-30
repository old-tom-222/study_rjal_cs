namespace CSproject.UI.Forms
{
    partial class BusinessDashboardForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
 private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dashboardHeaderPanel = new System.Windows.Forms.Panel();
            this.btnExportDashboard = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSetDashboardRange = new System.Windows.Forms.Button();
            this.dtpDashboardRange = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.kpiPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.salesKpiCard = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSalesChange = new System.Windows.Forms.Label();
            this.lblTotalSalesAmount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.purchaseKpiCard = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPurchaseChange = new System.Windows.Forms.Label();
            this.lblTotalPurchaseAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.profitKpiCard = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblProfitChange = new System.Windows.Forms.Label();
            this.lblTotalProfit = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.inventoryKpiCard = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrentInventoryValue = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.salesOrdersKpiCard = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSalesOrdersCount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.purchaseOrdersKpiCard = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblPurchaseOrdersCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lowStockKpiCard = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.lblLowStockItemsCount = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.dailySalesKpiCard = new System.Windows.Forms.Panel();
            this.label17 = new System.Windows.Forms.Label();
            this.lblAvgDailySales = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.chartsPanel = new System.Windows.Forms.Panel();
            this.chartSalesTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartInventoryStatus = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartSalesByCategory = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.topProductsTab = new System.Windows.Forms.TabPage();
            this.dgvTopSellingProducts = new System.Windows.Forms.DataGridView();
            this.recentTransactionsTab = new System.Windows.Forms.TabPage();
            this.dgvRecentTransactions = new System.Windows.Forms.DataGridView();
            this.dataTabControl = new System.Windows.Forms.TabControl();
            this.dashboardHeaderPanel.SuspendLayout();
            this.kpiPanel.SuspendLayout();
            this.salesKpiCard.SuspendLayout();
            this.purchaseKpiCard.SuspendLayout();
            this.profitKpiCard.SuspendLayout();
            this.inventoryKpiCard.SuspendLayout();
            this.salesOrdersKpiCard.SuspendLayout();
            this.purchaseOrdersKpiCard.SuspendLayout();
            this.lowStockKpiCard.SuspendLayout();
            this.dailySalesKpiCard.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.chartsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesTrend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartInventoryStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSalesByCategory)).BeginInit();
            this.dataPanel.SuspendLayout();
            this.topProductsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSellingProducts)).BeginInit();
            this.recentTransactionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentTransactions)).BeginInit();
            this.dataTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashboardHeaderPanel
            // 
            this.dashboardHeaderPanel.Controls.Add(this.btnExportDashboard);
            this.dashboardHeaderPanel.Controls.Add(this.btnRefresh);
            this.dashboardHeaderPanel.Controls.Add(this.btnSetDashboardRange);
            this.dashboardHeaderPanel.Controls.Add(this.dtpDashboardRange);
            this.dashboardHeaderPanel.Controls.Add(this.label1);
            this.dashboardHeaderPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.dashboardHeaderPanel.Location = new System.Drawing.Point(0, 0);
            this.dashboardHeaderPanel.Name = "dashboardHeaderPanel";
            this.dashboardHeaderPanel.Size = new System.Drawing.Size(1024, 50);
            this.dashboardHeaderPanel.TabIndex = 0;
            // 
            // btnExportDashboard
            // 
            this.btnExportDashboard.Location = new System.Drawing.Point(1067, 12);
            this.btnExportDashboard.Name = "btnExportDashboard";
            this.btnExportDashboard.Size = new System.Drawing.Size(75, 23);
            this.btnExportDashboard.TabIndex = 5;
            this.btnExportDashboard.Text = "导出";
            this.btnExportDashboard.UseVisualStyleBackColor = true;
            this.btnExportDashboard.Click += new System.EventHandler(this.BtnExportDashboard_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(986, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // btnSetDashboardRange
            // 
            this.btnSetDashboardRange.Location = new System.Drawing.Point(805, 12);
            this.btnSetDashboardRange.Name = "btnSetDashboardRange";
            this.btnSetDashboardRange.Size = new System.Drawing.Size(75, 23);
            this.btnSetDashboardRange.TabIndex = 3;
            this.btnSetDashboardRange.Text = "设置";
            this.btnSetDashboardRange.UseVisualStyleBackColor = true;
            this.btnSetDashboardRange.Click += new System.EventHandler(this.BtnSetDashboardRange_Click);
            // 
            // dtpDashboardRange
            // 
            this.dtpDashboardRange.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDashboardRange.Location = new System.Drawing.Point(150, 15);
            this.dtpDashboardRange.Name = "dtpDashboardRange";
            this.dtpDashboardRange.Size = new System.Drawing.Size(150, 21);
            this.dtpDashboardRange.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "统计数据起始日期：";
            // 
            // kpiPanel
            // 
            this.kpiPanel.AutoScroll = true;
            this.kpiPanel.Controls.Add(this.salesKpiCard);
            this.kpiPanel.Controls.Add(this.purchaseKpiCard);
            this.kpiPanel.Controls.Add(this.profitKpiCard);
            this.kpiPanel.Controls.Add(this.inventoryKpiCard);
            this.kpiPanel.Controls.Add(this.salesOrdersKpiCard);
            this.kpiPanel.Controls.Add(this.purchaseOrdersKpiCard);
            this.kpiPanel.Controls.Add(this.lowStockKpiCard);
            this.kpiPanel.Controls.Add(this.dailySalesKpiCard);
            this.kpiPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.kpiPanel.Location = new System.Drawing.Point(0, 50);
            this.kpiPanel.Name = "kpiPanel";
            this.kpiPanel.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.kpiPanel.Size = new System.Drawing.Size(1024, 140);
            this.kpiPanel.TabIndex = 1;
            // 
            // salesKpiCard
            // 
            this.salesKpiCard.BackColor = System.Drawing.Color.White;
            this.salesKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salesKpiCard.Controls.Add(this.label3);
            this.salesKpiCard.Controls.Add(this.lblSalesChange);
            this.salesKpiCard.Controls.Add(this.lblTotalSalesAmount);
            this.salesKpiCard.Controls.Add(this.label2);
            this.salesKpiCard.Location = new System.Drawing.Point(10, 10);
            this.salesKpiCard.Name = "salesKpiCard";
            this.salesKpiCard.Size = new System.Drawing.Size(120, 110);
            this.salesKpiCard.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "环比变化:";
            // 
            // lblSalesChange
            // 
            this.lblSalesChange.AutoSize = true;
            this.lblSalesChange.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesChange.Location = new System.Drawing.Point(70, 80);
            this.lblSalesChange.Name = "lblSalesChange";
            this.lblSalesChange.Size = new System.Drawing.Size(41, 17);
            this.lblSalesChange.TabIndex = 2;
            this.lblSalesChange.Text = "0.0%";
            // 
            // lblTotalSalesAmount
            // 
            this.lblTotalSalesAmount.AutoSize = true;
            this.lblTotalSalesAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSalesAmount.Location = new System.Drawing.Point(10, 40);
            this.lblTotalSalesAmount.Name = "lblTotalSalesAmount";
            this.lblTotalSalesAmount.Size = new System.Drawing.Size(46, 25);
            this.lblTotalSalesAmount.TabIndex = 1;
            this.lblTotalSalesAmount.Text = "0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "销售总额";
            // 
            // purchaseKpiCard
            // 
            this.purchaseKpiCard.BackColor = System.Drawing.Color.White;
            this.purchaseKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.purchaseKpiCard.Controls.Add(this.label5);
            this.purchaseKpiCard.Controls.Add(this.lblPurchaseChange);
            this.purchaseKpiCard.Controls.Add(this.lblTotalPurchaseAmount);
            this.purchaseKpiCard.Controls.Add(this.label4);
            this.purchaseKpiCard.Location = new System.Drawing.Point(140, 10);
            this.purchaseKpiCard.Name = "purchaseKpiCard";
            this.purchaseKpiCard.Size = new System.Drawing.Size(120, 110);
            this.purchaseKpiCard.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "环比变化:";
            // 
            // lblPurchaseChange
            // 
            this.lblPurchaseChange.AutoSize = true;
            this.lblPurchaseChange.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseChange.Location = new System.Drawing.Point(70, 80);
            this.lblPurchaseChange.Name = "lblPurchaseChange";
            this.lblPurchaseChange.Size = new System.Drawing.Size(41, 17);
            this.lblPurchaseChange.TabIndex = 2;
            this.lblPurchaseChange.Text = "0.0%";
            // 
            // lblTotalPurchaseAmount
            // 
            this.lblTotalPurchaseAmount.AutoSize = true;
            this.lblTotalPurchaseAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPurchaseAmount.Location = new System.Drawing.Point(10, 40);
            this.lblTotalPurchaseAmount.Name = "lblTotalPurchaseAmount";
            this.lblTotalPurchaseAmount.Size = new System.Drawing.Size(46, 25);
            this.lblTotalPurchaseAmount.TabIndex = 1;
            this.lblTotalPurchaseAmount.Text = "0.0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "采购总额";
            // 
            // profitKpiCard
            // 
            this.profitKpiCard.BackColor = System.Drawing.Color.White;
            this.profitKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.profitKpiCard.Controls.Add(this.label7);
            this.profitKpiCard.Controls.Add(this.lblProfitChange);
            this.profitKpiCard.Controls.Add(this.lblTotalProfit);
            this.profitKpiCard.Controls.Add(this.label6);
            this.profitKpiCard.Location = new System.Drawing.Point(270, 10);
            this.profitKpiCard.Name = "profitKpiCard";
            this.profitKpiCard.Size = new System.Drawing.Size(120, 110);
            this.profitKpiCard.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(10, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "环比变化:";
            // 
            // lblProfitChange
            // 
            this.lblProfitChange.AutoSize = true;
            this.lblProfitChange.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfitChange.Location = new System.Drawing.Point(70, 80);
            this.lblProfitChange.Name = "lblProfitChange";
            this.lblProfitChange.Size = new System.Drawing.Size(41, 17);
            this.lblProfitChange.TabIndex = 2;
            this.lblProfitChange.Text = "0.0%";
            // 
            // lblTotalProfit
            // 
            this.lblTotalProfit.AutoSize = true;
            this.lblTotalProfit.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProfit.Location = new System.Drawing.Point(10, 40);
            this.lblTotalProfit.Name = "lblTotalProfit";
            this.lblTotalProfit.Size = new System.Drawing.Size(46, 25);
            this.lblTotalProfit.TabIndex = 1;
            this.lblTotalProfit.Text = "0.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "利润总额";
            // 
            // inventoryKpiCard
            // 
            this.inventoryKpiCard.BackColor = System.Drawing.Color.White;
            this.inventoryKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inventoryKpiCard.Controls.Add(this.label9);
            this.inventoryKpiCard.Controls.Add(this.lblCurrentInventoryValue);
            this.inventoryKpiCard.Controls.Add(this.label8);
            this.inventoryKpiCard.Location = new System.Drawing.Point(400, 10);
            this.inventoryKpiCard.Name = "inventoryKpiCard";
            this.inventoryKpiCard.Size = new System.Drawing.Size(120, 110);
            this.inventoryKpiCard.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "当前库存价值";
            // 
            // lblCurrentInventoryValue
            // 
            this.lblCurrentInventoryValue.AutoSize = true;
            this.lblCurrentInventoryValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentInventoryValue.Location = new System.Drawing.Point(10, 40);
            this.lblCurrentInventoryValue.Name = "lblCurrentInventoryValue";
            this.lblCurrentInventoryValue.Size = new System.Drawing.Size(46, 25);
            this.lblCurrentInventoryValue.TabIndex = 1;
            this.lblCurrentInventoryValue.Text = "0.0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "库存价值";
            // 
            // salesOrdersKpiCard
            // 
            this.salesOrdersKpiCard.BackColor = System.Drawing.Color.White;
            this.salesOrdersKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.salesOrdersKpiCard.Controls.Add(this.label11);
            this.salesOrdersKpiCard.Controls.Add(this.lblSalesOrdersCount);
            this.salesOrdersKpiCard.Controls.Add(this.label10);
            this.salesOrdersKpiCard.Location = new System.Drawing.Point(530, 10);
            this.salesOrdersKpiCard.Name = "salesOrdersKpiCard";
            this.salesOrdersKpiCard.Size = new System.Drawing.Size(120, 110);
            this.salesOrdersKpiCard.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(10, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "销售订单数量";
            // 
            // lblSalesOrdersCount
            // 
            this.lblSalesOrdersCount.AutoSize = true;
            this.lblSalesOrdersCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesOrdersCount.Location = new System.Drawing.Point(10, 40);
            this.lblSalesOrdersCount.Name = "lblSalesOrdersCount";
            this.lblSalesOrdersCount.Size = new System.Drawing.Size(20, 25);
            this.lblSalesOrdersCount.TabIndex = 1;
            this.lblSalesOrdersCount.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "销售订单";
            // 
            // purchaseOrdersKpiCard
            // 
            this.purchaseOrdersKpiCard.BackColor = System.Drawing.Color.White;
            this.purchaseOrdersKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.purchaseOrdersKpiCard.Controls.Add(this.label13);
            this.purchaseOrdersKpiCard.Controls.Add(this.lblPurchaseOrdersCount);
            this.purchaseOrdersKpiCard.Controls.Add(this.label12);
            this.purchaseOrdersKpiCard.Location = new System.Drawing.Point(660, 10);
            this.purchaseOrdersKpiCard.Name = "purchaseOrdersKpiCard";
            this.purchaseOrdersKpiCard.Size = new System.Drawing.Size(120, 110);
            this.purchaseOrdersKpiCard.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(10, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "采购订单数量";
            // 
            // lblPurchaseOrdersCount
            // 
            this.lblPurchaseOrdersCount.AutoSize = true;
            this.lblPurchaseOrdersCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseOrdersCount.Location = new System.Drawing.Point(10, 40);
            this.lblPurchaseOrdersCount.Name = "lblPurchaseOrdersCount";
            this.lblPurchaseOrdersCount.Size = new System.Drawing.Size(20, 25);
            this.lblPurchaseOrdersCount.TabIndex = 1;
            this.lblPurchaseOrdersCount.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "采购订单";
            // 
            // lowStockKpiCard
            // 
            this.lowStockKpiCard.BackColor = System.Drawing.Color.White;
            this.lowStockKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lowStockKpiCard.Controls.Add(this.label15);
            this.lowStockKpiCard.Controls.Add(this.lblLowStockItemsCount);
            this.lowStockKpiCard.Controls.Add(this.label14);
            this.lowStockKpiCard.Location = new System.Drawing.Point(790, 10);
            this.lowStockKpiCard.Name = "lowStockKpiCard";
            this.lowStockKpiCard.Size = new System.Drawing.Size(120, 110);
            this.lowStockKpiCard.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(10, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 17);
            this.label15.TabIndex = 2;
            this.label15.Text = "库存预警产品";
            // 
            // lblLowStockItemsCount
            // 
            this.lblLowStockItemsCount.AutoSize = true;
            this.lblLowStockItemsCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockItemsCount.Location = new System.Drawing.Point(10, 40);
            this.lblLowStockItemsCount.Name = "lblLowStockItemsCount";
            this.lblLowStockItemsCount.Size = new System.Drawing.Size(20, 25);
            this.lblLowStockItemsCount.TabIndex = 1;
            this.lblLowStockItemsCount.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 20);
            this.label14.TabIndex = 0;
            this.label14.Text = "库存预警";
            // 
            // dailySalesKpiCard
            // 
            this.dailySalesKpiCard.BackColor = System.Drawing.Color.White;
            this.dailySalesKpiCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dailySalesKpiCard.Controls.Add(this.label17);
            this.dailySalesKpiCard.Controls.Add(this.lblAvgDailySales);
            this.dailySalesKpiCard.Controls.Add(this.label16);
            this.dailySalesKpiCard.Location = new System.Drawing.Point(920, 10);
            this.dailySalesKpiCard.Name = "dailySalesKpiCard";
            this.dailySalesKpiCard.Size = new System.Drawing.Size(120, 110);
            this.dailySalesKpiCard.TabIndex = 7;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(10, 80);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 17);
            this.label17.TabIndex = 2;
            this.label17.Text = "日均销售额";
            // 
            // lblAvgDailySales
            // 
            this.lblAvgDailySales.AutoSize = true;
            this.lblAvgDailySales.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgDailySales.Location = new System.Drawing.Point(10, 40);
            this.lblAvgDailySales.Name = "lblAvgDailySales";
            this.lblAvgDailySales.Size = new System.Drawing.Size(46, 25);
            this.lblAvgDailySales.TabIndex = 1;
            this.lblAvgDailySales.Text = "0.0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(10, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "日均销售";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.chartsPanel);
            this.contentPanel.Controls.Add(this.dataPanel);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 190);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1024, 560);
            this.contentPanel.TabIndex = 2;
            // 
            // chartsPanel
            // 
            this.chartsPanel.Controls.Add(this.chartSalesTrend);
            this.chartsPanel.Controls.Add(this.chartInventoryStatus);
            this.chartsPanel.Controls.Add(this.chartSalesByCategory);
            this.chartsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartsPanel.Location = new System.Drawing.Point(0, 0);
            this.chartsPanel.Name = "chartsPanel";
            this.chartsPanel.Size = new System.Drawing.Size(1024, 300);
            this.chartsPanel.TabIndex = 0;
            // 
            // chartSalesTrend
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSalesTrend.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSalesTrend.Legends.Add(legend1);
            this.chartSalesTrend.Location = new System.Drawing.Point(10, 10);
            this.chartSalesTrend.Name = "chartSalesTrend";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartSalesTrend.Series.Add(series1);
            this.chartSalesTrend.Size = new System.Drawing.Size(500, 280);
            this.chartSalesTrend.TabIndex = 0;
            this.chartSalesTrend.Text = "销售趋势图";
            // 
            // chartInventoryStatus
            // 
            chartArea2.Name = "ChartArea1";
            this.chartInventoryStatus.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartInventoryStatus.Legends.Add(legend2);
            this.chartInventoryStatus.Location = new System.Drawing.Point(520, 10);
            this.chartInventoryStatus.Name = "chartInventoryStatus";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartInventoryStatus.Series.Add(series2);
            this.chartInventoryStatus.Size = new System.Drawing.Size(240, 280);
            this.chartInventoryStatus.TabIndex = 1;
            this.chartInventoryStatus.Text = "库存状态图";
            // 
            // chartSalesByCategory
            // 
            chartArea3.Name = "ChartArea1";
            this.chartSalesByCategory.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartSalesByCategory.Legends.Add(legend3);
            this.chartSalesByCategory.Location = new System.Drawing.Point(770, 10);
            this.chartSalesByCategory.Name = "chartSalesByCategory";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartSalesByCategory.Series.Add(series3);
            this.chartSalesByCategory.Size = new System.Drawing.Size(240, 280);
            this.chartSalesByCategory.TabIndex = 2;
            this.chartSalesByCategory.Text = "销售分类图";
            // 
            // dataPanel
            // 
            this.dataPanel.Controls.Add(this.dataTabControl);
            this.dataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataPanel.Location = new System.Drawing.Point(0, 300);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(1024, 260);
            this.dataPanel.TabIndex = 1;
            // 
            // topProductsTab
            // 
            this.topProductsTab.Controls.Add(this.dgvTopSellingProducts);
            this.topProductsTab.Location = new System.Drawing.Point(4, 22);
            this.topProductsTab.Name = "topProductsTab";
            this.topProductsTab.Padding = new System.Windows.Forms.Padding(3);
            this.topProductsTab.Size = new System.Drawing.Size(1016, 234);
            this.topProductsTab.TabIndex = 0;
            this.topProductsTab.Text = "热销产品";
            this.topProductsTab.UseVisualStyleBackColor = true;
            // 
            // dgvTopSellingProducts
            // 
            this.dgvTopSellingProducts.AllowUserToAddRows = false;
            this.dgvTopSellingProducts.AllowUserToDeleteRows = false;
            this.dgvTopSellingProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopSellingProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopSellingProducts.Location = new System.Drawing.Point(0, 0);
            this.dgvTopSellingProducts.Name = "dgvTopSellingProducts";
            this.dgvTopSellingProducts.ReadOnly = true;
            this.dgvTopSellingProducts.Size = new System.Drawing.Size(1016, 234);
            this.dgvTopSellingProducts.TabIndex = 0;
            // 
            // recentTransactionsTab
            // 
            this.recentTransactionsTab.Controls.Add(this.dgvRecentTransactions);
            this.recentTransactionsTab.Location = new System.Drawing.Point(4, 22);
            this.recentTransactionsTab.Name = "recentTransactionsTab";
            this.recentTransactionsTab.Padding = new System.Windows.Forms.Padding(3);
            this.recentTransactionsTab.Size = new System.Drawing.Size(1016, 234);
            this.recentTransactionsTab.TabIndex = 1;
            this.recentTransactionsTab.Text = "最近交易";
            this.recentTransactionsTab.UseVisualStyleBackColor = true;
            // 
            // dgvRecentTransactions
            // 
            this.dgvRecentTransactions.AllowUserToAddRows = false;
            this.dgvRecentTransactions.AllowUserToDeleteRows = false;
            this.dgvRecentTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentTransactions.Location = new System.Drawing.Point(0, 0);
            this.dgvRecentTransactions.Name = "dgvRecentTransactions";
            this.dgvRecentTransactions.ReadOnly = true;
            this.dgvRecentTransactions.Size = new System.Drawing.Size(1016, 234);
            this.dgvRecentTransactions.TabIndex = 0;
            // 
            // dataTabControl
            // 
            this.dataTabControl.Controls.Add(this.topProductsTab);
            this.dataTabControl.Controls.Add(this.recentTransactionsTab);
            this.dataTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataTabControl.Location = new System.Drawing.Point(0, 0);
            this.dataTabControl.Name = "dataTabControl";
            this.dataTabControl.SelectedIndex = 0;
            this.dataTabControl.Size = new System.Drawing.Size(1024, 260);
            this.dataTabControl.TabIndex = 0;
            // 
            // BusinessDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 750);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.kpiPanel);
            this.Controls.Add(this.dashboardHeaderPanel);
            this.Name = "BusinessDashboardForm";
            this.Text = "经营看板";
            this.Load += new System.EventHandler(this.BusinessDashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel dashboardHeaderPanel;
        private System.Windows.Forms.Button btnExportDashboard;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSetDashboardRange;
        private System.Windows.Forms.DateTimePicker dtpDashboardRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel kpiPanel;
        private System.Windows.Forms.Panel salesKpiCard;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSalesChange;
        private System.Windows.Forms.Label lblTotalSalesAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel purchaseKpiCard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPurchaseChange;
        private System.Windows.Forms.Label lblTotalPurchaseAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel profitKpiCard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblProfitChange;
        private System.Windows.Forms.Label lblTotalProfit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel inventoryKpiCard;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCurrentInventoryValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel salesOrdersKpiCard;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSalesOrdersCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel purchaseOrdersKpiCard;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblPurchaseOrdersCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel lowStockKpiCard;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblLowStockItemsCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel dailySalesKpiCard;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblAvgDailySales;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel chartsPanel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesTrend;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartInventoryStatus;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSalesByCategory;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.TabPage topProductsTab;
        private System.Windows.Forms.DataGridView dgvTopSellingProducts;
        private System.Windows.Forms.TabPage recentTransactionsTab;
        private System.Windows.Forms.DataGridView dgvRecentTransactions;
        private System.Windows.Forms.TabControl dataTabControl;

    }
}