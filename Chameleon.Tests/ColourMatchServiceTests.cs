using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chameleon.Tests
{
    [TestClass]
    public class ColourMatchServiceTests
    {
        [TestMethod]
        public async Task ColourMatchService_MatchExampleImage()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(h => h.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient());

            var uri = "https://pwintyimages.blob.core.windows.net/samples/stars/test-sample-teal.png";
            var palette = new[]
            {
               new PaletteOption
               {
                    Key = "Teal",
                    RGB =  new RGBPixel
                    {
                        Red = 123,
                        Green = 123,
                        Blue = 123
                    }
               },
               new PaletteOption
               {
                    Key = "Grey",
                    RGB =  new RGBPixel
                    {
                        Red = 0,
                        Green = 0,
                        Blue = 0
                    }
               },
           };

            var testClass = new ColourMatchService(httpClientFactory.Object, new KMeansColourMatcher());

            var result = await testClass.GetMatch(new Uri(uri), palette);
        }
    }
}
