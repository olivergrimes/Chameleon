using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Chameleon.Tests
{
    [TestClass]
    public class BitmapConverterTests
    {
        [TestMethod]
        public void CorrectColourReturned()
        {
            using var bitmap = new Bitmap(1, 2);
            bitmap.SetPixel(0, 0, Color.Red);
            bitmap.SetPixel(0, 1, Color.Blue);

            var result = BitmapConverter.GetRGBPixels(bitmap)
                .ToList();

            Assert.AreEqual(255, result[0].Red);
            Assert.AreEqual(0, result[0].Green);
            Assert.AreEqual(0, result[0].Blue);

            Assert.AreEqual(0, result[1].Red);
            Assert.AreEqual(0, result[1].Green);
            Assert.AreEqual(255, result[1].Blue);
        }

        [TestMethod]
        public void CorrectDimensionsReturned()
        {
            using var bitmap = new Bitmap(50, 50);

            var result = BitmapConverter.GetRGBPixels(bitmap)
              .ToList();

            Assert.AreEqual(50 * 50, result.Count);
        }

        [TestMethod]
        public void NullBitmapReturnsNothing()
        {
            var result = BitmapConverter.GetRGBPixels(null);

            Assert.AreEqual(0, result.Count);
        }
    }
}
