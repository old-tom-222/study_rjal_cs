namespace CSproject.UI.Forms
{
    partial class SalesReportForm
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
            this.tabProductSales = new System.Windows.Forms.TabPage();
            this.btnExportProductSales = new System.Windows.Forms.Button();
            this.btnLoadProductSales = new System.Windows.Forms.Button();
            this.dtpProductSalesEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpProductSalesStart = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProductSalesTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProductCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalQuantity = new System.Windows.Forms.Label();
            this.dgvProductSales = new System.Windows.Forms.DataGridView();

            this.tabTrendReport = new System.Windows.Forms.TabPage();
            this.btnExportTrendReport = new System.Windows.Forms.Button();
            this.btnLoadTrendReport = new System.Windows.Forms.Button();
            this.cmbTrendGranularity = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dtpTrendEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpTrendStart = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.lblTrendTotalAmount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblAvgPeriodAmount = new System.Windows.Forms.Label();
            this.dgvSalesTrend = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabProductSales.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSales)).BeginInit();

            this.tabTrendReport.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesTrend)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabProductSales);
            // 移除每日销售报表标签页
            this.tabControl1.Controls.Add(this.tabTrendReport);
            if (this.tabCustomerRankings != null)
            {
                this.tabControl1.Controls.Add(this.tabCustomerRankings);
            }
            if (this.tabProductRankings != null)
            {
                this.tabControl1.Controls.Add(this.tabProductRankings);
            }
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabProductSales
            // 
            if (this.tabProductSales != null)
            {
                this.tabProductSales.Controls.Add(this.btnExportProductSales);
                this.tabProductSales.Controls.Add(this.btnLoadProductSales);
                this.tabProductSales.Controls.Add(this.dtpProductSalesEnd);
                this.tabProductSales.Controls.Add(this.dtpProductSalesStart);
                this.tabProductSales.Controls.Add(this.label1);
                this.tabProductSales.Controls.Add(this.label2);
                this.tabProductSales.Controls.Add(this.flowLayoutPanel1);
                this.tabProductSales.Controls.Add(this.dgvProductSales);
                this.tabProductSales.Location = new System.Drawing.Point(4, 22);
                this.tabProductSales.Name = "tabProductSales";
                this.tabProductSales.Padding = new System.Windows.Forms.Padding(3);
                this.tabProductSales.Size = new System.Drawing.Size(792, 424);
                this.tabProductSales.TabIndex = 0;
                this.tabProductSales.Text = "产品销售报表";
                this.tabProductSales.UseVisualStyleBackColor = true;
            }
            // 
            // btnExportProductSales
            // 
            this.btnExportProductSales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportProductSales.Location = new System.Drawing.Point(700, 10);
            this.btnExportProductSales.Name = "btnExportProductSales";
            this.btnExportProductSales.Size = new System.Drawing.Size(80, 30);
            this.btnExportProductSales.TabIndex = 7;
            this.btnExportProductSales.Text = "导出";
            this.btnExportProductSales.UseVisualStyleBackColor = true;
            this.btnExportProductSales.Click += new System.EventHandler(this.BtnExportProductSalesClick);
            // 
            // btnLoadProductSales
            // 
            this.btnLoadProductSales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadProductSales.Location = new System.Drawing.Point(610, 10);
            this.btnLoadProductSales.Name = "btnLoadProductSales";
            this.btnLoadProductSales.Size = new System.Drawing.Size(80, 30);
            this.btnLoadProductSales.TabIndex = 6;
            this.btnLoadProductSales.Text = "加载";
            this.btnLoadProductSales.UseVisualStyleBackColor = true;
            this.btnLoadProductSales.Click += new System.EventHandler(this.BtnLoadProductSalesClick);
            // 
            // dtpProductSalesEnd
            // 
            this.dtpProductSalesEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProductSalesEnd.Location = new System.Drawing.Point(400, 14);
            this.dtpProductSalesEnd.Name = "dtpProductSalesEnd";
            this.dtpProductSalesEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpProductSalesEnd.TabIndex = 5;
            // 
            // dtpProductSalesStart
            // 
            this.dtpProductSalesStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpProductSalesStart.Location = new System.Drawing.Point(120, 14);
            this.dtpProductSalesStart.Name = "dtpProductSalesStart";
            this.dtpProductSalesStart.Size = new System.Drawing.Size(120, 21);
            this.dtpProductSalesStart.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "销售开始日期：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "销售结束日期：";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.lblProductSalesTotal);
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
            this.label3.Text = "销售总额: ";
            // 
            // lblProductSalesTotal
            // 
            this.lblProductSalesTotal.AutoSize = true;
            this.lblProductSalesTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductSalesTotal.Location = new System.Drawing.Point(86, 0);
            this.lblProductSalesTotal.Name = "lblProductSalesTotal";
            this.lblProductSalesTotal.Size = new System.Drawing.Size(41, 17);
            this.lblProductSalesTotal.TabIndex = 1;
            this.lblProductSalesTotal.Text = "0.00";
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
            this.label7.Text = "销售总量: ";
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
            // dgvProductSales
            // 
            this.dgvProductSales.AllowUserToAddRows = false;
            this.dgvProductSales.AllowUserToDeleteRows = false;
            this.dgvProductSales.AllowUserToOrderColumns = true;
            this.dgvProductSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductSales.Location = new System.Drawing.Point(6, 86);
            this.dgvProductSales.Name = "dgvProductSales";
            this.dgvProductSales.ReadOnly = true;
            this.dgvProductSales.Size = new System.Drawing.Size(776, 332);
            this.dgvProductSales.TabIndex = 0;
            // 移除每日销售报表标签页定义
            // 
            // 每日销售报表导出按钮已移除
            // 
            // 每日销售报表加载按钮已移除
            // 
            // 每日销售报表日期选择器已移除
            // 
            // 每日销售报表日期选择器已移除
            // 
            // 每日销售报表标签已移除
            // 
            // 每日销售报表标签已移除
            // 每日销售报表流布局面板已移除
            // 
            // 每日销售报表标签已移除
            // 每日销售报表标签已移除
            // 每日销售报表标签已移除
            // 每日销售报表标签已移除
            // 每日销售报表标签已移除
            // 每日销售报表平均销售额标签已移除
            // 每日销售报表数据网格已移除
            // 
            // tabTrendReport
            // 
            if (this.tabTrendReport != null)
            {
                this.tabTrendReport.Controls.Add(this.btnExportTrendReport);
                this.tabTrendReport.Controls.Add(this.btnLoadTrendReport);
                this.tabTrendReport.Controls.Add(this.cmbTrendGranularity);
                this.tabTrendReport.Controls.Add(this.label13);
                this.tabTrendReport.Controls.Add(this.dtpTrendEnd);
                this.tabTrendReport.Controls.Add(this.dtpTrendStart);
                this.tabTrendReport.Controls.Add(this.label14);
                this.tabTrendReport.Controls.Add(this.label15);
                this.tabTrendReport.Controls.Add(this.flowLayoutPanel3);
                this.tabTrendReport.Controls.Add(this.dgvSalesTrend);
                this.tabTrendReport.Location = new System.Drawing.Point(4, 22);
                this.tabTrendReport.Name = "tabTrendReport";
                this.tabTrendReport.Padding = new System.Windows.Forms.Padding(3);
                this.tabTrendReport.Size = new System.Drawing.Size(792, 424);
                this.tabTrendReport.TabIndex = 2;
                this.tabTrendReport.Text = "销售趋势报表";
                this.tabTrendReport.UseVisualStyleBackColor = true;
            }
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
            // dtpTrendEnd
            // 
            this.dtpTrendEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTrendEnd.Location = new System.Drawing.Point(400, 14);
            this.dtpTrendEnd.Name = "dtpTrendEnd";
            this.dtpTrendEnd.Size = new System.Drawing.Size(120, 21);
            this.dtpTrendEnd.TabIndex = 5;
            // 
            // dtpTrendStart
            // 
            this.dtpTrendStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTrendStart.Location = new System.Drawing.Point(120, 14);
            this.dtpTrendStart.Name = "dtpTrendStart";
            this.dtpTrendStart.Size = new System.Drawing.Size(120, 21);
            this.dtpTrendStart.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 12);
            this.label14.TabIndex = 3;
            this.label14.Text = "销售开始日期：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(250, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "销售结束日期：";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel3.Controls.Add(this.label16);
            this.flowLayoutPanel3.Controls.Add(this.lblTrendTotalAmount);
            this.flowLayoutPanel3.Controls.Add(this.label18);
            this.flowLayoutPanel3.Controls.Add(this.lblAvgPeriodAmount);
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
            this.label16.Text = "销售总额: ";
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
            this.label18.Text = "平均期间销售: ";
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
            // dgvSalesTrend
            // 
            this.dgvSalesTrend.AllowUserToAddRows = false;
            this.dgvSalesTrend.AllowUserToDeleteRows = false;
            this.dgvSalesTrend.AllowUserToOrderColumns = true;
            this.dgvSalesTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSalesTrend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesTrend.Location = new System.Drawing.Point(6, 117);
            this.dgvSalesTrend.Name = "dgvSalesTrend";
            this.dgvSalesTrend.ReadOnly = true;
            this.dgvSalesTrend.Size = new System.Drawing.Size(776, 301);
            this.dgvSalesTrend.TabIndex = 0;
            // 
            // SalesReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "SalesReportForm";
            this.Text = "销售报表";
            this.Load += new System.EventHandler(this.SalesReportForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabProductSales.ResumeLayout(false);
            if (this.tabProductSales != null)
            {
                this.tabProductSales.PerformLayout();
            }
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductSales)).EndInit();

            if (this.tabTrendReport != null)
            {
                this.tabTrendReport.ResumeLayout(false);
                this.tabTrendReport.PerformLayout();
            }
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesTrend)).EndInit();
            // 
            // tabCustomerRankings
            // 
            if (this.tabCustomerRankings != null)
            {
                this.tabCustomerRankings.Controls.Add(this.btnExportCustomerRankings);
                this.tabCustomerRankings.Controls.Add(this.btnLoadCustomerRankings);
                this.tabCustomerRankings.Controls.Add(this.nudCustomerTopN);
                this.tabCustomerRankings.Controls.Add(this.label35);
                this.tabCustomerRankings.Controls.Add(this.dtpCustomerRankingsEnd);
                this.tabCustomerRankings.Controls.Add(this.dtpCustomerRankingsStart);
                this.tabCustomerRankings.Controls.Add(this.label33);
                this.tabCustomerRankings.Controls.Add(this.label34);
                this.tabCustomerRankings.Controls.Add(this.flowLayoutPanel4);
                this.tabCustomerRankings.Controls.Add(this.dgvCustomerRankings);
                this.tabCustomerRankings.Location = new System.Drawing.Point(4, 22);
                this.tabCustomerRankings.Name = "tabCustomerRankings";
                this.tabCustomerRankings.Padding = new System.Windows.Forms.Padding(3);
                this.tabCustomerRankings.Size = new System.Drawing.Size(792, 424);
                this.tabCustomerRankings.TabIndex = 3;
                this.tabCustomerRankings.Text = "客户销售排名";
                this.tabCustomerRankings.UseVisualStyleBackColor = true;
            }
            // 
            // btnExportCustomerRankings
            // 
            if (this.btnExportCustomerRankings != null)
            {
                this.btnExportCustomerRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.btnExportCustomerRankings.Location = new System.Drawing.Point(700, 10);
                this.btnExportCustomerRankings.Name = "btnExportCustomerRankings";
                this.btnExportCustomerRankings.Size = new System.Drawing.Size(80, 30);
                this.btnExportCustomerRankings.TabIndex = 9;
                this.btnExportCustomerRankings.Text = "导出";
                this.btnExportCustomerRankings.UseVisualStyleBackColor = true;
                this.btnExportCustomerRankings.Click += new System.EventHandler(this.BtnExportCustomerRankingsClick);
            }
            // 
            // btnLoadCustomerRankings
            // 
            if (this.btnLoadCustomerRankings != null)
            {
                this.btnLoadCustomerRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.btnLoadCustomerRankings.Location = new System.Drawing.Point(610, 10);
                this.btnLoadCustomerRankings.Name = "btnLoadCustomerRankings";
                this.btnLoadCustomerRankings.Size = new System.Drawing.Size(80, 30);
                this.btnLoadCustomerRankings.TabIndex = 8;
                this.btnLoadCustomerRankings.Text = "加载";
                this.btnLoadCustomerRankings.UseVisualStyleBackColor = true;
                this.btnLoadCustomerRankings.Click += new System.EventHandler(this.BtnLoadCustomerRankingsClick);
            }
            // 
            // nudCustomerTopN
            // 
            if (this.nudCustomerTopN != null)
            {
                this.nudCustomerTopN.Location = new System.Drawing.Point(570, 14);
                this.nudCustomerTopN.Minimum = new decimal(new int[] {
                1, 0, 0, 0});
                this.nudCustomerTopN.Name = "nudCustomerTopN";
                this.nudCustomerTopN.Size = new System.Drawing.Size(34, 21);
                this.nudCustomerTopN.TabIndex = 7;
                this.nudCustomerTopN.Value = new decimal(new int[] {
                10, 0, 0, 0});
            }
            // 
            // label35
            // 
            if (this.label35 != null)
            {
                this.label35.AutoSize = true;
                this.label35.Location = new System.Drawing.Point(526, 17);
                this.label35.Name = "label35";
                this.label35.Size = new System.Drawing.Size(35, 12);
                this.label35.TabIndex = 6;
                this.label35.Text = "前N名:";
            }
            // 
            // dtpCustomerRankingsEnd
            // 
            if (this.dtpCustomerRankingsEnd != null)
            {
                this.dtpCustomerRankingsEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                this.dtpCustomerRankingsEnd.Location = new System.Drawing.Point(400, 14);
                this.dtpCustomerRankingsEnd.Name = "dtpCustomerRankingsEnd";
                this.dtpCustomerRankingsEnd.Size = new System.Drawing.Size(120, 21);
                this.dtpCustomerRankingsEnd.TabIndex = 5;
            }
            // 
            // dtpCustomerRankingsStart
            // 
            if (this.dtpCustomerRankingsStart != null)
            {
                this.dtpCustomerRankingsStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                this.dtpCustomerRankingsStart.Location = new System.Drawing.Point(120, 14);
                this.dtpCustomerRankingsStart.Name = "dtpCustomerRankingsStart";
                this.dtpCustomerRankingsStart.Size = new System.Drawing.Size(120, 21);
                this.dtpCustomerRankingsStart.TabIndex = 4;
            }
            // 
            // label33
            // 
            if (this.label33 != null)
            {
                this.label33.AutoSize = true;
                this.label33.Location = new System.Drawing.Point(6, 17);
                this.label33.Name = "label33";
                this.label33.Size = new System.Drawing.Size(107, 12);
                this.label33.TabIndex = 3;
                this.label33.Text = "销售开始日期：";
            }
            // 
            // label34
            // 
            if (this.label34 != null)
            {
                this.label34.AutoSize = true;
                this.label34.Location = new System.Drawing.Point(250, 17);
                this.label34.Name = "label34";
                this.label34.Size = new System.Drawing.Size(107, 12);
                this.label34.TabIndex = 2;
                this.label34.Text = "销售结束日期：";
            }
            // 
            // flowLayoutPanel4
            // 
            if (this.flowLayoutPanel4 != null)
            {
                this.flowLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
                if (this.label36 != null) this.flowLayoutPanel4.Controls.Add(this.label36);
                if (this.lblCustomerTotalSales != null) this.flowLayoutPanel4.Controls.Add(this.lblCustomerTotalSales);
                if (this.label37 != null) this.flowLayoutPanel4.Controls.Add(this.label37);
                if (this.lblCustomerCount != null) this.flowLayoutPanel4.Controls.Add(this.lblCustomerCount);
                this.flowLayoutPanel4.Location = new System.Drawing.Point(6, 45);
                this.flowLayoutPanel4.Name = "flowLayoutPanel4";
                this.flowLayoutPanel4.Size = new System.Drawing.Size(514, 35);
                this.flowLayoutPanel4.TabIndex = 1;
            }
            // 
            // label36
            // 
            if (this.label36 != null)
            {
                this.label36.AutoSize = true;
                this.label36.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label36.Location = new System.Drawing.Point(3, 0);
                this.label36.Name = "label36";
                this.label36.Size = new System.Drawing.Size(101, 17);
                this.label36.TabIndex = 0;
                this.label36.Text = "总销售额: ";
            }
            // 
            // lblCustomerTotalSales
            // 
            if (this.lblCustomerTotalSales != null)
            {
                this.lblCustomerTotalSales.AutoSize = true;
                this.lblCustomerTotalSales.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblCustomerTotalSales.Location = new System.Drawing.Point(110, 0);
                this.lblCustomerTotalSales.Name = "lblCustomerTotalSales";
                this.lblCustomerTotalSales.Size = new System.Drawing.Size(41, 17);
                this.lblCustomerTotalSales.TabIndex = 1;
                this.lblCustomerTotalSales.Text = "0.00";
            }
            // 
            // label37
            // 
            if (this.label37 != null)
            {
                this.label37.AutoSize = true;
                this.label37.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label37.Location = new System.Drawing.Point(157, 0);
                this.label37.Name = "label37";
                this.label37.Size = new System.Drawing.Size(77, 17);
                this.label37.TabIndex = 2;
                this.label37.Text = "客户数量: ";
            }
            // 
            // lblCustomerCount
            // 
            if (this.lblCustomerCount != null)
            {
                this.lblCustomerCount.AutoSize = true;
                this.lblCustomerCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblCustomerCount.Location = new System.Drawing.Point(240, 0);
                this.lblCustomerCount.Name = "lblCustomerCount";
                this.lblCustomerCount.Size = new System.Drawing.Size(17, 17);
                this.lblCustomerCount.TabIndex = 3;
                this.lblCustomerCount.Text = "0";
            }
            // 
            // dgvCustomerRankings
            // 
            if (this.dgvCustomerRankings != null)
            {
                this.dgvCustomerRankings.AllowUserToAddRows = false;
                this.dgvCustomerRankings.AllowUserToDeleteRows = false;
                this.dgvCustomerRankings.AllowUserToOrderColumns = true;
                this.dgvCustomerRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
                this.dgvCustomerRankings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvCustomerRankings.Location = new System.Drawing.Point(6, 86);
                this.dgvCustomerRankings.Name = "dgvCustomerRankings";
                this.dgvCustomerRankings.ReadOnly = true;
                this.dgvCustomerRankings.Size = new System.Drawing.Size(776, 332);
                this.dgvCustomerRankings.TabIndex = 0;
                // 检查是否需要添加CellFormatting事件处理（如果方法存在）
            }
            // 
            // tabProductRankings
            // 
            if (this.tabProductRankings != null)
            {
                this.tabProductRankings.Controls.Add(this.btnExportProductRankings);
                this.tabProductRankings.Controls.Add(this.btnLoadProductRankings);
                this.tabProductRankings.Controls.Add(this.nudProductTopN);
                this.tabProductRankings.Controls.Add(this.label38);
                this.tabProductRankings.Controls.Add(this.dtpProductRankingsEnd);
                this.tabProductRankings.Controls.Add(this.dtpProductRankingsStart);
                this.tabProductRankings.Controls.Add(this.label39);
                this.tabProductRankings.Controls.Add(this.label40);
                this.tabProductRankings.Controls.Add(this.flowLayoutPanel5);
                this.tabProductRankings.Controls.Add(this.dgvProductRankings);
                this.tabProductRankings.Location = new System.Drawing.Point(4, 22);
                this.tabProductRankings.Name = "tabProductRankings";
                this.tabProductRankings.Padding = new System.Windows.Forms.Padding(3);
                this.tabProductRankings.Size = new System.Drawing.Size(792, 424);
                this.tabProductRankings.TabIndex = 4;
                this.tabProductRankings.Text = "产品销售排名";
                this.tabProductRankings.UseVisualStyleBackColor = true;
            }
            // 
            // btnExportProductRankings
            // 
            if (this.btnExportProductRankings != null)
            {
                this.btnExportProductRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.btnExportProductRankings.Location = new System.Drawing.Point(700, 10);
                this.btnExportProductRankings.Name = "btnExportProductRankings";
                this.btnExportProductRankings.Size = new System.Drawing.Size(80, 30);
                this.btnExportProductRankings.TabIndex = 9;
                this.btnExportProductRankings.Text = "导出";
                this.btnExportProductRankings.UseVisualStyleBackColor = true;
                this.btnExportProductRankings.Click += new System.EventHandler(this.BtnExportProductRankingsClick);
            }
            // 
            // btnLoadProductRankings
            // 
            if (this.btnLoadProductRankings != null)
            {
                this.btnLoadProductRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
                this.btnLoadProductRankings.Location = new System.Drawing.Point(610, 10);
                this.btnLoadProductRankings.Name = "btnLoadProductRankings";
                this.btnLoadProductRankings.Size = new System.Drawing.Size(80, 30);
                this.btnLoadProductRankings.TabIndex = 8;
                this.btnLoadProductRankings.Text = "加载";
                this.btnLoadProductRankings.UseVisualStyleBackColor = true;
                this.btnLoadProductRankings.Click += new System.EventHandler(this.BtnLoadProductRankingsClick);
            }
            // 
            // nudProductTopN
            // 
            if (this.nudProductTopN != null)
            {
                this.nudProductTopN.Location = new System.Drawing.Point(570, 14);
                this.nudProductTopN.Minimum = new decimal(new int[] {
                1, 0, 0, 0});
                this.nudProductTopN.Name = "nudProductTopN";
                this.nudProductTopN.Size = new System.Drawing.Size(34, 21);
                this.nudProductTopN.TabIndex = 7;
                this.nudProductTopN.Value = new decimal(new int[] {
                10, 0, 0, 0});
            }
            // 
            // label38
            // 
            if (this.label38 != null)
            {
                this.label38.AutoSize = true;
                this.label38.Location = new System.Drawing.Point(526, 17);
                this.label38.Name = "label38";
                this.label38.Size = new System.Drawing.Size(35, 12);
                this.label38.TabIndex = 6;
                this.label38.Text = "前N名:";
            }
            // 
            // dtpProductRankingsEnd
            // 
            if (this.dtpProductRankingsEnd != null)
            {
                this.dtpProductRankingsEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                this.dtpProductRankingsEnd.Location = new System.Drawing.Point(400, 14);
                this.dtpProductRankingsEnd.Name = "dtpProductRankingsEnd";
                this.dtpProductRankingsEnd.Size = new System.Drawing.Size(120, 21);
                this.dtpProductRankingsEnd.TabIndex = 5;
            }
            // 
            // dtpProductRankingsStart
            // 
            if (this.dtpProductRankingsStart != null)
            {
                this.dtpProductRankingsStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
                this.dtpProductRankingsStart.Location = new System.Drawing.Point(120, 14);
                this.dtpProductRankingsStart.Name = "dtpProductRankingsStart";
                this.dtpProductRankingsStart.Size = new System.Drawing.Size(120, 21);
                this.dtpProductRankingsStart.TabIndex = 4;
            }
            // 
            // label39
            // 
            if (this.label39 != null)
            {
                this.label39.AutoSize = true;
                this.label39.Location = new System.Drawing.Point(6, 17);
                this.label39.Name = "label39";
                this.label39.Size = new System.Drawing.Size(107, 12);
                this.label39.TabIndex = 3;
                this.label39.Text = "销售开始日期：";
            }
            // 
            // label40
            // 
            if (this.label40 != null)
            {
                this.label40.AutoSize = true;
                this.label40.Location = new System.Drawing.Point(250, 17);
                this.label40.Name = "label40";
                this.label40.Size = new System.Drawing.Size(107, 12);
                this.label40.TabIndex = 2;
                this.label40.Text = "销售结束日期：";
            }
            // 
            // flowLayoutPanel5
            // 
            if (this.flowLayoutPanel5 != null)
            {
                this.flowLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
                if (this.label41 != null) this.flowLayoutPanel5.Controls.Add(this.label41);
                if (this.lblProductRankTotalSales != null) this.flowLayoutPanel5.Controls.Add(this.lblProductRankTotalSales);
                if (this.label42 != null) this.flowLayoutPanel5.Controls.Add(this.label42);
                if (this.lblProductRankCount != null) this.flowLayoutPanel5.Controls.Add(this.lblProductRankCount);
                this.flowLayoutPanel5.Location = new System.Drawing.Point(6, 45);
                this.flowLayoutPanel5.Name = "flowLayoutPanel5";
                this.flowLayoutPanel5.Size = new System.Drawing.Size(514, 35);
                this.flowLayoutPanel5.TabIndex = 1;
            }
            // 
            // label41
            // 
            if (this.label41 != null)
            {
                this.label41.AutoSize = true;
                this.label41.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label41.Location = new System.Drawing.Point(3, 0);
                this.label41.Name = "label41";
                this.label41.Size = new System.Drawing.Size(101, 17);
                this.label41.TabIndex = 0;
                this.label41.Text = "总销售额: ";
            }
            // 
            // lblProductRankTotalSales
            // 
            if (this.lblProductRankTotalSales != null)
            {
                this.lblProductRankTotalSales.AutoSize = true;
                this.lblProductRankTotalSales.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblProductRankTotalSales.Location = new System.Drawing.Point(110, 0);
                this.lblProductRankTotalSales.Name = "lblProductRankTotalSales";
                this.lblProductRankTotalSales.Size = new System.Drawing.Size(41, 17);
                this.lblProductRankTotalSales.TabIndex = 1;
            }            if (this.lblProductRankTotalSales != null) this.lblProductRankTotalSales.Text = "0.00";
            // 
            // label42
            // 
            if (this.label42 != null)
            {
                this.label42.AutoSize = true;
                this.label42.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label42.Location = new System.Drawing.Point(157, 0);
                this.label42.Name = "label42";
                this.label42.Size = new System.Drawing.Size(77, 17);
                this.label42.TabIndex = 2;
                this.label42.Text = "产品数量: ";
            }
            // 
            // lblProductRankCount
            // 
            if (this.lblProductRankCount != null)
            {
                this.lblProductRankCount.AutoSize = true;
                this.lblProductRankCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblProductRankCount.Location = new System.Drawing.Point(240, 0);
                this.lblProductRankCount.Name = "lblProductRankCount";
                this.lblProductRankCount.Size = new System.Drawing.Size(17, 17);
                this.lblProductRankCount.TabIndex = 3;
                this.lblProductRankCount.Text = "0";
            }
            // 
            // dgvProductRankings
            // 
            if (this.dgvProductRankings != null)
            {
                this.dgvProductRankings.AllowUserToAddRows = false;
                this.dgvProductRankings.AllowUserToDeleteRows = false;
                this.dgvProductRankings.AllowUserToOrderColumns = true;
                this.dgvProductRankings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
                this.dgvProductRankings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dgvProductRankings.Location = new System.Drawing.Point(6, 86);
                this.dgvProductRankings.Name = "dgvProductRankings";
                this.dgvProductRankings.ReadOnly = true;
                this.dgvProductRankings.Size = new System.Drawing.Size(776, 332);
                this.dgvProductRankings.TabIndex = 0;
            }
            // 
            // 初始化新选项卡的组件
            // 
            if (this.dgvCustomerRankings != null)
            {
                ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerRankings)).BeginInit();
            }
            if (this.dgvProductRankings != null)
            {
                ((System.ComponentModel.ISupportInitialize)(this.dgvProductRankings)).BeginInit();
            }
            
            // 为tabCustomerRankings添加空引用检查
            if (this.tabCustomerRankings != null)
            {
                this.tabCustomerRankings.ResumeLayout(false);
                this.tabCustomerRankings.PerformLayout();
            }
            
            // 为tabProductRankings添加空引用检查
            if (this.tabProductRankings != null)
            {
                this.tabProductRankings.ResumeLayout(false);
                this.tabProductRankings.PerformLayout();
            }
            
            this.ResumeLayout(false);

        }

        #endregion

       private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabProductSales;
        private System.Windows.Forms.TabPage tabTrendReport;
        private System.Windows.Forms.DataGridView dgvProductSales;
        private System.Windows.Forms.DateTimePicker dtpProductSalesStart;
        private System.Windows.Forms.DateTimePicker dtpProductSalesEnd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoadProductSales;
        private System.Windows.Forms.Button btnExportProductSales;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProductSalesTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProductCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalQuantity;
        private System.Windows.Forms.DataGridView dgvSalesTrend;
        private System.Windows.Forms.DateTimePicker dtpTrendStart;
        private System.Windows.Forms.DateTimePicker dtpTrendEnd;
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
        private System.Windows.Forms.TabPage tabCustomerRankings;
        private System.Windows.Forms.DataGridView dgvCustomerRankings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblCustomerTotalSales;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.DateTimePicker dtpCustomerRankingsStart;
        private System.Windows.Forms.DateTimePicker dtpCustomerRankingsEnd;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Button btnLoadCustomerRankings;
        private System.Windows.Forms.Button btnExportCustomerRankings;
        private System.Windows.Forms.NumericUpDown nudCustomerTopN;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TabPage tabProductRankings;
        private System.Windows.Forms.DataGridView dgvProductRankings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblProductRankTotalSales;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label lblProductRankCount;
        private System.Windows.Forms.DateTimePicker dtpProductRankingsStart;
        private System.Windows.Forms.DateTimePicker dtpProductRankingsEnd;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btnLoadProductRankings;
        private System.Windows.Forms.Button btnExportProductRankings;
        private System.Windows.Forms.NumericUpDown nudProductTopN;
        private System.Windows.Forms.Label label38;
    }
}