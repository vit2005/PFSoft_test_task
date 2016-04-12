using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD.Tools
{
    class Pencil : ITool
    {
        private Point? _Previous = null;
        private Pen _Pen;

        private PictureBox _pictureBox;
        private string _name = "Pencil";

        public event ImageEventHandler Done;
        public event ProgressEventHandler ReportProgress;

        public PictureBox pictureBox
        {
            set { this._pictureBox = value; }
        }

        public string Name
        {
            get { return this._name; }
        }

        public void MouseDown(MouseEventArgs e, Color c1, Color? c2 = null)
        {
            _Previous = new Point(e.X, e.Y);
            _Pen = new Pen(c1, Settings.Instance.defaultToolsThickness);
            MouseMove(e);
        }

        public void MouseMove(MouseEventArgs e)
        {
            if (_Previous != null)
            {
                if (_pictureBox.Image == null)
                {
                    Bitmap bmp = new Bitmap(_pictureBox.Width, _pictureBox.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                    }
                    _pictureBox.Image = bmp;
                }
                using (Graphics g = Graphics.FromImage(_pictureBox.Image))
                {
                    g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);
                }
                _pictureBox.Invalidate();
                _Previous = new Point(e.X, e.Y);
            }
        }
        public void MouseUp(MouseEventArgs e)
        {
            Graphics g = Graphics.FromImage(_pictureBox.Image);
            g.DrawLine(_Pen, _Previous.Value.X, _Previous.Value.Y, e.X, e.Y);

            _Previous = null;
        }

        public void Selected() { }
    }
}
