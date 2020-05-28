using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chameleon
{
    public class ColourMatchService : IColourMatchService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IColourMatcher _colourMatcher;

        private const int MaxImageHeight = 250;

        public ColourMatchService(IHttpClientFactory httpClientFactory, IColourMatcher colourMatcher)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _colourMatcher = colourMatcher ?? throw new ArgumentNullException(nameof(colourMatcher));
        }

        public async Task<ColourMatch> GetMatch(Uri uri, IEnumerable<PaletteOption> palette)
        {
            var httpClient = _httpClientFactory.CreateClient();

            using var imageBytes = await httpClient.GetStreamAsync(uri);
            using var bitmap = new Bitmap(imageBytes);
            using var shrunkBitmap = ImageResizer.MaintainRatio(bitmap, newHeight: MaxImageHeight);

            var rgbPixels = BitmapConverter.GetRGBPixels(shrunkBitmap);
            var match = _colourMatcher.GetClosestMatch(rgbPixels, palette);

            return match;
        }
    }
}
