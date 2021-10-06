using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace Basics
{
    public class ref_Basics
    {
        /// <summary>
        /// As the name states it apply a filter function on each pixel of the image
        /// </summary>
        /// <param name="image"> The image to modify</param>
        /// <param name="filter"> The function to apply on each pixel</param>
        public static Bitmap ApplyFilter(Bitmap image, Func<Color, Color> filter)
        {
            Bitmap new_image = new Bitmap(image.Width, image.Height);
            for (int line = 0; line < image.Width; line++)
            {
                for (int column = 0; column < image.Height; column++)
                {
                    Color pixel_color = image.GetPixel(line, column);
                    Color new_pixel_color = filter(pixel_color);
                    new_image.SetPixel(line, column, new_pixel_color);
                }
            }
            return new_image;
        }
        /// <summary>
        /// A Black and White filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color BlackAndWhite(Color color)
        {
            int average = (color.R + color.G + color.B) / 3;
            int new_val = average > 127 ? 255 : 0;
            return Color.FromArgb(new_val, new_val, new_val);
        }

        /// <summary>
        /// A Yellow filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Yellow(Color color) => Color.FromArgb(color.R, color.G, 0);

        /// <summary>
        /// A Grayscale filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Grayscale(Color color)
        {
            int avg = (21 * color.R + 72 * color.G + 7 * color.B) / 100;
            return Color.FromArgb(avg, avg, avg);
        }

        /// <summary>
        /// A Negative filter
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color Negative(Color color)
        {
            return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }

        private static int Max3(int a, int b, int c)
        {
            int max = a;
            if (max < b)
                max = b;
            if (max < c)
                max = c;

            return max;
        }

        /// <summary>
        /// Remove the maxes of the composants of the color
        /// </summary>
        /// <param name="color"> The color to modify </param>
        /// <returns> The new color</returns>
        public static Color RemoveMaxes(Color color)
        {
            int r, g, b;
            (r, g, b) = (color.R, color.G, color.B);
            int max = Max3(r, g, b);
            r = max == r ? 0 : r;
            g = max == g ? 0 : g;
            b = max == b ? 0 : b;
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Create the new_image as if the image was seen in a mirror
        /// ....o.  =>  .o....
        /// ...o..  =>  ..o...
        /// ..o...  =>  ...o..
        /// .o....  =>  ....o.
        /// o.....  =>  .....o
        /// </summary>
        /// <param name="image"> The image to 'mirror'</param>
        /// <returns> The new image</returns>
        public static Bitmap Mirror(Bitmap image)
        {
            Bitmap new_image = new Bitmap(image);
            for (int line = 0; line < image.Width; line++)
            {
                for (int column = 0; column < image.Height; column++)
                {
                    Color pixel_color = image.GetPixel(line, column);
                    new_image.SetPixel(image.Width - 1 - line, column, pixel_color);
                }
            }
            return new_image;
        }

        /// <summary>
        /// Apply a right rotation
        /// </summary>
        /// <param name="image"> The image to rotate</param>
        /// <returns> The new_image</returns>
        public static Bitmap RotateRight(Bitmap image)
        {
            Bitmap new_image = new Bitmap(image.Height, image.Width);
            for (int line = 0; line < image.Width; line++)
            {
                for (int column = 0; column < image.Height; column++)
                {
                    Color pixel_color = image.GetPixel(line, column);
                    new_image.SetPixel(image.Height - 1 - column, line, pixel_color);
                }
            }
            return new_image;
        }

        /// <summary>
        /// <!> Bonus <!>
        /// Rotate to the right n times
        /// </summary>
        /// <param name="image"> The image to rotate</param>
        /// <param name="n"> Number of rotation (n can be negative and thus must be handled properly)</param>
        /// <returns> The new_image</returns>
        public static Bitmap RotateN(Bitmap image, int n)
        {
            n = n % 4;
            if (n < 0)
                n += 4;
            Bitmap new_image = new Bitmap(image);
            for (int i = 0; i < n; ++i)
            {
                new_image = RotateRight(new_image);
            }
            return new_image;
        }
    }
}
