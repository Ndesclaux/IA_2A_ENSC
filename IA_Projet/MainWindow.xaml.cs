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

namespace IA_Projet
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static double _xStart;
        public static double _yStart;
        public static double _xEnd;
        public static double _yEnd;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _grid.Children.Clear();
            
            if (xStartTextBox.Text != null && yStartTextBox.Text != null && xEndTextBox.Text != null &&
                yEndTextBox.Text != null)
            {
                Point pStart = new Point(Double.Parse(xStartTextBox.Text), Double.Parse(yStartTextBox.Text));
                Point pEnd = new Point(Double.Parse(xEndTextBox.Text), Double.Parse(yEndTextBox.Text));

                Ellipse eStart = new Ellipse();
                eStart.Width = 5;
                eStart.Height = 5;
                SolidColorBrush brush = new SolidColorBrush(Colors.Green);
                eStart.Stroke = brush;
                eStart.Fill = brush;
                eStart.HorizontalAlignment = HorizontalAlignment.Left;
                eStart.VerticalAlignment = VerticalAlignment.Top;
                eStart.Margin = new Thickness(pStart.X, pStart.Y, 0, 0);

                Ellipse eEnd = new Ellipse();
                eEnd.Width = 5;
                eEnd.Height = 5;
                SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
                eEnd.Stroke = brushRed;
                eEnd.Fill = brushRed;
                eEnd.HorizontalAlignment = HorizontalAlignment.Left;
                eEnd.VerticalAlignment = VerticalAlignment.Top;
                eEnd.Margin = new Thickness(pEnd.X, pEnd.Y, 0, 0);

                _grid.Children.Add(eStart);
                _grid.Children.Add(eEnd);
            }
        }

        private void comboBox_vent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string res = comboBox_vent.SelectedItem.ToString();

            switch (res)
            {
                case "a":

                    _xStart = 100;
                    _yStart = 200;

                    _xEnd = 200;
                    _yEnd = 100;

                    break;

                case "b":

                    _xStart = 100;
                    _yStart = 200;

                    _xEnd = 200;
                    _yEnd = 100;

                    break;

                case "c":

                    _xStart = 200;
                    _yStart = 100;

                    _xEnd = 100;
                    _yEnd = 200;

                    break;
            }
        }
    }
}
