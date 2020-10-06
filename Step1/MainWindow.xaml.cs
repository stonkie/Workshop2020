using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Step1
{
    public partial class MainWindow : Window
    {
        private readonly BackgroundWorker _workerIO = new BackgroundWorker() {WorkerReportsProgress = true };
        private readonly BackgroundWorker _workerCPU = new BackgroundWorker() { WorkerReportsProgress = true };

        public MainWindow()
        {
            InitializeComponent();

            _workerIO.DoWork += (sender, args) => { ReadFile(); };
            _workerIO.ProgressChanged += (sender, args) => Progress_Main.Value = args.ProgressPercentage;

            _workerCPU.DoWork += (sender, args) => { Calculate(); };
            _workerCPU.ProgressChanged += (sender, args) => Progress_Main.Value = args.ProgressPercentage;
        }

        private void Btn_SyncCPU_OnClick(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            Thread.Sleep(500);
            Progress_Main.Value = 50;

            Calculate();
        }

        private void Btn_SyncIO_OnClick(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            Thread.Sleep(500);
            Progress_Main.Value = 50;

            ReadFile();
        }

        private void Btn_ThreadCPU_OnClick(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            Thread.Sleep(500);
            _workerCPU.RunWorkerAsync();
        }

        private void Btn_ThreadIO_OnClick(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            Thread.Sleep(500);
            _workerIO.RunWorkerAsync();
        }

        private async void Btn_AsyncCPU_OnClickAsync(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            await Task.Delay(500);

            Progress_Main.Value = 50;
            await Calculator.CalculateAsync();
            Progress_Main.Value = 100;
        }

        private async void Btn_AsyncIO_OnClickAsync(object sender, RoutedEventArgs e)
        {
            Progress_Main.Value = 0;
            await Task.Delay(500);

            Progress_Main.Value = 50;
            await FileWrapper.ReadFileAsync("test.txt");
            Progress_Main.Value = 100;
        }

        private void ReadFile()
        {
            _workerIO.ReportProgress(50);
            FileWrapper.ReadFile("test.txt");
            _workerIO.ReportProgress(100);
        }

        private void Calculate()
        {
            _workerCPU.ReportProgress(50);
            Calculator.Calculate();
            _workerCPU.ReportProgress(100);
        }

    }
}
