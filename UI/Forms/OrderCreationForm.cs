using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CSproject.Data.Repositories;

namespace CSproject.UI.Forms
{
    public partial class OrderCreationForm : Form
    {
        private readonly PurchaseOrderRepository _purchaseOrderRepo;
        private readonly Form2 _parentForm;
        private int _orderId; // 存储订单ID，0表示新建订单
        private bool _isReadOnly; // 是否为只读模式
        private int _currentUserId; // 当前登录用户ID
        private string _currentUserName; // 当前登录用户名

        public OrderCreationForm(PurchaseOrderRepository purchaseOrderRepo, Form2 parentForm, int orderId = 0, bool isReadOnly = false)
        {
            InitializeComponent();
            _purchaseOrderRepo = purchaseOrderRepo;
            _parentForm = parentForm;
            _orderId = orderId;
            _isReadOnly = isReadOnly;
            _currentUserId = 0;
            _currentUserName = string.Empty;
        }
        
        // 带用户信息的构造函数
        public OrderCreationForm(PurchaseOrderRepository purchaseOrderRepo, Form2 parentForm, int currentUserId, string currentUserName, int orderId = 0, bool isReadOnly = false)
        {
            InitializeComponent();
            _purchaseOrderRepo = purchaseOrderRepo;
            _parentForm = parentForm;
            _orderId = orderId;
            _isReadOnly = isReadOnly;
            _currentUserId = currentUserId;
            _currentUserName = currentUserName;
        }

        private void OrderCreationForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 加载供应商、仓库和采购员数据
                LoadSuppliers();
                LoadWarehouses();
                LoadPurchasers();
                
                // 设置默认值
                cboStatus.SelectedItem = "草稿";
                
