using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Step4.RouteObjects;

namespace Step4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();

        private readonly List<TextBox> _workerResultTextBoxes = new List<TextBox>();

        private readonly List<Point2D> _cityLocations = new List<Point2D>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_GenerateCities_OnClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TBox_CitiesCount.Text, out int count))
            {
                MessageBox.Show($"Cities count '{TBox_CitiesCount.Text}' could not be parsed");
            }
            else if (!int.TryParse(TBox_MapHeight.Text, out int mapHeight))
            {
                MessageBox.Show($"Map height '{TBox_MapHeight.Text}' could not be parsed");
            }
            else if (!int.TryParse(TBox_MapWidth.Text, out int mapWidth))
            {
                MessageBox.Show($"Map width '{TBox_MapWidth.Text}' could not be parsed");
            }
            else
            {
                _cityLocations.Clear();

                for (int i = 0; i < count; i++)
                {
                    _cityLocations.Add(new Point2D(_random.NextDouble() * mapWidth, _random.NextDouble() * mapHeight));
                }
            }
        }

        private void Btn_Start_OnClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TBox_MaximumDistance.Text, out double maximumDistance))
            {
                int workerIndex = _workerResultTextBoxes.Count;

                TextBox resultTextBox = new TextBox() { IsReadOnly = true };

                Stack_Results.Children.Add(resultTextBox);
                _workerResultTextBoxes.Add(resultTextBox);

                Thread thread = new Thread(() =>
                {
                    List<Point2D> cityLocations;
                    lock (_cityLocations)
                    {
                        cityLocations = new List<Point2D>(_cityLocations);
                    }

                    IRouteCalculator calculator = new RouteCalculator(cityLocations, maximumDistance);
                    DoWork(calculator, workerIndex);
                });

                thread.IsBackground = true;
                thread.Start();
            }
            else
            {
                MessageBox.Show($"Maximum distance '{TBox_MaximumDistance.Text}' could not be parsed");
            }
        }

        private void DoWork(IRouteCalculator calculator, int workerIndex)
        {
            Random workerRandom = new Random();

            long iterationCount = 0;
            Stopwatch totalStopwatch = new Stopwatch();
            totalStopwatch.Start();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // StringBuilder routesLog = new StringBuilder();

            while (true)
            {
                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    Dispatcher.InvokeAsync(() => _workerResultTextBoxes[workerIndex].Text = $"Calculated {iterationCount} in {totalStopwatch.Elapsed} (avg. {iterationCount / totalStopwatch.Elapsed.TotalSeconds} / s)");
                    stopwatch.Restart();
                }

                FinalRoute? route = calculator.PlotCourse(workerRandom.Next(calculator.CitiesCount), workerRandom.Next(calculator.CitiesCount));

                // routesLog.AppendLine(route?.ToString());

                iterationCount++;
            }
        }

    }
}
