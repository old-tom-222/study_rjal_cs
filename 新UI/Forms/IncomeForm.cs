using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class IncomeForm : Form
    {
        private IncomeService _incomeService = new IncomeService();
        private AccountingSubjectService _subjectService = new AccountingSubjectService();
        private int? _incomeId = null;

        public IncomeForm(int? incomeId = null)
        {
            InitializeComponent();
            _incomeId = incomeId;
            InitializeControls();
            if (_incomeId.HasValue)
            {
                LoadIncomeData(_incomeId.Value);
                this.Text = "编辑收入记录";
            }
            else
            {
                this.Text = "添加收入记录";
            }
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
            cmbSubject.SelectedIndex = -1;

            // 设置默认日期为当前日期
            dtpIncomeDate.Value = DateTime.Now;
        }

        private void LoadIncomeData(int incomeId)
        {
            try
            {
                var income = _incomeService.GetIncomeById(incomeId);
                if (income != null)
                {
                    txtIncomeNo.Text = income.IncomeNo;
                    dtpIncomeDate.Value = income.IncomeDate;
                    txtAmount.Text = income.Amount.ToString("F2");
                    txtDescription.Text = income.Description;
                    txtCreator.Text = income.Creator;
                    dtpCreatedAt.Value = income.CreatedAt;

                    // 设置会计科目
                    for (int i = 0; i < cmbSubject.Items.Count; i++)
                    {
                        var item = (dynamic)cmbSubject.Items[i];
                        if (item.Value == income.SubjectId)
                        {
                            cmbSubject.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载收入记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInput())
                {
                    return;
                }

                // 创建或更新收入记录
                Income income = new Income();

                if (_incomeId.HasValue)
                {
                    income.Id = _incomeId.Value;
                    income.IncomeNo = txtIncomeNo.Text.Trim();
                    income.CreatedAt = dtpCreatedAt.Value;
                    income.Creator = txtCreator.Text.Trim();
                }

                income.IncomeDate = dtpIncomeDate.Value;
                income.SubjectId = (int)((dynamic)cmbSubject.SelectedItem).Value;
                income.Amount = decimal.Parse(txtAmount.Text.Trim());
                income.Description = txtDescription.Text.Trim();

                if (_incomeId.HasValue)
                {
                    // 更新收入记录
                    _incomeService.UpdateIncome(income);
                    MessageBox.Show("收入记录更新成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // 创建收入记录
                    _incomeService.CreateIncome(income);
                    MessageBox.Show("收入记录添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存收入记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // 验证会计科目
            if (cmbSubject.SelectedIndex == -1)
            {
                MessageBox.Show("请选择会计科目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbSubject.Focus();
                return false;
            }

            // 验证金额
            decimal amount;
            if (!decimal.TryParse(txtAmount.Text.Trim(), out amount) || amount <= 0)
            {
                MessageBox.Show("请输入有效的金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAmount.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}