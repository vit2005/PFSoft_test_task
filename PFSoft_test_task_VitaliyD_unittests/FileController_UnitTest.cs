using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PFSoft_test_task_VitaliyD;

namespace PFSoft_test_task_VitaliyD_unittests
{
    [TestClass]
    public class FileController_UnitTest
    {
        FileController f;

        public FileController_UnitTest()
        {
            f = new FileController();
        }

        [TestMethod]
        public void TestSaveErrorCode1()
        {
            Bitmap b = new Bitmap(1, 1);
            Image i = (Image)b;
            int? code = f.Save("asdasdfasfdasd", i);
            Assert.AreEqual(1, code);
        }

        [TestMethod]
        public void TestLoadErrorCode1()
        {
            Bitmap b = new Bitmap(1, 1);
            int? code = null;
            Image i = f.Load("asdasdfasfdasd", out code);
            Assert.AreEqual(1, code);
        }

        [TestMethod]
        public void TestSaveErrorCode5()
        {
            Bitmap b = new Bitmap(1, 1);
            Image i = (Image)b;
            int? code = f.Save("asdasdfasfdasd.exe", i);
            Assert.AreEqual(5, code);
        }

        [TestMethod]
        public void TestLoadErrorCode5()
        {
            Bitmap b = new Bitmap(1, 1);
            int? code = null;
            Image i = f.Load("asdasdfasfdasd.exe", out code);
            Assert.AreEqual(5, code);
        }
    }
}
