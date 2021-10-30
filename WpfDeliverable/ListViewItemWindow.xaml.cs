using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfDeliverable
{
    public partial class ListViewItemWindow : Window
    {
        public ListViewDataList DataList { get; private set; }

        public int ListAreaWidth { get; private set; }

        public int ListAreaHeight { get; private set; }

        public ListViewItemWindow()
        {
            InitializeComponent();
            DataContext = this;
            Init();
        }

        private void Init()
        {
            SetPosition((int)ActualWidth, (int)ActualHeight);
            DataList = new ListViewDataList();
        }

        private void SetPosition(int actualWidth, int actualHeight)
        {
            int scrollBar = 10;                     // 10: 経験則
            ListAreaWidth = actualWidth - scrollBar;
            ListAreaHeight = actualHeight - scrollBar;
        }

        private void MouseRightButtonClicked(object sender, MouseButtonEventArgs e)
        {
            MouseRightButtonClickedPrepare(sender);

            // 何か処理
        }

        private void MouseRightButtonClickedPrepare(object sender)
        {
            // 以下の場合に、選択中ファイルを今右クリックされた Item に変える
            // (1) ファイル未選択の場合
            // (2) ファイル選択中に別ファイルに対して右クリックした場合
            if (sender is ListViewItem lvi && lvi.Content is ListViewData eventItem)
            {
                ListViewData now = (ListViewData)DataList.SelectedItem;
                if (now == null || eventItem.Name != now.Name)
                {
                    DataList.SelectedItem = eventItem;
                }
            }
        }

        private void KeyDowned(object sender, KeyEventArgs e)
        {
            // 何か処理
            // KeyDowned(e.Key, e.SystemKey, e.KeyboardDevice.Modifiers);
        }

        private void MouseLeftButtonClicked(object sender, MouseButtonEventArgs e)
        {
            // もし ListViewItem が StackPanel の中にあるなら↓により ScrollViewer を取得できる
            //  → StackPanel との兼ね合いやマウス＆カーソルキー操作による Windows の Explorer 機能を再現するなら MyExplorer を見た方が良い
            // MouseLeftButtonDownClicked(e.GetPosition((UIElement)sender));

            // 何か処理
        }

        private void MouseLeftButtonDownClicked(Point p)
        {
            HitTestResult result = VisualTreeHelper.HitTest(this, p);
            dynamic obj = result.VisualHit;
            DoLeftMouseEvent(obj);
        }

        private void DoLeftMouseEvent(dynamic obj)
        {
            // 何か処理
        }
    }
}
