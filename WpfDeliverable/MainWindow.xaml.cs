using System.Windows;

namespace WpfDeliverable
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BindButtonClick(object sender, RoutedEventArgs e)
        {
            ShowBindWindow();
        }

        private void ShowBindWindow()
        {
            Bind b = new Bind();
            b.Show();
        }

        private void CardsButtonClick(object sender, RoutedEventArgs e)
        {
            new Cards().Show();
        }

        private void ImageButtonClick(object sender, RoutedEventArgs e)
        {
            new ImageWindow().Show();
        }

        private void MessageBoxButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MessageBoxTest", "test_message");
        }
    }
}
