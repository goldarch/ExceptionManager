using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using 全局错误报告测试.ExceptionManager;

namespace 全局错误报告测试
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RunningExceptionTool.Run(MainCall, SendErrors, args);
        }

        private static void MainCall(string[] args)
        {
            Application.Run(new Form1());
        }

        /// <summary>
        /// 自定义异常信息处理逻辑
        /// </summary>
        private static void SendErrors(object sender, Exception ex, string customString)
        {
            // 自定义异常信息处理逻辑
            //比如上传log信息，其实可以通过这里处理"错误"部分的！

        }
    }
}