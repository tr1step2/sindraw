using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        private TranslateTransform transform = new TranslateTransform();

        private Point anchorPoint;
        private Point currentPoint;
        private bool isInDrag = false;

        private SinusGraph mSinusGraph;

        public DraggablePoint(SinusGraph graph, Point pt)
        {
            InitializeComponent();

            mSinusGraph = graph;

            drawSin(mSinusGraph.ParentCanvas, pt);
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

                drawSin(mSinusGraph.ParentCanvas, new Point(currentPoint.X, currentPoint.Y));

                Panel.SetZIndex((UIElement)this, 2);
            }
        }
    }
}

namespace sindraw
{
    public partial class DraggablePoint : UserControl
    {
        private Polyline linkedLine = null;
        private Point lastPoint;

        public PointCollection Points;

        private void drawSin(Canvas canvas, Point pt)
        {
            if (linkedLine != null)
                canvas.Children.Remove(linkedLine);

            double halfHeight = canvas.ActualHeight / 2;
            double frameStep = canvas.ActualWidth / 280;

            double Xideal = Math.PI / 2 * 10 * frameStep;
            double Yideal = halfHeight - 100; 

            double a = (halfHeight - pt.Y) / 100;
            double k = Xideal / pt.X;

            linkedLine = SinDrawer.draw(canvas, a, k);
            linkedLine.Stroke = Brushes.White;
            linkedLine.StrokeThickness = 2;

            canvas.Children.Add(linkedLine);

            lastPoint = pt;
            Points = linkedLine.Points;

            //call redraw sum
            mSinusGraph.redrawSum();
        }

        public void redrawSin()
        {
            drawSin(mSinusGraph.ParentCanvas, lastPoint);
        }

        public Point getPoint()
        {
            return lastPoint;
        }
    }
}