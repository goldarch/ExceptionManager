using System;
using System.Windows.Forms;

namespace 全局错误报告测试
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button零除测试_Click(object sender, System.EventArgs e)
        {
            var a = 0;
            var c = 1 / a;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            throw new Exception("message抛出测试!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        }
    }
}
