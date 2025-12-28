using System;
using System.Windows.Forms;
using CSproject.Business.Services;
using CSproject.Business.Models;

namespace CSproject.UI.Forms
{
    public partial class DepartmentForm : Form
    {
        private DepartmentService _departmentService = new DepartmentService();

        private int _departmentId = 0;
        private bool _isEditMode = false;

        public DepartmentForm()
        {
            InitializeComponent();
            _isEditMode = false;
        }

        public DepartmentForm(int departmentId)
        {
            InitializeComponent();
            _departmentId = departmentId;
            _isEditMode = true;
            LoadDepartmentData();
        }

        private void LoadDepartmentData()
        {
            try
            {
                var department = _departmentService.GetDepartmentById(_departmentId);
                if (department != null)
                {
                    txtCode.Text = department.Code;
                    txtName.Text = department.Name;
                    txtDescription.Text = department.Description;
                    chkStatus.Checked = department.Status == "启用";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载部门数据失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var department = new DepartmentModel
                {
                    Code = txtCode.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    Status = chkStatus.Checked ? "启用" : "禁用",
                    CreatedBy = "当前用户" // 实际应用中应该从登录信息获取
                };

                // 根据模式执行保存或更新操作
                if (_isEditMode)
                {
                    department.Id = _departmentId;
                    department.UpdatedBy = "当前用户";
                    _departmentService.UpdateDepartment(department);
                    MessageBox.Show("部门信息已成功更新", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _departmentService.CreateDepartment(department);
                    MessageBox.Show("部门信息已成功创建", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // 关闭表单
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存部门信息失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // 验证部门编号
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("请输入部门编号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();
                return false;
            }

            // 验证部门名称
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("请输入部门名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Focus();
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