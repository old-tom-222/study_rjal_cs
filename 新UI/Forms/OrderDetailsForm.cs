using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSproject.Data.Repositories;

namespace CSproject.UI.Forms
{
    public partial class OrderDetailsForm : Form
    {
        private PurchaseOrderRepository _purchaseOrderRepo;
        private int _orderId;
        private Form2 _parentForm;
        private DataTable _orderItemsTable;

        public OrderDetailsForm(PurchaseOrderRepository purchaseOrderRepo, Form2 parentForm, int orderId)
        {
            InitializeComponent();
            _purchaseOrderRepo = purchaseOrderRepo;
            _parentForm = parentForm;
            _orderId = orderId;
            InitializeDataGridView();
        }

        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            LoadOrderDetails();
            LoadOrderItems();
        }

        private void InitializeDataGridView()
        {
            // 设置DataGridView样式
            dgvOrderItems.BackgroundColor = Color.White;
            dgvOrderItems.GridColor = Color.LightGray;
            dgvOrderItems.BorderStyle = BorderStyle.Fixed3D;
            dgvOrderItems.AllowUserToAddRows = true;
            dgvOrderItems.AllowUserToDeleteRows = true;
            dgvOrderItems.AutoGenerateColumns = false;

            // 添加列
            DataGridViewTextBoxColumn colProductName = new DataGridViewTextBoxColumn();
            colProductName.Name = "product_name";
            colProductName.HeaderText = "产品名称";
            colProductName.DataPropertyName = "product_name";
            colProductName.Width = 150;
            colProductName.ReadOnly = false;

            DataGridViewTextBoxColumn colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.Name = "quantity";
            colQuantity.HeaderText = "数量";
            colQuantity.DataPropertyName = "quantity";
            colQuantity.Width = 80;
            colQuantity.ReadOnly = false;
            colQuantity.DefaultCellStyle.Format = "N0";

            DataGridViewTextBoxColumn colUnitPrice = new DataGridViewTextBoxColumn();
            colUnitPrice.Name = "unit_price";
            colUnitPrice.HeaderText = "单价";
            colUnitPrice.DataPropertyName = "unit_price";
            colUnitPrice.Width = 100;
            colUnitPrice.ReadOnly = false;
            colUnitPrice.DefaultCellStyle.Format = "N2";

            DataGridViewTextBoxColumn colTotalPrice = new DataGridViewTextBoxColumn();
            colTotalPrice.Name = "total_price";
            colTotalPrice.HeaderText = "总价";
            colTotalPrice.DataPropertyName = "total_price";
            colTotalPrice.Width = 100;
            colTotalPrice.ReadOnly = true;
            colTotalPrice.DefaultCellStyle.Format = "N2";

            // 添加列到DataGridView
            dgvOrderItems.Columns.Add(colProductName);
            dgvOrderItems.Columns.Add(colQuantity);
            dgvOrderItems.Columns.Add(colUnitPrice);
            dgvOrderItems.Columns.Add(colTotalPrice);

            // 注册事件
            dgvOrderItems.CellEndEdit += dgvOrderItems_CellEndEdit;
            dgvOrderItems.RowsAdded += dgvOrderItems_RowsAdded;
        }

        private string _orderStatus; // 用于存储订单状态

