using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CSproject.Data.Repositories;

namespace CSproject.UI.Forms
{
    public partial class PurchaseOrderListForm : Form
    {
        private readonly PurchaseOrderRepository _purchaseOrderRepo;
        private readonly int _currentUserId;
        private readonly string _currentUserName;

        public PurchaseOrderListForm()
        {
            InitializeComponent();
            _purchaseOrderRepo = new PurchaseOrderRepository();
            _currentUserId = 0;
            _currentUserName = "";
        }
        
        public PurchaseOrderListForm(int currentUserId, string currentUserName)
        {
            InitializeComponent();
            _purchaseOrderRepo = new PurchaseOrderRepository();
            _currentUserId = currentUserId;
            _currentUserName = currentUserName;
        }

        public void LoadPurchaseOrders()
        {
            try
            {
                // 获取采购订单列表
                DataTable dt = _purchaseOrderRepo.GetAllPurchaseOrders();
                
                // 设置DataGridView的数据源
                dgvOrders.DataSource = dt;
                
                // 设置列标题和可见性
                SetupDataGridViewColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载采购订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViewColumns()
        {
            if (dgvOrders.Columns.Count == 0)
                return;

            // 设置列标题
            if (dgvOrders.Columns.Contains("id"))
                dgvOrders.Columns["id"].HeaderText = "订单ID";
            if (dgvOrders.Columns.Contains("order_no"))
                dgvOrders.Columns["order_no"].HeaderText = "订单号";
            if (dgvOrders.Columns.Contains("supplier_name"))
                dgvOrders.Columns["supplier_name"].HeaderText = "供应商";
            if (dgvOrders.Columns.Contains("warehouse_name"))
                dgvOrders.Columns["warehouse_name"].HeaderText = "仓库";
            if (dgvOrders.Columns.Contains("total_amount"))
                dgvOrders.Columns["total_amount"].HeaderText = "订单总金额";
            if (dgvOrders.Columns.Contains("status"))
                dgvOrders.Columns["status"].HeaderText = "状态";
            if (dgvOrders.Columns.Contains("purchaser_name"))
                dgvOrders.Columns["purchaser_name"].HeaderText = "采购员";
            if (dgvOrders.Columns.Contains("created_at"))
                dgvOrders.Columns["created_at"].HeaderText = "创建时间";
            if (dgvOrders.Columns.Contains("updated_at"))
                dgvOrders.Columns["updated_at"].HeaderText = "更新时间";

            // 设置列宽
            if (dgvOrders.Columns.Contains("id"))
                dgvOrders.Columns["id"].Width = 60;
            if (dgvOrders.Columns.Contains("order_no"))
                dgvOrders.Columns["order_no"].Width = 120;
            if (dgvOrders.Columns.Contains("supplier_name"))
                dgvOrders.Columns["supplier_name"].Width = 150;
            if (dgvOrders.Columns.Contains("warehouse_name"))
                dgvOrders.Columns["warehouse_name"].Width = 120;
            if (dgvOrders.Columns.Contains("total_amount"))
            {
                dgvOrders.Columns["total_amount"].Width = 120;
                dgvOrders.Columns["total_amount"].DefaultCellStyle.Format = "C2";
                dgvOrders.Columns["total_amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvOrders.Columns.Contains("status"))
                dgvOrders.Columns["status"].Width = 100;
            if (dgvOrders.Columns.Contains("purchaser_name"))
                dgvOrders.Columns["purchaser_name"].Width = 100;
            if (dgvOrders.Columns.Contains("created_at"))
                dgvOrders.Columns["created_at"].Width = 150;
            if (dgvOrders.Columns.Contains("updated_at"))
                dgvOrders.Columns["updated_at"].Width = 150;

            // 隐藏不需要显示的列
            if (dgvOrders.Columns.Contains("supplier_id"))
                dgvOrders.Columns["supplier_id"].Visible = false;
            if (dgvOrders.Columns.Contains("warehouse_id"))
                dgvOrders.Columns["warehouse_id"].Visible = false;
            if (dgvOrders.Columns.Contains("purchaser_id"))
                dgvOrders.Columns["purchaser_id"].Visible = false;
            if (dgvOrders.Columns.Contains("created_by"))
                dgvOrders.Columns["created_by"].Visible = false;
            if (dgvOrders.Columns.Contains("updated_by"))
                dgvOrders.Columns["updated_by"].Visible = false;
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // 获取父级Form2
                Form2 parentForm = (Form2)this.ParentForm;
                
                // 创建新订单，传递当前用户信息
                OrderCreationForm orderForm = new OrderCreationForm(_purchaseOrderRepo, parentForm, _currentUserId, _currentUserName);
                orderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.SelectedRows.Count > 0)
                {
                    // 获取选中的订单ID
                    int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["id"].Value);
                    
                    // 获取父级Form2
                    Form2 parentForm = (Form2)this.ParentForm;
                    
                    // 编辑订单
                    OrderCreationForm orderForm = new OrderCreationForm(_purchaseOrderRepo, parentForm, orderId);
                    orderForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择要编辑的订单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("编辑订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.SelectedRows.Count > 0)
                {
                    int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["id"].Value);
                    string orderNo = dgvOrders.SelectedRows[0].Cells["order_no"].Value.ToString();

                    if (MessageBox.Show(string.Format("确定要删除订单 {0} 吗？", orderNo), "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // 删除订单
                        _purchaseOrderRepo.DeletePurchaseOrder(orderId);
                        
                        // 刷新订单列表
                        LoadPurchaseOrders();
                        
                        MessageBox.Show("订单删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("请选择要删除的订单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrderDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvOrders.SelectedRows.Count > 0)
                {
                    // 获取选中的订单ID
                    int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["id"].Value);
                    
                    // 获取父级Form2
                    Form2 parentForm = (Form2)this.ParentForm;
                    
                    // 打开订单明细页面
                    OrderDetailsForm orderDetailsForm = new OrderDetailsForm(_purchaseOrderRepo, parentForm, orderId);
                    orderDetailsForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择要查看的订单！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPurchaseOrders();
        }

        private void PurchaseOrderListForm_Load(object sender, EventArgs e)
        {
            // 加载采购订单列表
            LoadPurchaseOrders();
        }

        #region Windows 窗体设计器生成的代码
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(15, 80);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersWidth = 51;
            this.dgvOrders.RowTemplate.Height = 27;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(650, 450);
            this.dgvOrders.TabIndex = 0;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.btnCreateOrder.BackColor = System.Drawing.Color.LightGreen;
            this.btnCreateOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateOrder.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnCreateOrder.Location = new System.Drawing.Point(15, 35);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(100, 35);
            this.btnCreateOrder.TabIndex = 1;
            this.btnCreateOrder.Text = "创建订单";
            this.btnCreateOrder.UseVisualStyleBackColor = false;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // btnEditOrder
            // 
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.btnEditOrder.BackColor = System.Drawing.Color.LightBlue;
            this.btnEditOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnEditOrder.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnEditOrder.Location = new System.Drawing.Point(130, 35);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(100, 35);
            this.btnEditOrder.TabIndex = 2;
            this.btnEditOrder.Text = "编辑订单";
            this.btnEditOrder.UseVisualStyleBackColor = false;
            this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnDeleteOrder.BackColor = System.Drawing.Color.LightSalmon;
            this.btnDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteOrder.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteOrder.Location = new System.Drawing.Point(245, 35);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteOrder.TabIndex = 3;
            this.btnDeleteOrder.Text = "删除订单";
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // btnOrderDetails
            // 
            this.btnOrderDetails = new System.Windows.Forms.Button();
            this.btnOrderDetails.BackColor = System.Drawing.Color.LightYellow;
            this.btnOrderDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrderDetails.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrderDetails.ForeColor = System.Drawing.Color.DarkOrange;
            this.btnOrderDetails.Location = new System.Drawing.Point(360, 35);
            this.btnOrderDetails.Name = "btnOrderDetails";
            this.btnOrderDetails.Size = new System.Drawing.Size(100, 35);
            this.btnOrderDetails.TabIndex = 5;
            this.btnOrderDetails.Text = "订单明细";
            this.btnOrderDetails.UseVisualStyleBackColor = false;
            this.btnOrderDetails.Click += new System.EventHandler(this.btnOrderDetails_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRefresh.BackColor = System.Drawing.Color.LightGray;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(565, 35);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(680, 25);
            this.panelTop.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(15, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "采购订单管理";
            // 
            // PurchaseOrderListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 550);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnOrderDetails);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnEditOrder);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.dgvOrders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchaseOrderListForm";
            this.Text = "采购订单管理";
            this.Load += new System.EventHandler(this.PurchaseOrderListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Button btnEditOrder;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOrderDetails;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label label1;
        #endregion
    }
}