using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class SalesOrderCreateForm : Form
    {
        private readonly SalesOrderService _salesOrderService;
        private readonly CustomerService _customerService;
        private readonly InventoryService _inventoryService;
        private BindingList<SalesOrderDetailModel> _orderDetails;

        public SalesOrderCreateForm()
        {
            InitializeComponent();
            _salesOrderService = new SalesOrderService();
            _customerService = new CustomerService();
            _inventoryService = new InventoryService();
            _orderDetails = new BindingList<SalesOrderDetailModel>();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // 初始化订单日期为当前日期
            dtpOrderDate.Value = DateTime.Now;
            dtpOrderDate.Enabled = true;

            // 加载客户列表
            LoadCustomers();
            
            // 添加"添加新客户"按钮
            var btnAddCustomer = new Button();
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Text = "添加新客户";
            btnAddCustomer.Size = new System.Drawing.Size(90, 23);
            btnAddCustomer.Location = new System.Drawing.Point(cmbCustomer.Right + 10, cmbCustomer.Top);
            btnAddCustomer.Click += new EventHandler(btnAddCustomer_Click);
            this.Controls.Add(btnAddCustomer);

            // 加载产品列表
            LoadProducts();

            // 初始化订单明细表格
            InitializeOrderDetailsGrid();

            // 初始化订单号（临时）
            txtOrderNumber.Text = GenerateTemporaryOrderNumber();

            // 禁用编辑模式相关控件
            btnEditMode.Visible = false;
            btnCancelEdit.Visible = false;

            // 更新订单总金额
            UpdateTotalAmount();
        }

        private void LoadCustomers()
        {
            try
            {
                // 清空下拉列表
                cmbCustomer.Items.Clear();
                
                // 加载客户数据
                var customers = _customerService.GetAllCustomers();
                
                // 绑定到下拉列表
                cmbCustomer.DataSource = customers;
                cmbCustomer.DisplayMember = "CustomerName";
                cmbCustomer.ValueMember = "CustomerId";
                cmbCustomer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载客户数据失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用简单的TextBox和Form来获取客户名称
                Form inputForm = new Form();
                inputForm.Text = "添加新客户";
                inputForm.Width = 300;
                inputForm.Height = 150;
                inputForm.StartPosition = FormStartPosition.CenterScreen;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;
                
                Label label = new Label();
                label.Text = "请输入客户名称：";
                label.Location = new Point(15, 20);
                label.Width = 120;
                
                TextBox textBox = new TextBox();
                textBox.Location = new Point(15, 40);
                textBox.Width = 240;
                
                Button okButton = new Button();
                okButton.Text = "确定";
                okButton.Location = new Point(60, 70);
                okButton.DialogResult = DialogResult.OK;
                
                Button cancelButton = new Button();
                cancelButton.Text = "取消";
                cancelButton.Location = new Point(150, 70);
                cancelButton.DialogResult = DialogResult.Cancel;
                
                inputForm.Controls.Add(label);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(okButton);
                inputForm.Controls.Add(cancelButton);
                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;
                
                // 显示对话框并获取结果
                string customerName = "";
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    customerName = textBox.Text;
                }
                
                if (!string.IsNullOrWhiteSpace(customerName))
                {
                    // 创建新客户对象 - 使用正确的属性名称
                    var newCustomer = new Customer
                    {
                        CustomerCode = "CUST" + DateTime.Now.ToString("yyyyMMddHHmmss"), // 生成简单的客户代码
                        CustomerName = customerName,
                        ContactPerson = "",
                        ContactPhone = "",
                        Email = "",
                        Address = "",
                        City = "",
                        Province = "", // 正确的属性名
                        PostalCode = "",
                        CustomerType = "", // 正确的属性名
                        Status = "1", // 数字状态值
                        CreatedDate = DateTime.Now,
                        LastUpdated = DateTime.Now, // 正确的属性名
                        Notes = ""
                    };
                    
                    // 调用服务创建客户
                    _customerService.CreateCustomer(newCustomer);
                    
                    // 重新加载客户列表
                    LoadCustomers();
                    
                    // 自动选择新添加的客户
                    var customers = (BindingList<Customer>)cmbCustomer.DataSource;
                    var justAddedCustomer = customers.FirstOrDefault(c => c.CustomerCode == newCustomer.CustomerCode);
                    if (justAddedCustomer != null)
                    {
                        cmbCustomer.SelectedValue = justAddedCustomer.CustomerId;
                    }
                    
                    MessageBox.Show("客户添加成功并已自动选择！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加客户失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                var inventories = _inventoryService.GetInventoryModels(null, null, null)
                    .Where(i => i.Quantity > 0) // 只显示库存不为0的产品
                    .ToList();
                cmbProduct.DataSource = inventories;
                cmbProduct.DisplayMember = "ProductName";
                cmbProduct.ValueMember = "ProductId";
                cmbProduct.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载产品数据失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeOrderDetailsGrid()
        {
            dgvOrderDetails.AutoGenerateColumns = false;
            dgvOrderDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderDetails.ReadOnly = true;

            // 添加列
            DataGridViewTextBoxColumn productColumn = new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                DataPropertyName = "ProductName",
                HeaderText = "产品名称",
                Width = 150
            };

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                DataPropertyName = "Quantity",
                HeaderText = "数量",
                Width = 80
            };

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                DataPropertyName = "UnitPrice",
                HeaderText = "单价",
                Width = 100,
                DefaultCellStyle = { Format = "N2" }
            };

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                DataPropertyName = "Amount",
                HeaderText = "金额",
                Width = 120,
                DefaultCellStyle = { Format = "N2" }
            };

            dgvOrderDetails.Columns.AddRange(productColumn, quantityColumn, priceColumn, amountColumn);

            // 添加删除按钮列
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn
            {
                Name = "Delete",
                HeaderText = "操作",
                Text = "删除",
                UseColumnTextForButtonValue = true,
                Width = 60
            };
            dgvOrderDetails.Columns.Add(deleteColumn);
        }

        private string GenerateTemporaryOrderNumber()
        {
            // 生成临时订单号，正式保存时会替换
            return "TEMP-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem is InventoryModel inventory)
            {
                txtUnitPrice.Text = inventory.UnitPrice.ToString("N2");
                txtAvailableStock.Text = inventory.Quantity.ToString();
                // 默认数量为1
                txtQuantity.Text = "1";
            }
            else
            {
                txtUnitPrice.Clear();
                txtAvailableStock.Clear();
                txtQuantity.Clear();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            // 验证输入是否为数字
            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                txtQuantity.Text = "1";
                quantity = 1;
            }

            // 验证数量是否大于0
            if (quantity <= 0)
            {
                txtQuantity.Text = "1";
                quantity = 1;
            }

            // 验证库存
            if (cmbProduct.SelectedItem is InventoryModel inventory && quantity > inventory.Quantity)
            {
                MessageBox.Show("选择数量超过库存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Text = inventory.Quantity.ToString();
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (cmbProduct.SelectedItem == null)
            {
                MessageBox.Show("请选择产品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            var inventory = (InventoryModel)cmbProduct.SelectedItem;
            if (inventory.ProductId <= 0)
            {
                MessageBox.Show("无效的产品，请重新选择！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("请输入有效的数量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) || unitPrice <= 0)
            {
                MessageBox.Show("请输入有效的单价！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 检查库存
            if (quantity > inventory.Quantity)
            {
                MessageBox.Show("选择数量超过库存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 临时解除数据绑定，避免添加项目时的冲突
            dgvOrderDetails.DataSource = null;
            
            // 检查是否已添加该产品
            var existingDetail = _orderDetails.FirstOrDefault(d => d.ProductId == inventory.ProductId);
            if (existingDetail != null)
            {
                // 更新数量和金额
                existingDetail.Quantity += quantity;
                existingDetail.Amount = existingDetail.Quantity * existingDetail.UnitPrice;
                
                // 更新数量和金额完成，静默更新
            }
            else
            {
                // 添加新的订单明细
                var detail = new SalesOrderDetailModel
                {
                    ProductId = inventory.ProductId,
                    ProductName = inventory.ProductName,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    Amount = quantity * unitPrice
                };
                _orderDetails.Add(detail);
                
                // 添加产品完成，静默更新
            }

            // 刷新订单明细表格
            RefreshOrderDetailsGrid();
            
            // 确保表格可见并选中最后一行
            if (dgvOrderDetails.Rows.Count > 0)
            {
                dgvOrderDetails.CurrentCell = dgvOrderDetails.Rows[dgvOrderDetails.Rows.Count - 1].Cells[0];
                dgvOrderDetails.FirstDisplayedScrollingRowIndex = dgvOrderDetails.Rows.Count - 1;
            }

            // 清空产品选择，但保持数量为1以便快速添加下一个产品
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Text = "1"; // 默认保持为1
            txtUnitPrice.Clear();
            txtAvailableStock.Clear();
            
            // 聚焦到产品下拉框，方便继续添加
            cmbProduct.Focus();
        }

        private void RefreshOrderDetailsGrid()
        {
            // 过滤掉无效的订单明细项
            var validOrderDetails = _orderDetails.Where(d => d.ProductId > 0).ToList();
            
            // 确保所有有效订单明细项的Amount属性正确计算
            
            // 确保所有有效订单明细项的Amount属性正确计算
            foreach (var detail in validOrderDetails)
            {
                detail.Amount = detail.Quantity * detail.UnitPrice;
            }
            
            // 重新设置数据源，使用BindingList确保数据变更能正确反映到界面
            dgvOrderDetails.DataSource = null;
            dgvOrderDetails.DataSource = validOrderDetails;
            
            // 强制刷新控件
            dgvOrderDetails.Refresh();
            
            // 更新总金额（只计算有效项）
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            // 只计算有效的订单明细项的总金额
            decimal totalAmount = _orderDetails.Where(d => d.ProductId > 0).Sum(d => d.Amount);
            txtTotalAmount.Text = totalAmount.ToString("N2");
        }

        private void dgvOrderDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理删除按钮点击
            if (e.ColumnIndex == dgvOrderDetails.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                if (MessageBox.Show("确定要删除该产品吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _orderDetails.RemoveAt(e.RowIndex);
                    RefreshOrderDetailsGrid();
                }
            }
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // 验证必填项
            if (cmbCustomer.SelectedItem == null)
            {
                MessageBox.Show("请选择客户！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_orderDetails.Count == 0)
            {
                MessageBox.Show("请添加产品！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 验证所有订单明细的产品ID是否有效
            var invalidItems = _orderDetails.Where(d => d.ProductId <= 0).ToList();
            if (invalidItems.Count > 0)
            {
                MessageBox.Show("订单中包含无效的产品信息，请移除或重新添加这些产品！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 创建订单对象
                // 创建SalesOrder类型而不是SalesOrderModel类型
                var order = new SalesOrder
                {
                    OrderDate = dtpOrderDate.Value,
                    CustomerId = (int)cmbCustomer.SelectedValue,
                    CustomerName = cmbCustomer.Text,
                    OrderStatus = "待审核",
                    TotalAmount = _orderDetails.Sum(d => d.Amount),
                    CreatedDate = DateTime.Now,
                    // 将SalesOrderDetailModel列表转换为SalesOrderItem列表
                    OrderItems = _orderDetails.Select(d => new SalesOrderItem
                    {
                        ProductId = d.ProductId,
                        ProductName = d.ProductName,
                        Quantity = d.Quantity,
                        UnitPrice = d.UnitPrice
                        // 添加其他必要的属性映射
                    }).ToList()
                };

                // 设置创建者ID
                string userId = System.Environment.UserName; // 或者使用其他方式获取当前用户ID
                order.CreatedBy = userId;
                
                // 验证客户状态
                int selectedCustomerId = (int)cmbCustomer.SelectedValue;
                Customer selectedCustomer = _customerService.GetCustomerById(selectedCustomerId);
                string statusValue = selectedCustomer.Status?.Trim() ?? "null";
                bool isActive1 = statusValue == "1";
                bool isActiveTrue = statusValue.ToLower() == "true";
                bool isActive = isActive1 || isActiveTrue;
                
                // 保存订单
                int orderId = _salesOrderService.CreateOrder(order, userId);

                if (orderId > 0)
                {
                    MessageBox.Show("订单创建成功！订单ID: " + orderId, "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 重置表单
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("订单创建失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("订单创建失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            // 重置表单控件
            cmbCustomer.SelectedIndex = -1;
            dtpOrderDate.Value = DateTime.Now;
            txtOrderNumber.Text = GenerateTemporaryOrderNumber();
            _orderDetails.Clear();
            RefreshOrderDetailsGrid();
            cmbProduct.SelectedIndex = -1;
            txtQuantity.Clear();
            txtUnitPrice.Clear();
            txtAvailableStock.Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }

        // 编辑模式相关方法
        private void btnEditMode_Click(object sender, EventArgs e)
        {
            // 进入编辑模式
            EnableEditMode();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            // 取消编辑
            DisableEditMode();
        }

        private void EnableEditMode()
        {
            // 启用编辑控件
            cmbCustomer.Enabled = true;
            dtpOrderDate.Enabled = true;
            btnSaveOrder.Visible = true;
            btnCancelEdit.Visible = true;
            btnEditMode.Visible = false;
            btnCancel.Text = "关闭";
        }

        private void DisableEditMode()
        {
            // 禁用编辑控件
            cmbCustomer.Enabled = false;
            dtpOrderDate.Enabled = false;
            btnSaveOrder.Visible = false;
            btnCancelEdit.Visible = false;
            btnEditMode.Visible = true;
            btnCancel.Text = "取消";
        }

        // 为后续编辑功能预留，当从订单列表打开时可以加载订单数据
        public void LoadOrderData(int orderId)
        {
            try
            {
                var order = _salesOrderService.GetOrderById(orderId);
                if (order != null)
                {
                    // 填充订单基本信息
                    txtOrderNumber.Text = order.OrderNumber;
                    dtpOrderDate.Value = order.OrderDate;
                    
                    // 查找并设置客户
                    for (int i = 0; i < cmbCustomer.Items.Count; i++)
                    {
                        var customer = (Customer)cmbCustomer.Items[i];
                        if (customer.CustomerId == order.CustomerId)
                        {
                            cmbCustomer.SelectedIndex = i;
                            break;
                        }
                    }

                    // 填充订单明细
                    // 将SalesOrderItem列表转换为SalesOrderDetailModel列表
                    if (order.SalesOrderDetails != null) {
                        _orderDetails.Clear();
                        foreach (var item in order.SalesOrderDetails) {
                            _orderDetails.Add(new SalesOrderDetailModel {
                                ProductId = item.ProductId,
                                ProductName = item.ProductName,
                                Quantity = item.Quantity,
                                UnitPrice = item.UnitPrice,
                                Amount = item.Quantity * item.UnitPrice
                            });
                        }
                    } else {
                        _orderDetails.Clear();
                    }
                    RefreshOrderDetailsGrid();

                    // 根据订单状态决定是否启用编辑
                    if (order.OrderStatus == "待审核")
                    {
                        btnEditMode.Visible = true;
                    }
                    else
                    {
                        // 已审核订单不允许编辑
                        DisableEditMode();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载订单数据失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}