using System.Windows;

namespace WpfDeliverable
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Bind b = new Bind();
            b.Show();
            this.Close();
        }
    }
}
