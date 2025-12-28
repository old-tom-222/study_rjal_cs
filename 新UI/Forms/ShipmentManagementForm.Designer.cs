namespace CSproject.UI.Forms
{
    partial class ShipmentManagementForm
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dgvPendingShipments = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnExportData = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingShipments)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1100, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(20, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "发货管理";
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.BackColor = System.Drawing.SystemColors.Control;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.btnReset);
            this.panelSearch.Controls.Add(this.btnSearch);
            this.panelSearch.Controls.Add(this.dtpEndDate);
            this.panelSearch.Controls.Add(this.dtpStartDate);
            this.panelSearch.Controls.Add(this.label6);
            this.panelSearch.Controls.Add(this.label5);
            this.panelSearch.Controls.Add(this.txtCustomerName);
            this.panelSearch.Controls.Add(this.label4);
            this.panelSearch.Controls.Add(this.txtOrderNumber);
            this.panelSearch.Controls.Add(this.label3);
            this.panelSearch.Location = new System.Drawing.Point(0, 50);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(1100, 80);
            this.panelSearch.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(950, 25);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(80, 30);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(850, 25);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 30);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Checked = false;
            this.dtpEndDate.CustomFormat = "";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(700, 30);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.ShowCheckBox = true;
            this.dtpEndDate.Size = new System.Drawing.Size(130, 21);
            this.dtpEndDate.TabIndex = 7;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Checked = false;
            this.dtpStartDate.CustomFormat = "";
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(500, 30);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowCheckBox = true;
            this.dtpStartDate.Size = new System.Drawing.Size(130, 21);
            this.dtpStartDate.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(650, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "结束日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(450, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "开始日期";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(280, 30);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(150, 21);
            this.txtCustomerName.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "客户名称";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(80, 30);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(120, 21);
            this.txtOrderNumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "订单编号";
            // 
            // panelContent
            // 
            this.panelContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContent.BackColor = System.Drawing.SystemColors.Control;
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.dgvPendingShipments);
            this.panelContent.Location = new System.Drawing.Point(0, 130);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1100, 400);
            this.panelContent.TabIndex = 2;
            // 
            // dgvPendingShipments
            // 
            this.dgvPendingShipments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPendingShipments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingShipments.Location = new System.Drawing.Point(10, 10);
            this.dgvPendingShipments.Name = "dgvPendingShipments";
            this.dgvPendingShipments.RowTemplate.Height = 23;
            this.dgvPendingShipments.Size = new System.Drawing.Size(1078, 378);
            this.dgvPendingShipments.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFooter.Controls.Add(this.btnExportData);
            this.panelFooter.Controls.Add(this.btnRefresh);
            this.panelFooter.Controls.Add(this.lblTotalProducts);
            this.panelFooter.Controls.Add(this.lblTotalAmount);
            this.panelFooter.Controls.Add(this.lblTotalOrders);
            this.panelFooter.Location = new System.Drawing.Point(0, 530);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1100, 60);
            this.panelFooter.TabIndex = 3;
            // 
            // btnExportData
            // 
            this.btnExportData.Location = new System.Drawing.Point(950, 15);
            this.btnExportData.Name = "btnExportData";
            this.btnExportData.Size = new System.Drawing.Size(80, 30);
            this.btnExportData.TabIndex = 4;
            this.btnExportData.Text = "导出数据";
            this.btnExportData.UseVisualStyleBackColor = true;
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(850, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Location = new System.Drawing.Point(300, 25);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(89, 12);
            this.lblTotalProducts.TabIndex = 2;
            this.lblTotalProducts.Text = "待发货商品种类: 0";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(150, 25);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(89, 12);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "待发货总金额: 0.00";
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Location = new System.Drawing.Point(20, 25);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(89, 12);
            this.lblTotalOrders.TabIndex = 0;
            this.lblTotalOrders.Text = "待发货订单总数: 0";
            // 
            // ShipmentManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 590);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelHeader);
            this.Name = "ShipmentManagementForm";
            this.Text = "发货管理";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingShipments)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.DataGridView dgvPendingShipments;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Button btnExportData;
        private System.Windows.Forms.Button btnRefresh;
    }
}