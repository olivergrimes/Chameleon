using System.Collections.Generic;

namespace Chameleon
{
    public interface IColourMatcher
    {
        ColourMatch GetClosestMatch(IEnumerable<RGBPixel> sourcePixels, IEnumerable<PaletteOption> targetPalette);
    }
}
