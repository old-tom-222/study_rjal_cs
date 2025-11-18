namespace CSproject.UI.Forms
{
    partial class InventoryReportForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabInventoryOverview = new System.Windows.Forms.TabPage();
            this.btnExportOverview = new System.Windows.Forms.Button();
            this.btnLoadOverview = new System.Windows.Forms.Button();
            this.dgvInventoryOverview = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLowStockInOverview = new System.Windows.Forms.Label();
            this.tabLowStock = new System.Windows.Forms.TabPage();
            this.btnExportLowStock = new System.Windows.Forms.Button();
            this.btnLoadLowStock = new System.Windows.Forms.Button();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.lblLowStockCount = new System.Windows.Forms.Label();
            this.tabTurnover = new System.Windows.Forms.TabPage();
            this.btnExportTurnover = new System.Windows.Forms.Button();
            this.btnLoadTurnover = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvInventoryTurnover = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAvgTurnoverRate = new System.Windows.Forms.Label();
            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.btnExportTransactions = new System.Windows.Forms.Button();
            this.btnLoadTransactions = new System.Windows.Forms.Button();
            this.dtpTransEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpTransStartDate = new System.Windows.Forms.DateTimePicker();
            this.labelTransEndDate = new System.Windows.Forms.Label();
            this.labelTransStartDate = new System.Windows.Forms.Label();
            this.txtTxnProductId = new System.Windows.Forms.TextBox();
            this.txtTxnWarehouseId = new System.Windows.Forms.TextBox();
            this.labelTxnProductId = new System.Windows.Forms.Label();
            this.labelTxnWarehouseId = new System.Windows.Forms.Label();
            this.dgvInventoryTransactions = new System.Windows.Forms.DataGridView();
            this.lblTransactionCount = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabInventoryOverview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryOverview)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.tabTurnover.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryTurnover)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabInventoryOverview);
        this.tabControl1.Controls.Add(this.tabLowStock);
        this.tabControl1.Controls.Add(this.tabTurnover);
        this.tabControl1.Controls.Add(this.tabTransactions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabInventoryOverview
            // 
            this.tabInventoryOverview.Controls.Add(this.btnExportOverview);
            this.tabInventoryOverview.Controls.Add(this.btnLoadOverview);
            this.tabInventoryOverview.Controls.Add(this.dgvInventoryOverview);
            this.tabInventoryOverview.Controls.Add(this.flowLayoutPanel1);
            this.tabInventoryOverview.Location = new System.Drawing.Point(4, 22);
            this.tabInventoryOverview.Name = "tabInventoryOverview";
            this.tabInventoryOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventoryOverview.Size = new System.Drawing.Size(792, 424);
            this.tabInventoryOverview.TabIndex = 0;
            this.tabInventoryOverview.Text = "库存概览";
            this.tabInventoryOverview.UseVisualStyleBackColor = true;
            // 
            // btnExportOverview
            // 
            this.btnExportOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportOverview.Location = new System.Drawing.Point(700, 10);
            this.btnExportOverview.Name = "btnExportOverview";
            this.btnExportOverview.Size = new System.Drawing.Size(80, 30);
            this.btnExportOverview.TabIndex = 3;
            this.btnExportOverview.Text = "导出";
            this.btnExportOverview.UseVisualStyleBackColor = true;
            this.btnExportOverview.Click += new System.EventHandler(this.BtnExportOverviewClick);
            // 
            // btnLoadOverview
            // 
            this.btnLoadOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadOverview.Location = new System.Drawing.Point(610, 10);
            this.btnLoadOverview.Name = "btnLoadOverview";
            this.btnLoadOverview.Size = new System.Drawing.Size(80, 30);
            this.btnLoadOverview.TabIndex = 2;
            this.btnLoadOverview.Text = "加载";
            this.btnLoadOverview.UseVisualStyleBackColor = true;
            this.btnLoadOverview.Click += new System.EventHandler(this.BtnLoadOverviewClick);
            // 
            // dgvInventoryOverview
            // 
            this.dgvInventoryOverview.AllowUserToAddRows = false;
            this.dgvInventoryOverview.AllowUserToDeleteRows = false;
            this.dgvInventoryOverview.AllowUserToOrderColumns = true;
            this.dgvInventoryOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventoryOverview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryOverview.Location = new System.Drawing.Point(6, 80);
            this.dgvInventoryOverview.Name = "dgvInventoryOverview";
            this.dgvInventoryOverview.ReadOnly = true;
            this.dgvInventoryOverview.Size = new System.Drawing.Size(776, 334);
            this.dgvInventoryOverview.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lblTotalProducts);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.lblTotalValue);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.lblLowStockInOverview);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 10);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(600, 60);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品总数: ";
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProducts.Location = new System.Drawing.Point(86, 0);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(17, 17);
            this.lblTotalProducts.TabIndex = 1;
            this.lblTotalProducts.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(109, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "库存总值: ";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalValue.Location = new System.Drawing.Point(192, 0);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(41, 17);
            this.lblTotalValue.TabIndex = 3;
            this.lblTotalValue.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(239, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "低库存数: ";
            // 
            // lblLowStockInOverview
            // 
            this.lblLowStockInOverview.AutoSize = true;
            this.lblLowStockInOverview.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockInOverview.ForeColor = System.Drawing.Color.Red;
            this.lblLowStockInOverview.Location = new System.Drawing.Point(322, 0);
            this.lblLowStockInOverview.Name = "lblLowStockInOverview";
            this.lblLowStockInOverview.Size = new System.Drawing.Size(17, 17);
            this.lblLowStockInOverview.TabIndex = 5;
            this.lblLowStockInOverview.Text = "0";
            // 
            // tabLowStock
            // 
            this.tabLowStock.Controls.Add(this.btnExportLowStock);
            this.tabLowStock.Controls.Add(this.btnLoadLowStock);
            this.tabLowStock.Controls.Add(this.dgvLowStock);
            this.tabLowStock.Controls.Add(this.lblLowStockCount);
            this.tabLowStock.Location = new System.Drawing.Point(4, 22);
            this.tabLowStock.Name = "tabLowStock";
            this.tabLowStock.Padding = new System.Windows.Forms.Padding(3);
            this.tabLowStock.Size = new System.Drawing.Size(792, 424);
            this.tabLowStock.TabIndex = 1;
            this.tabLowStock.Text = "低库存预警";
            this.tabLowStock.UseVisualStyleBackColor = true;
            // 
            // btnExportLowStock
            // 
            this.btnExportLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportLowStock.Location = new System.Drawing.Point(700, 10);
            this.btnExportLowStock.Name = "btnExportLowStock";
            this.btnExportLowStock.Size = new System.Drawing.Size(80, 30);
            this.btnExportLowStock.TabIndex = 3;
            this.btnExportLowStock.Text = "导出";
            this.btnExportLowStock.UseVisualStyleBackColor = true;
            this.btnExportLowStock.Click += new System.EventHandler(this.BtnExportLowStockClick);
            // 
            // btnLoadLowStock
            // 
            this.btnLoadLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadLowStock.Location = new System.Drawing.Point(610, 10);
            this.btnLoadLowStock.Name = "btnLoadLowStock";
            this.btnLoadLowStock.Size = new System.Drawing.Size(80, 30);
            this.btnLoadLowStock.TabIndex = 2;
            this.btnLoadLowStock.Text = "加载";
            this.btnLoadLowStock.UseVisualStyleBackColor = true;
            this.btnLoadLowStock.Click += new System.EventHandler(this.BtnLoadLowStockClick);
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AllowUserToDeleteRows = false;
            this.dgvLowStock.AllowUserToOrderColumns = true;
            this.dgvLowStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowStock.Location = new System.Drawing.Point(6, 50);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.Size = new System.Drawing.Size(776, 364);
            this.dgvLowStock.TabIndex = 1;
            // 
            // lblLowStockCount
            // 
            this.lblLowStockCount.AutoSize = true;
            this.lblLowStockCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLowStockCount.Location = new System.Drawing.Point(6, 16);
            this.lblLowStockCount.Name = "lblLowStockCount";
            this.lblLowStockCount.Size = new System.Drawing.Size(161, 17);
            this.lblLowStockCount.TabIndex = 0;
            this.lblLowStockCount.Text = "共 0 个产品低于安全库存";
            // 
        // tabTurnover
        // 
        this.tabTurnover.Controls.Add(this.btnExportTurnover);
        this.tabTurnover.Controls.Add(this.btnLoadTurnover);
        this.tabTurnover.Controls.Add(this.dtpEndDate);
        this.tabTurnover.Controls.Add(this.dtpStartDate);
        this.tabTurnover.Controls.Add(this.label7);
        this.tabTurnover.Controls.Add(this.label6);
        this.tabTurnover.Controls.Add(this.dgvInventoryTurnover);
        this.tabTurnover.Controls.Add(this.label8);
        this.tabTurnover.Controls.Add(this.lblAvgTurnoverRate);
        this.tabTurnover.Location = new System.Drawing.Point(4, 22);
        this.tabTurnover.Name = "tabTurnover";
        this.tabTurnover.Padding = new System.Windows.Forms.Padding(3);
        this.tabTurnover.Size = new System.Drawing.Size(792, 424);
        this.tabTurnover.TabIndex = 2;
        this.tabTurnover.Text = "库存周转率";
        this.tabTurnover.UseVisualStyleBackColor = true;
        // 
        // tabTransactions
        // 
        this.tabTransactions.Controls.Add(this.btnExportTransactions);
        this.tabTransactions.Controls.Add(this.btnLoadTransactions);
        this.tabTransactions.Controls.Add(this.dtpTransEndDate);
        this.tabTransactions.Controls.Add(this.dtpTransStartDate);
        this.tabTransactions.Controls.Add(this.labelTransEndDate);
        this.tabTransactions.Controls.Add(this.labelTransStartDate);
        this.tabTransactions.Controls.Add(this.txtTxnProductId);
        this.tabTransactions.Controls.Add(this.txtTxnWarehouseId);
        this.tabTransactions.Controls.Add(this.labelTxnProductId);
        this.tabTransactions.Controls.Add(this.labelTxnWarehouseId);
        this.tabTransactions.Controls.Add(this.dgvInventoryTransactions);
        this.tabTransactions.Controls.Add(this.lblTransactionCount);
        this.tabTransactions.Location = new System.Drawing.Point(4, 22);
        this.tabTransactions.Name = "tabTransactions";
        this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
        this.tabTransactions.Size = new System.Drawing.Size(792, 424);
        this.tabTransactions.TabIndex = 3;
        this.tabTransactions.Text = "库存流水";
        this.tabTransactions.UseVisualStyleBackColor = true;
        // 
        // btnExportTransactions
        // 
        this.btnExportTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnExportTransactions.Location = new System.Drawing.Point(700, 10);
        this.btnExportTransactions.Name = "btnExportTransactions";
        this.btnExportTransactions.Size = new System.Drawing.Size(80, 30);
        this.btnExportTransactions.TabIndex = 6;
        this.btnExportTransactions.Text = "导出";
        this.btnExportTransactions.UseVisualStyleBackColor = true;
        this.btnExportTransactions.Click += new System.EventHandler(this.BtnExportTransactionsClick);
        // 
        // btnLoadTransactions
        // 
        this.btnLoadTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnLoadTransactions.Location = new System.Drawing.Point(610, 10);
        this.btnLoadTransactions.Name = "btnLoadTransactions";
        this.btnLoadTransactions.Size = new System.Drawing.Size(80, 30);
        this.btnLoadTransactions.TabIndex = 5;
        this.btnLoadTransactions.Text = "加载";
        this.btnLoadTransactions.UseVisualStyleBackColor = true;
        this.btnLoadTransactions.Click += new System.EventHandler(this.BtnLoadTransactionsClick);
        // 
        // dtpTransEndDate
        // 
        this.dtpTransEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dtpTransEndDate.Location = new System.Drawing.Point(400, 14);
        this.dtpTransEndDate.Name = "dtpTransEndDate";
        this.dtpTransEndDate.Size = new System.Drawing.Size(120, 21);
        this.dtpTransEndDate.TabIndex = 8;
        // 
        // dtpTransStartDate
        // 
        this.dtpTransStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.dtpTransStartDate.Location = new System.Drawing.Point(250, 14);
        this.dtpTransStartDate.Name = "dtpTransStartDate";
        this.dtpTransStartDate.Size = new System.Drawing.Size(120, 21);
        this.dtpTransStartDate.TabIndex = 7;
        // 
        // labelTransStartDate
        // 
        this.labelTransStartDate.AutoSize = true;
        this.labelTransStartDate.Location = new System.Drawing.Point(200, 17);
        this.labelTransStartDate.Name = "labelTransStartDate";
        this.labelTransStartDate.Size = new System.Drawing.Size(47, 12);
        this.labelTransStartDate.TabIndex = 6;
        this.labelTransStartDate.Text = "开始日期";
        // 
        // labelTransEndDate
        // 
        this.labelTransEndDate.AutoSize = true;
        this.labelTransEndDate.Location = new System.Drawing.Point(370, 17);
        this.labelTransEndDate.Name = "labelTransEndDate";
        this.labelTransEndDate.Size = new System.Drawing.Size(47, 12);
        this.labelTransEndDate.TabIndex = 5;
        this.labelTransEndDate.Text = "结束日期";
        // 
        // txtTxnProductId
        // 
        this.txtTxnProductId.Location = new System.Drawing.Point(70, 14);
        this.txtTxnProductId.Name = "txtTxnProductId";
        this.txtTxnProductId.Size = new System.Drawing.Size(60, 21);
        this.txtTxnProductId.TabIndex = 1;
        // 
        // txtTxnWarehouseId
        // 
        this.txtTxnWarehouseId.Location = new System.Drawing.Point(180, 14);
        this.txtTxnWarehouseId.Name = "txtTxnWarehouseId";
        this.txtTxnWarehouseId.Size = new System.Drawing.Size(60, 21);
        this.txtTxnWarehouseId.TabIndex = 3;
        // 
        // labelTxnProductId
        // 
        this.labelTxnProductId.AutoSize = true;
        this.labelTxnProductId.Location = new System.Drawing.Point(6, 17);
        this.labelTxnProductId.Name = "labelTxnProductId";
        this.labelTxnProductId.Size = new System.Drawing.Size(47, 12);
        this.labelTxnProductId.TabIndex = 0;
        this.labelTxnProductId.Text = "商品ID:";
        // 
        // labelTxnWarehouseId
        // 
        this.labelTxnWarehouseId.AutoSize = true;
        this.labelTxnWarehouseId.Location = new System.Drawing.Point(140, 17);
        this.labelTxnWarehouseId.Name = "labelTxnWarehouseId";
        this.labelTxnWarehouseId.Size = new System.Drawing.Size(47, 12);
        this.labelTxnWarehouseId.TabIndex = 2;
        this.labelTxnWarehouseId.Text = "仓库ID:";
        // 
        // labelTransEndDate
        // 
        this.labelTransEndDate.AutoSize = true;
        this.labelTransEndDate.Location = new System.Drawing.Point(180, 17);
        this.labelTransEndDate.Name = "labelTransEndDate";
        this.labelTransEndDate.Size = new System.Drawing.Size(47, 12);
        this.labelTransEndDate.TabIndex = 2;
        this.labelTransEndDate.Text = "结束日期";
        // 
        // labelTransStartDate
        // 
        this.labelTransStartDate.AutoSize = true;
        this.labelTransStartDate.Location = new System.Drawing.Point(6, 17);
        this.labelTransStartDate.Name = "labelTransStartDate";
        this.labelTransStartDate.Size = new System.Drawing.Size(47, 12);
        this.labelTransStartDate.TabIndex = 1;
        this.labelTransStartDate.Text = "开始日期";
        // 
        // dgvInventoryTransactions
        // 
        this.dgvInventoryTransactions.AllowUserToAddRows = false;
        this.dgvInventoryTransactions.AllowUserToDeleteRows = false;
        this.dgvInventoryTransactions.AllowUserToOrderColumns = true;
        this.dgvInventoryTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.dgvInventoryTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvInventoryTransactions.Location = new System.Drawing.Point(6, 50);
        this.dgvInventoryTransactions.Name = "dgvInventoryTransactions";
        this.dgvInventoryTransactions.ReadOnly = true;
        this.dgvInventoryTransactions.Size = new System.Drawing.Size(776, 364);
        this.dgvInventoryTransactions.TabIndex = 0;
        // 
        // lblTransactionCount
        // 
        this.lblTransactionCount.AutoSize = true;
        this.lblTransactionCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.lblTransactionCount.Location = new System.Drawing.Point(6, 16);
        this.lblTransactionCount.Name = "lblTransactionCount";
        this.lblTransactionCount.Size = new System.Drawing.Size(107, 17);
        this.lblTransactionCount.TabIndex = 7;
        this.lblTransactionCount.Text = "共 0 条流水记录";
            // 
            // btnExportTurnover
            // 
            this.btnExportTurnover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportTurnover.Location = new System.Drawing.Point(700, 10);
            this.btnExportTurnover.Name = "btnExportTurnover";
            this.btnExportTurnover.Size = new System.Drawing.Size(80, 30);
            this.btnExportTurnover.TabIndex = 8;
            this.btnExportTurnover.Text = "导出";
            this.btnExportTurnover.UseVisualStyleBackColor = true;
            this.btnExportTurnover.Click += new System.EventHandler(this.BtnExportTurnoverClick);
            // 
            // btnLoadTurnover
            // 
            this.btnLoadTurnover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTurnover.Location = new System.Drawing.Point(610, 10);
            this.btnLoadTurnover.Name = "btnLoadTurnover";
            this.btnLoadTurnover.Size = new System.Drawing.Size(80, 30);
            this.btnLoadTurnover.TabIndex = 7;
            this.btnLoadTurnover.Text = "加载";
            this.btnLoadTurnover.UseVisualStyleBackColor = true;
            this.btnLoadTurnover.Click += new System.EventHandler(this.BtnLoadTurnoverClick);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(230, 14);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 21);
            this.dtpEndDate.TabIndex = 6;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(50, 14);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(120, 21);
            this.dtpStartDate.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "结束日期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "开始日期";
            // 
            // dgvInventoryTurnover
            // 
            this.dgvInventoryTurnover.AllowUserToAddRows = false;
            this.dgvInventoryTurnover.AllowUserToDeleteRows = false;
            this.dgvInventoryTurnover.AllowUserToOrderColumns = true;
            this.dgvInventoryTurnover.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInventoryTurnover.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventoryTurnover.Location = new System.Drawing.Point(6, 80);
            this.dgvInventoryTurnover.Name = "dgvInventoryTurnover";
            this.dgvInventoryTurnover.ReadOnly = true;
            this.dgvInventoryTurnover.Size = new System.Drawing.Size(776, 334);
            this.dgvInventoryTurnover.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "平均周转率: ";
            // 
            // lblAvgTurnoverRate
            // 
            this.lblAvgTurnoverRate.AutoSize = true;
            this.lblAvgTurnoverRate.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgTurnoverRate.Location = new System.Drawing.Point(95, 55);
            this.lblAvgTurnoverRate.Name = "lblAvgTurnoverRate";
            this.lblAvgTurnoverRate.Size = new System.Drawing.Size(43, 17);
            this.lblAvgTurnoverRate.TabIndex = 1;
            this.lblAvgTurnoverRate.Text = "0.00%";
            // 
            // InventoryReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "InventoryReportForm";
            this.Text = "库存报表";
            this.Load += new System.EventHandler(this.InventoryReportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabInventoryOverview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryOverview)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabLowStock.ResumeLayout(false);
            this.tabLowStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.tabTurnover.ResumeLayout(false);
            this.tabTurnover.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventoryTurnover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabInventoryOverview;
        private System.Windows.Forms.TabPage tabLowStock;
        private System.Windows.Forms.TabPage tabTurnover;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLowStockInOverview;
        private System.Windows.Forms.DataGridView dgvInventoryOverview;
        private System.Windows.Forms.Button btnLoadOverview;
        private System.Windows.Forms.Button btnExportOverview;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Label lblLowStockCount;
        private System.Windows.Forms.Button btnLoadLowStock;
        private System.Windows.Forms.Button btnExportLowStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLoadTurnover;
        private System.Windows.Forms.Button btnExportTurnover;
        private System.Windows.Forms.DataGridView dgvInventoryTurnover;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAvgTurnoverRate;
        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.Button btnLoadTransactions;
        private System.Windows.Forms.Button btnExportTransactions;
        private System.Windows.Forms.DataGridView dgvInventoryTransactions;
        private System.Windows.Forms.DateTimePicker dtpTransStartDate;
        private System.Windows.Forms.DateTimePicker dtpTransEndDate;
        private System.Windows.Forms.Label labelTransStartDate;
        private System.Windows.Forms.Label labelTransEndDate;
        private System.Windows.Forms.Label lblTransactionCount;
        private System.Windows.Forms.TextBox txtTxnProductId;
        private System.Windows.Forms.TextBox txtTxnWarehouseId;
        private System.Windows.Forms.Label labelTxnProductId;
        private System.Windows.Forms.Label labelTxnWarehouseId;
    }
}