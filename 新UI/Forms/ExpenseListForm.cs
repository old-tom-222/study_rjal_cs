using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class ExpenseListForm : Form
    {
        private ExpenseService _expenseService = new ExpenseService();
        private AccountingSubjectService _subjectService = new AccountingSubjectService();

        public ExpenseListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeControls();
        }

        private void ExpenseListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有支出记录
            LoadExpenses();
            // 设置日期范围为最近一个月
            dtpStartDate.Value = DateTime.Now.AddMonths(-1);
            dtpEndDate.Value = DateTime.Now;
        }

        private void InitializeControls()
        {
            // 初始化会计科目下拉框
            cmbSubject.Items.Clear();
            var subjects = _subjectService.GetAllSubjects(isActive: true);
            cmbSubject.Items.Add(new { Value = 0, Text = "所有科目" });
            foreach (var subject in subjects)
            {
                cmbSubject.Items.Add(new { Value = subject.Id, Text = subject.Code + " - " + subject.Name });
            }
            cmbSubject.DisplayMember = "Text";
            cmbSubject.ValueMember = "Value";
            cmbSubject.SelectedIndex = 0;
        }

        private void InitializeDataGridView()
        {
            // 配置DataGridView的列
            dgvExpenses.AutoGenerateColumns = false;
            dgvExpenses.Columns.Clear();

            // 支出ID列（隐藏）
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            // 支出编号列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpenseNo",
                HeaderText = "支出编号",
                DataPropertyName = "ExpenseNo",
                Width = 120
            });

            // 支出日期列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpenseDate",
                HeaderText = "支出日期",
                DataPropertyName = "ExpenseDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            // 会计科目列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subject",
                HeaderText = "会计科目",
                DataPropertyName = "Subject",
                Width = 150
            });

            // 金额列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "金额",
                DataPropertyName = "Amount",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });

            // 描述列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "描述",
                DataPropertyName = "Description",
                Width = 200
            });

            // 参考号列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reference",
                HeaderText = "参考号",
                DataPropertyName = "Reference",
                Width = 120
            });

            // 创建人列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Creator",
                HeaderText = "创建人",
                DataPropertyName = "Creator",
                Width = 100
            });

            // 创建日期列
            dgvExpenses.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedAt",
                HeaderText = "创建日期",
                DataPropertyName = "CreatedAt",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            });

            // 操作列
            var actionColumn = new DataGridViewButtonColumn
            {
                Name = "Action",
                HeaderText = "操作",
                Text = "查看详情",
                UseColumnTextForButtonValue = true,
                Width = 100
            };
            dgvExpenses.Columns.Add(actionColumn);

            // 配置DataGridView的其他属性
            dgvExpenses.AllowUserToAddRows = false;
            dgvExpenses.ReadOnly = true;
            dgvExpenses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvExpenses.MultiSelect = false;
        }

        private void LoadExpenses()
        {
            try
            {
                // 获取筛选条件
                string code = txtCode.Text.Trim();
                int? subjectId = (int)((dynamic)cmbSubject.SelectedItem).Value;
                if (subjectId == 0) subjectId = null;
                DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;
                DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;

                // 加载支出列表
                var expenses = _expenseService.GetAllExpenses(code, subjectId, startDate, endDate);
                dgvExpenses.DataSource = expenses;

                // 计算总计金额
                decimal totalAmount = expenses.Sum(e => e.Amount);
                lblTotal.Text = string.Format("总计：{0:C2}", totalAmount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载支出记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadExpenses();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 重置筛选条件
            txtCode.Text = "";
            cmbSubject.SelectedIndex = 0;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            LoadExpenses();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 打开添加支出表单
            ExpenseForm expenseForm = new ExpenseForm();
            if (expenseForm.ShowDialog() == DialogResult.OK)
            {
                LoadExpenses(); // 重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count > 0)
            {
                int expenseId = (int)dgvExpenses.SelectedRows[0].Cells["Id"].Value;
                ExpenseForm expenseForm = new ExpenseForm(expenseId);
                if (expenseForm.ShowDialog() == DialogResult.OK)
                {
                    LoadExpenses(); // 重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的支出记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpenses.SelectedRows.Count > 0)
            {
                int expenseId = (int)dgvExpenses.SelectedRows[0].Cells["Id"].Value;
                string expenseNo = dgvExpenses.SelectedRows[0].Cells["ExpenseNo"].Value.ToString();

                if (MessageBox.Show("确定要删除支出记录 " + expenseNo + " 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _expenseService.DeleteExpense(expenseId);
                        MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadExpenses(); // 重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的支出记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvExpenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理操作列的点击事件
            if (e.ColumnIndex == dgvExpenses.Columns["Action"].Index && e.RowIndex >= 0)
            {
                int expenseId = (int)dgvExpenses.Rows[e.RowIndex].Cells["Id"].Value;
                ExpenseForm expenseForm = new ExpenseForm(expenseId);
                if (expenseForm.ShowDialog() == DialogResult.OK)
                {
                    LoadExpenses(); // 重新加载数据
                }
            }
        }
    }
}