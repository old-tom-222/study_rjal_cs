namespace CSproject.UI.Forms
{
    partial class SalesOrderCreateForm
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
            this.panelOrderInfo = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.panelProductSelection = new System.Windows.Forms.Panel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.txtAvailableStock = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUnitPrice = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panelOrderDetails = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnEditMode = new System.Windows.Forms.Button();
            this.btnCancelEdit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelOrderInfo.SuspendLayout();
            this.panelProductSelection.SuspendLayout();
            this.panelOrderDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelHeader.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Location = new System.Drawing.Point(12, 12);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(974, 50);
            this.panelHeader.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "新建销售订单";
            // 
            // panelOrderInfo
            // 
            this.panelOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderInfo.BackColor = System.Drawing.SystemColors.Control;
            this.panelOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrderInfo.Controls.Add(this.label5);
            this.panelOrderInfo.Controls.Add(this.txtTotalAmount);
            this.panelOrderInfo.Controls.Add(this.label4);
            this.panelOrderInfo.Controls.Add(this.txtOrderNumber);
            this.panelOrderInfo.Controls.Add(this.label3);
            this.panelOrderInfo.Controls.Add(this.dtpOrderDate);
            this.panelOrderInfo.Controls.Add(this.label2);
            this.panelOrderInfo.Controls.Add(this.cmbCustomer);
            this.panelOrderInfo.Location = new System.Drawing.Point(12, 68);
            this.panelOrderInfo.Name = "panelOrderInfo";
            this.panelOrderInfo.Size = new System.Drawing.Size(974, 80);
            this.panelOrderInfo.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(720, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "订单金额";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(780, 24);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 21);
            this.txtTotalAmount.TabIndex = 6;
            this.txtTotalAmount.Text = "0.00";
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(450, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "订单日期";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(80, 24);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.ReadOnly = true;
            this.txtOrderNumber.Size = new System.Drawing.Size(150, 21);
            this.txtOrderNumber.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "订单号";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Location = new System.Drawing.Point(510, 24);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(180, 21);
            this.dtpOrderDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(250, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户";
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(300, 24);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(120, 20);
            this.cmbCustomer.TabIndex = 0;
            // 
            // panelProductSelection
            // 
            this.panelProductSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProductSelection.BackColor = System.Drawing.SystemColors.Control;
            this.panelProductSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProductSelection.Controls.Add(this.btnAddProduct);
            this.panelProductSelection.Controls.Add(this.txtAvailableStock);
            this.panelProductSelection.Controls.Add(this.label10);
            this.panelProductSelection.Controls.Add(this.txtUnitPrice);
            this.panelProductSelection.Controls.Add(this.txtQuantity);
            this.panelProductSelection.Controls.Add(this.label9);
            this.panelProductSelection.Controls.Add(this.label8);
            this.panelProductSelection.Controls.Add(this.cmbProduct);
            this.panelProductSelection.Controls.Add(this.label7);
            this.panelProductSelection.Location = new System.Drawing.Point(12, 154);
            this.panelProductSelection.Name = "panelProductSelection";
            this.panelProductSelection.Size = new System.Drawing.Size(974, 60);
            this.panelProductSelection.TabIndex = 2;
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(780, 20);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(80, 25);
            this.btnAddProduct.TabIndex = 8;
            this.btnAddProduct.Text = "添加产品";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // txtAvailableStock
            // 
            this.txtAvailableStock.Location = new System.Drawing.Point(680, 21);
            this.txtAvailableStock.Name = "txtAvailableStock";
            this.txtAvailableStock.ReadOnly = true;
            this.txtAvailableStock.Size = new System.Drawing.Size(80, 21);
            this.txtAvailableStock.TabIndex = 7;
            this.txtAvailableStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(610, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "可用库存";
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(510, 21);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.ReadOnly = true;
            this.txtUnitPrice.Size = new System.Drawing.Size(80, 21);
            this.txtUnitPrice.TabIndex = 5;
            this.txtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(410, 21);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(80, 21);
            this.txtQuantity.TabIndex = 4;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(460, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "单价";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(370, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "数量";
            // 
            // cmbProduct
            // 
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(80, 20);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(260, 20);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.cmbProduct_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "产品";
            // 
            // panelOrderDetails
            // 
            this.panelOrderDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderDetails.BackColor = System.Drawing.SystemColors.Control;
            this.panelOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.panelOrderDetails.Controls.Add(this.label6);
            this.panelOrderDetails.Location = new System.Drawing.Point(12, 220);
            this.panelOrderDetails.Name = "panelOrderDetails";
            this.panelOrderDetails.Size = new System.Drawing.Size(974, 300);
            this.panelOrderDetails.TabIndex = 3;
            // 
            // dgvOrderDetails
            // 
            this.dgvOrderDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderDetails.Location = new System.Drawing.Point(10, 30);
            this.dgvOrderDetails.Name = "dgvOrderDetails";
            this.dgvOrderDetails.RowTemplate.Height = 23;
            this.dgvOrderDetails.Size = new System.Drawing.Size(952, 258);
            this.dgvOrderDetails.TabIndex = 1;
            this.dgvOrderDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrderDetails_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "订单明细";
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFooter.Controls.Add(this.btnEditMode);
            this.panelFooter.Controls.Add(this.btnCancelEdit);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSaveOrder);
            this.panelFooter.Location = new System.Drawing.Point(12, 526);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(974, 50);
            this.panelFooter.TabIndex = 4;
            // 
            // btnEditMode
            // 
            this.btnEditMode.Location = new System.Drawing.Point(680, 10);
            this.btnEditMode.Name = "btnEditMode";
            this.btnEditMode.Size = new System.Drawing.Size(80, 25);
            this.btnEditMode.TabIndex = 3;
            this.btnEditMode.Text = "编辑";
            this.btnEditMode.UseVisualStyleBackColor = true;
            this.btnEditMode.Click += new System.EventHandler(this.btnEditMode_Click);
            // 
            // btnCancelEdit
            // 
            this.btnCancelEdit.Location = new System.Drawing.Point(780, 10);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(80, 25);
            this.btnCancelEdit.TabIndex = 2;
            this.btnCancelEdit.Text = "取消编辑";
            this.btnCancelEdit.UseVisualStyleBackColor = true;
            this.btnCancelEdit.Click += new System.EventHandler(this.btnCancelEdit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(880, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.Location = new System.Drawing.Point(780, 10);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(80, 25);
            this.btnSaveOrder.TabIndex = 0;
            this.btnSaveOrder.Text = "保存订单";
            this.btnSaveOrder.UseVisualStyleBackColor = true;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // SalesOrderCreateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 588);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelOrderDetails);
            this.Controls.Add(this.panelProductSelection);
            this.Controls.Add(this.panelOrderInfo);
            this.Controls.Add(this.panelHeader);
            this.Name = "SalesOrderCreateForm";
            this.Text = "新建销售订单";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelOrderInfo.ResumeLayout(false);
            this.panelOrderInfo.PerformLayout();
            this.panelProductSelection.ResumeLayout(false);
            this.panelProductSelection.PerformLayout();
            this.panelOrderDetails.ResumeLayout(false);
            this.panelOrderDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderDetails)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelOrderInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Panel panelProductSelection;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.TextBox txtAvailableStock;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUnitPrice;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelOrderDetails;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnEditMode;
        private System.Windows.Forms.Button btnCancelEdit;
    }
}