using System.Threading.Tasks;
using System.Windows;

namespace Step2
{
    public partial class MainWindow : Window
    {
        private readonly Engine _engine = new Engine();

        public MainWindow()
        {
            InitializeComponent();

            _engine.FuelLevelUpdated += (sender, args) =>
            {
                Dispatcher.InvokeAsync(() => Progress_Main.Value = _engine.FuelLevel);
            };
        }

        private async void Btn_Start_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            Progress_Main.Value = _engine.FuelLevel;
            Progress_Main.Visibility = Visibility.Visible;

            _engine.Start();
        }

        private async void Btn_Refuel_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            _engine.Refuel();
        }

        private async void Btn_ForceShutdownAsync_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            await _engine.ForceShutdownAsync();

            Progress_Main.Visibility = Visibility.Hidden;
        }

        private async void Btn_CarefulShutdownAsync_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            await _engine.CarefulShutdownAsync();

            Progress_Main.Visibility = Visibility.Hidden;
        }
    }
}
