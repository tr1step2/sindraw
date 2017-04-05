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
        private PointCollection mSumPoints;

        public DraggablePoint(Canvas parent, PointCollection sumPoints, Point pt)
        {
            InitializeComponent();
           
            parentCanvas = parent;
            mSumPoints = sumPoints;

            drawSin(parentCanvas, sumPoints, pt);
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

                drawSin(parentCanvas, mSumPoints, new Point(currentPoint.X, currentPoint.Y));

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

        private void drawSin(Canvas canvas, PointCollection sumPoints, Point pt)
        {
            if (linkedLine != null)
                canvas.Children.Remove(linkedLine);

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / 280;

            double Xideal = Math.PI / 2 * 10 * frameStep;
            double Yideal = halfHeight - 100; 

            double a = (halfHeight - pt.Y) / 100;
            double k = Xideal / pt.X;

            linkedLine = SinDrawer.draw(canvas, sumPoints, a, k);
            linkedLine.Stroke = Brushes.White;

            canvas.Children.Add(linkedLine);

            lastPoint = pt;
        }

        public void redrawSin()
        {
            drawSin(parentCanvas, mSumPoints, lastPoint);
        }

        public Point getPoint()
        {
            return lastPoint;
        }
    }
}