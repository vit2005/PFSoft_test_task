using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using PFSoft_test_task_VitaliyD.ImageFormats;

namespace PFSoft_test_task_VitaliyD
{
    /// <summary>
    /// Controller of saving and loading image
    /// </summary>
    public class FileController
    {
        #region Singleton
        private static FileController _instance = new FileController();

        public static FileController Instance
        {
            get { return _instance; }
        }
        #endregion

        #region Fields

        /// <summary>
        /// List of aviable controllers (each controller must inherit IImageFormat interface)
        /// </summary>
        List<IImageFormat> AviableControllers;

        /// <summary>
        /// Dictionary where keys - extencions, values - controllers which can save and load file in that extencion
        /// </summary>
        /// <example>{ ".jpg", DefaultImageFormat }</example>
        Dictionary<string, IImageFormat> reservedFormats;

        #endregion

        /// <summary>
        /// List for fill filter in OpenFileDialog and SaveFileDialog, conteins all extensions which can be loaded and saved
        /// </summary>
        public List<string> AviableFormats
        {
            get 
            {
                return reservedFormats.Keys.ToList<string>();
            }
        }

        /// <summary>
        /// Constructor without parameters. Initialize private fields
        /// </summary>
        public FileController()
        {
            AviableControllers = new List<IImageFormat>();
            reservedFormats = new Dictionary<string, IImageFormat>();

            AddController(new DefaultImageFormat());
        }

        #region Public methods

        /// <summary>
        /// Adds controller to collection
        /// </summary>
        /// <param name="i">Controller which can save and load image format (must inherit IImageFormat)</param>
        public void AddController(IImageFormat i)
        {
            AviableControllers.Add(i);
            RefreshReservedFormats();
        }
        
        /// <summary>
        /// Load image
        /// </summary>
        /// <param name="filename">Url of file</param>
        /// <param name="errorCode">Error code</param>
        /// <returns>Loaded image</returns>
        public Image Load(string filename, out int? errorCode)
        {
            Image result = null;
            errorCode = CheckExtension(filename);

            if (errorCode == null)
                errorCode = reservedFormats[Path.GetExtension(filename)].Load(filename, out result);

            return result;
        }

        /// <summary>
        /// Save image
        /// </summary>
        /// <param name="filename">Url of file</param>
        /// <param name="image">Image for being saved</param>
        /// <returns>Error code</returns>
        public int? Save(string filename, Image image)
        {
            int? errorCode = CheckExtension(filename);

            if (errorCode == null)
                errorCode = reservedFormats[Path.GetExtension(filename)].Save(filename, image);

            return errorCode;
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Refreshing reservedFormats dictionary 
        /// </summary>
        private void RefreshReservedFormats()
        {
            foreach (IImageFormat i in AviableControllers)
            {
                foreach (string s in i.AviableFormats)
                {
                    if (reservedFormats.ContainsKey(s))
                        reservedFormats[s] = i;
                    else
                        reservedFormats.Add(s, i);
                }
            }
        }

        /// <summary>
        /// Preload and presave checking of url
        /// </summary>
        /// <param name="filename">Url of file</param>
        /// <returns>Error code</returns>
        private int? CheckExtension(string filename)
        {
            int? errorCode = null;

            if (!Path.HasExtension(filename))
                errorCode = 1; //Url has no extension
            else
            {
                string s = Path.GetExtension(filename);
                if (!reservedFormats.ContainsKey(s))
                    errorCode = 5; //Undefined format
            }

            return errorCode;
        }
        #endregion
    }
}