                if (_orderId > 0)
                {
                    // 编辑模式或查看模式：加载订单详情
                    LoadOrderDetails();
                    
                    if (_isReadOnly)
                    {
                        this.Text = "订单明细";
                        SetReadOnlyMode(true);
                    }
                    else
                    {
                        this.Text = "编辑采购订单";
                    }
                }
                else
                {
                    // 新建模式：生成订单号
                    txtOrderNo.Text = _purchaseOrderRepo.GenerateOrderNo();
                    this.Text = "创建采购订单";
                    
                    // 自动设置采购人为当前登录用户
                    if (!string.IsNullOrEmpty(_currentUserName))
                    {
                        SetPurchaserSelectedItem(_currentUserName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载表单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadPurchasers()
        {
            try
            {
                var users = _purchaseOrderRepo.GetAllUsers();
                cboPurchaser.DataSource = users;
                cboPurchaser.DisplayMember = "Name";
                cboPurchaser.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载采购员失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadOrderDetails()
        {
            try
            {
                // 获取订单详情
                var orderTable = _purchaseOrderRepo.GetPurchaseOrderById(_orderId);
                if (orderTable != null && orderTable.Rows.Count > 0)
                {
                    DataRow orderRow = orderTable.Rows[0];
                    txtOrderNo.Text = orderRow["order_no"].ToString();
                    cboSupplier.Text = orderRow["supplier_name"].ToString();
                    cboWarehouse.Text = orderRow["warehouse_name"].ToString();
                    
                    // 设置状态下拉框选中项
                    string status = orderRow["status"].ToString();
                    cboStatus.SelectedItem = status;
                    
                    // 设置采购员
                    string purchaserName = orderRow["purchaser_name"].ToString();
                    SetPurchaserSelectedItem(purchaserName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载订单详情失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // 设置只读模式
        private void SetReadOnlyMode(bool isReadOnly)
        {
            if (isReadOnly)
            {
                // 设置所有下拉框为只读
                cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                cboSupplier.Enabled = false;
                cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
                cboWarehouse.Enabled = false;
                cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cboStatus.Enabled = false;
                cboPurchaser.DropDownStyle = ComboBoxStyle.DropDownList;
                cboPurchaser.Enabled = false;
                
                // 隐藏保存按钮
                btnSaveOrder.Visible = false;
                
                // 调整取消按钮位置
                btnCancel.Location = new Point(160, btnCancel.Location.Y);
            }
            else
            {
                // 恢复下拉框的原始样式
                cboSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
                cboSupplier.Enabled = true;
                cboWarehouse.DropDownStyle = ComboBoxStyle.DropDownList;
                cboWarehouse.Enabled = true;
                cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                cboStatus.Enabled = true;
                cboPurchaser.DropDownStyle = ComboBoxStyle.DropDown;
                cboPurchaser.Enabled = true;
                
                // 显示保存按钮
                btnSaveOrder.Visible = true;
                
                // 恢复取消按钮位置
                btnCancel.Location = new Point(220, btnCancel.Location.Y);
            }
        }
        
        // 辅助方法：通过名称查找用户并设置选中项
        private void SetPurchaserSelectedItem(string purchaserName)
        {
            if (string.IsNullOrEmpty(purchaserName))
                return;
                
            try
            {
                // 先尝试通过Text匹配
                cboPurchaser.Text = purchaserName;
                
                // 再尝试通过遍历查找
                for (int i = 0; i < cboPurchaser.Items.Count; i++)
                {
                    // 因为Items是DataRowView类型，所以需要通过Row属性访问数据
                    var rowView = cboPurchaser.Items[i] as DataRowView;
                    if (rowView != null && rowView.Row != null)
                    {
                        // 确保DisplayMember已设置
                        if (!string.IsNullOrEmpty(cboPurchaser.DisplayMember))
                        {
                            // 根据DisplayMember的设置，获取对应的列值
                            object itemNameObj = rowView.Row[cboPurchaser.DisplayMember];
                            if (itemNameObj != DBNull.Value)
                            {
                                string itemName = itemNameObj.ToString();
                                if (itemName.Equals(purchaserName, StringComparison.OrdinalIgnoreCase))
                                {
                                    cboPurchaser.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录错误但不中断程序
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                var suppliers = _purchaseOrderRepo.GetAllSuppliers();
                cboSupplier.DataSource = suppliers;
                cboSupplier.DisplayMember = "name";
                cboSupplier.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载供应商失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWarehouses()
        {
            try
            {
                var warehouses = _purchaseOrderRepo.GetAllWarehouses();
                cboWarehouse.DataSource = warehouses;
                cboWarehouse.DisplayMember = "name";
                cboWarehouse.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载仓库失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            try
            {
                string orderNo = txtOrderNo.Text.Trim();
                
                // 验证必填字段
                if (string.IsNullOrEmpty(orderNo))
                {
                    MessageBox.Show("订单号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOrderNo.Focus();
                    return;
                }

                // 获取供应商和仓库信息
                int supplierId = cboSupplier.SelectedValue != null ? Convert.ToInt32(cboSupplier.SelectedValue) : -1;
                string supplierName = cboSupplier.Text.Trim();
                int warehouseId = cboWarehouse.SelectedValue != null ? Convert.ToInt32(cboWarehouse.SelectedValue) : -1;
                string warehouseName = cboWarehouse.Text.Trim();

                // 检查供应商名称是否为空
                if (string.IsNullOrEmpty(supplierName))
                {
                    MessageBox.Show("请选择供应商！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboSupplier.Focus();
                    return;
                }

                // 检查仓库名称是否为空
                if (string.IsNullOrEmpty(warehouseName))
                {
                    MessageBox.Show("请选择仓库！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboWarehouse.Focus();
                    return;
                }
                
                // 检查采购员是否为空
                if (cboPurchaser.SelectedItem == null && string.IsNullOrEmpty(cboPurchaser.Text.Trim()))
                {
                    MessageBox.Show("请选择或输入采购员！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 获取采购员信息
                int purchaserId = cboPurchaser.SelectedValue != null ? Convert.ToInt32(cboPurchaser.SelectedValue) : -1;
                string purchaserName = cboPurchaser.Text.Trim();
                
                // 如果没有选择现有用户但输入了名称，则获取或创建用户
                if (purchaserId == -1 && !string.IsNullOrEmpty(purchaserName))
                {
                    purchaserId = _purchaseOrderRepo.GetOrCreateUserIdByName(purchaserName);
                }

                if (_orderId > 0)
                {
                    // 获取状态和采购员信息
                    string status = cboStatus.SelectedItem != null ? cboStatus.SelectedItem.ToString() : "草稿";
                    
                    // 更新订单，使用0作为updatedById的默认值
                    _purchaseOrderRepo.UpdatePurchaseOrder(_orderId, orderNo, supplierId, supplierName, warehouseId, warehouseName, status, purchaserId, 0);
                    MessageBox.Show("订单更新成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        // 创建订单，使用purchaserId替代purchaserName
                        _purchaseOrderRepo.CreatePurchaseOrder(orderNo, supplierId, supplierName, warehouseId, warehouseName, purchaserId);
                        MessageBox.Show("订单创建成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        // 添加调试信息
                        MessageBox.Show(string.Format("创建订单失败：{0}", ex.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        throw;
                    }
                }
                
                // 关闭窗口并刷新父窗口
                _parentForm.LoadPurchaseOrders();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
            this.groupControl1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboPurchaser = new System.Windows.Forms.ComboBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboWarehouse = new System.Windows.Forms.ComboBox();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1 - 优化布局
            // 
            this.groupControl1.Controls.Add(this.cboPurchaser);
            this.groupControl1.Controls.Add(this.cboStatus);
            this.groupControl1.Controls.Add(this.cboWarehouse);
            this.groupControl1.Controls.Add(this.cboSupplier);
            this.groupControl1.Controls.Add(this.txtOrderNo);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupControl1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupControl1.Location = new System.Drawing.Point(15, 15);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(380, 270);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "订单信息";
            this.groupControl1.BackColor = System.Drawing.Color.AliceBlue;
            // 
            // label6 - 优化样式
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(40, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "采购员：";

            // 
            // label4 - 优化样式
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(40, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "状态：";
            // 
            // label3 - 优化样式
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(40, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "仓库：";
            // 
            // label2 - 优化样式
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(40, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "供应商：";
            // 
            // label1设置
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(40, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单号：";
            // 
            // cboPurchaser - 优化样式，允许用户输入
            // 
            this.cboPurchaser.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cboPurchaser.FormattingEnabled = true;
            this.cboPurchaser.Location = new System.Drawing.Point(115, 182);
            this.cboPurchaser.Name = "cboPurchaser";
            this.cboPurchaser.Size = new System.Drawing.Size(230, 25);
            this.cboPurchaser.TabIndex = 6;
            this.cboPurchaser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;

            // 
            // cboStatus - 优化样式
            // 
            this.cboStatus.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] { "草稿", "已提交", "待审核", "已审核", "已完成", "已取消" });
            this.cboStatus.Location = new System.Drawing.Point(115, 142);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(230, 25);
            this.cboStatus.TabIndex = 9;
            // 
            // cboWarehouse - 优化样式，允许用户输入
            // 
            this.cboWarehouse.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cboWarehouse.FormattingEnabled = true;
            this.cboWarehouse.Location = new System.Drawing.Point(115, 102);
            this.cboWarehouse.Name = "cboWarehouse";
            this.cboWarehouse.Size = new System.Drawing.Size(230, 25);
            this.cboWarehouse.TabIndex = 5;
            this.cboWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            // 
            // cboSupplier - 优化样式，允许用户输入
            // 
            this.cboSupplier.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.Location = new System.Drawing.Point(115, 62);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(230, 25);
            this.cboSupplier.TabIndex = 4;
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            // 
            // txtOrderNo - 优化样式，允许用户输入
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtOrderNo.Location = new System.Drawing.Point(115, 22);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.ReadOnly = false;
            this.txtOrderNo.Size = new System.Drawing.Size(230, 23);
            this.txtOrderNo.TabIndex = 3;
            this.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnSaveOrder - 美化样式
            // 
            this.btnSaveOrder.BackColor = System.Drawing.Color.LightGreen;
            this.btnSaveOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveOrder.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnSaveOrder.Location = new System.Drawing.Point(100, 300);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(100, 35);
            this.btnSaveOrder.TabIndex = 1;
            this.btnSaveOrder.Text = "保存";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // btnCancel - 美化样式
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightSalmon;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(220, 300);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OrderCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 360);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderCreationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "创建采购订单";
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.OrderCreationForm_Load);
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupControl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboPurchaser;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboWarehouse;
        private System.Windows.Forms.ComboBox cboSupplier;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnCancel;
        #endregion
    }
}