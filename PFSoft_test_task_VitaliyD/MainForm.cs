using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PFSoft_test_task_VitaliyD.Tools;
using System.Threading;

namespace PFSoft_test_task_VitaliyD
{
    /// <summary>
    /// Main form class
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields

        #region ColorPanel
        /// <summary>
        /// Button of first color (color of pen, line, bounds of figures)
        /// </summary>
        private Button b1;

        /// <summary>
        /// Button of second color (color of fill of figures)
        /// </summary>
        private Button b2;

        /// <summary>
        /// Saves which color (first or second) are changing on color panel
        /// </summary>
        private bool isb1;

        /// <summary>
        /// First color (color of pen, line, bounds of figures)
        /// </summary>
        private Color c1;

        /// <summary>
        /// Second color (color of fill of figures)
        /// </summary>
        private Color c2;
        #endregion

        #region Controllers
        /// <summary>
        /// Currently selected tool (Pen, Line, Circle, Rectangle, Inversion)
        /// </summary>
        private ITool SelectedTool;

        /// <summary>
        /// This variable is used for short writing Settings.Instance
        /// </summary>
        private Settings s;

        /// <summary>
        /// This variable is used for short writing FileController.Instance
        /// </summary>
        private FileController f;
        #endregion

        #endregion

        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitiateVariables();
            InitiateDrawfield();
            DrawTools();
            DrawSelectedColorButtons();
            DrawColorPanel();
        }

        #region Initialization

        /// <summary>
        /// Initializing private fields
        /// </summary>
        private void InitiateVariables()
        {
            isb1 = true;
            s = Settings.Instance;
            c1 = s.colors[0];
            c2 = s.colors[s.colors.Count - 1];
            f = FileController.Instance;
            SelectedTool = s.ToolsList[0];
        }

        /// <summary>
        /// Initializing PictureBox element for drawing
        /// </summary>
        private void InitiateDrawfield()
        {
            Bitmap bmp = new Bitmap(DrawField.Width, DrawField.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            DrawField.Image = bmp;
        }
        
        /// <summary>
        /// Initializing buttons for each tool
        /// </summary>
        private void DrawTools()
        {
            for (int i = 0; i < s.ToolsList.Count; i++)
            {
                ITool t = s.ToolsList[i];
                t.pictureBox = this.DrawField;
                Button b = new Button();
                b.Name = t.Name;
                b.Text = t.Name;
                b.Size = new Size(s.buttonToolWidth, s.buttonToolHeight);
                b.Location = new Point(0, s.buttonToolHeightWithPadding*i + this.menu.Height);
                b.Click += buttonTool_Click;
                this.Controls.Add(b);
            }
        }

        /// <summary>
        /// Initializing two buttons which used for selectinf first and second color
        /// </summary>
        private void DrawSelectedColorButtons()
        {
            b1 = new Button();
            b1.Name = s.buttonColor1Name;
            b1.Location = new Point(s.buttonColor1X, s.buttonColor1Y);
            b1.Size = new Size(s.buttonColorSize, s.buttonColorSize);
            b1.FlatAppearance.BorderSize = s.buttonColorSelectedThickness;
            b1.FlatStyle = FlatStyle.Flat;
            b1.Click += BorderFillSwich;

            b2 = new Button();
            b2.Name = s.buttonColor2Name;
            b2.Location = new Point(s.buttonColor2X, s.buttonColor2Y);
            b2.Size = new Size(s.buttonColorSize, s.buttonColorSize);
            b2.FlatAppearance.BorderSize = 0;
            b2.FlatStyle = FlatStyle.Flat;
            b2.Click += BorderFillSwich;

            if ((s.colors != null) && (s.colors.Count > 0))
            {
                b1.BackColor = s.colors[0];
                b2.BackColor = s.colors[s.colors.Count - 1];
            }

            this.Controls.Add(b1);
            this.Controls.Add(b2);
        }

        /// <summary>
        /// Initializing color panel with colors from settings
        /// </summary>
        private void DrawColorPanel()
        {
            int xOffset = s.colorPanelXOffset;
            int yOffset = s.colorPanelYOffset + s.buttonColorSizeWithPadding;
            for (int i = 0; i < s.colors.Count; i++)
            {
                Panel p = new Panel();
                p.BackColor = s.colors[i];
                int xMarg = i % s.colorPanelColumns;
                int yMarg = i / s.colorPanelColumns;

                p.Location = new System.Drawing.Point(s.colorRectangleSizeWithPadding * xMarg + xOffset, s.colorRectangleSizeWithPadding * yMarg + yOffset);
                p.Size = new System.Drawing.Size(s.colorRectangleSize, s.colorRectangleSize);
                p.Click += ColorPick;
                this.Controls.Add(p);
            }
        }

        #endregion

        #region EventHandlers

        #region InThreadEvents

        /// <summary>
        /// Raizes when Invertion is done
        /// </summary>
        /// <param name="img">New image, which will be setted to PictureBox</param>
        private void OnDone(Image img)
        {
            progressBW.Invoke(new Action<int>((progr) => progressBW.Value = progr), 0);
            DrawField.Invoke(new Action<Image>((i) => DrawField.Image = i), img);
        }

        /// <summary>
        /// Raizes when new percentage reported
        /// </summary>
        /// <param name="i">Percentage, which will be setted to ProgressBar</param>
        private void OnProgressReported(int i)
        {
            progressBW.Invoke(new Action<int>((progr) => progressBW.Value = progr), i);
        }

        #endregion

        #region ButtonsEvents

        /// <summary>
        /// Chosing color, which will be setted as first or second
        /// </summary>
        /// <param name="sender">Plane with color</param>
        /// <param name="e">Default event parameters</param>
        private void ColorPick(object sender, EventArgs e)
        {
            if (isb1)
            {
                b1.BackColor = c1 = (sender as Panel).BackColor;
            }
            else
            {
                b2.BackColor = c2 = (sender as Panel).BackColor;
            }
        }

        /// <summary>
        /// Chosing between first and second color selected for changing
        /// </summary>
        /// <param name="sender">Button b1 or b2</param>
        /// <param name="e">Default event parameters</param>
        private void BorderFillSwich(object sender, EventArgs e)
        {
            isb1 = (sender as Button).Name == s.buttonColor1Name;
            b1.FlatAppearance.BorderSize = (isb1) ? s.buttonColorSelectedThickness : 0;
            b2.FlatAppearance.BorderSize = (isb1) ? 0 : s.buttonColorSelectedThickness;
        }

        /// <summary>
        /// Selecting tool
        /// </summary>
        /// <param name="sender">Button with tool's name</param>
        /// <param name="e">Default event parameters</param>
        private void buttonTool_Click(object sender, EventArgs e)
        {
            SelectedTool = s.ToolsList.FirstOrDefault(x => x.Name == (sender as Button).Name);

            SelectedTool.Done += OnDone;
            SelectedTool.ReportProgress += OnProgressReported;
            Thread t = new Thread(SelectedTool.Selected);
            t.Start();
        }

        #endregion

        #region DrawEvents

        /// <summary>
        /// MouseDown event on PictureBox
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Default mouse event parameters with position</param>
        private void DrawField_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedTool.MouseDown(e,c1,c2);
        }

        /// <summary>
        /// MouseMove event on PictureBox
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Default mouse event parameters with position</param>
        private void DrawField_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedTool.MouseMove(e);
        }

        /// <summary>
        /// MouseUp event on PictureBox
        /// </summary>
        /// <param name="sender">Mouse</param>
        /// <param name="e">Default mouse event parameters with position</param>
        private void DrawField_MouseUp(object sender, MouseEventArgs e)
        {
            SelectedTool.MouseUp(e);
        }

        #endregion

        #region FileDialogsEvents

        /// <summary>
        /// Load image 
        /// </summary>
        /// <param name="sender">Menu item</param>
        /// <param name="e">Default event parameters</param>
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = GetAviableFormats();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = s.openFileDialogTitle;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                int? error = null;
                Image i = f.Load(openFileDialog1.FileName, out error);
                if (error == null)
                    DrawField.Image = i;
                else
                    MessageBox.Show(ErrorCodes.Instance.Errors[error.Value]);
            }
        }

        /// <summary>
        /// Save image
        /// </summary>
        /// <param name="sender">Menu item</param>
        /// <param name="e">Default event parameters</param>
        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = GetAviableFormats();
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = s.saveFileDialogTitle;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                f.Save(saveFileDialog1.FileName, DrawField.Image);
            }
        }

        private string GetAviableFormats()
        {
            string filter = string.Empty;
            foreach (string key in f.AviableFormats)
            {
                filter = string.Format("{0}|{1}-image (*{1})|*{1}", filter, key);
            }
            return filter.TrimStart('|');
        }

        #endregion

        #endregion
    }
}
