using System;
using System.Collections.Generic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dot = new DraggablePoint();
            Canvas canvas = sender as Canvas;

            canvas.Children.Add(dot);

            Canvas.SetLeft(dot, e.GetPosition(canvas).X - 5.0);
            Canvas.SetTop(dot, e.GetPosition(canvas).Y - 5.0);
        }
    }
}
