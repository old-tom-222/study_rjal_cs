using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class BankAccountForm : Form
    {
        private BankAccountService _bankAccountService = new BankAccountService();
        private int _bankAccountId = 0;
        private bool _isEditMode = false;

        public BankAccountForm()
        {
            InitializeComponent();
            this.Text = "添加银行账户";
        }

        public BankAccountForm(int bankAccountId)
        {
            InitializeComponent();
            _bankAccountId = bankAccountId;
            _isEditMode = true;
            this.Text = "编辑银行账户";
            LoadBankAccountData();
        }

        private void LoadBankAccountData()
        {
            try
            {
                var bankAccount = _bankAccountService.GetBankAccountById(_bankAccountId);
                if (bankAccount != null)
                {
                    txtAccountName.Text = bankAccount.AccountName;
                    txtBankName.Text = bankAccount.BankName;
                    txtAccountNumber.Text = bankAccount.AccountNumber;
                    txtInitialBalance.Text = bankAccount.InitialBalance.ToString("F2");
                    chkStatus.Checked = bankAccount.Status == "启用";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载银行账户数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtAccountName.Text.Trim()))
            {
                MessageBox.Show("请输入账户名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            {
                MessageBox.Show("请输入银行名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAccountNumber.Text.Trim()))
            {
                MessageBox.Show("请输入银行账号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNumber.Focus();
                return false;
            }

            decimal initialBalance;
            if (!decimal.TryParse(txtInitialBalance.Text, out initialBalance))
            {
                MessageBox.Show("请输入有效的初始余额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInitialBalance.Focus();
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                BankAccount bankAccount = new BankAccount
                {
                    Id = _isEditMode ? _bankAccountId : 0,
                    AccountName = txtAccountName.Text.Trim(),
                    BankName = txtBankName.Text.Trim(),
                    AccountNumber = txtAccountNumber.Text.Trim(),
                    InitialBalance = decimal.Parse(txtInitialBalance.Text),
                    Status = chkStatus.Checked ? "启用" : "禁用",
                    CreateDate = _isEditMode ? DateTime.Now : DateTime.Now
                };

                if (_isEditMode)
                {
                    _bankAccountService.UpdateBankAccount(bankAccount);
                    MessageBox.Show("银行账户更新成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _bankAccountService.AddBankAccount(bankAccount);
                    MessageBox.Show("银行账户添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(_isEditMode ? "更新银行账户失败：" + ex.Message : "添加银行账户失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}