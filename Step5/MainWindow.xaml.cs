using System.Windows;

namespace Step5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _imageCount;
        private int _objectCount;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Btn_GenerateImages_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                ImageObject image = await ImageGenerator.GenerateAsync();

                _imageCount++;
                RefreshStatus();
            }
        }

        private void RefreshStatus()
        {
            TxtBox_Status.Text = $"Images Generated : {_imageCount} - Unrelated Objects Generated {_objectCount}";
        }

        private void Btn_SomethingUnrelated_OnClick(object sender, RoutedEventArgs e)
        {
            UnrelatedObject unrelatedObject = UnrelatedGenerator.GenerateObject();

            _objectCount++;

            RefreshStatus();
        }
    }
}
