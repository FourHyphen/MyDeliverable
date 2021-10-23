using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;

namespace TestWpfDeliverable
{
    public class BindWindowDriver
    {
        private WindowControl BindWindow { get; }

        public WPFButtonBase OKButton { get; }

        public WPFButtonBase CancelButton { get; }

        public BindWindowDriver(WindowsAppFriend app)
        {
            BindWindow = WindowControl.IdentifyFromWindowText(app, "Bind");

            dynamic d = BindWindow.AppVar.Dynamic();
            OKButton = new WPFButtonBase(d.OKButton);
            CancelButton = new WPFButtonBase(d.CancelButton);
        }

        public string Text()
        {
            return BindWindow.Dynamic().AfterNameTextBox.Text;
        }

        public void CloseWindow()
        {
            BindWindow.Close();
        }
    }
}