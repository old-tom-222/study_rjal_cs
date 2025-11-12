namespace CSproject.UI.Forms
{
    partial class PurchaseReportForm
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
            this.tabProductPurchase = new System.Windows.Forms.TabPage();
            this.btnExportProductPurchase = new System.Windows.Forms.Button();
            this.btnLoadProductPurchase = new System.Windows.Forms.Button();
            this.dtpProductPurchaseEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpProductPurchaseStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPurchaseTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalQuantity = new System.Windows.Forms.Label();
            this.dgvProductPurchase = new System.Windows.Forms.DataGridView();
            this.tabSupplierPerformance = new System.Windows.Forms.TabPage();
            this.btnExportSupplierPerformance = new System.Windows.Forms.Button();
            this.btnLoadSupplierPerformance = new System.Windows.Forms.Button();
            this.dtpSupplierPerformanceEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpSupplierPerformanceStart = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblSupplierTotalAmount = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSupplierCount = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAvgDeliveryTime = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblAvgQualityScore = new System.Windows.Forms.Label();
            this.dgvSupplierPerformance = new System.Windows.Forms.DataGridView();
            this.tabTrendReport = new System.Windows.Forms.TabPage();
            this.btnExportTrendReport = new System.Windows.Forms.Button();
            this.btnLoadTrendReport = new System.Windows.Forms.Button();
            this.cmbTrendGranularity = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpPurchaseTrendEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpPurchaseTrendStart = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTrendTotalAmount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblAvgPeriodAmount = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.dgvPurchaseTrend = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabProductPurchase.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductPurchase)).BeginInit();
            this.tabSupplierPerformance.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierPerformance)).BeginInit();
            this.tabTrendReport.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseTrend)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProductPurchase);
            this.tabControl1.Controls.Add(this.tabSupplierPerformance);
            this.tabControl1.Controls.Add(this.tabTrendReport);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProductPurchase
            // 
            this.tabProductPurchase.Controls.Add(this.btnExportProductPurchase);
            this.tabProductPurchase.Controls.Add(this.btnLoadProductPurchase);
            this.tabProductPurchase.Controls.Add(this.dtpProductPurchaseEnd);
            this.tabProductPurchase.Controls.Add(this.dtpProductPurchaseStart);
            this.tabProductPurchase.Controls.Add(this.label1);
            this.tabProductPurchase.Controls.Add(this.label2);
            this.tabProductPurchase.Controls.Add(this.flowLayoutPanel1);
            this.tabProductPurchase.Controls.Add(this.dgvProductPurchase);
            this.tabProductPurchase.Location = new System.Drawing.Point(4, 22);
            this.tabProductPurchase.Name = "tabProductPurchase";
            this.tabProductPurchase.Padding = new System.Windows.Forms.Padding(3);
            this.tabProductPurchase.Size = new System.Drawing.Size(792, 424);
            this.tabProductPurchase.TabIndex = 0;
            this.tabProductPurchase.Text = "产品采购报表";
            this.tabProductPurchase.UseVisualStyleBackColor = true;
            // 
            // btnExportProductPurchase
            // 
            this.btnExportProductPurchase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportProductPurchase.Location = new System.Drawing.Point(700, 10);
            this.btnExportProductPurchase.Name = "btnExportProductPurchase";
            this.btnExportProductPurchase.Size = new System.Drawing.Size(80, 30);
            this.btnExportProductPurchase.TabIndex = 7;
            this.btnExportProductPurchase.Text = "导出";
            this.btnExportProductPurchase.UseVisualStyleBackColor = true;
            this.btnExportProductPurchase.Click += new System.EventHandler(this.BtnExportProductPurchaseClick);
            // 
            // btnLoadProductPurchase
            // 
            this.btnLoadProductPurchase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadProductPurchase.Location = new System.Drawing.Point(610, 10);
            this.btnLoadProductPurchase.Name = "btnLoadProductPurchase";
            this.btnLoadProductPurchase.Size = new System.Drawing.Size(80, 30);
            this.btnLoadProductPurchase.TabIndex = 6;
            this.btnLoadProductPurchase.Text = "加载";
            this.btnLoadProductPurchase.UseVisualStyleBackColor = true;
            this.btnLoadProductPurchase.Click += new System.EventHandler(this.BtnLoadProductPurchaseClick);
            // 
            // dtpProductPurchaseEnd
            // 
            this.dtpProductPurchaseEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProductPurchaseEnd.Location = new System.Drawing.Point(400, 14);
            this.dtpProductPurchaseEnd.Name = "dtpProductPurchaseEnd";
            this.dtpProductPurchaseEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpProductPurchaseEnd.TabIndex = 5;
            // 
            // dtpProductPurchaseStart
            // 
            this.dtpProductPurchaseStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProductPurchaseStart.Location = new System.Drawing.Point(120, 14);
            this.dtpProductPurchaseStart.Name = "dtpProductPurchaseStart";
            this.dtpProductPurchaseStart.Size = new System.Drawing.Size(120, 21);
            this.dtpProductPurchaseStart.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "采购开始日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "采购结束日期：";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.lblPurchaseTotal);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.lblProductCount);
            this.flowLayoutPanel1.Controls.Add(this.label7);
            this.flowLayoutPanel1.Controls.Add(this.lblTotalQuantity);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 45);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(514, 35);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "采购总额: ";
            // 
            // lblPurchaseTotal
            // 
            this.lblPurchaseTotal.AutoSize = true;
            this.lblPurchaseTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPurchaseTotal.Location = new System.Drawing.Point(86, 0);
            this.lblPurchaseTotal.Name = "lblPurchaseTotal";
            this.lblPurchaseTotal.Size = new System.Drawing.Size(41, 17);
            this.lblPurchaseTotal.TabIndex = 1;
            this.lblPurchaseTotal.Text = "0.00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(133, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "产品数量: ";
            // 
            // lblProductCount
            // 
            this.lblProductCount.AutoSize = true;
            this.lblProductCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductCount.Location = new System.Drawing.Point(216, 0);
            this.lblProductCount.Name = "lblProductCount";
            this.lblProductCount.Size = new System.Drawing.Size(17, 17);
            this.lblProductCount.TabIndex = 3;
            this.lblProductCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(239, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "采购总量: ";
            // 
            // lblTotalQuantity
            // 
            this.lblTotalQuantity.AutoSize = true;
            this.lblTotalQuantity.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalQuantity.Location = new System.Drawing.Point(322, 0);
            this.lblTotalQuantity.Name = "lblTotalQuantity";
            this.lblTotalQuantity.Size = new System.Drawing.Size(17, 17);
            this.lblTotalQuantity.TabIndex = 5;
            this.lblTotalQuantity.Text = "0";
            // 
            // dgvProductPurchase
            // 
            this.dgvProductPurchase.AllowUserToAddRows = false;
            this.dgvProductPurchase.AllowUserToDeleteRows = false;
            this.dgvProductPurchase.AllowUserToOrderColumns = true;
            this.dgvProductPurchase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductPurchase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductPurchase.Location = new System.Drawing.Point(6, 86);
            this.dgvProductPurchase.Name = "dgvProductPurchase";
            this.dgvProductPurchase.ReadOnly = true;
            this.dgvProductPurchase.Size = new System.Drawing.Size(776, 332);
            this.dgvProductPurchase.TabIndex = 0;
            // 
            // tabSupplierPerformance
            // 
            this.tabSupplierPerformance.Controls.Add(this.btnExportSupplierPerformance);
            this.tabSupplierPerformance.Controls.Add(this.btnLoadSupplierPerformance);
            this.tabSupplierPerformance.Controls.Add(this.dtpSupplierPerformanceEnd);
            this.tabSupplierPerformance.Controls.Add(this.dtpSupplierPerformanceStart);
            this.tabSupplierPerformance.Controls.Add(this.label4);
            this.tabSupplierPerformance.Controls.Add(this.label6);
            this.tabSupplierPerformance.Controls.Add(this.flowLayoutPanel2);
            this.tabSupplierPerformance.Controls.Add(this.dgvSupplierPerformance);
            this.tabSupplierPerformance.Location = new System.Drawing.Point(4, 22);
            this.tabSupplierPerformance.Name = "tabSupplierPerformance";
            this.tabSupplierPerformance.Padding = new System.Windows.Forms.Padding(3);
            this.tabSupplierPerformance.Size = new System.Drawing.Size(792, 424);
            this.tabSupplierPerformance.TabIndex = 1;
            this.tabSupplierPerformance.Text = "供应商表现报表";
            this.tabSupplierPerformance.UseVisualStyleBackColor = true;
            // 
            // btnExportSupplierPerformance
            // 
            this.btnExportSupplierPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportSupplierPerformance.Location = new System.Drawing.Point(700, 10);
            this.btnExportSupplierPerformance.Name = "btnExportSupplierPerformance";
            this.btnExportSupplierPerformance.Size = new System.Drawing.Size(80, 30);
            this.btnExportSupplierPerformance.TabIndex = 7;
            this.btnExportSupplierPerformance.Text = "导出";
            this.btnExportSupplierPerformance.UseVisualStyleBackColor = true;
            this.btnExportSupplierPerformance.Click += new System.EventHandler(this.BtnExportSupplierPerformanceClick);
            // 
            // btnLoadSupplierPerformance
            // 
            this.btnLoadSupplierPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadSupplierPerformance.Location = new System.Drawing.Point(610, 10);
            this.btnLoadSupplierPerformance.Name = "btnLoadSupplierPerformance";
            this.btnLoadSupplierPerformance.Size = new System.Drawing.Size(80, 30);
            this.btnLoadSupplierPerformance.TabIndex = 6;
            this.btnLoadSupplierPerformance.Text = "加载";
            this.btnLoadSupplierPerformance.UseVisualStyleBackColor = true;
            this.btnLoadSupplierPerformance.Click += new System.EventHandler(this.BtnLoadSupplierPerformanceClick);
            // 
            // dtpSupplierPerformanceEnd
            // 
            this.dtpSupplierPerformanceEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSupplierPerformanceEnd.Location = new System.Drawing.Point(400, 14);
            this.dtpSupplierPerformanceEnd.Name = "dtpSupplierPerformanceEnd";
            this.dtpSupplierPerformanceEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpSupplierPerformanceEnd.TabIndex = 5;
            // 
            // dtpSupplierPerformanceStart
            // 
            this.dtpSupplierPerformanceStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSupplierPerformanceStart.Location = new System.Drawing.Point(120, 14);
            this.dtpSupplierPerformanceStart.Name = "dtpSupplierPerformanceStart";
            this.dtpSupplierPerformanceStart.Size = new System.Drawing.Size(120, 21);
            this.dtpSupplierPerformanceStart.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "采购开始日期：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "采购结束日期：";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.label8);
            this.flowLayoutPanel2.Controls.Add(this.lblSupplierTotalAmount);
            this.flowLayoutPanel2.Controls.Add(this.label10);
            this.flowLayoutPanel2.Controls.Add(this.lblSupplierCount);
            this.flowLayoutPanel2.Controls.Add(this.label12);
            this.flowLayoutPanel2.Controls.Add(this.lblAvgDeliveryTime);
            this.flowLayoutPanel2.Controls.Add(this.label17);
            this.flowLayoutPanel2.Controls.Add(this.lblAvgQualityScore);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 45);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(694, 35);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "采购总额: ";
            // 
            // lblSupplierTotalAmount
            // 
            this.lblSupplierTotalAmount.AutoSize = true;
            this.lblSupplierTotalAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierTotalAmount.Location = new System.Drawing.Point(86, 0);
            this.lblSupplierTotalAmount.Name = "lblSupplierTotalAmount";
            this.lblSupplierTotalAmount.Size = new System.Drawing.Size(41, 17);
            this.lblSupplierTotalAmount.TabIndex = 1;
            this.lblSupplierTotalAmount.Text = "0.00";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(133, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "供应商数: ";
            // 
            // lblSupplierCount
            // 
            this.lblSupplierCount.AutoSize = true;
            this.lblSupplierCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierCount.Location = new System.Drawing.Point(216, 0);
            this.lblSupplierCount.Name = "lblSupplierCount";
            this.lblSupplierCount.Size = new System.Drawing.Size(17, 17);
            this.lblSupplierCount.TabIndex = 3;
            this.lblSupplierCount.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(239, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "平均交货时间: ";
            // 
            // lblAvgDeliveryTime
            // 
            this.lblAvgDeliveryTime.AutoSize = true;
            this.lblAvgDeliveryTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgDeliveryTime.Location = new System.Drawing.Point(346, 0);
            this.lblAvgDeliveryTime.Name = "lblAvgDeliveryTime";
            this.lblAvgDeliveryTime.Size = new System.Drawing.Size(17, 17);
            this.lblAvgDeliveryTime.TabIndex = 5;
            this.lblAvgDeliveryTime.Text = "0";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(369, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 17);
            this.label17.TabIndex = 6;
            this.label17.Text = "质量评分: ";
            // 
            // lblAvgQualityScore
            // 
            this.lblAvgQualityScore.AutoSize = true;
            this.lblAvgQualityScore.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgQualityScore.Location = new System.Drawing.Point(452, 0);
            this.lblAvgQualityScore.Name = "lblAvgQualityScore";
            this.lblAvgQualityScore.Size = new System.Drawing.Size(17, 17);
            this.lblAvgQualityScore.TabIndex = 7;
            this.lblAvgQualityScore.Text = "0";
            // 
            // dgvSupplierPerformance
            // 
            this.dgvSupplierPerformance.AllowUserToAddRows = false;
            this.dgvSupplierPerformance.AllowUserToDeleteRows = false;
            this.dgvSupplierPerformance.AllowUserToOrderColumns = true;
            this.dgvSupplierPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSupplierPerformance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSupplierPerformance.Location = new System.Drawing.Point(6, 86);
            this.dgvSupplierPerformance.Name = "dgvSupplierPerformance";
            this.dgvSupplierPerformance.ReadOnly = true;
            this.dgvSupplierPerformance.Size = new System.Drawing.Size(776, 332);
            this.dgvSupplierPerformance.TabIndex = 0;
            // 
            // tabTrendReport
            // 
            this.tabTrendReport.Controls.Add(this.btnExportTrendReport);
            this.tabTrendReport.Controls.Add(this.btnLoadTrendReport);
            this.tabTrendReport.Controls.Add(this.cmbTrendGranularity);
            this.tabTrendReport.Controls.Add(this.label13);
            this.tabTrendReport.Controls.Add(this.dtpPurchaseTrendEnd);
            this.tabTrendReport.Controls.Add(this.dtpPurchaseTrendStart);
            this.tabTrendReport.Controls.Add(this.label14);
            this.tabTrendReport.Controls.Add(this.label15);
            this.tabTrendReport.Controls.Add(this.flowLayoutPanel3);
            this.tabTrendReport.Controls.Add(this.dgvPurchaseTrend);
            this.tabTrendReport.Location = new System.Drawing.Point(4, 22);
            this.tabTrendReport.Name = "tabTrendReport";
            this.tabTrendReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabTrendReport.Size = new System.Drawing.Size(792, 424);
            this.tabTrendReport.TabIndex = 2;
            this.tabTrendReport.Text = "采购趋势报表";
            this.tabTrendReport.UseVisualStyleBackColor = true;
            // 
            // btnExportTrendReport
            // 
            this.btnExportTrendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportTrendReport.Location = new System.Drawing.Point(700, 40);
            this.btnExportTrendReport.Name = "btnExportTrendReport";
            this.btnExportTrendReport.Size = new System.Drawing.Size(80, 30);
            this.btnExportTrendReport.TabIndex = 9;
            this.btnExportTrendReport.Text = "导出";
            this.btnExportTrendReport.UseVisualStyleBackColor = true;
            this.btnExportTrendReport.Click += new System.EventHandler(this.BtnExportTrendReportClick);
            // 
            // btnLoadTrendReport
            // 
            this.btnLoadTrendReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTrendReport.Location = new System.Drawing.Point(610, 40);
            this.btnLoadTrendReport.Name = "btnLoadTrendReport";
            this.btnLoadTrendReport.Size = new System.Drawing.Size(80, 30);
            this.btnLoadTrendReport.TabIndex = 8;
            this.btnLoadTrendReport.Text = "加载";
            this.btnLoadTrendReport.UseVisualStyleBackColor = true;
            this.btnLoadTrendReport.Click += new System.EventHandler(this.BtnLoadTrendReportClick);
            // 
            // cmbTrendGranularity
            // 
            this.cmbTrendGranularity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTrendGranularity.FormattingEnabled = true;
            this.cmbTrendGranularity.Location = new System.Drawing.Point(400, 44);
            this.cmbTrendGranularity.Name = "cmbTrendGranularity";
            this.cmbTrendGranularity.Size = new System.Drawing.Size(120, 20);
            this.cmbTrendGranularity.TabIndex = 7;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(310, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 12);
            this.label13.TabIndex = 6;
            this.label13.Text = "时间粒度：";
            // 
            // dtpPurchaseTrendEnd
            // 
            this.dtpPurchaseTrendEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseTrendEnd.Location = new System.Drawing.Point(400, 14);
            this.dtpPurchaseTrendEnd.Name = "dtpPurchaseTrendEnd";
            this.dtpPurchaseTrendEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpPurchaseTrendEnd.TabIndex = 5;
            // 
            // dtpPurchaseTrendStart
            // 
            this.dtpPurchaseTrendStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseTrendStart.Location = new System.Drawing.Point(120, 14);
            this.dtpPurchaseTrendStart.Name = "dtpPurchaseTrendStart";
            this.dtpPurchaseTrendStart.Size = new System.Drawing.Size(120, 21);
            this.dtpPurchaseTrendStart.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "采购开始日期：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(250, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "采购结束日期：";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.Controls.Add(this.label16);
            this.flowLayoutPanel3.Controls.Add(this.lblTrendTotalAmount);
            this.flowLayoutPanel3.Controls.Add(this.label18);
            this.flowLayoutPanel3.Controls.Add(this.lblAvgPeriodAmount);
            this.flowLayoutPanel3.Controls.Add(this.label19);
            this.flowLayoutPanel3.Controls.Add(this.lblTotalOrders);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(6, 76);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(514, 35);
            this.flowLayoutPanel3.TabIndex = 1;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(3, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "采购总额: ";
            // 
            // lblTrendTotalAmount
            // 
            this.lblTrendTotalAmount.AutoSize = true;
            this.lblTrendTotalAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrendTotalAmount.Location = new System.Drawing.Point(86, 0);
            this.lblTrendTotalAmount.Name = "lblTrendTotalAmount";
            this.lblTrendTotalAmount.Size = new System.Drawing.Size(41, 17);
            this.lblTrendTotalAmount.TabIndex = 1;
            this.lblTrendTotalAmount.Text = "0.00";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(133, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 17);
            this.label18.TabIndex = 2;
            this.label18.Text = "平均期间采购: ";
            // 
            // lblAvgPeriodAmount
            // 
            this.lblAvgPeriodAmount.AutoSize = true;
            this.lblAvgPeriodAmount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgPeriodAmount.Location = new System.Drawing.Point(240, 0);
            this.lblAvgPeriodAmount.Name = "lblAvgPeriodAmount";
            this.lblAvgPeriodAmount.Size = new System.Drawing.Size(41, 17);
            this.lblAvgPeriodAmount.TabIndex = 3;
            this.lblAvgPeriodAmount.Text = "0.00";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(287, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 17);
            this.label19.TabIndex = 4;
            this.label19.Text = "订单总数: ";
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.Location = new System.Drawing.Point(370, 0);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(17, 17);
            this.lblTotalOrders.TabIndex = 5;
            this.lblTotalOrders.Text = "0";
            // 
            // dgvPurchaseTrend
            // 
            this.dgvPurchaseTrend.AllowUserToAddRows = false;
            this.dgvPurchaseTrend.AllowUserToDeleteRows = false;
            this.dgvPurchaseTrend.AllowUserToOrderColumns = true;
            this.dgvPurchaseTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPurchaseTrend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchaseTrend.Location = new System.Drawing.Point(6, 117);
            this.dgvPurchaseTrend.Name = "dgvPurchaseTrend";
            this.dgvPurchaseTrend.ReadOnly = true;
            this.dgvPurchaseTrend.Size = new System.Drawing.Size(776, 301);
            this.dgvPurchaseTrend.TabIndex = 0;
            // 
            // PurchaseReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "PurchaseReportForm";
            this.Text = "采购报表";
            this.Load += new System.EventHandler(this.PurchaseReportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabProductPurchase.ResumeLayout(false);
            this.tabProductPurchase.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductPurchase)).EndInit();
            this.tabSupplierPerformance.ResumeLayout(false);
            this.tabSupplierPerformance.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSupplierPerformance)).EndInit();
            this.tabTrendReport.ResumeLayout(false);
            this.tabTrendReport.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseTrend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProductPurchase;
        private System.Windows.Forms.TabPage tabSupplierPerformance;
        private System.Windows.Forms.TabPage tabTrendReport;
        private System.Windows.Forms.DataGridView dgvProductPurchase;
        private System.Windows.Forms.DateTimePicker dtpProductPurchaseStart;
        private System.Windows.Forms.DateTimePicker dtpProductPurchaseEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadProductPurchase;
        private System.Windows.Forms.Button btnExportProductPurchase;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPurchaseTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalQuantity;
        private System.Windows.Forms.DataGridView dgvSupplierPerformance;
        private System.Windows.Forms.DateTimePicker dtpSupplierPerformanceStart;
        private System.Windows.Forms.DateTimePicker dtpSupplierPerformanceEnd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnLoadSupplierPerformance;
        private System.Windows.Forms.Button btnExportSupplierPerformance;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSupplierTotalAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSupplierCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAvgDeliveryTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblAvgQualityScore;
        private System.Windows.Forms.DataGridView dgvPurchaseTrend;
        private System.Windows.Forms.DateTimePicker dtpPurchaseTrendStart;
        private System.Windows.Forms.DateTimePicker dtpPurchaseTrendEnd;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbTrendGranularity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnLoadTrendReport;
        private System.Windows.Forms.Button btnExportTrendReport;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblTrendTotalAmount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblAvgPeriodAmount;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblTotalOrders;
    }
}