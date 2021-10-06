using System;
using System.Drawing;


namespace Basics
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Bitmap image = new Bitmap("../../../../harry_potter.png");
                Bitmap tmp = Basics.ApplyFilter(image, Basics.BlackAndWhite);
                // tmp.Save("../../../../black_and_white.png", System.Drawing.Imaging.ImageFormat.Png);


                tmp = Basics.ApplyFilter(image, Basics.Yellow);
                // tmp.Save("../../../../yellow.png", System.Drawing.Imaging.ImageFormat.Png);


                tmp = Basics.ApplyFilter(image, Basics.Grayscale);
                // tmp.Save("../../../../grayscale.png", System.Drawing.Imaging.ImageFormat.Png);


                tmp = Basics.ApplyFilter(image, Basics.Negative);
                // tmp.Save("../../../../negative.png", System.Drawing.Imaging.ImageFormat.Png);


                tmp = Basics.ApplyFilter(image, Basics.RemoveMaxes);
                // tmp.Save("../../../../removeMaxes.png", System.Drawing.Imaging.ImageFormat.Png);

                Bitmap mirror_image = Basics.Mirror(image);
                // mirror_image.Save("../../../../mirror.png", System.Drawing.Imaging.ImageFormat.Png);


                Bitmap rotated_image = Basics.RotateRight(image);
                // rotated_image.Save("../../../../rotateright.png", System.Drawing.Imaging.ImageFormat.Png);


                rotated_image = Basics.RotateN(image, 6);
                // rotated_image.Save("../../../../rotateN.png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (ArgumentException)
            {
                Console.Error.WriteLine("Invalid path to the image file");
                return -1;
            }
            return 0;
        }
    }
}
