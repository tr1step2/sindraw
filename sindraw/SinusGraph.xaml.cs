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

        public SinusGraph()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var canvas = sender as Canvas;
            Debug.WriteLine(canvas.Name);
            var dot = new DraggablePoint(canvas, e.GetPosition(canvas));

            canvas.Children.Add(dot);
            points.Add(dot);

            Canvas.SetLeft(dot, e.GetPosition(canvas).X - 5.0);
            Canvas.SetTop(dot, e.GetPosition(canvas).Y - 5.0);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            redraw();
        }

        private void redraw()
        {
            foreach(var p in points)
            {
                p.redrawSin();
            }
        }
    }
}
