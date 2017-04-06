using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sindraw
{
    public static class Drawer
    {
        public static Polyline getSinusCurve(Canvas canvas, Point pt)
        {
            Polyline pl = new Polyline();

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / Constants.Segments;

            double Xxtr = Math.PI / 2 * 10 * frameStep;
            double Yxtr = halfHeight - 100;

            double a = (halfHeight - pt.Y) / 100;
            double k = Xxtr / pt.X;

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

        public static Polyline getAvgCurve(List<DraggablePoint> points)
        {
            Polyline pl = new Polyline();

            pl.Stroke = Brushes.Yellow;
            pl.StrokeThickness = Constants.LineStrokeThickness;

            for (int i = 0; i < Constants.Segments; ++i)
            {
                double avgX = 0;
                double avgY = 0;
                foreach (var dragp in points)
                {
                    avgX += dragp.Points[i].X;
                    avgY += dragp.Points[i].Y;
                }

                avgX /= points.Count;
                avgY /= points.Count;

                pl.Points.Add(new Point(avgX, avgY));
            }

            return pl;
        }
    }
}
