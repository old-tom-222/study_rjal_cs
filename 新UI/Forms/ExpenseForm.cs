using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;

namespace CSproject.UI.Forms
{
    public partial class ExpenseForm : Form
    {
        private ExpenseService _expenseService = new ExpenseService();
        private AccountingSubjectService _subjectService = new AccountingSubjectService();
        private PaymentMethodService _paymentMethodService = new PaymentMethodService();
        private BankAccountService _bankAccountService = new BankAccountService();

        private int _expenseId = 0;
        private bool _isEditMode = false;

        public ExpenseForm()
        {
            InitializeComponent();
            _isEditMode = false;
            InitializeControls();
        }

        public ExpenseForm(int expenseId)
        {
            InitializeComponent();
            _expenseId = expenseId;
            _isEditMode = true;
            InitializeControls();
            LoadExpenseData();
        }

        private void InitializeControls()
        {
            // 初始化会计科目下拉框
            cmbSubject.Items.Clear();
            var subjects = _subjectService.GetAllSubjects(isActive: true);
            foreach (var subject in subjects)
            {
                cmbSubject.Items.Add(new { Value = subject.Id, Text = subject.Code + " - " + subject.Name });
            }
            cmbSubject.DisplayMember = "Text";
            cmbSubject.ValueMember = "Value";

            // 初始化付款方式下拉框
            cmbPaymentMethod.Items.Clear();
            var paymentMethods = _paymentMethodService.GetAllPaymentMethods(isActive: true);
            foreach (var method in paymentMethods)
            {
                cmbPaymentMethod.Items.Add(new { Value = method.Id, Text = method.Name });
            }
            cmbPaymentMethod.DisplayMember = "Text";
            cmbPaymentMethod.ValueMember = "Value";

            // 初始化银行账户下拉框
            cmbBankAccount.Items.Clear();
            var bankAccounts = _bankAccountService.GetAllBankAccounts(isActive: true);
            foreach (var account in bankAccounts)
            {
                cmbBankAccount.Items.Add(new { Value = account.Id, Text = account.BankName + " - " + account.AccountNumber });
            }
            cmbBankAccount.DisplayMember = "Text";
            cmbBankAccount.ValueMember = "Value";

            // 设置默认日期为当前日期
            dtpExpenseDate.Value = DateTime.Now;
        }

        private void LoadExpenseData()
        {
            try
            {
                var expense = _expenseService.GetExpenseById(_expenseId);
                if (expense != null)
                {
                    txtExpenseNo.Text = expense.ExpenseNo;
                    dtpExpenseDate.Value = expense.ExpenseDate;
                    
                    // 设置会计科目
                    foreach (var item in cmbSubject.Items)
                    {
                        if ((int)((dynamic)item).Value == expense.SubjectId)
                        {
                            cmbSubject.SelectedItem = item;
                            break;
                        }
                    }
                    
                    txtAmount.Text = expense.Amount.ToString("F2");
                    txtDescription.Text = expense.Description;
                    txtReference.Text = expense.Reference;
                    
                    // 设置付款方式
                    foreach (var item in cmbPaymentMethod.Items)
                    {
                        if ((int)((dynamic)item).Value == expense.PaymentMethodId)
                        {
                            cmbPaymentMethod.SelectedItem = item;
                            break;
                        }
                    }
                    
                    // 设置银行账户
                    if (expense.BankAccountId.HasValue)
                    {
                        foreach (var item in cmbBankAccount.Items)
                        {
                            if ((int)((dynamic)item).Value == expense.BankAccountId.Value)
                            {
                                cmbBankAccount.SelectedItem = item;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载支出数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                    return;

                // 准备数据
                var expense = new ExpenseModel
                {
                    ExpenseNo = txtExpenseNo.Text.Trim(),
                    ExpenseDate = dtpExpenseDate.Value,
                    SubjectId = (int)((dynamic)cmbSubject.SelectedItem).Value,
                    Amount = decimal.Parse(txtAmount.Text),
                    Description = txtDescription.Text.Trim(),
                    Reference = txtReference.Text.Trim(),
                    PaymentMethodId = (int)((dynamic)cmbPaymentMethod.SelectedItem).Value,
                    BankAccountId = cmbBankAccount.SelectedItem != null ? (int?)((dynamic)cmbBankAccount.SelectedItem).Value : null,
                    CreatedBy = "当前用户" // 实际应用中应该从登录信息获取
                };

                // 根据模式执行保存或更新操作
                if (_isEditMode)
                {
                    expense.Id = _expenseId;
                    expense.UpdatedBy = "当前用户";
                    _expenseService.UpdateExpense(expense);
                    MessageBox.Show("支出记录已成功更新", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _expenseService.CreateExpense(expense);
                    MessageBox.Show("支出记录已成功创建", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 关闭表单
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存支出记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // 验证支出编号
            if (string.IsNullOrEmpty(txtExpenseNo.Text))
            {
                MessageBox.Show("请输入支出编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExpenseNo.Focus();
                return false;
            }

            // 验证会计科目
            if (cmbSubject.SelectedItem == null)
            {
                MessageBox.Show("请选择会计科目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSubject.Focus();
                return false;
            }

            // 验证金额
            decimal amount;
            if (string.IsNullOrEmpty(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                MessageBox.Show("请输入有效的金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
                return false;
            }

            // 验证付款方式
            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("请选择付款方式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbPaymentMethod.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // 关闭表单
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}