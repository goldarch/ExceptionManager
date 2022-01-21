using System;
using System.Text;
using System.Windows.Forms;

namespace 全局错误报告测试.ExceptionManager
{
    public partial class FormShowException : Form
    {
        private Exception _ex;

        public FormShowException()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //本按键可以加上权限判断，非专业人员不能看(detailRight )
            new FormShowExceptionDetail(_ex).Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LogManager.GetLogger("").Error(GetErrors());
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        /// <param name="detailRight">查看明细的权限</param>
        public void Show(string sender, Exception ex, bool detailRight = false)
        {
            _ex = ex;
            Show(GetInnerExceptionMsg(), ex.Message, detailRight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ex"></param>
        /// <param name="custormString"></param>
        /// <param name="detailRight">查看明细的权限</param>
        public void Show(string sender, Exception ex, string custormString, bool detailRight = false)
        {
            _ex = ex;
            Show(custormString, ex.Message, detailRight);
        }

        void Show(string summaryString, string descriptionString, bool detailRight)
        {
            textBox摘要.Text = summaryString;

            //内容相同时，不重复显示
            if (!summaryString.Equals(descriptionString))
            {
                textBoxDescription.Text = descriptionString;
            }

            button1.Enabled = detailRight;
        }

        private string GetErrors()
        {
            var stackTraceAll = new StringBuilder();
            //
            //_ex.IsFatal();
            //也可以打平再处理
            //https://stackoverflow.com/questions/19266338/catching-aggregateexception
            //Fatal的理解：实现一个flatten函数，将一个嵌套多层的数组 array（数组） (嵌套可以是任何层数)转换为只有一层的数组
            //，数组中元素仅基本类型的元素或数组，https://www.cnblogs.com/Mie929094441/p/7462967.html

            var i = 0;

            var ex = _ex;

            while (ex != null)
            {
                var err = $"异常类型:{Environment.NewLine}" +
                          $"{ex.GetType().FullName}{Environment.NewLine}" +
                          "".PadRight(30, '-') + Environment.NewLine +
                          $"异常模块:{Environment.NewLine}" +
                          $"{ex.Source}{Environment.NewLine}" +
                          "".PadRight(30, '-') + Environment.NewLine +
                          "异常信息:" + Environment.NewLine +
                          $"{ex.Message}{Environment.NewLine}" +
                          "".PadRight(30, '-') + Environment.NewLine +
                          "异常跟踪:" + Environment.NewLine +
                          $"{ex.StackTrace}" + Environment.NewLine +
                          "".PadRight(30, '-') + Environment.NewLine;

                stackTraceAll.Append(
                    $"[{i}]{Environment.NewLine}=========={Environment.NewLine}{err}=========={Environment.NewLine}");

                ex = ex.InnerException;
                i++;
            }

            return stackTraceAll.ToString();
        }

        private string GetInnerExceptionMsg()
        {
            var ex = _ex;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex == null ? "" : ex.Message;
        }

        /// <summary>
        /// 生成自定义异常消息
        /// 原文链接：https://blog.csdn.net/scimence/article/details/81871185
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常信息****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }

            sb.AppendLine("***************************************************************");

            return sb.ToString();
        }

        //http://stackoverflow.com/questions/3729899/opening-a-winform-with-topmost-true-but-not-having-it-steal-focus
        //This is what worked for me.It provides TopMost but without focus-stealing.

        //    protected override bool ShowWithoutActivation
        //        {
        //            get { return true; }
        //        }
        //        private const int WS_EX_TOPMOST = 0x00000008;
        //        protected override CreateParams CreateParams
        //        {
        //            get
        //            {
        //                CreateParams createParams = base.CreateParams;
        //                createParams.ExStyle |= WS_EX_TOPMOST;
        //                return createParams;
        //            }
        //        }
        //        Remember to omit setting TopMost in Visual Studio designer, or elsewhere.
        //        This is stolen, err, borrowed, from here (click on Workarounds):
        //https://connect.microsoft.com/VisualStudio/feedback/details/401311/showwithoutactivation-is-not-supported-with-topmost


        //以下两个过程控制目的：
        //1》激活但不得到焦点
        //2》显示在最上层
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WsExTopmost = 0x00000008;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WsExTopmost;
                return createParams;
            }
        }
    }
}