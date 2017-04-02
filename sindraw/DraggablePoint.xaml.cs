using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        private TranslateTransform transform = new TranslateTransform();

        private Point anchorPoint;
        private Point currentPoint;
        private bool isInDrag = false;

        private Canvas parentCanvas;

        public DraggablePoint(Canvas parent, Point pt)
        {
            InitializeComponent();

            parentCanvas = parent;
            drawSin(parentCanvas, pt);
        }

        private void MainCircle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            anchorPoint = e.GetPosition(null);
            element.CaptureMouse();
            isInDrag = true;
            e.Handled = true;
        }

        private void MainCircle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isInDrag)
            {
                var element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                isInDrag = false;
                e.Handled = true;
            }
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isInDrag)
            {
                var element = sender as FrameworkElement;
                currentPoint = e.GetPosition(null);

                transform.X += currentPoint.X - anchorPoint.X;
                transform.Y += (currentPoint.Y - anchorPoint.Y);
                this.RenderTransform = transform;
                anchorPoint = currentPoint;

                drawSin(parentCanvas, currentPoint);
            }
        }
    }
}

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        private double xmin = 0;

        private double xmax = 6.5;
        private double ymin = -1.1;
        private double ymax = 1.1;

        Line linkedLine = null;

        private void drawSin(Canvas canvas, Point pt)
        {
            if (linkedLine != null)
                canvas.Children.Remove(linkedLine);

            linkedLine = new Line();
            linkedLine.Stroke = Brushes.White;

            linkedLine.X1 = 0;
            linkedLine.Y1 = canvas.ActualHeight / 2;
            linkedLine.X2 = pt.X;
            linkedLine.Y2 = pt.Y;

            canvas.Children.Add(linkedLine);

            /*var pl = new Polyline();
            pl.Stroke = Brushes.White;

            for (int i = 0; i < 100; ++i)
            {
                double x = i / 5.0;
                double y = Math.Sin(0.5 * x);

                pl.Points.Add(castPoint(canvas, new Point(x, y)));
            }

            canvas.Children.Add(pl);*/
        }

        private Point castPoint(Canvas canvas, Point pt)
        {
            Point result = new Point();

            result.X = (pt.X - xmin) * canvas.ActualWidth / (xmax - xmin);
            result.Y = canvas.ActualHeight - (pt.Y - ymin) * canvas.ActualHeight / (ymax - ymin);
            return result;
        }
    }
}