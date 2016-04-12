using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using PFSoft_test_task_VitaliyD.Tools;

namespace PFSoft_test_task_VitaliyD
{
    /// <summary>
    /// Settings of visual interface of application (later this class can be transformed to loading settings from files or other sources). 
    /// This class contains collection with all tools.
    /// </summary>
    public class Settings
    {
        #region Singleton
        private static Settings _instance = new Settings();

        public static Settings Instance
        {
            get { return _instance; }
        }
        #endregion

        #region Fields

        public List<ITool> ToolsList = new List<ITool>();

        public readonly int menuHeight = 24;

        #region Color Panel
        public readonly int colorPanelXOffset = 0;
        public int colorPanelYOffset;
        public readonly List<Color> colors = new List<Color>() { Color.Black, Color.Brown, Color.Chocolate, Color.DarkRed, Color.ForestGreen, Color.Green, Color.Gray, Color.GreenYellow, 
                Color.Red, Color.HotPink, Color.LightBlue, Color.LightGray, Color.Orange, Color.LightYellow, Color.White };
        public readonly int colorPanelColumns = 3;
        public readonly int colorRectangleSize = 20;
        public readonly int colorRectangleSizeWithPadding = 22;
        public readonly string buttonColor1Name = "b1";
        public readonly string buttonColor2Name = "b2";
        public readonly int buttonColorSize = 30;
        public readonly int buttonColorSizeWithPadding = 33;
        public readonly int buttonColor1X = 0;
        public int buttonColor1Y;
        public readonly int buttonColor2X = 33;
        public int buttonColor2Y;
        public readonly int buttonColorSelectedThickness = 2;
        #endregion

        #region Tools Panel
        public readonly int buttonToolWidth = 66;
        public readonly int buttonToolHeight = 20;
        public readonly int buttonToolHeightWithPadding = 22;
        public readonly int defaultToolsThickness = 2;
        #endregion

        #region FileDialogs titles
        public readonly string openFileDialogTitle = "Open...";
        public readonly string saveFileDialogTitle = "Save...";
        #endregion

        #endregion

        /// <summary>
        /// Constructor without parameters
        /// </summary>
        public Settings()
        {
            _instance = this;
            ToolsList.Add(new Pencil());
            ToolsList.Add(new Line());
            ToolsList.Add(new PFSoft_test_task_VitaliyD.Tools.Rectangle());
            ToolsList.Add(new PFSoft_test_task_VitaliyD.Tools.Circle());
            ToolsList.Add(new Invertion());
            RefreshInterface();
        }

        #region Public methods

        /// <summary>
        /// Public method for extended tools 
        /// </summary>
        /// <param name="tool">Tool, which will be added to list with other tools</param>
        public void AddTool(ITool tool)
        {
            if (tool != null)
                ToolsList.Add(tool);
        }

        /// <summary>
        /// Refreshing variables (calculates heights all tool's buttons)
        /// </summary>
        public void RefreshInterface()
        {
            colorPanelYOffset = ToolsList.Count() * buttonToolHeightWithPadding + menuHeight;
            buttonColor1Y = buttonColor2Y = colorPanelYOffset;
        }

        #endregion
    }
}
