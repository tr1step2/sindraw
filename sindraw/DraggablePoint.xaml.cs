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
    /// Interaction logic for DraggablePoint.xaml
    /// </summary>
    public partial class DraggablePoint : UserControl
    {
        private TranslateTransform transform = new TranslateTransform();

        Point anchorPoint;
        Point currentPoint;
        bool isInDrag = false;

        public DraggablePoint()
        {
            InitializeComponent();
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
            }
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
    }
}
