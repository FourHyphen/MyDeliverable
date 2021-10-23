using System;
using System.Windows;
using System.Windows.Media;

namespace WpfDeliverable
{
    public partial class ImageWindow : Window
    {
        private System.Windows.Controls.Image DisplayImage { get; set; }

        public ImageWindow(string imagePath = "../../Resource/Image.jpg")
        {
            InitializeComponent();
            Init(imagePath);
        }

        private void Init(string imagePath)
        {
            CreateDisplayImage(System.IO.Path.GetFullPath(imagePath));
            ImageArea.Children.Add(DisplayImage);
        }

        private void CreateDisplayImage(string imagePath)
        {
            DisplayImage = new System.Windows.Controls.Image();
            DisplayImage.Source = Image.GetShowImage(imagePath, 200, 112);    // 112 = 200 * 9 / 16;
            DisplayImage.Stretch = Stretch.None;
            DisplayImage.VerticalAlignment = VerticalAlignment.Top;
            DisplayImage.HorizontalAlignment = HorizontalAlignment.Left;
            DisplayImage.Name = "ImageCreated";
        }

        private void MoveXPlusButtonClick(object sender, RoutedEventArgs e)
        {
            MoveImage(10, 0);
        }

        private void MoveXMinusButtonClick(object sender, RoutedEventArgs e)
        {
            MoveImage(-10, 0);
        }

        private void MoveYPlusButtonClick(object sender, RoutedEventArgs e)
        {
            MoveImage(0, 10);
        }

        private void MoveYMinusButtonClick(object sender, RoutedEventArgs e)
        {
            MoveImage(0, -10);
        }

        private void MoveImage(int x, int y)
        {
            Point now = GetRelativePoint(ImageArea, DisplayImage);
            Image.Move(DisplayImage, now.X + x, now.Y + y);
        }

        private Point GetRelativePoint(dynamic parent, dynamic child)
        {
            return child.TranslatePoint(new Point(0, 0), parent);
        }
    }
}
