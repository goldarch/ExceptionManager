
namespace 全局错误报告测试
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button零除测试 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button零除测试
            // 
            this.button零除测试.Location = new System.Drawing.Point(61, 49);
            this.button零除测试.Name = "button零除测试";
            this.button零除测试.Size = new System.Drawing.Size(161, 23);
            this.button零除测试.TabIndex = 0;
            this.button零除测试.Text = "零除测试";
            this.button零除测试.UseVisualStyleBackColor = true;
            this.button零除测试.Click += new System.EventHandler(this.button零除测试_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(61, 88);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "主动抛出异常";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 181);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button零除测试);
            this.Name = "Form1";
            this.Text = "异常测试";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button零除测试;
        private System.Windows.Forms.Button button1;
    }
}

