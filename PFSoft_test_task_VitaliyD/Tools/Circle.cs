using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD.Tools
{
    class Circle : ITool
    {
        private Point? _Previous = null;
        private Pen _Pen;
        private SolidBrush _c2;

        private PictureBox _pictureBox;
        private string _name = "Circle";

        public event ImageEventHandler Done;
        public event ProgressEventHandler ReportProgress;

        public PictureBox pictureBox
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
            _c2 = new SolidBrush(c2.Value);
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
                    DrawCircle(e, g);
                }
            }
        }

        public void MouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(_pictureBox.Image))
            {
                DrawCircle(e, g);
            }
            
            _Previous = null;
        }

        private void DrawCircle(MouseEventArgs e, Graphics g)
        {
            int size = Math.Min(e.X - _Previous.Value.X, e.Y - _Previous.Value.Y);
            
            g.FillEllipse(_c2, _Previous.Value.X, _Previous.Value.Y, size, size);
            g.DrawEllipse(_Pen, _Previous.Value.X, _Previous.Value.Y, size, size);
        }


        public void Selected() { }
    }
}
