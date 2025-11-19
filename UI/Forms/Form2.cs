using System;
using System.Drawing;
using System.Windows.Forms;
using CSproject.UI.Forms;
using CSproject.Data.Repositories;


namespace CSproject.UI.Forms
{
    public partial class Form2 : Form
    {
        private PurchaseOrderListForm _purchaseOrderListForm;

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
            // 隐藏销售管理子导航面板
            panelSalesSubMenu.Visible = false;
            // 加载采购订单管理界面到内容面板
            this.panelContent.Controls.Clear();
            _purchaseOrderListForm = new PurchaseOrderListForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(_purchaseOrderListForm);
            _purchaseOrderListForm.Show();
        }

        public void LoadPurchaseOrders()
        {
            if (_purchaseOrderListForm != null)
            {
                _purchaseOrderListForm.LoadPurchaseOrders();
            }
        }
        }

        // 销售订单 - 订单列表
        private void menuSalesOrderList_Click(object sender, EventArgs e)
        {
            // 加载销售订单列表页面（主要页面）
            this.panelContent.Controls.Clear();
            var salesOrderListForm = new SalesOrderListForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(salesOrderListForm);
            salesOrderListForm.Show();
        }
        
        // 销售订单 - 新建订单
        private void menuSalesOrderCreate_Click(object sender, EventArgs e)
        {
            // 加载新建订单页面
            this.panelContent.Controls.Clear();
            var salesOrderCreateForm = new SalesOrderCreateForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(salesOrderCreateForm);
            salesOrderCreateForm.Show();
        }

        // 发货管理 - 待发货订单
        private void menuShipmentManagement_Click(object sender, EventArgs e)
        {
            // 加载发货管理页面
            this.panelContent.Controls.Clear();
            var shipmentManagementForm = new ShipmentManagementForm
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill
            };
            this.panelContent.Controls.Add(shipmentManagementForm);
            shipmentManagementForm.Show();
        }

        // 销售报表 - 销售统计
        private void menuSalesStatistics_Click(object sender, EventArgs e)
        {   
            try
            {   
                // 隐藏销售管理子导航面板
                panelSalesSubMenu.Visible = false;
                
                // 加载销售报表页面 - 销售统计
                this.panelContent.Controls.Clear();
                var salesReportForm = new SalesReportForm
                {   
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                this.panelContent.Controls.Add(salesReportForm);
                salesReportForm.Show();
            }
            catch (Exception ex)
            {   
                this.panelContent.Controls.Clear();
                var errorLbl = new Label
                {   
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular),
                    Text = $"加载销售统计报表失败: {ex.Message}"
                };
                this.panelContent.Controls.Add(errorLbl);
                // 可以选择记录详细错误日志
            }
        }
        
        // 销售报表 - 销售排名
        private void menuSalesRanking_Click(object sender, EventArgs e)
        {   
            try
            {   
                // 隐藏销售管理子导航面板
                panelSalesSubMenu.Visible = false;
                
                // 加载销售报表页面 - 销售排名
                this.panelContent.Controls.Clear();
                var salesReportForm = new SalesReportForm
                {   
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                this.panelContent.Controls.Add(salesReportForm);
                salesReportForm.Show();
            }
            catch (Exception ex)
            {   
                this.panelContent.Controls.Clear();
                var errorLbl = new Label
                {   
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular),
                    Text = $"加载销售排名报表失败: {ex.Message}"
                };
                this.panelContent.Controls.Add(errorLbl);
                // 可以选择记录详细错误日志
            }
        }

        private void BtnMenuInventoryClick(object sender, EventArgs e)
        {
            // 隐藏销售管理子导航面板
            panelSalesSubMenu.Visible = false;
            
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
            // 隐藏销售管理子导航面板
            panelSalesSubMenu.Visible = false;
            ShowPlaceholder("财务管理");
        }

        private void BtnMenuBasicClick(object sender, EventArgs e)
        {
            // 隐藏销售管理子导航面板
            panelSalesSubMenu.Visible = false;
            ShowPlaceholder("基础数据");
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            // 切换销售管理子导航面板的显示状态
            panelSalesSubMenu.Visible = !panelSalesSubMenu.Visible;
            
            // 如果显示了子导航面板，则加载默认页面
            if (panelSalesSubMenu.Visible)
            {
                // 加载销售订单列表页面（主要页面）
                menuSalesOrderList_Click(sender, e);
            }
        }

        private void BtnMenuReportsClick(object sender, EventArgs e)
        {
            // 隐藏销售管理子导航面板
            panelSalesSubMenu.Visible = false;
            
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
        
        // 销售管理子导航按钮点击事件
        private void btnSalesOrderList_Click(object sender, EventArgs e)
        {
            // 调用现有的菜单点击事件处理方法
            menuSalesOrderList_Click(sender, e);
        }
        
        private void btnSalesOrderCreate_Click(object sender, EventArgs e)
        {
            // 调用现有的菜单点击事件处理方法
            menuSalesOrderCreate_Click(sender, e);
        }
        
        private void btnShipmentManagement_Click(object sender, EventArgs e)
        {
            // 调用现有的菜单点击事件处理方法
            menuShipmentManagement_Click(sender, e);
        }
        
        private void btnSalesStatistics_Click(object sender, EventArgs e)
        {
            // 调用现有的菜单点击事件处理方法
            menuSalesStatistics_Click(sender, e);
        }
        
        private void btnSalesRanking_Click(object sender, EventArgs e)
        {
            // 调用现有的菜单点击事件处理方法
            menuSalesRanking_Click(sender, e);
        }
    }
}