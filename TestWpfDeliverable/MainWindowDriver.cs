using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
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

        public MainWindowDriver(WindowsAppFriend app)
        {
            MainWindow = app.Type("System.Windows.Application").Current.MainWindow;
            App = app;
            LabelTest = new LabelDriver(MainWindow, "LabelTest");
            BindButton = new WPFButtonBase(MainWindow.BindButton);
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
