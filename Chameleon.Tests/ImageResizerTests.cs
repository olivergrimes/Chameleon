using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Chameleon.Tests
{
    [TestClass]
    public class ImageResizerTests
    {
        [TestMethod]
        public void Resize_MaintainsRatio()
        {
            using var bitmap = new Bitmap(500, 1000);

            var testResult = ImageResizer.MaintainRatio(bitmap, 100);

            Assert.AreEqual(50, testResult.Width);
            Assert.AreEqual(100, testResult.Height);
        }

        [TestMethod]
        public void Resize_IgnoreSmallerImage()
        {
            using var bitmap = new Bitmap(5, 10);

            var testResult = ImageResizer.MaintainRatio(bitmap, 100);

            Assert.AreEqual(5, testResult.Width);
            Assert.AreEqual(10, testResult.Height);
        }

        [TestMethod]
        public void Resize_IgnoreInvalidRequest()
        {
            using var bitmap = new Bitmap(10, 100);

            var testResult = ImageResizer.MaintainRatio(bitmap, 9); //Can't maintain ratio with these parameters

            Assert.AreEqual(10, testResult.Width);
            Assert.AreEqual(100, testResult.Height);
        }

        [TestMethod]
        public void Resize_NullReturnsNull()
        {
            var testResult = ImageResizer.MaintainRatio(null, 100);

            Assert.IsNull(testResult);
        }
    }
}
