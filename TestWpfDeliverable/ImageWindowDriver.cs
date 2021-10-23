using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System;
using System.Windows;

namespace TestWpfDeliverable
{
    public class ImageWindowDriver
    {
        private WindowControl ImageWindow { get; }

        public WPFButtonBase MoveXPlusButton { get; }

        public WPFButtonBase MoveXMinusButton { get; }

        public WPFButtonBase MoveYPlusButton { get; }

        public WPFButtonBase MoveYMinusButton { get; }

        public dynamic Image { get; private set; }

        public ImageWindowDriver(WindowsAppFriend app)
        {
            ImageWindow = WindowControl.IdentifyFromWindowText(app, "ImageWindow");

            dynamic d = ImageWindow.AppVar.Dynamic();
            MoveXPlusButton = new WPFButtonBase(d.MoveXPlusButton);
            MoveXMinusButton = new WPFButtonBase(d.MoveXMinusButton);
            MoveYPlusButton = new WPFButtonBase(d.MoveYPlusButton);
            MoveYMinusButton = new WPFButtonBase(d.MoveYMinusButton);

            InitImage();
        }

        private void InitImage()
        {
            var tree = GetLogicalTree();
            Image = tree.ByType<System.Windows.Controls.Image>().ByName("ImageCreated").Single().Dynamic();
        }

        private IWPFDependencyObjectCollection<DependencyObject> GetLogicalTree()
        {
            return new WindowControl(ImageWindow.AppVar.Dynamic()).LogicalTree();    // 現在の画面状況を取得
        }

        public (double X, double Y) GetImageLeftTop()
        {
            dynamic window = ImageWindow.AppVar.Dynamic();
            Point p = window.GetRelativePoint(window.ImageArea, Image);
            return (p.X, p.Y);
        }

        public void CloseWindow()
        {
            ImageWindow.Close();
        }
    }
}
