using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Chameleon.Tests
{
    [TestClass]
    public class KMeansColourMatcherTests
    {
        private static IEnumerable<RGBPixel> GetTestPixels(Color colour, int count)
        {
            var rgbPixel = new RGBPixel
            {
                Blue = colour.B,
                Green = colour.G,
                Red = colour.R
            };

            return Enumerable.Repeat(rgbPixel, count);
        }

        private static IEnumerable<PaletteOption> GetTestPaletteOptions(params Color[] colours)
        {
            return colours.Select(c => PaletteOption.FromColor(c));
        }

        [TestMethod]
        public void SolidColour_ExactMatch()
        {
            var testClass = new KMeansColourMatcher();

            var rgbPixels = GetTestPixels(Color.Red, 1000);
            var palette = GetTestPaletteOptions(Color.Red, Color.Blue, Color.Green);

            var testResult = testClass.GetClosestMatch(rgbPixels, palette);

            Assert.IsNotNull(testResult);
            Assert.AreEqual(Color.Red.Name, testResult.Colour.Key);
            Assert.AreEqual(0, testResult.Distance);
        }

        [TestMethod]
        public void HalfColour_Match()
        {
            var testClass = new KMeansColourMatcher();

            var rgbPixels = GetTestPixels(Color.Red, 500)
                .Concat(GetTestPixels(Color.Black, 500));

            var palette = GetTestPaletteOptions(Color.Red, Color.Blue, Color.Green);

            var testResult = testClass.GetClosestMatch(rgbPixels, palette);

            Assert.IsNotNull(testResult);
            Assert.AreEqual(Color.Red.Name, testResult.Colour.Key);
            Assert.AreNotEqual(0, testResult.Distance);
        }

        [TestMethod]
        public void SolidColour_NoMatch()
        {
            var testClass = new KMeansColourMatcher();

            var rgbPixels = GetTestPixels(Color.Red, 1000);
            var palette = GetTestPaletteOptions(Color.Black, Color.Blue, Color.Green);

            var testResult = testClass.GetClosestMatch(rgbPixels, palette);

            Assert.IsNull(testResult);
        }

        [TestMethod]
        public void NullPixels_ReturnsNull()
        {
            var testClass = new KMeansColourMatcher();

            var palette = GetTestPaletteOptions(Color.Black, Color.Blue, Color.Green);

            var testResult = testClass.GetClosestMatch(null, palette);

            Assert.IsNull(testResult);
        }

        [TestMethod]
        public void NullPalette_ReturnsNull()
        {
            var testClass = new KMeansColourMatcher();

            var rgbPixels = GetTestPixels(Color.Red, 1000);

            var testResult = testClass.GetClosestMatch(rgbPixels, null);

            Assert.IsNull(testResult);
        }

        [TestMethod]
        public void EmptyPixels_ReturnsNull()
        {
            var testClass = new KMeansColourMatcher();

            var pixels = GetTestPixels(Color.Red, 0);
            var palette = GetTestPaletteOptions(Color.Black, Color.Blue, Color.Green);

            var testResult = testClass.GetClosestMatch(pixels, palette);

            Assert.IsNull(testResult);
        }

        [TestMethod]
        public void EmptyPalette_ReturnsNull()
        {
            var testClass = new KMeansColourMatcher();

            var rgbPixels = GetTestPixels(Color.Red, 1000);
            var palette = GetTestPaletteOptions(new Color[0]);

            var testResult = testClass.GetClosestMatch(rgbPixels, palette);

            Assert.IsNull(testResult);
        }
    }
}
