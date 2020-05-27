using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Chameleon
{
    public static class ImageResizer
    {
        /* Decided to reduce the image size to speed up the colour
         * matching process.  The code below is an adaptation of
         * the following example:
         * https://www.codeproject.com/Articles/2941/Resizing-a-Photographic-image-with-GDI-for-NET.
         * Used NearestNeighbor interpolation to preserve original colours.
         */

        public static Bitmap MaintainRatio(Bitmap bitmap, int newHeight)
        {
            if (bitmap == null)
            {
                return null;
            }

            if (bitmap.Height <= newHeight)
            {
                return bitmap;
            }

            if (newHeight < bitmap.Height / bitmap.Width)
            {
                return bitmap;
            }

            var percent = newHeight / (float)bitmap.Height;

            int sourceWidth = bitmap.Width;
            int sourceHeight = bitmap.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * percent);
            int destHeight = (int)(sourceHeight * percent);

            var bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);

            using var grPhoto = Graphics.FromImage(bmPhoto);

            grPhoto.InterpolationMode = InterpolationMode.NearestNeighbor;

            grPhoto.DrawImage(bitmap,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            return bmPhoto;
        }
    }
}
