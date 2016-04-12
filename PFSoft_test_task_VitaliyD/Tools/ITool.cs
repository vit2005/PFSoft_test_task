using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD
{
    /// <summary>
    /// Delegate of method which places new image on image holder control
    /// </summary>
    /// <param name="i">New image</param>
    public delegate void ImageEventHandler(Image i);

    /// <summary>
    /// Delegate of method which set new percent value to progress control
    /// </summary>
    /// <param name="i">Percentage of process</param>
    public delegate void ProgressEventHandler(int i);

    /// <summary>
    /// Interface for all tools in application (like pen, line, circle, rectangle, invertion)
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// Throwing when new image needs to placed on image holder control
        /// </summary>
        event ImageEventHandler Done;

        /// <summary>
        /// Throwing when new percentage needs to placed on progress control
        /// </summary>
        event ProgressEventHandler ReportProgress;

        /// <summary>
        /// Image holder
        /// </summary>
        PictureBox pictureBox { set; }

        /// <summary>
        /// Name of tool (this string will be on button of tool)
        /// </summary>
        string Name { get; }

        /// <summary>
        /// MouseDown event on PictureBox
        /// </summary>
        /// <param name="e">Mouse event parameters, which contains coordinates of mouse position</param>
        /// <param name="c1">First color (for pen, line and bounds of circle and rectangle)</param>
        /// <param name="c2">Second color (for fill circle and rectangle)</param>
        void MouseDown(MouseEventArgs e, Color c1, Color? c2 = null);

        /// <summary>
        /// MouseMove event on PictureBox
        /// </summary>
        /// <param name="e">Mouse event parameters, which contains coordinates of mouse position</param>
        void MouseMove(MouseEventArgs e);

        /// <summary>
        /// MouseMove event on PictureBox
        /// </summary>
        /// <param name="e">Mouse event parameters, which contains coordinates of mouse position</param>
        void MouseUp(MouseEventArgs e);

        /// <summary>
        /// Method, called when user select the tool
        /// </summary>
        void Selected();
    }
}
