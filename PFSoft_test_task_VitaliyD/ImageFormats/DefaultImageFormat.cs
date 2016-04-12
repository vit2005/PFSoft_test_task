using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace PFSoft_test_task_VitaliyD.ImageFormats
{
    class DefaultImageFormat : IImageFormat
    {
        private List<string> _aviableFormats = new List<string>() { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".tiff" };

        public List<string> AviableFormats
        {
            get { return _aviableFormats; }
        }

        public int? Load(string filename, out Image image)
        {
            image = null;
            try
            {
                if (!Path.HasExtension(filename))
                    return 1; //Url has no extension
            
                image = Image.FromFile(filename);
                return null;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 4; //Loading error
            }
        }

        public int? Save(string filename, Image image)
        {
            try
            {
                if (!Path.HasExtension(filename))
                    return 1; //Url has no extension
            
                switch (Path.GetExtension(filename))
                {
                    case ".bmp":
                        image.Save(filename, ImageFormat.Bmp);
                        return null;
                    case ".gif":
                        image.Save(filename, ImageFormat.Gif);
                        return null;
                    case ".jpeg":
                    case ".jpg":
                        image.Save(filename, ImageFormat.Jpeg);
                        return null;
                    case ".png":
                        image.Save(filename, ImageFormat.Png);
                        return null;
                    case ".tiff":
                        image.Save(filename, ImageFormat.Tiff);
                        return null;
                    default:
                        return 2; //Url has wrong extension
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 3; //Saving error
            }
        }
    }
}
