using System;
using System.Text;
using System.Windows.Forms;

namespace 全局错误报告测试.ExceptionManager
{
    public partial class FormShowExceptionDetail : Form
    {
        private Exception _ex;

        public FormShowExceptionDetail(Exception ex)
        {
            InitializeComponent();
            _ex = ex;

            ShowStackTrace();
        }

        private void ShowStackTrace()
        {
            var stackTraceAll=new StringBuilder();

            var i = 0;
            while (_ex != null)
            {
                var node = treeView1.Nodes.Add("e1", $"[{i}]" + _ex.Message);

                node.Tag = $"异常类型:{Environment.NewLine}" +
                           $"{_ex.GetType().FullName}{Environment.NewLine}" +
                           "".PadRight(30, '-') + Environment.NewLine +
                           $"异常模块:{Environment.NewLine}" +
                           $"{_ex.Source}{Environment.NewLine}" +
                           "".PadRight(30, '-') + Environment.NewLine +
                           "异常信息:"+Environment.NewLine+
                           $"{_ex.Message}{Environment.NewLine}" +
                           "".PadRight(30, '-') + Environment.NewLine +
                           "异常跟踪:" + Environment.NewLine +
                           $"{_ex.StackTrace}" + Environment.NewLine +
                           "".PadRight(30, '-') + Environment.NewLine;

                stackTraceAll.Append($"[{i}]{Environment.NewLine}=========={Environment.NewLine}{node.Tag}=========={Environment.NewLine}") ;

                _ex = _ex.InnerException;
                i++;
            }

            treeView1.Nodes.Add("","[*]StackTrace").Tag=stackTraceAll;

            treeView1.SelectedNode = treeView1.Nodes[0];
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                textBox1.Text = e.Node.Tag.ToString();
            }
        }
    }
}