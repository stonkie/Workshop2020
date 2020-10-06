using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Step3
{
    public partial class MainWindow : Window
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private Block<int>? _latestBlock;
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_Read_OnClick(object sender, RoutedEventArgs e)
        {
            _lock.EnterReadLock();

            try
            {
                StringBuilder builder = new StringBuilder();

                Block<int>? currentBlock = _latestBlock;

                while (currentBlock != null)
                {
                    builder.AppendLine($"{currentBlock.Time} : {currentBlock.Value} : {BitConverter.ToString(currentBlock.Hash)}");

                    currentBlock = currentBlock.Previous;
                }

                TBox_Value.Text = builder.ToString();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        private async void Btn_Append_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await AppendBlockAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void Btn_AppendOnItsOwnThread_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () =>
            {
                try
                {
                    await AppendBlockAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
        }

        private async Task AppendBlockAsync()
        {
            _lock.EnterWriteLock();

            try
            {
                _latestBlock = await Block<int>.CreateBlockAsync(_latestBlock, _random.Next(0, 100), BitConverter.GetBytes);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
