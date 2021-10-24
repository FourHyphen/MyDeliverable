using System.Windows.Forms;

namespace WpfDeliverable
{
    public static class MessageBox
    {
        public static void Show(string title, string message)
        {
            System.Windows.Forms.MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
