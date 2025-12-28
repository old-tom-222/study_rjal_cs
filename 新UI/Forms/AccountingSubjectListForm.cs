using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class AccountingSubjectListForm : Form
    {
        private AccountingSubjectService _subjectService = new AccountingSubjectService();

        public AccountingSubjectListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void AccountingSubjectListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有会计科目记录
            LoadSubjects();
        }

        private void InitializeDataGridView()
        {
            // 配置DataGridView的列
            dgvSubjects.AutoGenerateColumns = false;
            dgvSubjects.Columns.Clear();

            // 科目ID列（隐藏）
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            // 科目编号列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Code",
                HeaderText = "科目编号",
                DataPropertyName = "Code",
                Width = 120
            });

            // 科目名称列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "科目名称",
                DataPropertyName = "Name",
                Width = 150
            });

            // 科目类型列
            dgvSubjects.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Type",
                HeaderText = "科目类型",
                DataPropertyName = "Type",
                Width = 100,
                DataSource = new object[] { "资产", "负债", "所有者权益", "成本", "损益" },
                ReadOnly = true
            });

            // 科目级别列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Level",
                HeaderText = "科目级别",
                DataPropertyName = "Level",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            // 父科目名称列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ParentName",
                HeaderText = "父科目",
                DataPropertyName = "ParentName",
                Width = 150
            });

            // 状态列
            dgvSubjects.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Status",
                HeaderText = "状态",
                DataPropertyName = "Status",
                Width = 100,
                DataSource = new object[] { "启用", "禁用" },
                ReadOnly = true
            });

            // 创建人列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Creator",
                HeaderText = "创建人",
                DataPropertyName = "Creator",
                Width = 100
            });

            // 创建日期列
            dgvSubjects.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvSubjects.Columns.Add(actionColumn);

            // 配置DataGridView的其他属性
            dgvSubjects.AllowUserToAddRows = false;
            dgvSubjects.ReadOnly = true;
            dgvSubjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSubjects.MultiSelect = false;
        }

        private void LoadSubjects()
        {
            try
            {
                // 获取筛选条件
                string code = txtCode.Text.Trim();
                string name = txtName.Text.Trim();
                int? type = null;
                if (cmbType.SelectedIndex > 0)
                    type = cmbType.SelectedIndex - 1;
                bool? isActive = null;
                if (cmbStatus.SelectedIndex == 1)
                    isActive = true;
                else if (cmbStatus.SelectedIndex == 2)
                    isActive = false;

                // 加载会计科目列表
                var subjects = _subjectService.GetAllSubjects(code, name, type, isActive);
                dgvSubjects.DataSource = subjects;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载会计科目记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 重置筛选条件
            txtCode.Text = "";
            txtName.Text = "";
            cmbType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            LoadSubjects();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 打开添加会计科目表单
            AccountingSubjectForm subjectForm = new AccountingSubjectForm();
            if (subjectForm.ShowDialog() == DialogResult.OK)
            {
                LoadSubjects(); // 重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                int subjectId = (int)dgvSubjects.SelectedRows[0].Cells["Id"].Value;
                AccountingSubjectForm subjectForm = new AccountingSubjectForm(subjectId);
                if (subjectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSubjects(); // 重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的会计科目记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count > 0)
            {
                int subjectId = (int)dgvSubjects.SelectedRows[0].Cells["Id"].Value;
                string subjectName = dgvSubjects.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show("确定要删除会计科目 " + subjectName + " 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _subjectService.DeleteSubject(subjectId);
                        MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSubjects(); // 重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的会计科目记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvSubjects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理操作列的点击事件
            if (e.ColumnIndex == dgvSubjects.Columns["Action"].Index && e.RowIndex >= 0)
            {
                int subjectId = (int)dgvSubjects.Rows[e.RowIndex].Cells["Id"].Value;
                AccountingSubjectForm subjectForm = new AccountingSubjectForm(subjectId);
                if (subjectForm.ShowDialog() == DialogResult.OK)
                {
                    LoadSubjects(); // 重新加载数据
                }
            }
        }
    }
}