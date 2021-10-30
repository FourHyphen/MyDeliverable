using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;

namespace TestWpfDeliverable
{
    public class ListViewItemWindowDriver
    {
        private WindowControl Window { get; }

        private WPFListView ListView { get; }

        public dynamic Items { get; }

        public ListViewItemWindowDriver(WindowsAppFriend app)
        {
            Window = WindowControl.IdentifyFromWindowText(app, "ListViewItemWindow");
            ListView = new WPFListView(Window.AppVar.Dynamic().FileList);
            Items = ListView.AppVar.Dynamic().Items;
        }

        public void CloseWindow()
        {
            Window.Close();
        }
    }
}