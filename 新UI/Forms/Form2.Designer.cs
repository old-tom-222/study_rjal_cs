namespace CSproject.UI.Forms
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnPurchase = new System.Windows.Forms.Button();
            this.btnSales = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnFinance = new System.Windows.Forms.Button();
            this.btnBasicData = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.panelSalesSubMenu = new System.Windows.Forms.Panel();
            this.lblSalesSubMenuTitle = new System.Windows.Forms.Label();
            this.btnSalesOrderList = new System.Windows.Forms.Button();
            this.btnSalesOrderCreate = new System.Windows.Forms.Button();
            this.btnShipmentManagement = new System.Windows.Forms.Button();
            this.btnSalesStatistics = new System.Windows.Forms.Button();
            this.btnSalesRanking = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.lblPlaceholder = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.btnLogout);
            this.panelTop.Controls.Add(this.lblWelcome);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 40);
            this.panelTop.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Location = new System.Drawing.Point(720, 12);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(59, 12);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "欢迎用户";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(800, 7);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(80, 25);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "退出登录";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogoutClick);
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.LightBlue;
            this.panelSidebar.Controls.Add(this.btnReports);
            this.panelSidebar.Controls.Add(this.btnBasicData);
            this.panelSidebar.Controls.Add(this.btnFinance);
            this.panelSidebar.Controls.Add(this.btnInventory);
            this.panelSidebar.Controls.Add(this.btnSales);
            this.panelSidebar.Controls.Add(this.btnPurchase);
            this.panelSidebar.Controls.Add(this.lblTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 40);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(220, 560);
            this.panelSidebar.TabIndex = 1;
            // 
            // panelSalesSubMenu
            // 
            this.panelSalesSubMenu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panelSalesSubMenu.Controls.Add(this.btnSalesRanking);
            this.panelSalesSubMenu.Controls.Add(this.btnSalesStatistics);
            this.panelSalesSubMenu.Controls.Add(this.btnShipmentManagement);
            this.panelSalesSubMenu.Controls.Add(this.btnSalesOrderCreate);
            this.panelSalesSubMenu.Controls.Add(this.btnSalesOrderList);
            this.panelSalesSubMenu.Controls.Add(this.lblSalesSubMenuTitle);
            this.panelSalesSubMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSalesSubMenu.Location = new System.Drawing.Point(220, 40);
            this.panelSalesSubMenu.Name = "panelSalesSubMenu";
            this.panelSalesSubMenu.Size = new System.Drawing.Size(180, 560);
            this.panelSalesSubMenu.TabIndex = 3;
            this.panelSalesSubMenu.Visible = false;
            // 
            // lblSalesSubMenuTitle
            // 
            this.lblSalesSubMenuTitle.AutoSize = true;
            this.lblSalesSubMenuTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalesSubMenuTitle.Location = new System.Drawing.Point(10, 10);
            this.lblSalesSubMenuTitle.Name = "lblSalesSubMenuTitle";
            this.lblSalesSubMenuTitle.Size = new System.Drawing.Size(54, 17);
            this.lblSalesSubMenuTitle.TabIndex = 0;
            this.lblSalesSubMenuTitle.Text = "销售管理";
            // 
            // btnSalesOrderList
            // 
            this.btnSalesOrderList.Location = new System.Drawing.Point(10, 40);
            this.btnSalesOrderList.Name = "btnSalesOrderList";
            this.btnSalesOrderList.Size = new System.Drawing.Size(160, 30);
            this.btnSalesOrderList.TabIndex = 1;
            this.btnSalesOrderList.Text = "订单列表（主要页面）";
            this.btnSalesOrderList.UseVisualStyleBackColor = true;
            this.btnSalesOrderList.Click += new System.EventHandler(this.btnSalesOrderList_Click);
            // 
            // btnSalesOrderCreate
            // 
            this.btnSalesOrderCreate.Location = new System.Drawing.Point(10, 80);
            this.btnSalesOrderCreate.Name = "btnSalesOrderCreate";
            this.btnSalesOrderCreate.Size = new System.Drawing.Size(160, 30);
            this.btnSalesOrderCreate.TabIndex = 2;
            this.btnSalesOrderCreate.Text = "新建订单";
            this.btnSalesOrderCreate.UseVisualStyleBackColor = true;
            this.btnSalesOrderCreate.Click += new System.EventHandler(this.btnSalesOrderCreate_Click);
            // 
            // btnShipmentManagement
            // 
            this.btnShipmentManagement.Location = new System.Drawing.Point(10, 120);
            this.btnShipmentManagement.Name = "btnShipmentManagement";
            this.btnShipmentManagement.Size = new System.Drawing.Size(160, 30);
            this.btnShipmentManagement.TabIndex = 3;
            this.btnShipmentManagement.Text = "待发货订单";
            this.btnShipmentManagement.UseVisualStyleBackColor = true;
            this.btnShipmentManagement.Click += new System.EventHandler(this.btnShipmentManagement_Click);
            // 
            // btnSalesStatistics
            // 
            this.btnSalesStatistics.Location = new System.Drawing.Point(10, 160);
            this.btnSalesStatistics.Name = "btnSalesStatistics";
            this.btnSalesStatistics.Size = new System.Drawing.Size(160, 30);
            this.btnSalesStatistics.TabIndex = 4;
            this.btnSalesStatistics.Text = "销售统计";
            this.btnSalesStatistics.UseVisualStyleBackColor = true;
            this.btnSalesStatistics.Click += new System.EventHandler(this.btnSalesStatistics_Click);
            // 
            // btnSalesRanking
            // 
            this.btnSalesRanking.Location = new System.Drawing.Point(10, 200);
            this.btnSalesRanking.Name = "btnSalesRanking";
            this.btnSalesRanking.Size = new System.Drawing.Size(160, 30);
            this.btnSalesRanking.TabIndex = 5;
            this.btnSalesRanking.Text = "销售排名";
            this.btnSalesRanking.UseVisualStyleBackColor = true;
            this.btnSalesRanking.Click += new System.EventHandler(this.btnSalesRanking_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(190, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "电子产品进销存管理系统";
            // 
            // btnPurchase
            // 
            this.btnPurchase.Location = new System.Drawing.Point(16, 50);
            this.btnPurchase.Name = "btnPurchase";
            this.btnPurchase.Size = new System.Drawing.Size(180, 35);
            this.btnPurchase.TabIndex = 1;
            this.btnPurchase.Text = "采购管理";
            this.btnPurchase.UseVisualStyleBackColor = true;
            this.btnPurchase.Click += new System.EventHandler(this.BtnMenuPurchaseClick);
            // 
            // btnSales
            // 
            this.btnSales.Location = new System.Drawing.Point(16, 95);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(180, 35);
            this.btnSales.TabIndex = 2;
            this.btnSales.Text = "销售管理";
            this.btnSales.UseVisualStyleBackColor = true;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(16, 140);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(180, 35);
            this.btnInventory.TabIndex = 3;
            this.btnInventory.Text = "库存管理";
            this.btnInventory.UseVisualStyleBackColor = true;
            this.btnInventory.Click += new System.EventHandler(this.BtnMenuInventoryClick);
            // 
            // btnFinance
            // 
            this.btnFinance.Location = new System.Drawing.Point(16, 185);
            this.btnFinance.Name = "btnFinance";
            this.btnFinance.Size = new System.Drawing.Size(180, 35);
            this.btnFinance.TabIndex = 4;
            this.btnFinance.Text = "财务管理";
            this.btnFinance.UseVisualStyleBackColor = true;
            this.btnFinance.Click += new System.EventHandler(this.BtnMenuFinanceClick);
            // 
            // btnBasicData
            // 
            this.btnBasicData.Location = new System.Drawing.Point(16, 230);
            this.btnBasicData.Name = "btnBasicData";
            this.btnBasicData.Size = new System.Drawing.Size(180, 35);
            this.btnBasicData.TabIndex = 5;
            this.btnBasicData.Text = "基础数据";
            this.btnBasicData.UseVisualStyleBackColor = true;
            this.btnBasicData.Click += new System.EventHandler(this.BtnMenuBasicClick);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(16, 275);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(180, 35);
            this.btnReports.TabIndex = 6;
            this.btnReports.Text = "报表分析";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.BtnMenuReportsClick);
            // 
            // 
            // 移除原始的菜单控件，使用左侧导航布局代替
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.White;
            this.panelContent.Controls.Add(this.lblPlaceholder);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(400, 40);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(500, 560);
            this.panelContent.TabIndex = 2;
            // 
            // lblPlaceholder
            // 
            this.lblPlaceholder.AutoSize = true;
            this.lblPlaceholder.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.lblPlaceholder.Location = new System.Drawing.Point(20, 20);
            this.lblPlaceholder.Name = "lblPlaceholder";
            this.lblPlaceholder.Size = new System.Drawing.Size(154, 21);
            this.lblPlaceholder.TabIndex = 0;
            this.lblPlaceholder.Text = "请选择左侧菜单...";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSalesSubMenu);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelTop);
            this.Name = "Form2";
            this.Text = "电子产品进销存管理系统";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPurchase;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnFinance;
        private System.Windows.Forms.Button btnBasicData;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblPlaceholder;
        private System.Windows.Forms.Panel panelSalesSubMenu;
        private System.Windows.Forms.Label lblSalesSubMenuTitle;
        private System.Windows.Forms.Button btnSalesOrderList;
        private System.Windows.Forms.Button btnSalesOrderCreate;
        private System.Windows.Forms.Button btnShipmentManagement;
        private System.Windows.Forms.Button btnSalesStatistics;
        private System.Windows.Forms.Button btnSalesRanking;
    }
}