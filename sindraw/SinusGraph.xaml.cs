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
        PointCollection mSumPointCollection = new PointCollection(Constants.Segments);
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

            Canvas.SetLeft(dot, e.GetPosition(ParentCanvas).X - Constants.PointInternalRadius);
            Canvas.SetTop(dot, e.GetPosition(ParentCanvas).Y - Constants.PointInternalRadius);

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
            if (null == ParentCanvas || points.Count < 2)
                return;

            ParentCanvas.Children.Remove(sumPolyLine);

            sumPolyLine = Drawer.getAvgCurve(points);
            ParentCanvas.Children.Add(sumPolyLine);
        }
    }
}
