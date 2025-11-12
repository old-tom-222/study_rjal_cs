using System;
using System.Drawing;
using System.Windows.Forms;
using CSproject.UI.Forms;


namespace CSproject.UI.Forms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void BtnLogoutClick(object sender, EventArgs e)
        {
            // 退出登录：关闭当前界面并回到登录界面
            this.Hide();
            CSproject.Form1 login = new CSproject.Form1();
            login.ShowDialog();
            this.Close();
        }

        private void ShowPlaceholder(string title)
        {
            this.panelContent.Controls.Clear();
            var lbl = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular),
                Text = title + "（占位页面）",
                Location = new Point(20, 20)
            };
            this.panelContent.Controls.Add(lbl);
        }

        private void BtnMenuPurchaseClick(object sender, EventArgs e)
        {
            ShowPlaceholder("采购管理");
        }

        private void BtnMenuSalesClick(object sender, EventArgs e)
        {
            ShowPlaceholder("销售管理");
        }

        private void BtnMenuInventoryClick(object sender, EventArgs e)
        {
            // 加载库存管理界面到内容面板
            this.panelContent.Controls.Clear();
            var invForm = new InventoryForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(invForm);
            invForm.Show();
        }

        private void BtnMenuFinanceClick(object sender, EventArgs e)
        {
            ShowPlaceholder("财务管理");
        }

        private void BtnMenuBasicClick(object sender, EventArgs e)
        {
            ShowPlaceholder("基础数据");
        }

        private void BtnMenuReportsClick(object sender, EventArgs e)
        {
            // 加载报表分析界面到内容面板
            this.panelContent.Controls.Clear();
            var reportsForm = new ReportsMainForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(reportsForm);
            reportsForm.Show();
        }
    }
}