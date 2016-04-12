using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD.Tools
{
    class Line : ITool
    {
        private Point? _Previous = null;
        private Pen _Pen;

        private PictureBox _pictureBox;
        private string _name = "Line";

        public event ImageEventHandler Done;
        public event ProgressEventHandler ReportProgress;

        public System.Windows.Forms.PictureBox pictureBox
        {
            set { _pictureBox = value; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void MouseDown(System.Windows.Forms.MouseEventArgs e, Color c1, Color? c2 = null)
        {
            _Previous = new Point(e.X, e.Y);
            _Pen = new Pen(c1, Settings.Instance.defaultToolsThickness);
            MouseMove(e);
        }

        public void MouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            if (_Previous != null)
            {
                _pictureBox.Invalidate();
                _pictureBox.Update();
                using (Graphics g = _pictureBox.CreateGraphics())
                {
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                
            }
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(_pictureBox.Image))
            {
                g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
            }

            _Previous = null;
        }

        public void Selected() { }
    }
}
