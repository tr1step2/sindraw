using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace sindraw
{
    /// <summary>
    /// Interaction logic for SinusGraph.xaml
    /// </summary>
    public partial class SinusGraph : UserControl
    {
        List<DraggablePoint> points = new List<DraggablePoint>();
        PointCollection mSumPointCollection = new PointCollection(280);
        Polyline sumPolyLine = new Polyline();

        //public 
        public Canvas ParentCanvas;

        public SinusGraph()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ParentCanvas = sender as Canvas;
            Debug.WriteLine(sender.ToString());

            var dot = new DraggablePoint(this, e.GetPosition(ParentCanvas));

            ParentCanvas.Children.Add(dot);
            points.Add(dot);

            Canvas.SetLeft(dot, e.GetPosition(ParentCanvas).X - 5.0);
            Canvas.SetTop(dot, e.GetPosition(ParentCanvas).Y - 5.0);

            redrawSum();
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redrawAll();
        }

        private void redrawAll()
        {
            foreach (var p in points)
            {
                p.redrawSin();
            }

            redrawSum();
        }

        public void redrawSum()
        {
            if (null == ParentCanvas)
                return;

            ParentCanvas.Children.Remove(sumPolyLine);

            sumPolyLine = new Polyline();
            sumPolyLine.Stroke = Brushes.Yellow;

            for (int i = 0; i < 280; ++i)
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

                sumPolyLine.Points.Add(new Point(avgX, avgY));
            }

            ParentCanvas.Children.Add(sumPolyLine);
        }
    }
}
