namespace CSproject
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.panelLogin = new System.Windows.Forms.Panel();
            this.labelLoginAccount = new System.Windows.Forms.Label();
            this.labelLoginPassword = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnGoRegister = new System.Windows.Forms.Button();
            this.panelRegister = new System.Windows.Forms.Panel();
            this.labelRegAccount = new System.Windows.Forms.Label();
            this.labelRegPassword = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelRole = new System.Windows.Forms.Label();
            this.txtRegAccount = new System.Windows.Forms.TextBox();
            this.txtRegPassword = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.btnRegisterAndLogin = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.panelRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.btnGoRegister);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.txtPassword);
            this.panelLogin.Controls.Add(this.txtAccount);
            this.panelLogin.Controls.Add(this.labelLoginPassword);
            this.panelLogin.Controls.Add(this.labelLoginAccount);
            this.panelLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLogin.Location = new System.Drawing.Point(0, 0);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(400, 300);
            this.panelLogin.TabIndex = 0;
            // 
            // labelLoginAccount
            // 
            this.labelLoginAccount.AutoSize = true;
            this.labelLoginAccount.Location = new System.Drawing.Point(60, 70);
            this.labelLoginAccount.Name = "labelLoginAccount";
            this.labelLoginAccount.Size = new System.Drawing.Size(41, 12);
            this.labelLoginAccount.TabIndex = 0;
            this.labelLoginAccount.Text = "账号：";
            // 
            // labelLoginPassword
            // 
            this.labelLoginPassword.AutoSize = true;
            this.labelLoginPassword.Location = new System.Drawing.Point(60, 110);
            this.labelLoginPassword.Name = "labelLoginPassword";
            this.labelLoginPassword.Size = new System.Drawing.Size(41, 12);
            this.labelLoginPassword.TabIndex = 1;
            this.labelLoginPassword.Text = "密码：";
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(120, 67);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(180, 21);
            this.txtAccount.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 107);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(180, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(120, 160);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 30);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.BtnLoginClick);
            // 
            // btnGoRegister
            // 
            this.btnGoRegister.Location = new System.Drawing.Point(225, 160);
            this.btnGoRegister.Name = "btnGoRegister";
            this.btnGoRegister.Size = new System.Drawing.Size(75, 30);
            this.btnGoRegister.TabIndex = 5;
            this.btnGoRegister.Text = "注册";
            this.btnGoRegister.UseVisualStyleBackColor = true;
            this.btnGoRegister.CausesValidation = false;
            this.btnGoRegister.Click += new System.EventHandler(this.BtnGoRegisterClick);
            // 
            // panelRegister
            // 
            this.panelRegister.Controls.Add(this.btnBack);
            this.panelRegister.Controls.Add(this.btnRegisterAndLogin);
            this.panelRegister.Controls.Add(this.cmbRole);
            this.panelRegister.Controls.Add(this.txtName);
            this.panelRegister.Controls.Add(this.txtRegPassword);
            this.panelRegister.Controls.Add(this.txtRegAccount);
            this.panelRegister.Controls.Add(this.labelRole);
            this.panelRegister.Controls.Add(this.labelName);
            this.panelRegister.Controls.Add(this.labelRegPassword);
            this.panelRegister.Controls.Add(this.labelRegAccount);
            this.panelRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegister.Location = new System.Drawing.Point(0, 0);
            this.panelRegister.Name = "panelRegister";
            this.panelRegister.Size = new System.Drawing.Size(400, 300);
            this.panelRegister.TabIndex = 1;
            this.panelRegister.Visible = false;
            // 
            // labelRegAccount
            // 
            this.labelRegAccount.AutoSize = true;
            this.labelRegAccount.Location = new System.Drawing.Point(60, 40);
            this.labelRegAccount.Name = "labelRegAccount";
            this.labelRegAccount.Size = new System.Drawing.Size(41, 12);
            this.labelRegAccount.TabIndex = 0;
            this.labelRegAccount.Text = "账号：";
            // 
            // labelRegPassword
            // 
            this.labelRegPassword.AutoSize = true;
            this.labelRegPassword.Location = new System.Drawing.Point(60, 80);
            this.labelRegPassword.Name = "labelRegPassword";
            this.labelRegPassword.Size = new System.Drawing.Size(41, 12);
            this.labelRegPassword.TabIndex = 1;
            this.labelRegPassword.Text = "密码：";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(60, 120);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(41, 12);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "姓名：";
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.Location = new System.Drawing.Point(60, 160);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(41, 12);
            this.labelRole.TabIndex = 3;
            this.labelRole.Text = "角色：";
            // 
            // txtRegAccount
            // 
            this.txtRegAccount.Location = new System.Drawing.Point(120, 37);
            this.txtRegAccount.Name = "txtRegAccount";
            this.txtRegAccount.Size = new System.Drawing.Size(180, 21);
            this.txtRegAccount.TabIndex = 4;
            // 
            // txtRegPassword
            // 
            this.txtRegPassword.Location = new System.Drawing.Point(120, 77);
            this.txtRegPassword.Name = "txtRegPassword";
            this.txtRegPassword.PasswordChar = '*';
            this.txtRegPassword.Size = new System.Drawing.Size(180, 21);
            this.txtRegPassword.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 117);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(180, 21);
            this.txtName.TabIndex = 6;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "staff",
            "admin"});
            this.cmbRole.Location = new System.Drawing.Point(120, 157);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(180, 20);
            this.cmbRole.TabIndex = 7;
            // 
            // btnRegisterAndLogin
            // 
            this.btnRegisterAndLogin.Location = new System.Drawing.Point(120, 210);
            this.btnRegisterAndLogin.Name = "btnRegisterAndLogin";
            this.btnRegisterAndLogin.Size = new System.Drawing.Size(100, 30);
            this.btnRegisterAndLogin.TabIndex = 8;
            this.btnRegisterAndLogin.Text = "注册并登录";
            this.btnRegisterAndLogin.UseVisualStyleBackColor = true;
            this.btnRegisterAndLogin.Click += new System.EventHandler(this.BtnRegisterAndLoginClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(240, 210);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(60, 30);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "返回";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBackClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.panelRegister);
            this.Controls.Add(this.panelLogin);
            this.AcceptButton = this.btnLogin;
            this.Name = "登录系统";
            this.Text = "登录系统";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelRegister.ResumeLayout(false);
            this.panelRegister.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label labelLoginAccount;
        private System.Windows.Forms.Label labelLoginPassword;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnGoRegister;
        private System.Windows.Forms.Panel panelRegister;
        private System.Windows.Forms.Label labelRegAccount;
        private System.Windows.Forms.Label labelRegPassword;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.TextBox txtRegAccount;
        private System.Windows.Forms.TextBox txtRegPassword;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Button btnRegisterAndLogin;
        private System.Windows.Forms.Button btnBack;
    }
}

