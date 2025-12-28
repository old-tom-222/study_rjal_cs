using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class EmployeeListForm : Form
    {
        private EmployeeService _employeeService = new EmployeeService();
        private DepartmentService _departmentService = new DepartmentService();

        public EmployeeListForm()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            // 设置DataGridView的属性
            dgvEmployees.AutoGenerateColumns = false;
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // 定义列
            DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
            colId.DataPropertyName = "Id";
            colId.HeaderText = "员工ID";
            colId.Visible = false;
            dgvEmployees.Columns.Add(colId);

            DataGridViewTextBoxColumn colEmployeeId = new DataGridViewTextBoxColumn();
            colEmployeeId.DataPropertyName = "EmployeeId";
            colEmployeeId.HeaderText = "员工编号";
            colEmployeeId.Width = 100;
            dgvEmployees.Columns.Add(colEmployeeId);

            DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "Name";
            colName.HeaderText = "姓名";
            colName.Width = 100;
            dgvEmployees.Columns.Add(colName);

            DataGridViewTextBoxColumn colDepartment = new DataGridViewTextBoxColumn();
            colDepartment.DataPropertyName = "DepartmentName";
            colDepartment.HeaderText = "所属部门";
            colDepartment.Width = 150;
            dgvEmployees.Columns.Add(colDepartment);

            DataGridViewTextBoxColumn colPhone = new DataGridViewTextBoxColumn();
            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "电话";
            colPhone.Width = 120;
            dgvEmployees.Columns.Add(colPhone);

            DataGridViewTextBoxColumn colEmail = new DataGridViewTextBoxColumn();
            colEmail.DataPropertyName = "Email";
            colEmail.HeaderText = "邮箱";
            colEmail.Width = 150;
            dgvEmployees.Columns.Add(colEmail);

            DataGridViewTextBoxColumn colCreateDate = new DataGridViewTextBoxColumn();
            colCreateDate.DataPropertyName = "CreateDate";
            colCreateDate.HeaderText = "入职日期";
            colCreateDate.Width = 120;
            colCreateDate.DefaultCellStyle.Format = "yyyy-MM-dd";
            dgvEmployees.Columns.Add(colCreateDate);

            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "状态";
            colStatus.Width = 80;
            dgvEmployees.Columns.Add(colStatus);
        }

        private void EmployeeListForm_Load(object sender, EventArgs e)
        {
            // 默认加载所有员工
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                string employeeId = txtEmployeeId.Text.Trim();
                string name = txtName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string status = cmbStatus.Text.Trim();

                var employees = _employeeService.GetAllEmployees(employeeId, name, phone, status);
                
                // 获取部门名称
                foreach (var employee in employees)
                {
                    if (employee.DepartmentId > 0)
                    {
                        var department = _departmentService.GetDepartmentById(employee.DepartmentId);
                        if (department != null)
                        {
                            employee.DepartmentName = department.Name;
                        }
                    }
                }

                dgvEmployees.DataSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载员工数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 清空筛选条件
            txtEmployeeId.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            cmbStatus.SelectedIndex = -1;

            // 重新加载数据
            LoadEmployees();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            if (employeeForm.ShowDialog() == DialogResult.OK)
            {
                LoadEmployees(); // 添加成功后重新加载数据
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int employeeId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);
                EmployeeForm employeeForm = new EmployeeForm(employeeId);
                if (employeeForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees(); // 编辑成功后重新加载数据
                }
            }
            else
            {
                MessageBox.Show("请选择要编辑的员工", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int employeeId = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["Id"].Value);
                string employeeName = dgvEmployees.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show($"确定要删除员工 '{employeeName}' 吗？", "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _employeeService.DeleteEmployee(employeeId);
                        MessageBox.Show("员工删除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadEmployees(); // 删除成功后重新加载数据
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("删除员工失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的员工", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}