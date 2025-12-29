using System;
using System.Windows.Forms;
using CSproject.Data.Helpers;

namespace CSproject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Console.WriteLine("Form1构造函数开始执行...");
            InitializeComponent();
            Console.WriteLine("Form1组件初始化完成...");
            
            // 设置表单启动位置和状态
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            Console.WriteLine("Form1启动位置和状态设置完成...");
            
            // 确保窗口可见
            this.Visible = true;
            Console.WriteLine("Form1.Visible设置为true...");
            
            // 添加Load事件处理程序
            this.Load += Form1_Load;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Form1_Load事件触发...");
            Console.WriteLine("窗口大小: " + this.Size.ToString());
            Console.WriteLine("窗口位置: " + this.Location.ToString());
            Console.WriteLine("窗口可见性: " + this.Visible.ToString());
            Console.WriteLine("panelLogin可见性: " + panelLogin.Visible.ToString());
        }

        private void BtnLoginClick(object sender, EventArgs e)
        {
            try
            {
                // 仅响应登录按钮点击，避免误触发
                if (!(sender is Button) || ((Button)sender) != btnLogin)
                {
                    return;
                }
                // 如果不在登录界面，忽略误触发
                if (!panelLogin.Visible)
                {
                    return;
                }

                // 添加UI控件的空引用检查
                if (txtAccount == null || txtPassword == null)
                {
                    MessageBox.Show("登录界面控件未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 验证输入
                if (string.IsNullOrEmpty(txtAccount.Text))
                {
                    MessageBox.Show("请输入账号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtAccount.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                
                // 提示用户正在连接数据库
                MessageBox.Show("正在连接数据库...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // 先测试数据库连接状态
                if (!DbHelper.TestConnection())
                {
                    MessageBox.Show("数据库连接失败，请检查数据库服务是否启动以及连接配置是否正确。\n错误：无法连接到数据库服务器。", "连接错误", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 验证用户登录
                if (DbHelper.ValidateUser(txtAccount.Text, txtPassword.Text))
                {
                    // 登录成功，跳转到Form2
                    MessageBox.Show("登录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    CSproject.UI.Forms.Form2 form2 = new CSproject.UI.Forms.Form2();
                    form2.ShowDialog();
                    this.Close();
                }
                else
                {
                    // 登录失败
                    MessageBox.Show("账号或密码错误，请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // 如果连接失败，显示错误消息
                MessageBox.Show("登录失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGoRegisterClick(object sender, EventArgs e)
        {
            // 切换到注册界面
            panelLogin.Visible = false;
            panelRegister.Visible = true;
            this.AcceptButton = btnRegisterAndLogin;
            // 安全地设置角色下拉框的默认值，避免空引用异常和无效索引错误
            if (cmbRole != null && cmbRole.Items.Count > 0 && cmbRole.SelectedIndex < 0)
            {
                cmbRole.SelectedIndex = 0;
            }
            // 添加空引用检查
            if (txtRegAccount != null) txtRegAccount.Text = "";
            if (txtRegPassword != null) txtRegPassword.Text = "";
            if (txtName != null) txtName.Text = "";
            if (txtRegAccount != null) txtRegAccount.Focus();
        }

        private void BtnBackClick(object sender, EventArgs e)
        {
            // 返回登录界面
            panelRegister.Visible = false;
            panelLogin.Visible = true;
            this.AcceptButton = btnLogin;
            txtAccount.Focus();
        }

        private void BtnRegisterAndLoginClick(object sender, EventArgs e)
        {
            try
            {
                // 添加UI控件的空引用检查
                if (txtRegAccount == null || txtRegPassword == null || txtName == null || cmbRole == null)
                {
                    MessageBox.Show("注册界面控件未正确初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // 不需要检查静态类DbHelper是否为null，因为静态类永远不会为null

                // 验证输入（注册界面）
                if (string.IsNullOrEmpty(txtRegAccount.Text))
                {
                    MessageBox.Show("请输入账号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRegAccount.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtRegPassword.Text))
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRegPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("请输入姓名", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                var role = (cmbRole.SelectedItem == null || string.IsNullOrWhiteSpace(cmbRole.SelectedItem.ToString())) ? "staff" : cmbRole.SelectedItem.ToString();

                // 检查用户是否已存在
                if (DbHelper.UserExists(txtRegAccount.Text))
                {
                    MessageBox.Show("该账号已被注册，请使用其他账号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 添加新用户
                if (DbHelper.AddUser(txtRegAccount.Text, txtRegPassword.Text, txtName.Text, role))
                {
                    // 注册成功后直接登录并进入后续界面
                    MessageBox.Show("注册成功，正在登录...", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    CSproject.UI.Forms.Form2 form2 = new CSproject.UI.Forms.Form2();
                    form2.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("注册失败，请稍后重试", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("注册失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
