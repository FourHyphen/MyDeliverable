using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using RM.Friendly.WPFStandardControls;
using System.Windows;

namespace TestWpfDeliverable
{
    class MainWindowDriver
    {
        private dynamic MainWindow { get; }

        private WindowsAppFriend App { get; }

        public LabelDriver LabelTest { get; }

        public WPFButtonBase BindButton { get; }

        public WPFButtonBase ImageButton { get; }

        private WPFButtonBase MessageBoxButton { get; }

        public WPFButtonBase EnableKeysButton { get; }

        public MainWindowDriver(WindowsAppFriend app)
        {
            MainWindow = app.Type("System.Windows.Application").Current.MainWindow;
            App = app;
            LabelTest = new LabelDriver(MainWindow, "LabelTest");
            BindButton = new WPFButtonBase(MainWindow.BindButton);
            ImageButton = new WPFButtonBase(MainWindow.ImageButton);
            MessageBoxButton = new WPFButtonBase(MainWindow.MessageBoxButton);
            EnableKeysButton = new WPFButtonBase(MainWindow.EnableKeysButton);
        }

        public string ShowMessageBox()
        {
            Codeer.Friendly.Async async = new Codeer.Friendly.Async();
            MessageBoxButton.EmulateClick(async);

            var msg = new NativeMessageBox(App.WaitForIdentifyFromWindowText("MessageBoxTest"));
            string message = msg.Message;
            msg.EmulateButtonClick("OK");
            async.WaitForCompletion();

            return message;
        }

        private int CountImage(string imageName)
        {
            var tree = GetLogicalTree();
            var display = tree.ByType<System.Windows.Controls.Image>().ByName(imageName);
            return display.Count;
        }

        private IWPFDependencyObjectCollection<DependencyObject> GetLogicalTree()
        {
            return new WindowControl(MainWindow).LogicalTree();    // 現在の画面状況を取得
        }
    }
}
