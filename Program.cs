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
            Console.WriteLine("应用程序开始启动...");
            
            try
            {
                Console.WriteLine("初始化应用程序样式...");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Console.WriteLine("初始化数据库...");
                CSproject.Data.Helpers.DbHelper.InitializeDatabaseIfNeeded();
                
                Console.WriteLine("创建登录表单...");
                // 使用Form1登录界面
                Form1 loginForm = new Form1();
                
                Console.WriteLine("显示登录表单...");
                Application.Run(loginForm);
                
                Console.WriteLine("应用程序正常退出");
            }
            catch (Exception ex)
            {
                Console.WriteLine("应用程序启动错误: " + ex.Message);
                Console.WriteLine("堆栈跟踪: " + ex.StackTrace);
                
                // 尝试显示消息框，但如果在无GUI环境中会失败
                try
                {
                    MessageBox.Show("应用程序启动错误: " + ex.Message, "启动错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    Console.WriteLine("无法显示消息框，可能在无GUI环境中运行");
                }
            }
        }
    }
}
