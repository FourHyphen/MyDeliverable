using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfDeliverable
{
    public class Image
    {
        public static BitmapSource GetShowImage(string imagePath)
        {
            Bitmap bitmap = new Bitmap(imagePath);
            return GetShowImage(bitmap);
        }

        public static BitmapSource GetShowImage(Bitmap bitmap)
        {
            return CreateBitmapSourceImage(bitmap);
        }

        public static BitmapSource GetShowImage(string imagePath, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(imagePath))
            {
                return GetShowImage(bitmap, width, height);
            }
        }

        public static BitmapSource GetShowImage(Bitmap bitmap, int width, int height)
        {
            using (Bitmap resized = CreateResizeBitmap(bitmap, width, height))
            {
                return CreateBitmapSourceImage(resized);
            }
        }

        private static Bitmap CreateResizeBitmap(Bitmap bitmap, int newWidth, int newHeight)
        {
            Bitmap reductionImage = new Bitmap(newWidth, newHeight);

            using (Graphics g = Graphics.FromImage(reductionImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bitmap, 0, 0, newWidth, newHeight);
            }

            return reductionImage;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        public static extern bool DeleteObject(IntPtr hObject);

        private static BitmapSource CreateBitmapSourceImage(Bitmap bitmapImage)
        {
            // 参考: http://qiita.com/KaoruHeart/items/dc130d5fc00629c1b6ea
            IntPtr handle = bitmapImage.GetHbitmap();
            try
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    handle,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions()
                    );
            }
            finally
            {
                DeleteObject(handle);
            }
        }

        /// <summary>
        /// (x, y)に移動する
        /// </summary>
        /// <param name="image"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void Move(System.Windows.Controls.Image image, double x, double y)
        {
            image.RenderTransform = GetNotesTransform(x, y);
        }

        private static Transform GetNotesTransform(double x, double y)
        {
            var transform = new TransformGroup();
            transform.Children.Add(new TranslateTransform(x, y));

            return transform;
        }
    }
}
