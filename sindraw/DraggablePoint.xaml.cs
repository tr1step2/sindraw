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

                drawSin(parentCanvas, new Point(currentPoint.X, currentPoint.Y));

                Panel.SetZIndex((UIElement)this, 2);
            }
        }
    }
}

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        Polyline linkedLine = null;
        Point lastPoint;

        private void drawSin(Canvas canvas, Point pt)
        {
            if (linkedLine != null)
                canvas.Children.Remove(linkedLine);

            Debug.WriteLine("{0} {1}", pt.X, pt.Y);

            linkedLine = new Polyline();
            linkedLine.Stroke = Brushes.White;

            double xtrmX = canvas.ActualWidth * 0.2;
            double xtrmY = canvas.ActualHeight * 0.166;

            double a = xtrmY / pt.Y;
            double k = xtrmX / pt.X;

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / 80;
            double frameX = 0;
            double frameY = halfHeight;

            for (int i = 0; i < 80; ++i, frameX += frameStep)
            {
                double x = (double)i / 10;
                double y = a * Math.Sin(k * x);
                
                frameY = halfHeight - y * 100;

                linkedLine.Points.Add(new Point(frameX, frameY));
            }

            canvas.Children.Add(linkedLine);

            lastPoint = pt;
        }

        public void redrawSin()
        {
            drawSin(parentCanvas, lastPoint);
        }
    }
}