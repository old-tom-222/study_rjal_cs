namespace CSproject.UI.Forms
{
    partial class ExpenseForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtExpenseNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpExpenseDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBankAccount = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "支出编号";
            // 
            // txtExpenseNo
            // 
            this.txtExpenseNo.Location = new System.Drawing.Point(117, 22);
            this.txtExpenseNo.Name = "txtExpenseNo";
            this.txtExpenseNo.Size = new System.Drawing.Size(200, 25);
            this.txtExpenseNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "支出日期";
            // 
            // dtpExpenseDate
            // 
            this.dtpExpenseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpenseDate.Location = new System.Drawing.Point(117, 59);
            this.dtpExpenseDate.Name = "dtpExpenseDate";
            this.dtpExpenseDate.Size = new System.Drawing.Size(200, 25);
            this.dtpExpenseDate.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "会计科目";
            // 
            // cmbSubject
            // 
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(117, 96);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(300, 23);
            this.cmbSubject.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "金额";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(117, 133);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 25);
            this.txtAmount.TabIndex = 7;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "描述";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(117, 170);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(300, 70);
            this.txtDescription.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "参考号";
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(117, 250);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(300, 25);
            this.txtReference.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 290);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "付款方式";
            // 
            // cmbPaymentMethod
            // 
            this.cmbPaymentMethod.FormattingEnabled = true;
            this.cmbPaymentMethod.Location = new System.Drawing.Point(117, 287);
            this.cmbPaymentMethod.Name = "cmbPaymentMethod";
            this.cmbPaymentMethod.Size = new System.Drawing.Size(300, 23);
            this.cmbPaymentMethod.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "银行账户";
            // 
            // cmbBankAccount
            // 
            this.cmbBankAccount.FormattingEnabled = true;
            this.cmbBankAccount.Location = new System.Drawing.Point(117, 324);
            this.cmbBankAccount.Name = "cmbBankAccount";
            this.cmbBankAccount.Size = new System.Drawing.Size(300, 23);
            this.cmbBankAccount.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(257, 374);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(342, 374);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ExpenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 426);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbBankAccount);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbPaymentMethod);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpExpenseDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtExpenseNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpenseForm";
            this.Text = "支出表单";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtExpenseNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpExpenseDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbPaymentMethod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBankAccount;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}