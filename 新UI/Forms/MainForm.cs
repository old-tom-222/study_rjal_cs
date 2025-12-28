using System;
using System.Windows.Forms;
using CSproject.Business.Services;

namespace CSproject.UI.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化应用程序，例如检查数据库连接等
            try
            {
                // 测试数据库连接
                var departmentService = new DepartmentService();
                departmentService.TestConnection();
                toolStripStatusLabel1.Text = "数据库连接成功 - 欢迎使用财务管理系统";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = "数据库连接失败: " + ex.Message;
                MessageBox.Show("数据库连接失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 收入管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<IncomeListForm>("收入管理");
        }

        private void 支出管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<ExpenseListForm>("支出管理");
        }

        private void 会计科目管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<AccountingSubjectListForm>("会计科目管理");
        }

        private void 部门管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<DepartmentListForm>("部门管理");
        }

        private void 员工管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<EmployeeListForm>("员工管理");
        }

        private void 银行账户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm<BankAccountListForm>("银行账户管理");
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出系统吗？", "确认退出", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 打开子窗体
        /// </summary>
        /// <typeparam name="T">窗体类型</typeparam>
        /// <param name="title">窗体标题</param>
        private void OpenChildForm<T>(string title) where T : Form, new()
        {
            // 检查是否已经打开了相同类型的窗体
            foreach (Form form in this.MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }

            // 创建新窗体
            T childForm = new T();
            childForm.MdiParent = this;
            childForm.Text = title;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }
    }
}