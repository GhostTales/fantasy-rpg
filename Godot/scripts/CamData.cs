using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;

namespace ProcessImage
{
    internal class ImageProcessor
    {
        public int threshold { get; set; }
        public Color ARGB { get; set; }

        public int avgX { get; private set; }
        public int avgY { get; private set; }
        private int Count;
        public Mat _frame { get; set; }


        public ImageProcessor(Color ARGB, int threshhold)
        {
            this.threshold = threshhold;
            this.ARGB = ARGB;
        }

        public void ProcessImage()
        {
            Bitmap bmp = _frame.ToBitmap();


            // Begin loop to walk through every pixel
            for (int x = 0; x < bmp.Width; x += 4)
            {
                for (int y = 0; y < bmp.Height; y += 4)
                {
                    // What is current color
                    Color currentColor = bmp.GetPixel(x, y);

                    float r1 = currentColor.R;
                    float g1 = currentColor.G;
                    float b1 = currentColor.B;
                    float r2 = ARGB.R;
                    float g2 = ARGB.G;
                    float b2 = ARGB.B;

                    float d = DistSq(r1, g1, b1, r2, g2, b2);

                    //Debug.WriteLine($"x,y = {x},{y}");

                    if (d < threshold * threshold)
                    {
                        avgX += x;
                        avgY += y;
                        Count++;
                    }
                }
            }

            if (Count > 0)
            {
                avgX = avgX / Count;
                avgY = avgY / Count;
            }
        }

        public float DistSq(float r1, float g1, float b1, float r2, float g2, float b2)
        {
            float Distance = (float)Math.Sqrt(Math.Pow(r2 - r1, 2) * Math.Pow(g2 - g1, 2) + Math.Pow(b2 - b1, 2));
            return Distance;
        }

    }
}
