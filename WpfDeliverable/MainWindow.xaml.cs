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
            new CardWindow().Show();
        }

        private void ImageButtonClick(object sender, RoutedEventArgs e)
        {
            new ImageWindow().Show();
        }

        private void MessageBoxButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("MessageBoxTest", "test_message");
        }

        private void EnableKeysButtonClick(object sender, RoutedEventArgs e)
        {
            new EnableKeysWindow().Show();
        }

        private void ListViewItemButtonClick(object sender, RoutedEventArgs e)
        {
            new ListViewItemWindow().Show();
        }

        private void MusicButtonClick(object sender, RoutedEventArgs e)
        {
            new MusicWindow().Show();
        }
    }
}
