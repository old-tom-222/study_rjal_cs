namespace CSproject.UI.Forms
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.财务信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.收入管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.支出管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会计科目管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基础数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部门管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.员工管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.银行账户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.财务信息ToolStripMenuItem, this.基础数据ToolStripMenuItem, this.退出ToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 财务信息ToolStripMenuItem
            // 
            this.财务信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.收入管理ToolStripMenuItem, this.支出管理ToolStripMenuItem, this.会计科目管理ToolStripMenuItem });
            this.财务信息ToolStripMenuItem.Name = "财务信息ToolStripMenuItem";
            this.财务信息ToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.财务信息ToolStripMenuItem.Text = "财务信息";
            // 
            // 收入管理ToolStripMenuItem
            // 
            this.收入管理ToolStripMenuItem.Name = "收入管理ToolStripMenuItem";
            this.收入管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.收入管理ToolStripMenuItem.Text = "收入管理";
            this.收入管理ToolStripMenuItem.Click += new System.EventHandler(this.收入管理ToolStripMenuItem_Click);
            // 
            // 支出管理ToolStripMenuItem
            // 
            this.支出管理ToolStripMenuItem.Name = "支出管理ToolStripMenuItem";
            this.支出管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.支出管理ToolStripMenuItem.Text = "支出管理";
            this.支出管理ToolStripMenuItem.Click += new System.EventHandler(this.支出管理ToolStripMenuItem_Click);
            // 
            // 会计科目管理ToolStripMenuItem
            // 
            this.会计科目管理ToolStripMenuItem.Name = "会计科目管理ToolStripMenuItem";
            this.会计科目管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.会计科目管理ToolStripMenuItem.Text = "会计科目管理";
            this.会计科目管理ToolStripMenuItem.Click += new System.EventHandler(this.会计科目管理ToolStripMenuItem_Click);
            // 
            // 基础数据ToolStripMenuItem
            // 
            this.基础数据ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.部门管理ToolStripMenuItem, this.员工管理ToolStripMenuItem, this.银行账户管理ToolStripMenuItem });
            this.基础数据ToolStripMenuItem.Name = "基础数据ToolStripMenuItem";
            this.基础数据ToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.基础数据ToolStripMenuItem.Text = "基础数据";
            // 
            // 部门管理ToolStripMenuItem
            // 
            this.部门管理ToolStripMenuItem.Name = "部门管理ToolStripMenuItem";
            this.部门管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.部门管理ToolStripMenuItem.Text = "部门管理";
            this.部门管理ToolStripMenuItem.Click += new System.EventHandler(this.部门管理ToolStripMenuItem_Click);
            // 
            // 员工管理ToolStripMenuItem
            // 
            this.员工管理ToolStripMenuItem.Name = "员工管理ToolStripMenuItem";
            this.员工管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.员工管理ToolStripMenuItem.Text = "员工管理";
            this.员工管理ToolStripMenuItem.Click += new System.EventHandler(this.员工管理ToolStripMenuItem_Click);
            // 
            // 银行账户管理ToolStripMenuItem
            // 
            this.银行账户管理ToolStripMenuItem.Name = "银行账户管理ToolStripMenuItem";
            this.银行账户管理ToolStripMenuItem.Size = new System.Drawing.Size(181, 26);
            this.银行账户管理ToolStripMenuItem.Text = "银行账户管理";
            this.银行账户管理ToolStripMenuItem.Click += new System.EventHandler(this.银行账户管理ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.toolStripStatusLabel1 });
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(900, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(67, 20);
            this.toolStripStatusLabel1.Text = "欢迎使用";
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 28);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(900, 494);
            this.panelMain.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 548);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "财务管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 财务信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 收入管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 支出管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 会计科目管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基础数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部门管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 员工管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 银行账户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelMain;
    }
}
