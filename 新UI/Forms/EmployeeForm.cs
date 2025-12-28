using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class EmployeeForm : Form
    {
        private EmployeeService _employeeService = new EmployeeService();
        private DepartmentService _departmentService = new DepartmentService();
        private int _employeeId = 0;
        private bool _isEditMode = false;

        public EmployeeForm()
        {
            InitializeComponent();
            this.Text = "添加员工";
            InitializeControls();
        }

        public EmployeeForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            _isEditMode = true;
            this.Text = "编辑员工";
            InitializeControls();
            LoadEmployeeData();
        }

        private void InitializeControls()
        {
            // 加载部门数据到下拉框
            try
            {
                var departments = _departmentService.GetAllDepartments("", "", "启用");
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "Name";
                cmbDepartment.ValueMember = "Id";
                cmbDepartment.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 设置默认日期为当前日期
            dtpCreateDate.Value = DateTime.Now;
        }

        private void LoadEmployeeData()
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(_employeeId);
                if (employee != null)
                {
                    txtEmployeeId.Text = employee.EmployeeId;
                    txtName.Text = employee.Name;
                    cmbDepartment.SelectedValue = employee.DepartmentId;
                    txtPhone.Text = employee.Phone;
                    txtEmail.Text = employee.Email;
                    dtpCreateDate.Value = employee.CreateDate;
                    chkStatus.Checked = employee.Status == "启用";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载员工数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtEmployeeId.Text.Trim()))
            {
                MessageBox.Show("请输入员工编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeId.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBox.Show("请输入员工姓名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cmbDepartment.SelectedValue == null || cmbDepartment.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择所属部门", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDepartment.Focus();
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
                Employee employee = new Employee
                {
                    Id = _isEditMode ? _employeeId : 0,
                    EmployeeId = txtEmployeeId.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    DepartmentId = Convert.ToInt32(cmbDepartment.SelectedValue),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    CreateDate = dtpCreateDate.Value,
                    Status = chkStatus.Checked ? "启用" : "禁用"
                };

                if (_isEditMode)
                {
                    _employeeService.UpdateEmployee(employee);
                    MessageBox.Show("员工信息更新成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _employeeService.AddEmployee(employee);
                    MessageBox.Show("员工添加成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(_isEditMode ? "更新员工信息失败：" + ex.Message : "添加员工失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}