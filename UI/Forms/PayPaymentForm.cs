using System;
using System.Drawing;
using System.Windows.Forms;
using CSproject.Business.Models;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class PayPaymentForm : Form
    {
        private readonly AccountPayableService _service = new AccountPayableService();
        private readonly AccountPayable _payable;

        public PayPaymentForm(AccountPayable payable)
        {
            _payable = payable;
            this.Load += PayPaymentForm_Load;
        }

        private void PayPaymentForm_Load(object sender, EventArgs e)
        {
            this.Text = "支付款项 - 订单号: " + _payable.OrderNo;

            // 创建控件
            var panel = new Panel { Dock = DockStyle.Fill };
            this.Controls.Add(panel);

            panel.Controls.Add(new Label { Text = "订单号:", Location = new Point(20, 20), AutoSize = true });
            panel.Controls.Add(new Label { Text = _payable.OrderNo, Location = new Point(100, 20), AutoSize = true, Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold) });

            panel.Controls.Add(new Label { Text = "供应商ID:", Location = new Point(20, 50), AutoSize = true });
            panel.Controls.Add(new Label { Text = _payable.SupplierId.ToString(), Location = new Point(100, 50), AutoSize = true });

            panel.Controls.Add(new Label { Text = "总金额:", Location = new Point(20, 80), AutoSize = true });
            panel.Controls.Add(new Label { Text = _payable.TotalAmount.ToString("F2"), Location = new Point(100, 80), AutoSize = true });

            panel.Controls.Add(new Label { Text = "已付金额:", Location = new Point(20, 110), AutoSize = true });
            panel.Controls.Add(new Label { Text = _payable.PaidAmount.ToString("F2"), Location = new Point(100, 110), AutoSize = true });

            panel.Controls.Add(new Label { Text = "未付金额:", Location = new Point(20, 140), AutoSize = true });
            panel.Controls.Add(new Label { Text = _payable.OutstandingAmount.ToString("F2"), Location = new Point(100, 140), AutoSize = true, Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold) });

            panel.Controls.Add(new Label { Text = "支付金额:", Location = new Point(20, 170), AutoSize = true });
            var txtAmount = new TextBox { Location = new Point(100, 170), Width = 100 };
            txtAmount.KeyPress += (s, args) =>
            {
                // 只允许输入数字和小数点
                if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) && args.KeyChar != '.')
                {
                    args.Handled = true;
                }
            };
            panel.Controls.Add(txtAmount);

            panel.Controls.Add(new Label { Text = "支付日期:", Location = new Point(20, 200), AutoSize = true });
            var dtpPaymentDate = new DateTimePicker { Location = new Point(100, 200), Width = 150, Value = DateTime.Now };
            panel.Controls.Add(dtpPaymentDate);

            panel.Controls.Add(new Label { Text = "备注:", Location = new Point(20, 230), AutoSize = true });
            var txtNotes = new TextBox { Location = new Point(100, 230), Width = 250, Multiline = true, Height = 60 };
            panel.Controls.Add(txtNotes);

            var btnSave = new Button { Text = "保存", Location = new Point(150, 300), Width = 80 };
            btnSave.Click += (s, args) =>
            {
                try
                {
                    decimal amount = decimal.Parse(txtAmount.Text);
                    if (amount <= 0)
                    {
                        MessageBox.Show("支付金额必须大于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (amount > _payable.OutstandingAmount)
                    {
                        MessageBox.Show("支付金额不能超过未付金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    _service.RecordPayment(_payable.PayableId, amount);
                    MessageBox.Show("支付记录成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (FormatException)
                {
                    MessageBox.Show("请输入有效的支付金额", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("支付记录失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            panel.Controls.Add(btnSave);

            var btnCancel = new Button { Text = "取消", Location = new Point(250, 300), Width = 80 };
            btnCancel.Click += (s, args) => { this.Close(); };
            panel.Controls.Add(btnCancel);

            this.ClientSize = new System.Drawing.Size(380, 350);
        }
    }
}