        private void LoadOrderDetails()
        {
            try
            {
                DataTable dtOrder = _purchaseOrderRepo.GetPurchaseOrderById(_orderId);
                if (dtOrder.Rows.Count > 0)
                {
                    DataRow row = dtOrder.Rows[0];
                    txtOrderNo.Text = row["order_no"].ToString();
                    txtSupplierName.Text = row["supplier_name"].ToString();
                    txtWarehouseName.Text = row["warehouse_name"].ToString();
                    txtTotalAmount.Text = row["total_amount"].ToString();
                    _orderStatus = row["status"].ToString(); // 保存订单状态
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载订单详情失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderItems()
        {
            try
            {
                _orderItemsTable = _purchaseOrderRepo.GetPurchaseOrderItems(_orderId);
                
                // 清空DataGridView
                dgvOrderItems.Rows.Clear();
                
                // 手动填充DataGridView，确保数据正确显示
                if (_orderItemsTable.Rows.Count > 0)
                {
                    foreach (DataRow row in _orderItemsTable.Rows)
                    {
                        string productName = row["product_name"].ToString();
                        int quantity = Convert.ToInt32(row["quantity"]);
                        decimal unitPrice = Convert.ToDecimal(row["unit_price"]);
                        decimal totalPrice = Convert.ToDecimal(row["total_price"]);
                        
                        // 手动添加行到DataGridView
                        int index = dgvOrderItems.Rows.Add();
                        dgvOrderItems.Rows[index].Cells["product_name"].Value = productName;
                        dgvOrderItems.Rows[index].Cells["quantity"].Value = quantity;
                        dgvOrderItems.Rows[index].Cells["unit_price"].Value = unitPrice;
                        dgvOrderItems.Rows[index].Cells["total_price"].Value = totalPrice;
                    }
                }
                
                // 重新计算并更新总金额
                UpdateOrderTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载订单产品失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvOrderItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // 计算总价
            if (e.ColumnIndex == 1 || e.ColumnIndex == 2) // 数量或单价列
            {
                DataGridViewRow row = dgvOrderItems.Rows[e.RowIndex];
                CalculateRowTotal(row);
            }
        }

        private void dgvOrderItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // 设置新行的默认值
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                DataGridViewRow row = dgvOrderItems.Rows[i];
                // 只有当行是新行时，才设置默认值（避免覆盖通过数据绑定加载的行）
                if (row.IsNewRow)
                {
                    row.Cells["quantity"].Value = 1;
                    row.Cells["unit_price"].Value = 0;
                    CalculateRowTotal(row);
                }
            }
        }

        private void CalculateRowTotal(DataGridViewRow row)
        {
            try
            {
                int quantity = 0;
                decimal unitPrice = 0;

                // 获取数量，处理DBNull.Value的情况
                object quantityValue = row.Cells["quantity"].Value;
                if (quantityValue != null && quantityValue != DBNull.Value && int.TryParse(quantityValue.ToString(), out quantity))
                {
                    // 获取单价，处理DBNull.Value的情况
                    object unitPriceValue = row.Cells["unit_price"].Value;
                    if (unitPriceValue != null && unitPriceValue != DBNull.Value && decimal.TryParse(unitPriceValue.ToString(), out unitPrice))
                    {
                        // 计算总价
                        decimal totalPrice = quantity * unitPrice;
                        row.Cells["total_price"].Value = totalPrice;

                        // 更新订单总金额
                        UpdateOrderTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算总价失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrderTotal()
        {
            decimal totalAmount = 0;
            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.IsNewRow) continue;
                object totalPriceValue = row.Cells["total_price"].Value;
                if (totalPriceValue != null && totalPriceValue != DBNull.Value)
                {
                    totalAmount += Convert.ToDecimal(totalPriceValue);
                }
            }
            txtTotalAmount.Text = totalAmount.ToString();
        }

        private void btnSaveOrderItems_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证数据
                if (!ValidateOrderItems())
                {
                    return;
                }

                // 保存订单基本信息
                string orderNo = txtOrderNo.Text;
                string supplierName = txtSupplierName.Text;
                string warehouseName = txtWarehouseName.Text;
                string status = _orderStatus;
                
                // 获取或创建用户ID（这里使用当前用户名作为示例，实际应用中应该从登录信息获取）
                int purchaserId = _purchaseOrderRepo.GetOrCreateUserIdByName("当前用户名"); // 需要替换为实际的登录用户名
                int updatedById = _purchaseOrderRepo.GetOrCreateUserIdByName("当前用户名"); // 需要替换为实际的登录用户名

                // 更新订单基本信息
                _purchaseOrderRepo.UpdatePurchaseOrder(
                    _orderId, 
                    orderNo, 
                    -1, // supplierId为-1，让仓库代码处理查找或创建
                    supplierName, 
                    -1, // warehouseId为-1，让仓库代码处理查找或创建
                    warehouseName, 
                    status, 
                    purchaserId, 
                    updatedById);

                // 保存订单产品明细
                SaveOrderItems();

                // 更新订单总金额
                UpdateOrderTotalAmount();

                MessageBox.Show("订单明细保存成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _parentForm.LoadPurchaseOrders();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存订单明细失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateOrderItems()
        {
            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.IsNewRow) continue;

                // 验证产品名称
                if (row.Cells["product_name"].Value == null || string.IsNullOrWhiteSpace(row.Cells["product_name"].Value.ToString()))
                {
                    MessageBox.Show("产品名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 验证数量
                int quantity;
                if (row.Cells["quantity"].Value == null || !int.TryParse(row.Cells["quantity"].Value.ToString(), out quantity) || quantity <= 0)
                {
                    MessageBox.Show("数量必须是大于0的整数！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // 验证单价
                decimal unitPrice;
                if (row.Cells["unit_price"].Value == null || !decimal.TryParse(row.Cells["unit_price"].Value.ToString(), out unitPrice) || unitPrice < 0)
                {
                    MessageBox.Show("单价必须是大于等于0的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void SaveOrderItems()
        {
            // 删除所有现有产品明细
            _purchaseOrderRepo.DeletePurchaseOrderItems(_orderId);

            // 添加新的产品明细
            foreach (DataGridViewRow row in dgvOrderItems.Rows)
            {
                if (row.IsNewRow) continue;

                // 处理产品名称
                object productNameValue = row.Cells["product_name"].Value;
                string productName = productNameValue != null && productNameValue != DBNull.Value ? productNameValue.ToString() : string.Empty;
                
                // 处理数量
                object quantityValue = row.Cells["quantity"].Value;
                int quantity = quantityValue != null && quantityValue != DBNull.Value ? Convert.ToInt32(quantityValue) : 0;
                
                // 处理单价
                object unitPriceValue = row.Cells["unit_price"].Value;
                decimal unitPrice = unitPriceValue != null && unitPriceValue != DBNull.Value ? Convert.ToDecimal(unitPriceValue) : 0;

                // 保存产品明细
                _purchaseOrderRepo.AddPurchaseOrderItem(_orderId, productName, quantity, unitPrice);
            }
        }

        private void UpdateOrderTotalAmount()
        {
            decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            _purchaseOrderRepo.UpdatePurchaseOrderTotalAmount(_orderId, totalAmount);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteOrderItem_Click(object sender, EventArgs e)
        {
            // 检查是否有选中的行
            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                // 弹出确认对话框
                DialogResult result = MessageBox.Show("确定要删除选中的订单记录吗？", "确认删除", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // 删除所有选中的行（从最后一行开始删除，避免索引问题）
                        for (int i = dgvOrderItems.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            DataGridViewRow row = dgvOrderItems.SelectedRows[i];
                            if (!row.IsNewRow) // 不要删除新行
                            {
                                dgvOrderItems.Rows.Remove(row);
                            }
                        }
                        
                        // 更新订单总金额
                        UpdateOrderTotal();
                        
                        // 保存更改到数据库
                        SaveOrderItems();
                        
                        // 更新订单总金额到数据库
                        UpdateOrderTotalAmount();
                        
                        // 更新父表单的订单列表
                        _parentForm.LoadPurchaseOrders();
                        
                        MessageBox.Show("订单记录删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除订单记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先选中要删除的订单记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            // 弹出确认对话框
            DialogResult result = MessageBox.Show("确定要删除整个订单吗？此操作不可撤销！", "确认删除订单", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    // 调用仓库类的删除订单方法
                    _purchaseOrderRepo.DeletePurchaseOrder(_orderId);
                    
                    MessageBox.Show("订单删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // 更新父表单的订单列表
                    _parentForm.LoadPurchaseOrders();
                    
                    // 关闭当前表单
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除订单失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.txtSupplierName = new System.Windows.Forms.TextBox();
            this.txtOrderNo = new System.Windows.Forms.TextBox();
            this.groupControl2 = new System.Windows.Forms.GroupBox();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.btnSaveOrderItems = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDeleteOrderItem = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.groupControl1.SuspendLayout();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtTotalAmount);
            this.groupControl1.Controls.Add(this.txtWarehouseName);
            this.groupControl1.Controls.Add(this.txtSupplierName);
            this.groupControl1.Controls.Add(this.txtOrderNo);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupControl1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(700, 100);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.TabStop = false;
            this.groupControl1.Text = "订单信息";
            this.groupControl1.BackColor = System.Drawing.Color.AliceBlue;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(450, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "总金额：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(250, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "仓库：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(250, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "供应商：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单号：";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtTotalAmount.Location = new System.Drawing.Point(520, 22);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(150, 23);
            this.txtTotalAmount.TabIndex = 7;
            this.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtWarehouseName.Location = new System.Drawing.Point(320, 22);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.ReadOnly = false;
            this.txtWarehouseName.Size = new System.Drawing.Size(110, 23);
            this.txtWarehouseName.TabIndex = 6;
            this.txtWarehouseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtSupplierName.Location = new System.Drawing.Point(320, 62);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.ReadOnly = false;
            this.txtSupplierName.Size = new System.Drawing.Size(350, 23);
            this.txtSupplierName.TabIndex = 5;
            this.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.txtOrderNo.Location = new System.Drawing.Point(90, 22);
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.ReadOnly = false;
            this.txtOrderNo.Size = new System.Drawing.Size(140, 23);
            this.txtOrderNo.TabIndex = 4;
            this.txtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.dgvOrderItems);
            this.groupControl2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupControl2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupControl2.Location = new System.Drawing.Point(12, 120);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(700, 300);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.TabStop = false;
            this.groupControl2.Text = "订单产品明细";
            this.groupControl2.BackColor = System.Drawing.Color.AliceBlue;
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderItems.Location = new System.Drawing.Point(3, 20);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.Size = new System.Drawing.Size(694, 277);
            this.dgvOrderItems.TabIndex = 0;
            // 
            // btnSaveOrderItems
            // 
            this.btnSaveOrderItems.BackColor = System.Drawing.Color.LightGreen;
            this.btnSaveOrderItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveOrderItems.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveOrderItems.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnSaveOrderItems.Location = new System.Drawing.Point(250, 430);
            this.btnSaveOrderItems.Name = "btnSaveOrderItems";
            this.btnSaveOrderItems.Size = new System.Drawing.Size(100, 35);
            this.btnSaveOrderItems.TabIndex = 2;
            this.btnSaveOrderItems.Text = "保存";
            this.btnSaveOrderItems.UseVisualStyleBackColor = false;
            this.btnSaveOrderItems.Click += new System.EventHandler(this.btnSaveOrderItems_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightSalmon;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(370, 430);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDeleteOrderItem
            // 
            this.btnDeleteOrderItem.BackColor = System.Drawing.Color.LightCoral;
            this.btnDeleteOrderItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrderItem.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteOrderItem.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteOrderItem.Location = new System.Drawing.Point(490, 430);
            this.btnDeleteOrderItem.Name = "btnDeleteOrderItem";
            this.btnDeleteOrderItem.Size = new System.Drawing.Size(100, 35);
            this.btnDeleteOrderItem.TabIndex = 4;
            this.btnDeleteOrderItem.Text = "删除选中行";
            this.btnDeleteOrderItem.UseVisualStyleBackColor = false;
            this.btnDeleteOrderItem.Click += new System.EventHandler(this.btnDeleteOrderItem_Click);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.BackColor = System.Drawing.Color.Red;
            this.btnDeleteOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteOrder.ForeColor = System.Drawing.Color.White;
            this.btnDeleteOrder.Location = new System.Drawing.Point(610, 430);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(80, 35);
            this.btnDeleteOrder.TabIndex = 5;
            this.btnDeleteOrder.Text = "删除订单";
            this.btnDeleteOrder.UseVisualStyleBackColor = false;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 481);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveOrderItems);
            this.Controls.Add(this.btnDeleteOrderItem);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "订单明细";
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.BackColor = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.OrderDetailsForm_Load);
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupControl1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtWarehouseName;
        private System.Windows.Forms.TextBox txtSupplierName;
        private System.Windows.Forms.TextBox txtOrderNo;
        private System.Windows.Forms.GroupBox groupControl2;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Button btnSaveOrderItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDeleteOrderItem;
        private System.Windows.Forms.Button btnDeleteOrder;
        #endregion
    }
}