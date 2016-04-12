using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PFSoft_test_task_VitaliyD
{
    static class Program
    {
        public static MainForm FormInstance;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormInstance = new MainForm();
            Application.Run(FormInstance);
        }
    }
}
