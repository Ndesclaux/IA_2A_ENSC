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
        public const int GRID_SIZE = 300;


        public static double _xStart;
        public static double _yStart;
        public static double _xEnd;
        public static double _yEnd;

        public static double _distNode = 2;
        public static int _nbNodeVoisin = 4;

        public static string _cas;

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
            //_grid.Children.Clear();

            
            if (xStartTextBox.Text != null && yStartTextBox.Text != null && xEndTextBox.Text != null &&
                yEndTextBox.Text != null)
            {
                double xTextStart = Double.Parse(xStartTextBox.Text);
                double yTextStart = Double.Parse(yStartTextBox.Text);

                double xTextEnd = Double.Parse(xEndTextBox.Text);
                double yTextEnd = Double.Parse(yEndTextBox.Text);


                //On ramène sur 300x300
                /*xTextStart = (xTextStart * _grid.ActualWidth) / GRID_SIZE;
                yTextStart = (yTextStart * _grid.ActualHeight) / GRID_SIZE;

                xTextEnd = (xTextEnd * _grid.ActualWidth) / GRID_SIZE;
                yTextEnd = (yTextEnd * _grid.ActualHeight) / GRID_SIZE;

                Debug.WriteLine("Start : ({0},{1})", xTextStart, yTextStart);
                Debug.WriteLine("End : ({0},{1})", xTextEnd, yTextEnd);*/

                Node2 pStart = new Node2(xTextStart, yTextStart);
                Node2 pEnd = new Node2(xTextEnd, yTextEnd);

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

                /*List<GenericNode> l = pStart.GetListSucc();

                foreach (var genericNode in l)
                {
                    Node2 N = (Node2) genericNode;

                    Ellipse ellipse = new Ellipse();
                    ellipse.Width = 5;
                    ellipse.Height = 5;
                    SolidColorBrush brush2 = new SolidColorBrush(Colors.Black);
                    ellipse.Stroke = brush2;
                    ellipse.Fill = brush2;
                    ellipse.HorizontalAlignment = HorizontalAlignment.Left;
                    ellipse.VerticalAlignment = VerticalAlignment.Top;
                    ellipse.Margin = new Thickness(N.X, N.Y, 0, 0);

                    _grid.Children.Add(ellipse);
                }*/

                SearchTree tree = new SearchTree();

                Stopwatch watch = new Stopwatch();

                watch.Start();
                List<GenericNode> solution = tree.RechercheSolutionAEtoile(new Node2(_xStart,_yStart));
                watch.Stop();

                PathFigure path = new PathFigure(new Point(pStart.X,pStart.Y), new List<PathSegment>(), false);

                foreach (var node in solution)
                {
                    Node2 p = (Node2) node;
                    path.Segments.Add(new LineSegment(new Point(p.X,p.Y), true));
                }
                
                chemin.Figures.Clear();
                chemin.Figures.Add(path);

                Node2 pFinal = (Node2)solution.Last();
                _tempsParcours.Content = pFinal.GetGCost().ToString();
                _tempsCalcul.Content = $"{watch.Elapsed.Minutes} min {watch.Elapsed.Seconds} sec {watch.Elapsed.Milliseconds} ms";
            }
        }

        private void comboBox_vent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _cas = ((Label)comboBox_vent.SelectedItem).Content.ToString();
            
            Debug.WriteLine("Cas : " + _cas);

            switch (_cas)
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

                default:
                    _cas = "a";

                    _xStart = 100;
                    _yStart = 200;

                    _xEnd = 200;
                    _yEnd = 100;
                    break;
            }

            Debug.WriteLine("Point de départ : ({0},{1})", _xStart, _yStart);
            Debug.WriteLine("Point d'arrivée : ({0},{1})", _xEnd, _yEnd);

            this.pointDepartGroupBox.IsEnabled = true;
            this.pointArriveeGroupBox.IsEnabled = true;

            this.xStartTextBox.Text = _xStart.ToString();
            this.yStartTextBox.Text = _yStart.ToString();
            this.xEndTextBox.Text = _xEnd.ToString();
            this.yEndTextBox.Text = _yEnd.ToString();

            this.pointDepartGroupBox.IsEnabled = false;
            this.pointArriveeGroupBox.IsEnabled = false;
        }
    }
}
