using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD.Tools
{
    class Invertion : ITool
    {
        private PictureBox _pictureBox;
        private string _name = "Invert";

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

        public void MouseDown(MouseEventArgs e, Color c1, Color? c2 = null) { }

        public void MouseMove(MouseEventArgs e) { }

        public void MouseUp(MouseEventArgs e) { }

        public void Selected()
        {
            byte A, R, G, B;
            Color pixelColor;
            Bitmap b = new Bitmap(_pictureBox.Image);

            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    pixelColor = b.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    b.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
                Thread.Sleep(10);
                int percent = (int)(((double)y / (double)b.Height) * (double)100);
                //Debug.WriteLine(percent);
                ReportProgress(percent);
            }
            //Debug.WriteLine("Done");
            Done((Image)b);
        }
    }
}
