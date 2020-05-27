using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chameleon
{
    public static class BitmapConverter
    {
        public static ICollection<RGBPixel> GetRGBPixels(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return new RGBPixel[0];
            }

            var pixels = new List<RGBPixel>();

            /* Iterating the pixels was slow. Found an unsafe 
             * method using a single lock from: https://stackoverflow.com/a/3795278/490282.
             */

            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                bitmap.PixelFormat); // make sure you check the pixel format as you will be looking directly at memory

            unsafe
            {
                // example assumes 24bpp image.  You need to verify your pixel depth
                // loop by row for better data locality
                for (int y = 0; y < data.Height; ++y)
                {
                    byte* pRow = (byte*)data.Scan0 + y * data.Stride;
                    for (int x = 0; x < data.Width; ++x)
                    {
                        // windows stores images in BGR pixel order
                        byte r = pRow[2];
                        byte g = pRow[1];
                        byte b = pRow[0];

                        pixels.Add(new RGBPixel
                        {
                            Red = r,
                            Green = g,
                            Blue = b
                        });

                        // next pixel in the row
                        pRow += 3;
                    }
                }
            }

            bitmap.UnlockBits(data);

            return pixels;
        }
    }
}
