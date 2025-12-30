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
    public partial class SalesOrderDetailForm : Form
    {
        // 定义订单明细项类
        private class OrderDetailItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Amount { get; set; }
        }
        private readonly SalesOrderService _salesOrderService;
        private int _orderId;
        private SalesOrder _currentOrder;

        public SalesOrderDetailForm(int orderId)
        {
            InitializeComponent();
            _salesOrderService = new SalesOrderService();
            _orderId = orderId;
            InitializeControls();
            LoadOrderDetails();
        }

        private void InitializeControls()
        {
            // 初始化订单明细表格
            InitializeOrderDetailsGrid();
            
            // 设置只读控件
            txtOrderId.ReadOnly = true;
            txtOrderNumber.ReadOnly = true;
            dtpOrderDate.Enabled = false;
            txtCustomer.ReadOnly = true;
            txtOrderStatus.ReadOnly = true;
            txtTotalAmount.ReadOnly = true;
            txtCreateTime.ReadOnly = true;
            txtUpdateTime.ReadOnly = true;
            txtRemark.ReadOnly = true;
            txtApprovalRemark.ReadOnly = true;
            txtShipmentRemark.ReadOnly = true;
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
                HeaderText = "产品名称",
                Width = 150
            };

            DataGridViewTextBoxColumn quantityColumn = new DataGridViewTextBoxColumn
            {
                Name = "Quantity",
                HeaderText = "数量",
                Width = 80
            };

            DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn
            {
                Name = "UnitPrice",
                HeaderText = "单价",
                Width = 100,
                DefaultCellStyle = { Format = "N2" }
            };

            DataGridViewTextBoxColumn amountColumn = new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "金额",
                Width = 120,
                DefaultCellStyle = { Format = "N2" }
            };

            dgvOrderDetails.Columns.AddRange(productColumn, quantityColumn, priceColumn, amountColumn);
        }

        private void LoadOrderDetails()
        {
            try
            {
                // 获取订单详情
                _currentOrder = _salesOrderService.GetOrderById(_orderId);
                
                if (_currentOrder != null)
                {
                    // 填充订单基本信息
                    txtOrderId.Text = _currentOrder.OrderId.ToString();
                    txtOrderNumber.Text = _currentOrder.OrderNumber;
                    dtpOrderDate.Value = _currentOrder.OrderDate;
                    txtCustomer.Text = _currentOrder.CustomerName;
                    txtOrderStatus.Text = _currentOrder.OrderStatus;
                    txtTotalAmount.Text = _currentOrder.TotalAmount.ToString("N2");
                    txtCreateTime.Text = _currentOrder.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                    txtUpdateTime.Text = _currentOrder.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss");
                    txtRemark.Text = _currentOrder.Notes ?? "";
                    txtApprovalRemark.Text = _currentOrder.ApprovedBy ?? "";
                    txtShipmentRemark.Text = "";

                    // 填充订单明细 - 使用BindingList进行数据绑定
                    try
                    {
                        // 清除DataGridView
                        dgvOrderDetails.DataSource = null;
                        
                        // 检查OrderItems是否存在
                        if (_currentOrder.OrderItems != null && _currentOrder.OrderItems.Count > 0)
                        {
                            // 创建一个BindingList用于数据绑定
                            var bindingList = new BindingList<object>();
                            
                            // 添加数据到BindingList
                            foreach (var item in _currentOrder.OrderItems)
                            {
                                bindingList.Add(new
                                {
                                    ProductName = item.ProductName,
                                    Quantity = item.Quantity,
                                    UnitPrice = item.UnitPrice,
                                    Amount = item.UnitPrice * item.Quantity
                                });
                            }
                            
                            // 设置AutoGenerateColumns为true，让系统自动创建列
                            dgvOrderDetails.AutoGenerateColumns = true;
                            
                            // 绑定数据源
                            dgvOrderDetails.DataSource = bindingList;
                            
                            // 调整列宽
                            dgvOrderDetails.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                        }
                    }
                    catch
                    {
                        // 错误处理
                    }

                    // 根据订单状态显示相应的操作按钮
                    UpdateActionButtons();
                }
                else
                {
                    MessageBox.Show("未找到订单信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载订单详情失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateActionButtons()
        {
            // 根据订单状态显示不同的操作按钮
            switch (_currentOrder.OrderStatus)
            {
                case "待审核":
                    // 显示审核和驳回按钮
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    btnShip.Visible = false;
                    btnEdit.Visible = true;
                    break;
                case "已审核":
                    // 显示发货按钮
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnShip.Visible = true;
                    btnEdit.Visible = false;
                    break;
                case "已发货":
                    // 所有操作按钮都隐藏
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnShip.Visible = false;
                    btnEdit.Visible = false;
                    break;
                case "已驳回":
                    // 只显示编辑按钮
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnShip.Visible = false;
                    btnEdit.Visible = true;
                    break;
                default:
                    // 默认隐藏所有操作按钮
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                    btnShip.Visible = false;
                    btnEdit.Visible = false;
                    break;
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            // 显示审核备注输入框
            if (string.IsNullOrEmpty(txtApprovalRemark.Text))
            {
                txtApprovalRemark.ReadOnly = false;
                btnConfirmApproval.Visible = true;
                btnCancelApproval.Visible = true;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                MessageBox.Show("请输入审核备注信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PerformApproval();
            }
        }

        private void btnConfirmApproval_Click(object sender, EventArgs e)
        {
            PerformApproval();
        }

        private void btnCancelApproval_Click(object sender, EventArgs e)
        {
            // 取消审核操作
            txtApprovalRemark.ReadOnly = true;
            btnConfirmApproval.Visible = false;
            btnCancelApproval.Visible = false;
            btnApprove.Visible = true;
            btnReject.Visible = true;
        }

        private void PerformApproval()
        {
            try
            {
                // 执行审核操作
                bool result = _salesOrderService.ApproveOrder(_orderId, txtApprovalRemark.Text);
                
                if (result)
                {
                    MessageBox.Show("订单审核成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 重新加载订单信息
                    LoadOrderDetails();
                }
                else
                {
                    MessageBox.Show("订单审核失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("订单审核失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            // 显示驳回备注输入框
            if (string.IsNullOrEmpty(txtApprovalRemark.Text))
            {
                txtApprovalRemark.ReadOnly = false;
                btnConfirmReject.Visible = true;
                btnCancelReject.Visible = true;
                btnApprove.Visible = false;
                btnReject.Visible = false;
                MessageBox.Show("请输入驳回原因", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PerformReject();
            }
        }

        private void btnConfirmReject_Click(object sender, EventArgs e)
        {
            PerformReject();
        }

        private void btnCancelReject_Click(object sender, EventArgs e)
        {
            // 取消驳回操作
            txtApprovalRemark.ReadOnly = true;
            btnConfirmReject.Visible = false;
            btnCancelReject.Visible = false;
            btnApprove.Visible = true;
            btnReject.Visible = true;
        }

        private void PerformReject()
        {
            try
            {
                // 执行驳回操作
                bool result = _salesOrderService.RejectOrder(_orderId, txtApprovalRemark.Text);
                
                if (result)
                {
                    MessageBox.Show("订单驳回成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 重新加载订单信息
                    LoadOrderDetails();
                }
                else
                {
                    MessageBox.Show("订单驳回失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("订单驳回失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShip_Click(object sender, EventArgs e)
        {
            // 显示发货备注输入框
            if (string.IsNullOrEmpty(txtShipmentRemark.Text))
            {
                txtShipmentRemark.ReadOnly = false;
                btnConfirmShip.Visible = true;
                btnCancelShip.Visible = true;
                btnShip.Visible = false;
                MessageBox.Show("请输入发货备注信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PerformShipment();
            }
        }

        private void btnConfirmShip_Click(object sender, EventArgs e)
        {
            PerformShipment();
        }

        private void btnCancelShip_Click(object sender, EventArgs e)
        {
            // 取消发货操作
            txtShipmentRemark.ReadOnly = true;
            btnConfirmShip.Visible = false;
            btnCancelShip.Visible = false;
            btnShip.Visible = true;
        }

        private void PerformShipment()
        {
            try
            {
                // 执行发货操作
                bool result = _salesOrderService.ShipOrder(_orderId, txtShipmentRemark.Text);
                
                if (result)
                {
                    MessageBox.Show("订单发货成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 重新加载订单信息
                    LoadOrderDetails();
                }
                else
                {
                    MessageBox.Show("订单发货失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("订单发货失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // 打开编辑订单页面
            using (var editForm = new SalesOrderCreateForm())
            {
                editForm.Text = "编辑销售订单";
                editForm.LoadOrderData(_orderId);
                editForm.ShowDialog();
            }
            
            // 编辑完成后重新加载订单信息
            LoadOrderDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 关闭当前窗口
            this.Close();
        }

        // 打印订单功能
        private void btnPrintOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // 这里简单实现，实际应用中应该使用专门的报表组件
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "文本文件|*.txt|所有文件|*.*",
                    FileName = $"订单_{_currentOrder.OrderNumber}.txt"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("========== 销售订单 ==========");
                    sb.AppendLine($"订单编号: {_currentOrder.OrderNumber}");
                    sb.AppendLine($"订单日期: {_currentOrder.OrderDate.ToString("yyyy-MM-dd")}");
                    sb.AppendLine($"客户名称: {_currentOrder.CustomerName}");
                    sb.AppendLine($"订单状态: {_currentOrder.OrderStatus}");
                    sb.AppendLine($"总金额: {_currentOrder.TotalAmount.ToString("N2")}");
                    sb.AppendLine("\n----- 订单明细 -----");
                    sb.AppendLine("产品名称\t数量\t单价\t金额");
                    
                    if (_currentOrder.OrderItems != null)
                    {
                        foreach (var detail in _currentOrder.OrderItems)
                        {
                            decimal amount = detail.UnitPrice * detail.Quantity;
                            sb.AppendLine($"{detail.ProductName}\t{detail.Quantity}\t{detail.UnitPrice.ToString("N2")}\t{amount.ToString("N2")}");
                        }
                    }
                    
                    sb.AppendLine("\n========== 订单结束 ==========");
                    
                    System.IO.File.WriteAllText(saveDialog.FileName, sb.ToString());
                    MessageBox.Show("订单已导出到文本文件！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出订单失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}