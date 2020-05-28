using System.Drawing;

namespace Chameleon
{
    public class PaletteOption
    {
        public string Key { get; set; }

        public RGBPixel RGB { get; set; }

        public static PaletteOption FromColor(Color color)
        {
            return new PaletteOption
            {
                Key = color.Name,
                RGB = new RGBPixel
                {
                    Blue = color.B,
                    Green = color.G,
                    Red = color.R
                }
            };
        }
    }
}
