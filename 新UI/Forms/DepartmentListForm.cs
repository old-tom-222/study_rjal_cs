using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSproject.UI.Forms
{
    public partial class DepartmentListForm : Form
    {
        private DepartmentService _departmentService = new DepartmentService();

        public DepartmentListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void DepartmentListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有部门记录
            LoadDepartments();
        }

        private void InitializeDataGridView()
        {
            // 配置DataGridView的列
            dgvDepartments.AutoGenerateColumns = false;
            dgvDepartments.Columns.Clear();

            // 部门ID列（隐藏）
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                Visible = false
            });

            // 部门编号列
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Code",
                HeaderText = "部门编号",
                DataPropertyName = "Code",
                Width = 120
            });

            // 部门名称列
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "部门名称",
                DataPropertyName = "Name",
                Width = 150
            });

            // 部门描述列
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "部门描述",
                DataPropertyName = "Description",
                Width = 200
            });

            // 状态列
            dgvDepartments.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Status",
                HeaderText = "状态",
                DataPropertyName = "Status",
                Width = 100,
                DataSource = new object[] { "启用", "禁用" },
                ReadOnly = true
            });

            // 创建人列
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Creator",
                HeaderText = "创建人",
                DataPropertyName = "Creator",
                Width = 100
            });

            // 创建日期列
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn
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
            dgvDepartments.Columns.Add(actionColumn);

            // 配置DataGridView的其他属性
            dgvDepartments.AllowUserToAddRows = false;
            dgvDepartments.ReadOnly = true;
            dgvDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDepartments.MultiSelect = false;
        }

        private void LoadDepartments()
        {
            try
            {
                // 获取筛选条件
                string code = txtCode.Text.Trim();
                string name = txtName.Text.Trim();
                bool? isActive = null;
                if (cmbStatus.SelectedIndex == 1)
                    isActive = true;
                else if (cmbStatus.SelectedIndex == 2)
                    isActive = false;

                // 加载部门列表
                var departments = _departmentService.GetAllDepartments(code, name, isActive);
                dgvDepartments.DataSource = departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门记录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 重置筛选条件
            txtCode.Text = "";
            txtName.Text = "";
            cmbStatus.SelectedIndex = 0;
            LoadDepartments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 打开添加部门表单
            DepartmentForm departmentForm = new DepartmentForm();
            if (departmentForm.ShowDialog() == DialogResult.OK)
            {
                LoadDepartments(); // 重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count > 0)
            {
                int departmentId = (int)dgvDepartments.SelectedRows[0].Cells["Id"].Value;
                DepartmentForm departmentForm = new DepartmentForm(departmentId);
                if (departmentForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDepartments(); // 重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的部门记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.SelectedRows.Count > 0)
            {
                int departmentId = (int)dgvDepartments.SelectedRows[0].Cells["Id"].Value;
                string departmentName = dgvDepartments.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show("确定要删除部门 " + departmentName + " 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _departmentService.DeleteDepartment(departmentId);
                        MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDepartments(); // 重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的部门记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDepartments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 处理操作列的点击事件
            if (e.ColumnIndex == dgvDepartments.Columns["Action"].Index && e.RowIndex >= 0)
            {
                int departmentId = (int)dgvDepartments.Rows[e.RowIndex].Cells["Id"].Value;
                DepartmentForm departmentForm = new DepartmentForm(departmentId);
                if (departmentForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDepartments(); // 重新加载数据
                }
            }
        }
    }
}