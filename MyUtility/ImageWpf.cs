using System;
using System.Drawing;

namespace MyUtility
{
    public static class ImageWpf
    {
        /// <summary>
        /// 表示用画像
        /// </summary>
        /// <returns></returns>
        public static System.Windows.Media.Imaging.BitmapSource GetShowImage(string imagePath, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(imagePath))
            {
                return GetShowImage(bitmap, width, height);
            }
        }

        public static System.Windows.Media.Imaging.BitmapSource GetShowImage(Bitmap bitmap, int width, int height)
        {
            using (Bitmap resized = CreateResizeBitmap(bitmap, width, height))
            {
                return CreateBitmapSourceImage(resized);
            }
        }

        private static System.Drawing.Bitmap CreateResizeBitmap(System.Drawing.Bitmap bitmap, int newWidth, int newHeight)
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

        private static System.Windows.Media.Imaging.BitmapSource CreateBitmapSourceImage(Bitmap bitmapImage)
        {
            // 参考: http://qiita.com/KaoruHeart/items/dc130d5fc00629c1b6ea
            IntPtr handle = bitmapImage.GetHbitmap();
            try
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    handle,
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions()
                    );
            }
            finally
            {
                DeleteObject(handle);
            }
        }
    }
}
