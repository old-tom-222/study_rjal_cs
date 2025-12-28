namespace CSproject.UI.Forms
{
    partial class IncomeForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtIncomeNo = new System.Windows.Forms.TextBox();
            this.dtpIncomeDate = new System.Windows.Forms.DateTimePicker();
            this.cmbSubject = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCreator = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpCreatedAt = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "收入号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "收入日:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "会计科目:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "金额:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "描述:";
            // 
            // txtIncomeNo
            // 
            this.txtIncomeNo.Location = new System.Drawing.Point(120, 17);
            this.txtIncomeNo.Name = "txtIncomeNo";
            this.txtIncomeNo.ReadOnly = true;
            this.txtIncomeNo.Size = new System.Drawing.Size(200, 22);
            this.txtIncomeNo.TabIndex = 5;
            // 
            // dtpIncomeDate
            // 
            this.dtpIncomeDate.CustomFormat = "yyyy-MM-dd";
            this.dtpIncomeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIncomeDate.Location = new System.Drawing.Point(120, 47);
            this.dtpIncomeDate.Name = "dtpIncomeDate";
            this.dtpIncomeDate.Size = new System.Drawing.Size(200, 22);
            this.dtpIncomeDate.TabIndex = 6;
            // 
            // cmbSubject
            // 
            this.cmbSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubject.FormattingEnabled = true;
            this.cmbSubject.Location = new System.Drawing.Point(120, 77);
            this.cmbSubject.Name = "cmbSubject";
            this.cmbSubject.Size = new System.Drawing.Size(300, 24);
            this.cmbSubject.TabIndex = 7;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(120, 107);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 22);
            this.txtAmount.TabIndex = 8;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 137);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(300, 80);
            this.txtDescription.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 270);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 220);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "创建人:";
            // 
            // txtCreator
            // 
            this.txtCreator.Location = new System.Drawing.Point(120, 217);
            this.txtCreator.Name = "txtCreator";
            this.txtCreator.ReadOnly = true;
            this.txtCreator.Size = new System.Drawing.Size(200, 22);
            this.txtCreator.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "创建时间:";
            // 
            // dtpCreatedAt
            // 
            this.dtpCreatedAt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpCreatedAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreatedAt.Location = new System.Drawing.Point(120, 247);
            this.dtpCreatedAt.Name = "dtpCreatedAt";
            this.dtpCreatedAt.Size = new System.Drawing.Size(200, 22);
            this.dtpCreatedAt.TabIndex = 15;
            this.dtpCreatedAt.Enabled = false;
            // 
            // IncomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 320);
            this.Controls.Add(this.dtpCreatedAt);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCreator);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmbSubject);
            this.Controls.Add(this.dtpIncomeDate);
            this.Controls.Add(this.txtIncomeNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "IncomeForm";
            this.Text = "添加收入记录";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtIncomeNo;
        private System.Windows.Forms.DateTimePicker dtpIncomeDate;
        private System.Windows.Forms.ComboBox cmbSubject;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCreator;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpCreatedAt;
    }
}