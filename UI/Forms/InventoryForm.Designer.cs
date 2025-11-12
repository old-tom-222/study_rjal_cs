namespace CSproject.UI.Forms
{
    partial class InventoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.gridInventory = new System.Windows.Forms.DataGridView();
            this.btnQuery = new System.Windows.Forms.Button();
            this.txtWarehouseId = new System.Windows.Forms.TextBox();
            this.txtProductId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();

            this.tabTransactions = new System.Windows.Forms.TabPage();
            this.gridTxn = new System.Windows.Forms.DataGridView();
            this.btnTxnQuery = new System.Windows.Forms.Button();
            this.chkUseTimeRange = new System.Windows.Forms.CheckBox();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTxnWarehouseId = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTxnProductId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tabWarnings = new System.Windows.Forms.TabPage();
            this.gridWarnings = new System.Windows.Forms.DataGridView();
            this.btnLoadWarnings = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).BeginInit();
            this.tabTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTxn)).BeginInit();
            this.tabWarnings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWarnings)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabQuery);
            this.tabMain.Controls.Add(this.tabTransactions);
            this.tabMain.Controls.Add(this.tabWarnings);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(900, 600);
            this.tabMain.TabIndex = 0;
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.gridInventory);
            this.tabQuery.Controls.Add(this.btnQuery);
            this.tabQuery.Controls.Add(this.txtWarehouseId);
            this.tabQuery.Controls.Add(this.txtProductId);
            this.tabQuery.Controls.Add(this.label2);
            this.tabQuery.Controls.Add(this.label1);
            this.tabQuery.Location = new System.Drawing.Point(4, 22);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(892, 574);
            this.tabQuery.TabIndex = 0;
            this.tabQuery.Text = "库存查询";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // gridInventory
            // 
            this.gridInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridInventory.Location = new System.Drawing.Point(20, 80);
            this.gridInventory.Name = "gridInventory";
            this.gridInventory.Size = new System.Drawing.Size(850, 470);
            this.gridInventory.TabIndex = 5;
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(400, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.BtnQueryClick);
            // 
            // txtWarehouseId
            // 
            this.txtWarehouseId.Location = new System.Drawing.Point(260, 32);
            this.txtWarehouseId.Name = "txtWarehouseId";
            this.txtWarehouseId.Size = new System.Drawing.Size(120, 21);
            this.txtWarehouseId.TabIndex = 3;
            // 
            // txtProductId
            // 
            this.txtProductId.Location = new System.Drawing.Point(90, 32);
            this.txtProductId.Name = "txtProductId";
            this.txtProductId.Size = new System.Drawing.Size(120, 21);
            this.txtProductId.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "仓库ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品ID";
            // 
            // tabTransactions
            // 
            this.tabTransactions.Controls.Add(this.gridTxn);
            this.tabTransactions.Controls.Add(this.btnTxnQuery);
            this.tabTransactions.Controls.Add(this.chkUseTimeRange);
            this.tabTransactions.Controls.Add(this.dtTo);
            this.tabTransactions.Controls.Add(this.dtFrom);
            this.tabTransactions.Controls.Add(this.label12);
            this.tabTransactions.Controls.Add(this.txtTxnWarehouseId);
            this.tabTransactions.Controls.Add(this.label11);
            this.tabTransactions.Controls.Add(this.txtTxnProductId);
            this.tabTransactions.Controls.Add(this.label10);
            this.tabTransactions.Location = new System.Drawing.Point(4, 22);
            this.tabTransactions.Name = "tabTransactions";
            this.tabTransactions.Padding = new System.Windows.Forms.Padding(3);
            this.tabTransactions.Size = new System.Drawing.Size(892, 574);
            this.tabTransactions.TabIndex = 2;
            this.tabTransactions.Text = "库存流水";
            this.tabTransactions.UseVisualStyleBackColor = true;
            // 
            // gridTxn
            // 
            this.gridTxn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTxn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTxn.Location = new System.Drawing.Point(20, 120);
            this.gridTxn.Name = "gridTxn";
            this.gridTxn.Size = new System.Drawing.Size(850, 430);
            this.gridTxn.TabIndex = 9;
            // 
            // btnTxnQuery
            // 
            this.btnTxnQuery.Location = new System.Drawing.Point(760, 30);
            this.btnTxnQuery.Name = "btnTxnQuery";
            this.btnTxnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnTxnQuery.TabIndex = 8;
            this.btnTxnQuery.Text = "查询";
            this.btnTxnQuery.UseVisualStyleBackColor = true;
            this.btnTxnQuery.Click += new System.EventHandler(this.BtnTxnQueryClick);
            // 
            // chkUseTimeRange
            // 
            this.chkUseTimeRange.AutoSize = true;
            this.chkUseTimeRange.Location = new System.Drawing.Point(470, 34);
            this.chkUseTimeRange.Name = "chkUseTimeRange";
            this.chkUseTimeRange.Size = new System.Drawing.Size(96, 16);
            this.chkUseTimeRange.TabIndex = 7;
            this.chkUseTimeRange.Text = "按时间过滤";
            this.chkUseTimeRange.UseVisualStyleBackColor = true;
            // 
            // dtTo
            // 
            this.dtTo.Location = new System.Drawing.Point(570, 32);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(180, 21);
            this.dtTo.TabIndex = 6;
            // 
            // dtFrom
            // 
            this.dtFrom.Location = new System.Drawing.Point(280, 32);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(180, 21);
            this.dtFrom.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(230, 36);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "起止";
            // 
            // txtTxnWarehouseId
            // 
            this.txtTxnWarehouseId.Location = new System.Drawing.Point(180, 32);
            this.txtTxnWarehouseId.Name = "txtTxnWarehouseId";
            this.txtTxnWarehouseId.Size = new System.Drawing.Size(40, 21);
            this.txtTxnWarehouseId.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(130, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "仓库ID";
            // 
            // txtTxnProductId
            // 
            this.txtTxnProductId.Location = new System.Drawing.Point(70, 32);
            this.txtTxnProductId.Name = "txtTxnProductId";
            this.txtTxnProductId.Size = new System.Drawing.Size(40, 21);
            this.txtTxnProductId.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "商品ID";
            // 
            // tabWarnings
            // 
            this.tabWarnings.Controls.Add(this.gridWarnings);
            this.tabWarnings.Controls.Add(this.btnLoadWarnings);
            this.tabWarnings.Location = new System.Drawing.Point(4, 22);
            this.tabWarnings.Name = "tabWarnings";
            this.tabWarnings.Padding = new System.Windows.Forms.Padding(3);
            this.tabWarnings.Size = new System.Drawing.Size(892, 574);
            this.tabWarnings.TabIndex = 3;
            this.tabWarnings.Text = "库存预警";
            this.tabWarnings.UseVisualStyleBackColor = true;
            // 
            // gridWarnings
            // 
            this.gridWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                        | System.Windows.Forms.AnchorStyles.Left) 
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridWarnings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWarnings.Location = new System.Drawing.Point(20, 70);
            this.gridWarnings.Name = "gridWarnings";
            this.gridWarnings.Size = new System.Drawing.Size(850, 480);
            this.gridWarnings.TabIndex = 1;
            // 
            // btnLoadWarnings
            // 
            this.btnLoadWarnings.Location = new System.Drawing.Point(20, 30);
            this.btnLoadWarnings.Name = "btnLoadWarnings";
            this.btnLoadWarnings.Size = new System.Drawing.Size(120, 23);
            this.btnLoadWarnings.TabIndex = 0;
            this.btnLoadWarnings.Text = "加载预警";
            this.btnLoadWarnings.UseVisualStyleBackColor = true;
            this.btnLoadWarnings.Click += new System.EventHandler(this.BtnLoadWarningsClick);
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.tabMain);
            this.Name = "InventoryForm";
            this.Text = "库存管理";
            this.tabMain.ResumeLayout(false);
            this.tabQuery.ResumeLayout(false);
            this.tabQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridInventory)).EndInit();
            this.tabTransactions.ResumeLayout(false);
            this.tabTransactions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTxn)).EndInit();
            this.tabWarnings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridWarnings)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabQuery;
        private System.Windows.Forms.DataGridView gridInventory;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.TextBox txtWarehouseId;
        private System.Windows.Forms.TextBox txtProductId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.TabPage tabTransactions;
        private System.Windows.Forms.DataGridView gridTxn;
        private System.Windows.Forms.Button btnTxnQuery;
        private System.Windows.Forms.CheckBox chkUseTimeRange;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTxnWarehouseId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTxnProductId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabWarnings;
        private System.Windows.Forms.DataGridView gridWarnings;
        private System.Windows.Forms.Button btnLoadWarnings;
    }
}