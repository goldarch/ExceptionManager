using System;
using System.Windows.Forms;

namespace 全局错误报告测试.ExceptionManager
{
    //借鉴 《C# 运行异常捕获》 https://blog.csdn.net/scimence/article/details/81871185?spm=1001.2014.3001.5501 重新封装
    //
    public class RunningExceptionTool
    {
        /// <summary>
        /// 定义委托接口处理函数，调用此类中的Main函数为应用添加异常信息捕获
        /// </summary>
        public delegate void MainFunction(string[] args = null);

        /// <summary>
        /// 异常信息回调处理逻辑,比如用于日志或其它的处理，并非替代默认的异常处理，而是对处理的延伸
        /// </summary>
        public delegate void ExceptionExtendedProcessing(object sender, Exception ex, string custormString);

        private static ExceptionExtendedProcessing _exCall = null;

        /*
        异常是通过Throw命令抛出，一路从抛出的模块里上抛，
        如果中途没有被try...catch...抓住的话就会一直抛到CLR（公共语言运行时）。
        如果用栈来描述这个过程的话，那就是异常会从栈的栈顶一路下沉，直到中途被try...catch...抓住或者直至沉到栈底，被CLR接住。
        CLR接收到异常之后的处理方式非常的简单粗暴——直接报错，然后关闭程序。

        不过根据刚刚我们所描述的异常上抛过程，我们不难发现：只要我们在程序把异常抛给CLR之前，抢先把异常捕获，那就可以做到全局异常处理了。
        不过这个try...catch...就必须放在栈的最下方。
        程序运行时栈的最下方函数其实就是程序运行时第一个调用的函数——main()函数。

         */

        /// <summary>
        /// MainFunction是主处理程序，可以为空！则Run只处理异常绑定，其它的不处理！
        /// </summary>
        /// <param name="main">把主程序传递过来的原因，是把其包括在一个根try中</param>
        /// <param name="exCall"></param>
        /// <param name="args"></param>
        public static void Run(MainFunction main, ExceptionExtendedProcessing exCall = null, string[] args = null)
        {
            try
            {
                if (_exCall != null) _exCall = exCall;

                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                //Application.ThreadException事件只能捕获程序主线程上发生的异常
                Application.ThreadException +=
                    new System.Threading.ThreadExceptionEventHandler(ApplicationThreadException);
                //子线程异常捕获。线程之间是不可能发生异常处理的交换关系的。子线程不会把异常交换（传递）给主线程的
                AppDomain.CurrentDomain.UnhandledException +=
                    new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);

                if (main != null)
                {
                    if (args == null) main();
                    else main(args);
                }
            }
            catch (Exception ex)
            {
                //只有上面的异常处理没有截获的，才会流到这里！
                ShowException(null,ex,"程序发现未能处理的异常！请截图联系信息管理人员");
                //
                if (_exCall != null) _exCall(null, ex, "");
            }
        }

        public static void ShowException(object sender, Exception ex)
        {
            var frm = new FormShowException();
            var senderName = sender == null ? "未指定模块" : sender.GetType().Name;
            frm.Show(senderName, ex, true);
            frm.ShowDialog();
        }

        public static void ShowException(object sender, Exception ex, string custormString)
        {
            var frm = new FormShowException();
            var senderName = sender == null ? "未指定模块" : sender.GetType().Name;
            frm.Show(senderName, ex, custormString, true);
            frm.ShowDialog();
        }

        //UI线程异常
        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowException(sender, e.Exception);
        }

        //未处理的异常，主动退出！防止发生数据意外！
        static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowException(sender, (Exception) e.ExceptionObject);

            //多线程异常的处理，要在线程中主动把异常抛给主线程！否则程序会非正常退出！
            //见E:\WIN相关\【02】异常处理\【好】C# 全局异常处理 中相关文章

            //C#子线程抛出的异常给主线程
            //https://www.cnblogs.com/sung/archive/2013/01/11/2856460.html

            //不会由win接管，弹出非正常关闭程序 
            //1为强制关闭
            Environment.Exit(-1);
        }
    }
}