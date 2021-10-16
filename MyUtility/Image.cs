using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MyUtility
{
    public static class Image
    {
        /// <summary>
        /// 画像のサイズを取得する
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns>タプル: (int Width, int Height)</returns>
        public static (int Width, int Height) GetSize(string imagePath)
        {
            // 画像を開く際に検証処理しないことで高速に読み込む
            using (System.IO.FileStream fs = System.IO.File.OpenRead(imagePath))
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs, false, false))
            {
                return (image.Width, image.Height);
            }
        }

        /// <summary>
        /// Bitmapの画素値をbyte配列にコピーする(並びはBGRABGRA...)
        /// </summary>
        /// <param name="src"></param>
        /// <returns>byte[].Length = Width * 4 * Height</returns>
        public static byte[] Copy(Bitmap src)
        {
            BitmapData data = src.LockBits(new Rectangle(0, 0, src.Width, src.Height),
                                               ImageLockMode.ReadWrite,
                                               PixelFormat.Format32bppArgb);
            byte[] dst = new byte[src.Width * 4 * src.Height];
            Marshal.Copy(data.Scan0, dst, 0, dst.Length);
            src.UnlockBits(data);

            return dst;
        }

        /// <summary>
        /// byte配列からBitmapを作成
        /// </summary>
        /// <param name="src">Length = Width * 4 * Height, 並びはBGRABGRA...</param>
        /// <param name="bitmapWidth"></param>
        /// <param name="bitmapHeight"></param>
        /// <returns></returns>
        public static Bitmap CreateBitmap(byte[] src, int bitmapWidth, int bitmapHeight)
        {
            Bitmap bitmap= new Bitmap(bitmapWidth, bitmapHeight);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                              ImageLockMode.ReadWrite,
                                              PixelFormat.Format32bppArgb);
            Marshal.Copy(src, 0, data.Scan0, src.Length);
            bitmap.UnlockBits(data);

            return bitmap;
        }
    }
}
