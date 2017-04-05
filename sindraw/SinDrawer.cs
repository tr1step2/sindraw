using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sindraw
{
    class SinDrawer
    {
        public static Polyline draw(Canvas canvas, double a, double k)
        {
            Polyline pl = new Polyline();

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / Constants.Segments;

            double Xideal = Math.PI / 2 * 10 * frameStep;
            double Yideal = halfHeight - 100;

            double frameX = 0;
            double frameY = halfHeight;

            for (int i = 0; i < Constants.Segments; ++i, frameX += frameStep)
            {
                double x = (double)i / 10;
                double y = a * Math.Sin(k * x);

                frameY = halfHeight - y * 100;

                Point p = new Point(frameX, frameY);
                pl.Points.Add(p);
            }

            return pl;
        }
    }
}
