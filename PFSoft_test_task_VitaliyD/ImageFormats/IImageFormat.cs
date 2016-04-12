using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PFSoft_test_task_VitaliyD.ImageFormats
{
    /// <summary>
    /// Interface for file formats of image
    /// </summary>
    public interface IImageFormat
    {
        /// <summary>
        /// List of aviable extension for particulary controller
        /// </summary>
        /// <example>{".psd"}</example>
        List<string> AviableFormats { get; }

        /// <summary>
        /// Load image
        /// </summary>
        /// <param name="filename">Url of file of image wich will be loaded</param>
        /// <param name="image">Image variable wich will contains loaded image</param>
        /// <returns>Code of error</returns>
        int? Load(string filename, out Image image);

        /// <summary>
        /// Save image
        /// </summary>
        /// <param name="filename">Url of file in wich will be saved image</param>
        /// <param name="image">Image wich will be saved</param>
        /// <returns>Code of error</returns>
        int? Save(string filename, Image image);

    }
}
