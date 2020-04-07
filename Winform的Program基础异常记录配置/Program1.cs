using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace VanPeng.Desktop
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
            System.Threading.Thread.Sleep(2000);
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcessesByName(process.ProcessName))
            {
                if (p.Id != process.Id)
                {
                    //关闭第二个启动的程序  
                    MessageBox.Show("当前程序已启动，请耐心等待……");
                    Environment.Exit(0);
                    return;

                }
            }
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.ThreadExit += new EventHandler(Application_ThreadExit);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
          
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.LogStart();
            Exception ex = (Exception)e.ExceptionObject;
            Logger.Error("当前域卸载异常", ex);
            Logger.LogEnd();
        }

        static void Application_ThreadExit(object sender, EventArgs e)
        {
            Logger.LogStart();

            //注销静态事件
            Application.ThreadException -= new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.ThreadExit -= new EventHandler(Application_ThreadExit);
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            if (sender != null)
                Logger.Debug("Application_ThreadExit.sender:" + sender.ToString());
            if (e != null)
                Logger.Debug("Application_ThreadExit.EventArgs:" + e.ToString());

            Logger.LogEnd();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Logger.LogStart();
            Exception ex = (Exception)e.Exception;
            Logger.Error(ex);
            Logger.LogEnd();
        }
    }
}
