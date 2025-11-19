namespace CSproject.UI.Forms
{
    partial class SalesOrderDetailForm
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
            this.txtUpdateTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCreateTime = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOrderStatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpOrderDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelRemarks = new System.Windows.Forms.Panel();
            this.txtShipmentRemark = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtApprovalRemark = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelOrderDetails = new System.Windows.Forms.Panel();
            this.dgvOrderDetails = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnShip = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancelApproval = new System.Windows.Forms.Button();
            this.btnConfirmApproval = new System.Windows.Forms.Button();
            this.btnCancelReject = new System.Windows.Forms.Button();
            this.btnConfirmReject = new System.Windows.Forms.Button();
            this.btnCancelShip = new System.Windows.Forms.Button();
            this.btnConfirmShip = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelOrderInfo.SuspendLayout();
            this.panelRemarks.SuspendLayout();
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
            this.label1.Text = "销售订单详情";
            // 
            // panelOrderInfo
            // 
            this.panelOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderInfo.BackColor = System.Drawing.SystemColors.Control;
            this.panelOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrderInfo.Controls.Add(this.txtUpdateTime);
            this.panelOrderInfo.Controls.Add(this.label12);
            this.panelOrderInfo.Controls.Add(this.txtCreateTime);
            this.panelOrderInfo.Controls.Add(this.label11);
            this.panelOrderInfo.Controls.Add(this.txtTotalAmount);
            this.panelOrderInfo.Controls.Add(this.label10);
            this.panelOrderInfo.Controls.Add(this.txtOrderStatus);
            this.panelOrderInfo.Controls.Add(this.label9);
            this.panelOrderInfo.Controls.Add(this.txtCustomer);
            this.panelOrderInfo.Controls.Add(this.label8);
            this.panelOrderInfo.Controls.Add(this.dtpOrderDate);
            this.panelOrderInfo.Controls.Add(this.label7);
            this.panelOrderInfo.Controls.Add(this.txtOrderNumber);
            this.panelOrderInfo.Controls.Add(this.label6);
            this.panelOrderInfo.Controls.Add(this.txtOrderId);
            this.panelOrderInfo.Controls.Add(this.label5);
            this.panelOrderInfo.Location = new System.Drawing.Point(12, 68);
            this.panelOrderInfo.Name = "panelOrderInfo";
            this.panelOrderInfo.Size = new System.Drawing.Size(974, 120);
            this.panelOrderInfo.TabIndex = 1;
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Location = new System.Drawing.Point(510, 80);
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.Size = new System.Drawing.Size(180, 21);
            this.txtUpdateTime.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(450, 84);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 14;
            this.label12.Text = "更新时间";
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Location = new System.Drawing.Point(250, 80);
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.Size = new System.Drawing.Size(180, 21);
            this.txtCreateTime.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(190, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 12;
            this.label11.Text = "创建时间";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(80, 80);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 21);
            this.txtTotalAmount.TabIndex = 11;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "订单金额";
            // 
            // txtOrderStatus
            // 
            this.txtOrderStatus.Location = new System.Drawing.Point(800, 24);
            this.txtOrderStatus.Name = "txtOrderStatus";
            this.txtOrderStatus.Size = new System.Drawing.Size(150, 21);
            this.txtOrderStatus.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(740, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "订单状态";
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(510, 24);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(210, 21);
            this.txtCustomer.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(450, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 6;
            this.label8.Text = "客户";
            // 
            // dtpOrderDate
            // 
            this.dtpOrderDate.Location = new System.Drawing.Point(250, 24);
            this.dtpOrderDate.Name = "dtpOrderDate";
            this.dtpOrderDate.Size = new System.Drawing.Size(180, 21);
            this.dtpOrderDate.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(190, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "订单日期";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(80, 24);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(100, 21);
            this.txtOrderNumber.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "订单编号";
            // 
            // txtOrderId
            // 
            this.txtOrderId.Location = new System.Drawing.Point(1250, 24);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(100, 21);
            this.txtOrderId.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1190, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "订单ID";
            // 
            // panelRemarks
            // 
            this.panelRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRemarks.BackColor = System.Drawing.SystemColors.Control;
            this.panelRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRemarks.Controls.Add(this.txtShipmentRemark);
            this.panelRemarks.Controls.Add(this.label14);
            this.panelRemarks.Controls.Add(this.txtApprovalRemark);
            this.panelRemarks.Controls.Add(this.label13);
            this.panelRemarks.Controls.Add(this.txtRemark);
            this.panelRemarks.Controls.Add(this.label4);
            this.panelRemarks.Location = new System.Drawing.Point(12, 194);
            this.panelRemarks.Name = "panelRemarks";
            this.panelRemarks.Size = new System.Drawing.Size(974, 120);
            this.panelRemarks.TabIndex = 2;
            // 
            // txtShipmentRemark
            // 
            this.txtShipmentRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShipmentRemark.Location = new System.Drawing.Point(80, 80);
            this.txtShipmentRemark.Multiline = true;
            this.txtShipmentRemark.Name = "txtShipmentRemark";
            this.txtShipmentRemark.Size = new System.Drawing.Size(882, 30);
            this.txtShipmentRemark.TabIndex = 5;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 84);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 4;
            this.label14.Text = "发货备注";
            // 
            // txtApprovalRemark
            // 
            this.txtApprovalRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApprovalRemark.Location = new System.Drawing.Point(490, 24);
            this.txtApprovalRemark.Multiline = true;
            this.txtApprovalRemark.Name = "txtApprovalRemark";
            this.txtApprovalRemark.Size = new System.Drawing.Size(472, 30);
            this.txtApprovalRemark.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(410, 28);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "审核备注";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Location = new System.Drawing.Point(80, 24);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(310, 30);
            this.txtRemark.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "备注";
            // 
            // panelOrderDetails
            // 
            this.panelOrderDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOrderDetails.BackColor = System.Drawing.SystemColors.Control;
            this.panelOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrderDetails.Controls.Add(this.dgvOrderDetails);
            this.panelOrderDetails.Controls.Add(this.label3);
            this.panelOrderDetails.Location = new System.Drawing.Point(12, 320);
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
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "订单明细";
            // 
            // panelFooter
            // 
            this.panelFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelFooter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFooter.Controls.Add(this.btnPrintOrder);
            this.panelFooter.Controls.Add(this.btnEdit);
            this.panelFooter.Controls.Add(this.btnShip);
            this.panelFooter.Controls.Add(this.btnReject);
            this.panelFooter.Controls.Add(this.btnApprove);
            this.panelFooter.Controls.Add(this.btnClose);
            this.panelFooter.Controls.Add(this.btnCancelApproval);
            this.panelFooter.Controls.Add(this.btnConfirmApproval);
            this.panelFooter.Controls.Add(this.btnCancelReject);
            this.panelFooter.Controls.Add(this.btnConfirmReject);
            this.panelFooter.Controls.Add(this.btnCancelShip);
            this.panelFooter.Controls.Add(this.btnConfirmShip);
            this.panelFooter.Location = new System.Drawing.Point(12, 626);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(974, 50);
            this.panelFooter.TabIndex = 4;
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.Location = new System.Drawing.Point(380, 10);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(80, 25);
            this.btnPrintOrder.TabIndex = 11;
            this.btnPrintOrder.Text = "导出订单";
            this.btnPrintOrder.UseVisualStyleBackColor = true;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(280, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(80, 25);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "编辑";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnShip
            // 
            this.btnShip.Location = new System.Drawing.Point(180, 10);
            this.btnShip.Name = "btnShip";
            this.btnShip.Size = new System.Drawing.Size(80, 25);
            this.btnShip.TabIndex = 9;
            this.btnShip.Text = "发货";
            this.btnShip.UseVisualStyleBackColor = true;
            this.btnShip.Click += new System.EventHandler(this.btnShip_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(90, 10);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(80, 25);
            this.btnReject.TabIndex = 8;
            this.btnReject.Text = "驳回";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(10, 10);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(80, 25);
            this.btnApprove.TabIndex = 7;
            this.btnApprove.Text = "审核";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(880, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancelApproval
            // 
            this.btnCancelApproval.Location = new System.Drawing.Point(100, 10);
            this.btnCancelApproval.Name = "btnCancelApproval";
            this.btnCancelApproval.Size = new System.Drawing.Size(80, 25);
            this.btnCancelApproval.TabIndex = 6;
            this.btnCancelApproval.Text = "取消";
            this.btnCancelApproval.UseVisualStyleBackColor = true;
            this.btnCancelApproval.Visible = false;
            this.btnCancelApproval.Click += new System.EventHandler(this.btnCancelApproval_Click);
            // 
            // btnConfirmApproval
            // 
            this.btnConfirmApproval.Location = new System.Drawing.Point(10, 10);
            this.btnConfirmApproval.Name = "btnConfirmApproval";
            this.btnConfirmApproval.Size = new System.Drawing.Size(80, 25);
            this.btnConfirmApproval.TabIndex = 5;
            this.btnConfirmApproval.Text = "确认审核";
            this.btnConfirmApproval.UseVisualStyleBackColor = true;
            this.btnConfirmApproval.Visible = false;
            this.btnConfirmApproval.Click += new System.EventHandler(this.btnConfirmApproval_Click);
            // 
            // btnCancelReject
            // 
            this.btnCancelReject.Location = new System.Drawing.Point(100, 10);
            this.btnCancelReject.Name = "btnCancelReject";
            this.btnCancelReject.Size = new System.Drawing.Size(80, 25);
            this.btnCancelReject.TabIndex = 4;
            this.btnCancelReject.Text = "取消";
            this.btnCancelReject.UseVisualStyleBackColor = true;
            this.btnCancelReject.Visible = false;
            this.btnCancelReject.Click += new System.EventHandler(this.btnCancelReject_Click);
            // 
            // btnConfirmReject
            // 
            this.btnConfirmReject.Location = new System.Drawing.Point(10, 10);
            this.btnConfirmReject.Name = "btnConfirmReject";
            this.btnConfirmReject.Size = new System.Drawing.Size(80, 25);
            this.btnConfirmReject.TabIndex = 3;
            this.btnConfirmReject.Text = "确认驳回";
            this.btnConfirmReject.UseVisualStyleBackColor = true;
            this.btnConfirmReject.Visible = false;
            this.btnConfirmReject.Click += new System.EventHandler(this.btnConfirmReject_Click);
            // 
            // btnCancelShip
            // 
            this.btnCancelShip.Location = new System.Drawing.Point(100, 10);
            this.btnCancelShip.Name = "btnCancelShip";
            this.btnCancelShip.Size = new System.Drawing.Size(80, 25);
            this.btnCancelShip.TabIndex = 2;
            this.btnCancelShip.Text = "取消";
            this.btnCancelShip.UseVisualStyleBackColor = true;
            this.btnCancelShip.Visible = false;
            this.btnCancelShip.Click += new System.EventHandler(this.btnCancelShip_Click);
            // 
            // btnConfirmShip
            // 
            this.btnConfirmShip.Location = new System.Drawing.Point(10, 10);
            this.btnConfirmShip.Name = "btnConfirmShip";
            this.btnConfirmShip.Size = new System.Drawing.Size(80, 25);
            this.btnConfirmShip.TabIndex = 1;
            this.btnConfirmShip.Text = "确认发货";
            this.btnConfirmShip.UseVisualStyleBackColor = true;
            this.btnConfirmShip.Visible = false;
            this.btnConfirmShip.Click += new System.EventHandler(this.btnConfirmShip_Click);
            // 
            // SalesOrderDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 688);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelOrderDetails);
            this.Controls.Add(this.panelRemarks);
            this.Controls.Add(this.panelOrderInfo);
            this.Controls.Add(this.panelHeader);
            this.Name = "SalesOrderDetailForm";
            this.Text = "销售订单详情";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelOrderInfo.ResumeLayout(false);
            this.panelOrderInfo.PerformLayout();
            this.panelRemarks.ResumeLayout(false);
            this.panelRemarks.PerformLayout();
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.TextBox txtOrderStatus;
        private System.Windows.Forms.TextBox txtCustomer;
        private System.Windows.Forms.DateTimePicker dtpOrderDate;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCreateTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtUpdateTime;
        private System.Windows.Forms.Panel panelRemarks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApprovalRemark;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtShipmentRemark;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panelOrderDetails;
        private System.Windows.Forms.DataGridView dgvOrderDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnShip;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCancelApproval;
        private System.Windows.Forms.Button btnConfirmApproval;
        private System.Windows.Forms.Button btnCancelReject;
        private System.Windows.Forms.Button btnConfirmReject;
        private System.Windows.Forms.Button btnCancelShip;
        private System.Windows.Forms.Button btnConfirmShip;
        private System.Windows.Forms.Button btnPrintOrder;
    }
}