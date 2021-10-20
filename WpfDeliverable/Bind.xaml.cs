using System.ComponentModel;
using System.Windows;

namespace WpfDeliverable
{
    public partial class Bind : Window
    {
        public BindData BData { get; set; }

        public Bind()
        {
            InitializeComponent();
            DataContext = this;    // Bind には必須
            BData = new BindData();
            BData.BindStr = "Init";
        }

        private void OKButtonClicked(object sender, RoutedEventArgs e)
        {
            BData.BindStr = "OK clicked";
        }

        private void CancelButtonClicked(object sender, RoutedEventArgs e)
        {
            BData.BindStr = "Cancel clicked";
        }
    }
}
