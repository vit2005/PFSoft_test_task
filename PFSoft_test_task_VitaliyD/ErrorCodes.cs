using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFSoft_test_task_VitaliyD
{
    /// <summary>
    /// Error codes for application
    /// </summary>
    public class ErrorCodes
    {
        #region Singleton
        private static ErrorCodes _instance = new ErrorCodes();

        public static ErrorCodes Instance
        {
            get { return _instance; }
        }
        #endregion

        /// <summary>
        /// Dictionary, where keys - codes of errors, values - strings with description of error
        /// </summary>
        public Dictionary<int, string> Errors;

        /// <summary>
        /// Constructor without parameters. Initialize dictionary with errors codes
        /// </summary>
        public ErrorCodes()
        {
            //TODO: Load from file
            Errors = new Dictionary<int, string>();
            Errors.Add(1, "Url has no extension");
            Errors.Add(2, "Url has wrong extension");
            Errors.Add(3, "Saving error");
            Errors.Add(4, "Loading error");
            Errors.Add(5, "Undefined format");
        }
    }
}
