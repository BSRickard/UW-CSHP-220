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
using System.Windows.Shapes;

// Course:  UW CSHP 220 C (2018 Winter)
// Project: Lesson 08
// Author:  RICKARD, Brian
// Date:    2018-03-23

namespace ContactApp
{
    /// <summary>
    /// Interaction logic for MouseWindow.xaml
    /// </summary>
    public partial class MouseWindow : Window
    {
        const int MaxEllipses = 10;
        static Ellipse SelectedEllipse;
        Random rand = new Random();

        public MouseWindow()
        {
            InitializeComponent();
            this.KeyUp += new KeyEventHandler(uxCanvas_KeyUp);
        }

        private void uxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateStatus(e);

            if (!isMoving) return;

            Point location = e.GetPosition(null);
            var ellipse = SelectedEllipse;

            if (InputHitTest(location).GetType() == typeof(Ellipse))
            {
                // Move the ellipse to the new location
                ellipse.Margin = new Thickness(
                    ellipse.Margin.Left - downPoint.X + location.X,
                    ellipse.Margin.Top  - downPoint.Y + location.Y,
                    0, 0);

                downPoint = location;
            }
        }

        private bool isMoving;
        private Point downPoint;

        private void uxCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point location = e.GetPosition(null);
            downPoint = location;
            try
            {
                SelectedEllipse = (Ellipse)InputHitTest(location);
            }
            catch
            {
                if (uxCanvas.Children.Count < MaxEllipses)
                {
                    var ellipse = new Ellipse();
                    SolidColorBrush mySolidColorBrush = new SolidColorBrush();

                    mySolidColorBrush.Color = Color.FromArgb(
                        127, (byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
                    ellipse.Fill = mySolidColorBrush;
                    ellipse.StrokeThickness = 2;
                    ellipse.Stroke = Brushes.Black;

                    ellipse.Height = 100;
                    ellipse.Width = 100;

                    ellipse.Margin = new Thickness(
                        location.X - ellipse.Width / 2,
                        location.Y - ellipse.Height / 2,
                        0, 0);

                    uxCanvas.Children.Add(ellipse);
                    SelectedEllipse = ellipse;
                    Panel.SetZIndex(uxStatusBar, 1);
                }
                else if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                {
                    uxCanvas_MouseDoubleClick(sender, e);
                }
            }

            if (InputHitTest(location).GetType() == typeof(Ellipse))
            {
                isMoving = true;
            }

            UpdateStatus(e);
        }

        private void uxCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMoving = false;
            UpdateStatus(e);
        }

        private void uxCanvas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ellipse = SelectedEllipse;

            Point location = e.GetPosition(null);

            // Move the ellipse to the new location
            ellipse.Margin = new Thickness(
                location.X - ellipse.Width  / 2,
                location.Y - ellipse.Height / 2,
                0, 0);

            downPoint = location;
        }

        private void UpdateStatus(MouseEventArgs e)
        {
            Point location = e.GetPosition(null);
            uxStatus.Text = string.Format("({0,5:N1},{1,5:N1}) IsMoving={2}", location.X, location.Y, isMoving);
        }

        private void uxCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && uxCanvas.Children.Count > 0)
            {
                uxCanvas.Children.Remove(SelectedEllipse);
            }
        }
    }
}
