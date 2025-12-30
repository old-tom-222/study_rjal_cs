using System;
using System.Windows.Forms;
using System.Drawing;

namespace CSproject
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                CSproject.Data.Helpers.DbHelper.InitializeDatabaseIfNeeded();
                
                // 使用Form1登录界面
                Form1 loginForm = new Form1();
                
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                // 尝试显示消息框，但如果在无GUI环境中会失败
                try
                {
                    MessageBox.Show("应用程序启动错误: " + ex.Message, "启动错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    // 无法显示消息框，可能在无GUI环境中运行
                }
            }
        }
    }
}