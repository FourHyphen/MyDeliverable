using System.Windows;
using System.Windows.Input;

namespace WpfDeliverable
{
    public partial class EnableKeysWindow : Window
    {
        public EnableKeysWindow()
        {
            InitializeComponent();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            WindowKeyDown(e.Key, e.KeyboardDevice.Modifiers);
        }

        private void WindowKeyDown(Key key, ModifierKeys modifier)
        {
            EnableKey.KeyShortcut ks = EnableKey.ToKeyShortcut(key, modifier);
            KeyTextBox.Text = ks.ToString();
        }
    }
}
