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
            }
        }
    }
}

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        Line linkedLine = null;
        Point lastPoint;

        private void drawSin(Canvas canvas, Point pt)
        {
            if (linkedLine != null)
                canvas.Children.Remove(linkedLine);

            var k = (pt.Y - canvas.ActualHeight / 2) / pt.X;
            var pt2 = new Point(canvas.ActualWidth, (k * canvas.ActualWidth + canvas.ActualHeight / 2));

            linkedLine = new Line();
            linkedLine.StrokeThickness = 2;

            linkedLine.StrokeDashCap = PenLineCap.Round;
            linkedLine.StrokeStartLineCap = PenLineCap.Round;
            linkedLine.StrokeEndLineCap = PenLineCap.Round;

            linkedLine.X1 = 0;
            linkedLine.Y1 = canvas.ActualHeight / 2;
            linkedLine.X2 = pt2.X;
            linkedLine.Y2 = pt2.Y;

            linkedLine.Stroke = Brushes.White;

            canvas.Children.Add(linkedLine);

            lastPoint = pt;
        }

        public void redrawSin()
        {
            drawSin(parentCanvas, lastPoint);
        }
    }
}