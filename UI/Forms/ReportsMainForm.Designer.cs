namespace CSproject.UI.Forms
{
    partial class ReportsMainForm
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
            this.panelReportSidebar = new System.Windows.Forms.Panel();
            this.btnBusinessDashboard = new System.Windows.Forms.Button();
            this.btnPurchaseReports = new System.Windows.Forms.Button();
            this.btnSalesReports = new System.Windows.Forms.Button();
            this.btnInventoryReports = new System.Windows.Forms.Button();
            this.panelReportHeader = new System.Windows.Forms.Panel();
            this.btnExportReport = new System.Windows.Forms.Button();
            this.btnRefreshData = new System.Windows.Forms.Button();
            this.lblModuleTitle = new System.Windows.Forms.Label();
            this.panelReportContent = new System.Windows.Forms.Panel();
            this.panelReportFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelReportSidebar.SuspendLayout();
            this.panelReportHeader.SuspendLayout();
            this.panelReportFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelReportSidebar
            // 
            this.panelReportSidebar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelReportSidebar.Controls.Add(this.btnBusinessDashboard);
            this.panelReportSidebar.Controls.Add(this.btnPurchaseReports);
            this.panelReportSidebar.Controls.Add(this.btnSalesReports);
            this.panelReportSidebar.Controls.Add(this.btnInventoryReports);
            this.panelReportSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelReportSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelReportSidebar.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelReportSidebar.Name = "panelReportSidebar";
            this.panelReportSidebar.Size = new System.Drawing.Size(330, 840);
            this.panelReportSidebar.TabIndex = 0;
            // 
            // btnBusinessDashboard
            // 
            this.btnBusinessDashboard.Location = new System.Drawing.Point(18, 306);
            this.btnBusinessDashboard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnBusinessDashboard.Name = "btnBusinessDashboard";
            this.btnBusinessDashboard.Size = new System.Drawing.Size(293, 61);
            this.btnBusinessDashboard.TabIndex = 3;
            this.btnBusinessDashboard.Text = "经营看板";
            this.btnBusinessDashboard.UseVisualStyleBackColor = true;
            this.btnBusinessDashboard.Click += new System.EventHandler(this.BtnBusinessDashboardClick);
            // 
            // btnPurchaseReports
            // 
            this.btnPurchaseReports.Location = new System.Drawing.Point(18, 228);
            this.btnPurchaseReports.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnPurchaseReports.Name = "btnPurchaseReports";
            this.btnPurchaseReports.Size = new System.Drawing.Size(293, 61);
            this.btnPurchaseReports.TabIndex = 2;
            this.btnPurchaseReports.Text = "采购报表";
            this.btnPurchaseReports.UseVisualStyleBackColor = true;
            this.btnPurchaseReports.Click += new System.EventHandler(this.BtnPurchaseReportsClick);
            // 
            // btnSalesReports
            // 
            this.btnSalesReports.Location = new System.Drawing.Point(18, 149);
            this.btnSalesReports.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnSalesReports.Name = "btnSalesReports";
            this.btnSalesReports.Size = new System.Drawing.Size(293, 61);
            this.btnSalesReports.TabIndex = 1;
            this.btnSalesReports.Text = "销售报表";
            this.btnSalesReports.UseVisualStyleBackColor = true;
            this.btnSalesReports.Click += new System.EventHandler(this.BtnSalesReportsClick);
            // 
            // btnInventoryReports
            // 
            this.btnInventoryReports.Location = new System.Drawing.Point(18, 70);
            this.btnInventoryReports.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnInventoryReports.Name = "btnInventoryReports";
            this.btnInventoryReports.Size = new System.Drawing.Size(293, 61);
            this.btnInventoryReports.TabIndex = 0;
            this.btnInventoryReports.Text = "库存报表";
            this.btnInventoryReports.UseVisualStyleBackColor = true;
            this.btnInventoryReports.Click += new System.EventHandler(this.BtnInventoryReportsClick);
            // 
            // panelReportHeader
            // 
            this.panelReportHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelReportHeader.Controls.Add(this.btnExportReport);
            this.panelReportHeader.Controls.Add(this.btnRefreshData);
            this.panelReportHeader.Controls.Add(this.lblModuleTitle);
            this.panelReportHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelReportHeader.Location = new System.Drawing.Point(330, 0);
            this.panelReportHeader.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelReportHeader.Name = "panelReportHeader";
            this.panelReportHeader.Size = new System.Drawing.Size(917, 105);
            this.panelReportHeader.TabIndex = 1;
            // 
            // btnExportReport
            // 
            this.btnExportReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportReport.Location = new System.Drawing.Point(660, 26);
            this.btnExportReport.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnExportReport.Name = "btnExportReport";
            this.btnExportReport.Size = new System.Drawing.Size(220, 52);
            this.btnExportReport.TabIndex = 2;
            this.btnExportReport.Text = "导出报表";
            this.btnExportReport.UseVisualStyleBackColor = true;
            this.btnExportReport.Click += new System.EventHandler(this.BtnExportReportClick);
            // 
            // btnRefreshData
            // 
            this.btnRefreshData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshData.Location = new System.Drawing.Point(422, 26);
            this.btnRefreshData.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnRefreshData.Name = "btnRefreshData";
            this.btnRefreshData.Size = new System.Drawing.Size(220, 52);
            this.btnRefreshData.TabIndex = 1;
            this.btnRefreshData.Text = "刷新数据";
            this.btnRefreshData.UseVisualStyleBackColor = true;
            this.btnRefreshData.Click += new System.EventHandler(this.BtnRefreshDataClick);
            // 
            // lblModuleTitle
            // 
            this.lblModuleTitle.AutoSize = true;
            this.lblModuleTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblModuleTitle.Location = new System.Drawing.Point(37, 32);
            this.lblModuleTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblModuleTitle.Name = "lblModuleTitle";
            this.lblModuleTitle.Size = new System.Drawing.Size(129, 37);
            this.lblModuleTitle.TabIndex = 0;
            this.lblModuleTitle.Text = "报表分析";
            // 
            // panelReportContent
            // 
            this.panelReportContent.BackColor = System.Drawing.Color.White;
            this.panelReportContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReportContent.Location = new System.Drawing.Point(330, 105);
            this.panelReportContent.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelReportContent.Name = "panelReportContent";
            this.panelReportContent.Size = new System.Drawing.Size(917, 735);
            this.panelReportContent.TabIndex = 2;
            // 
            // panelReportFooter
            // 
            this.panelReportFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelReportFooter.Controls.Add(this.lblStatus);
            this.panelReportFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelReportFooter.Location = new System.Drawing.Point(330, 788);
            this.panelReportFooter.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.panelReportFooter.Name = "panelReportFooter";
            this.panelReportFooter.Size = new System.Drawing.Size(917, 52);
            this.panelReportFooter.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 28);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReportsMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 840);
            this.Controls.Add(this.panelReportFooter);
            this.Controls.Add(this.panelReportContent);
            this.Controls.Add(this.panelReportHeader);
            this.Controls.Add(this.panelReportSidebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "ReportsMainForm";
            this.Text = "报表分析系统";
            this.Load += new System.EventHandler(this.ReportsMainForm_Load);
            this.panelReportSidebar.ResumeLayout(false);
            this.panelReportHeader.ResumeLayout(false);
            this.panelReportHeader.PerformLayout();
            this.panelReportFooter.ResumeLayout(false);
            this.panelReportFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelReportSidebar;
        private System.Windows.Forms.Button btnInventoryReports;
        private System.Windows.Forms.Button btnSalesReports;
        private System.Windows.Forms.Button btnPurchaseReports;
        private System.Windows.Forms.Button btnBusinessDashboard;
        private System.Windows.Forms.Panel panelReportHeader;
        private System.Windows.Forms.Label lblModuleTitle;
        private System.Windows.Forms.Button btnRefreshData;
        private System.Windows.Forms.Button btnExportReport;
        private System.Windows.Forms.Panel panelReportContent;
        private System.Windows.Forms.Panel panelReportFooter;
        private System.Windows.Forms.Label lblStatus;
    }
}