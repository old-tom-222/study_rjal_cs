using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class BankAccountListForm : Form
    {
        private BankAccountService _bankAccountService = new BankAccountService();

        public BankAccountListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // 设置DataGridView的属性
            dgvBankAccounts.AutoGenerateColumns = false;
            dgvBankAccounts.AllowUserToAddRows = false;
            dgvBankAccounts.ReadOnly = true;
            dgvBankAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 定义列
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "Id";
            colId.HeaderText = "账户ID";
            colId.Visible = false;
            dgvBankAccounts.Columns.Add(colId);

            DataGridViewTextBoxColumn colAccountName = new DataGridViewTextBoxColumn();
            colAccountName.DataPropertyName = "AccountName";
            colAccountName.HeaderText = "账户名称";
            colAccountName.Width = 150;
            dgvBankAccounts.Columns.Add(colAccountName);

            DataGridViewTextBoxColumn colBankName = new DataGridViewTextBoxColumn();
            colBankName.DataPropertyName = "BankName";
            colBankName.HeaderText = "银行名称";
            colBankName.Width = 150;
            dgvBankAccounts.Columns.Add(colBankName);

            DataGridViewTextBoxColumn colAccountNumber = new DataGridViewTextBoxColumn();
            colAccountNumber.DataPropertyName = "AccountNumber";
            colAccountNumber.HeaderText = "银行账号";
            colAccountNumber.Width = 180;
            dgvBankAccounts.Columns.Add(colAccountNumber);

            DataGridViewTextBoxColumn colBalance = new DataGridViewTextBoxColumn();
            colBalance.DataPropertyName = "Balance";
            colBalance.HeaderText = "当前余额";
            colBalance.Width = 100;
            colBalance.DefaultCellStyle.Format = "C2";
            colBalance.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvBankAccounts.Columns.Add(colBalance);

            DataGridViewTextBoxColumn colCreateDate = new DataGridViewTextBoxColumn();
            colCreateDate.DataPropertyName = "CreateDate";
            colCreateDate.HeaderText = "创建日期";
            colCreateDate.Width = 120;
            colCreateDate.DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvBankAccounts.Columns.Add(colCreateDate);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "状态";
            colStatus.Width = 80;
            dgvBankAccounts.Columns.Add(colStatus);
        }

        private void BankAccountListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有银行账户
            LoadBankAccounts();
        }

        private void LoadBankAccounts()
        {
            try
            {
                string accountName = txtAccountName.Text.Trim();
                string bankName = txtBankName.Text.Trim();
                string accountNumber = txtAccountNumber.Text.Trim();
                string status = cmbStatus.Text.Trim();

                var bankAccounts = _bankAccountService.GetAllBankAccounts(accountName, bankName, accountNumber, status);
                dgvBankAccounts.DataSource = bankAccounts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载银行账户数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBankAccounts();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 清空筛选条件
            txtAccountName.Text = "";
            txtBankName.Text = "";
            txtAccountNumber.Text = "";
            cmbStatus.SelectedIndex = -1;

            // 重新加载数据
            LoadBankAccounts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BankAccountForm bankAccountForm = new BankAccountForm();
            if (bankAccountForm.ShowDialog() == DialogResult.OK)
            {
                LoadBankAccounts(); // 添加成功后重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBankAccounts.SelectedRows.Count > 0)
            {
                int bankAccountId = Convert.ToInt32(dgvBankAccounts.SelectedRows[0].Cells["Id"].Value);
                BankAccountForm bankAccountForm = new BankAccountForm(bankAccountId);
                if (bankAccountForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBankAccounts(); // 编辑成功后重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的银行账户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBankAccounts.SelectedRows.Count > 0)
            {
                int bankAccountId = Convert.ToInt32(dgvBankAccounts.SelectedRows[0].Cells["Id"].Value);
                string accountName = dgvBankAccounts.SelectedRows[0].Cells["AccountName"].Value.ToString();

                if (MessageBox.Show($"确定要删除银行账户 '{accountName}' 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _bankAccountService.DeleteBankAccount(bankAccountId);
                        MessageBox.Show("银行账户删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBankAccounts(); // 删除成功后重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除银行账户失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的银行账户", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvBankAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}