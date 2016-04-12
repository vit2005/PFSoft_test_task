using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD.Tools
{
    class Rectangle : ITool
    {
        private Point? _Previous = null;
        private Pen _Pen;
        private SolidBrush _c2;

        private PictureBox _pictureBox;
        private string _name = "Rectangle";

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

        public void MouseDown(MouseEventArgs e, Color c1, Color? c2 = null)
        {
            _Previous = new Point(e.X, e.Y);
            _Pen = new Pen(c1, Settings.Instance.defaultToolsThickness);
            _c2 = new SolidBrush(c2.Value);
            MouseMove(e);
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (_Previous != null)
            {
                _pictureBox.Invalidate();
                _pictureBox.Update();
                Graphics g = _pictureBox.CreateGraphics();
                DrawRectangle(e,g);
                g.Dispose();
            }
        }

        public void MouseUp(MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(_pictureBox.Image);
            DrawRectangle(e, g);
            g.Dispose();
            _Previous = null;
        }

        private void DrawRectangle(MouseEventArgs e, Graphics g)
        {
            int x1 = Math.Min(_Previous.Value.X, e.X);
            int y1 = Math.Min(_Previous.Value.Y, e.Y);
            int width = Math.Abs(e.X - _Previous.Value.X);
            int height = Math.Abs(e.Y - _Previous.Value.Y);

            g.FillRectangle(_c2, x1, y1, width, height);
            g.DrawRectangle(_Pen, x1, y1, width, height);
        }

        public void Selected() { }
    }
}
