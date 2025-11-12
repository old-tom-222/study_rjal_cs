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
            this.tabAdjust = new System.Windows.Forms.TabPage();
            this.btnAdjust = new System.Windows.Forms.Button();
            this.txtAdjRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdjRef = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdjType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDeltaQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdjWarehouseId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAdjProductId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
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
            this.tabAdjust.SuspendLayout();
            this.tabTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTxn)).BeginInit();
            this.tabWarnings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridWarnings)).BeginInit();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabQuery);
            this.tabMain.Controls.Add(this.tabAdjust);
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
            // tabAdjust
            // 
            this.tabAdjust.Controls.Add(this.btnAdjust);
            this.tabAdjust.Controls.Add(this.txtAdjRemark);
            this.tabAdjust.Controls.Add(this.label6);
            this.tabAdjust.Controls.Add(this.txtAdjRef);
            this.tabAdjust.Controls.Add(this.label5);
            this.tabAdjust.Controls.Add(this.txtAdjType);
            this.tabAdjust.Controls.Add(this.label4);
            this.tabAdjust.Controls.Add(this.txtDeltaQty);
            this.tabAdjust.Controls.Add(this.label3);
            this.tabAdjust.Controls.Add(this.txtAdjWarehouseId);
            this.tabAdjust.Controls.Add(this.label8);
            this.tabAdjust.Controls.Add(this.txtAdjProductId);
            this.tabAdjust.Controls.Add(this.label7);
            this.tabAdjust.Location = new System.Drawing.Point(4, 22);
            this.tabAdjust.Name = "tabAdjust";
            this.tabAdjust.Padding = new System.Windows.Forms.Padding(3);
            this.tabAdjust.Size = new System.Drawing.Size(892, 574);
            this.tabAdjust.TabIndex = 1;
            this.tabAdjust.Text = "库存调整";
            this.tabAdjust.UseVisualStyleBackColor = true;
            // 
            // btnAdjust
            // 
            this.btnAdjust.Location = new System.Drawing.Point(90, 240);
            this.btnAdjust.Name = "btnAdjust";
            this.btnAdjust.Size = new System.Drawing.Size(100, 30);
            this.btnAdjust.TabIndex = 12;
            this.btnAdjust.Text = "确认调整";
            this.btnAdjust.UseVisualStyleBackColor = true;
            this.btnAdjust.Click += new System.EventHandler(this.BtnAdjustClick);
            // 
            // txtAdjRemark
            // 
            this.txtAdjRemark.Location = new System.Drawing.Point(90, 200);
            this.txtAdjRemark.Name = "txtAdjRemark";
            this.txtAdjRemark.Size = new System.Drawing.Size(300, 21);
            this.txtAdjRemark.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "备注";
            // 
            // txtAdjRef
            // 
            this.txtAdjRef.Location = new System.Drawing.Point(90, 160);
            this.txtAdjRef.Name = "txtAdjRef";
            this.txtAdjRef.Size = new System.Drawing.Size(200, 21);
            this.txtAdjRef.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "参考号";
            // 
            // txtAdjType
            // 
            this.txtAdjType.Location = new System.Drawing.Point(90, 120);
            this.txtAdjType.Name = "txtAdjType";
            this.txtAdjType.Size = new System.Drawing.Size(150, 21);
            this.txtAdjType.TabIndex = 7;
            this.txtAdjType.Text = "adjust";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "调整类型";
            // 
            // txtDeltaQty
            // 
            this.txtDeltaQty.Location = new System.Drawing.Point(90, 80);
            this.txtDeltaQty.Name = "txtDeltaQty";
            this.txtDeltaQty.Size = new System.Drawing.Size(150, 21);
            this.txtDeltaQty.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "变更数量";
            // 
            // txtAdjWarehouseId
            // 
            this.txtAdjWarehouseId.Location = new System.Drawing.Point(320, 40);
            this.txtAdjWarehouseId.Name = "txtAdjWarehouseId";
            this.txtAdjWarehouseId.Size = new System.Drawing.Size(150, 21);
            this.txtAdjWarehouseId.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(260, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "仓库ID";
            // 
            // txtAdjProductId
            // 
            this.txtAdjProductId.Location = new System.Drawing.Point(90, 40);
            this.txtAdjProductId.Name = "txtAdjProductId";
            this.txtAdjProductId.Size = new System.Drawing.Size(150, 21);
            this.txtAdjProductId.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "商品ID";
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
            this.tabAdjust.ResumeLayout(false);
            this.tabAdjust.PerformLayout();
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
        private System.Windows.Forms.TabPage tabAdjust;
        private System.Windows.Forms.TextBox txtAdjWarehouseId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAdjProductId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDeltaQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAdjType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAdjRef;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAdjRemark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAdjust;
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