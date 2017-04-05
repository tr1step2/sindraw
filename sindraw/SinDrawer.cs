using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sindraw
{
    class SinDrawer
    {
        public static Polyline draw(Canvas canvas, PointCollection sumPoints, double a, double k)
        {
            Polyline pl = new Polyline();

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / 280;

            double Xideal = Math.PI / 2 * 10 * frameStep;
            double Yideal = halfHeight - 100;

            double frameX = 0;
            double frameY = halfHeight;

            for (int i = 0; i < 280; ++i, frameX += frameStep)
            {
                double x = (double)i / 10;
                double y = a * Math.Sin(k * x);

                frameY = halfHeight - y * 100;

                Point p = new Point(frameX, frameY);
                pl.Points.Add(p);

                if (sumPoints.Count < 280)
                {
                    sumPoints.Add(p);
                    continue;
                }

                //update sum graph points
                Point psum = sumPoints[i];
                psum.X = (psum.X + frameX) / 2;
                psum.Y = (psum.Y + frameY) / 2;

                sumPoints[i] = psum;
            }

            return pl;
        }
    }
}
