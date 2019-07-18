using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
 

namespace WpfDependency
{
    public class Program
    {
        //首先添加Program.cs文件
        //然后设置项目属性-应用程序-启动对象，选择Program[当前项目的命名空间.Program]
        public const string processName = "XXXX";
        [STAThread]
        [DebuggerNonUserCode()]//设置degub时当前文件不进断点
        [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {
            try
            { 
                Process[] ps = Process.GetProcesses();
                int count = 0;
                foreach (Process item in ps)
                {
                    string currentProcessName = item.ProcessName.ToLower();
                    string processNameLower = processName.ToLower();
                    if (currentProcessName.Contains(processNameLower))
                    {
                        count++;

                    }
                }
                if (count > 1)
                {
                    
                    MessageBox.Show("您已运行了系统！", "警告");
                    return;
                }
                WpfDependency.App app = new WpfDependency.App();
                app.InitializeComponent();
                app.Run();

            }
            catch (Exception ex)
            {
             
            }
        }
    }
}
