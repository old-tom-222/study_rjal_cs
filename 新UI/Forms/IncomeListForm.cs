using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class IncomeListForm : Form
    {
        private IncomeService _incomeService = new IncomeService();
        private AccountingSubjectService _subjectService = new AccountingSubjectService();

        public IncomeListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeControls();
        }

        private void IncomeListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有收入记录
            LoadIncomes();
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
            dgvIncomes.AutoGenerateColumns = false;
            dgvIncomes.Columns.Clear();

            // 收入ID列（隐藏）
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            // 收入编号列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IncomeNo",
                HeaderText = "收入编号",
                DataPropertyName = "IncomeNo",
                Width = 120
            });

            // 收入日期列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IncomeDate",
                HeaderText = "收入日期",
                DataPropertyName = "IncomeDate",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd" }
            });

            // 会计科目列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Subject",
                HeaderText = "会计科目",
                DataPropertyName = "Subject",
                Width = 150
            });

            // 金额列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "金额",
                DataPropertyName = "Amount",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
            });

            // 描述列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "描述",
                DataPropertyName = "Description",
                Width = 200
            });

            // 创建人列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Creator",
                HeaderText = "创建人",
                DataPropertyName = "Creator",
                Width = 100
            });

            // 创建日期列
            dgvIncomes.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvIncomes.Columns.Add(actionColumn);

            // 配置DataGridView的其他属性
            dgvIncomes.AllowUserToAddRows = false;
            dgvIncomes.ReadOnly = true;
            dgvIncomes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIncomes.MultiSelect = false;
        }

        private void LoadIncomes()
        {
            try
            {
                // 获取筛选条件
                string code = txtCode.Text.Trim();
                int? subjectId = (int)((dynamic)cmbSubject.SelectedItem).Value;
                if (subjectId == 0) subjectId = null;
                DateTime? startDate = dtpStartDate.Checked ? dtpStartDate.Value : (DateTime?)null;
                DateTime? endDate = dtpEndDate.Checked ? dtpEndDate.Value : (DateTime?)null;

                // 加载收入列表
                var incomes = _incomeService.GetAllIncomes(code, subjectId, startDate, endDate);
                dgvIncomes.DataSource = incomes;

                // 计算总计金额
                decimal totalAmount = incomes.Sum(i => i.Amount);
                lblTotal.Text = string.Format("总计：{0:C2}", totalAmount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载收入记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadIncomes();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 重置筛选条件
            txtCode.Text = "";
            cmbSubject.SelectedIndex = 0;
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            LoadIncomes();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 打开添加收入表单
            IncomeForm incomeForm = new IncomeForm();
            if (incomeForm.ShowDialog() == DialogResult.OK)
            {
                LoadIncomes(); // 重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvIncomes.SelectedRows.Count > 0)
            {
                int incomeId = (int)dgvIncomes.SelectedRows[0].Cells["Id"].Value;
                IncomeForm incomeForm = new IncomeForm(incomeId);
                if (incomeForm.ShowDialog() == DialogResult.OK)
                {
                    LoadIncomes(); // 重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的收入记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvIncomes.SelectedRows.Count > 0)
            {
                int incomeId = (int)dgvIncomes.SelectedRows[0].Cells["Id"].Value;
                string incomeNo = dgvIncomes.SelectedRows[0].Cells["IncomeNo"].Value.ToString();

                if (MessageBox.Show("确定要删除收入记录 " + incomeNo + " 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _incomeService.DeleteIncome(incomeId);
                        MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadIncomes(); // 重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的收入记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvIncomes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理操作列的点击事件
            if (e.ColumnIndex == dgvIncomes.Columns["Action"].Index && e.RowIndex >= 0)
            {
                int incomeId = (int)dgvIncomes.Rows[e.RowIndex].Cells["Id"].Value;
                IncomeForm incomeForm = new IncomeForm(incomeId);
                if (incomeForm.ShowDialog() == DialogResult.OK)
                {
                    LoadIncomes(); // 重新加载数据
                }
            }
        }
    }
}