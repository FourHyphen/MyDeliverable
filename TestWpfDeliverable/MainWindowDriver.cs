using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Linq;
using System.Windows.Input;

namespace TestWpfDeliverable
{
    class MainWindowDriver
    {
        private dynamic MainWindow { get; }

        private WindowsAppFriend App { get; }

        private IWPFDependencyObjectCollection<System.Windows.DependencyObject> Tree { get; set; }

        private LabelAdapter LabelTest { get; set; }

        public MainWindowDriver(WindowsAppFriend app)
        {
            MainWindow = app.Type("System.Windows.Application").Current.MainWindow;
            App = app;
            LabelTest = new LabelAdapter("LabelTest");
            Tree = new WindowControl(MainWindow).LogicalTree();
        }

        public string GetLabelTest()
        {
            UpdateNowMainWindowStatus();
            return LabelTest.Content(Tree);
        }

        private void UpdateNowMainWindowStatus()
        {
            Tree = new WindowControl(MainWindow).LogicalTree();    // 現在の画面状況を取得
        }

        private int CountImage(string imageName)
        {
            UpdateNowMainWindowStatus();
            var display = Tree.ByType<System.Windows.Controls.Image>().ByName(imageName);
            return display.Count;
        }
    }
}
