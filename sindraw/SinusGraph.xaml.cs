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
    /// <summary>
    /// Interaction logic for SinusGraph.xaml
    /// </summary>
    public partial class SinusGraph : UserControl
    {
        List<DraggablePoint> points = new List<DraggablePoint>();
        PointCollection mSumPointCollection = new PointCollection(280);
        Polyline sumPolyLine;
        Canvas mCanvas;

        public SinusGraph()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mCanvas = sender as Canvas;
            Debug.WriteLine(sender.ToString());

            var dot = new DraggablePoint(mCanvas, mSumPointCollection, e.GetPosition(mCanvas));

            mCanvas.Children.Add(dot);
            points.Add(dot);

            Canvas.SetLeft(dot, e.GetPosition(mCanvas).X - 5.0);
            Canvas.SetTop(dot, e.GetPosition(mCanvas).Y - 5.0);

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

        private void redrawSum()
        {
            if (null == mCanvas)
                return;

            if (null != sumPolyLine)
                mCanvas.Children.Remove(sumPolyLine);

            sumPolyLine = new Polyline();
            sumPolyLine.Stroke = Brushes.Yellow;

            sumPolyLine.Points = mSumPointCollection;

            mCanvas.Children.Add(sumPolyLine);
        }
    }
}
