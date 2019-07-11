﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 默认为 false,即不接受用户拖动到其上的控件
            this.groupBox1.AllowDrop = true;
            this.groupBox2.AllowDrop = true;
            // 拖动对象进入控件边界时触发
            this.groupBox1.DragEnter += new DragEventHandler(groupBox_DragEnter);
            this.groupBox2.DragEnter += new DragEventHandler(groupBox_DragEnter);
            // 完成拖动时触发
            this.groupBox1.DragDrop += new DragEventHandler(groupBox_DragDrop);
            this.groupBox2.DragDrop += new DragEventHandler(groupBox_DragDrop);
        }

        /// <summary>
        /// 该指标指示被拖动的对象是否进入了控件的边界
        /// </summary>
        bool bMouseDown;

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateControls();
        }

        /// <summary>
        /// 生成一定数量的控件,本例中使用 Button
        /// 注册 Button 的鼠标点击事件
        /// </summary>
        private void CreateControls()
        {
            int x = 15;
            int y = 15;
            Button btn = null;
            for (int i = 1; i <= 5; i++)
            {
                btn = new Button();
                btn.Left = x;
                btn.Top = y;
                btn.Text = "Button " + i;
                btn.Width = 100;
                btn.Height = 50;
                x += btn.Width + 15;
                if (btn.Width > groupBox1.Width - x)
                {
                    x = 15;
                    y += btn.Height + 15;
                }
                btn.AllowDrop = true; // 默认为 false,即不可拖动
                btn.MouseDown += new MouseEventHandler(btn_MouseDown);
                this.groupBox1.Controls.Add(btn);
            }
        }

        /// <summary>
        /// 拖动对象进入本控件的边界
        /// </summary>
        void groupBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            bMouseDown = true;
        }

        /// <summary>
        /// 拖放操作完成
        /// </summary>
        void groupBox_DragDrop(object sender, DragEventArgs e)
        {
            if (bMouseDown)
            {
                // 从事件参数 DragEventArgs 中获取被拖动的元素
                string controlType = e.Data.GetFormats()[0];
                System.Type type = Type.GetType(controlType + ", System.Windows.Forms, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 ");
                Control btn = (Control)e.Data.GetData(type);
                GroupBox grp = (GroupBox)btn.Parent;
                ((GroupBox)sender).Controls.Add(btn);
                Point newPoint = PointToScreen(((GroupBox)sender).Location);
                btn.Location = new Point(e.X - newPoint.X, e.Y - newPoint.Y);
                bMouseDown = false;
            }
        }

        /// <summary>
        /// 按下鼠标后即开始执行拖放操作
        /// 这里指定了拖放操作的最终效果为一个枚举值: Move
        /// </summary>
        void btn_MouseDown(object sender, MouseEventArgs e)
        {
            Control btn = (Control)sender;
            btn.DoDragDrop(btn, DragDropEffects.Move);
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Control con = (Control)sender;
            con.DoDragDrop(con, DragDropEffects.Move);
        }

        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Control con = (Control)sender;
            con.DoDragDrop(con, DragDropEffects.Move);
        }
    }
}
