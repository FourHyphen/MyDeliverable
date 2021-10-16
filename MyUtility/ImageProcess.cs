using System.Drawing;

namespace MyUtility
{
    public static class ImageProcess
    {
        private static System.Drawing.Bitmap ExampleApplyFilter3x3(Bitmap bitmap)
        {
            byte[] buf = MyUtility.Image.Copy(bitmap);

            // フィルタ処理しない範囲の値を事前に埋めておく
            byte[] resultBuf = new byte[bitmap.Width * bitmap.Height * 4];
            buf.CopyTo(resultBuf, 0);

            int width = bitmap.Width;

            // 3x3フィルタを適用するため端を無視
            int maxWidth = bitmap.Width - 1;
            int maxHeight = bitmap.Height - 1;
            System.Threading.Tasks.Parallel.For(1, maxHeight, y =>
            {
                byte[] rBuf = new byte[9];
                byte[] gBuf = new byte[9];
                byte[] bBuf = new byte[9];
                for (int x = 1; x < maxWidth; x++)
                {
                    CreateFilter3x3(buf, x, y, width, ref rBuf, ref gBuf, ref bBuf);
                    Color c = Color.FromArgb(255, rBuf[4], gBuf[4], bBuf[4]);    // ここでフィルタ処理を実行
                    int i = y * width + x * 4;
                    resultBuf[i] = c.B;
                    resultBuf[i + 1] = c.G;
                    resultBuf[i + 2] = c.R;
                }
            });

            return MyUtility.Image.CreateBitmap(resultBuf, bitmap.Width, bitmap.Height);
        }

        private static void CreateFilter3x3(byte[] buf, int x, int y, int imageWidth, ref byte[] rBuf, ref byte[] gBuf, ref byte[] bBuf)
        {
            int width = imageWidth * 4;
            int xMinus = (x - 1) * 4;
            int xCenter = x * 4;
            int xPlus = (x + 1) * 4;
            int yMinus = (y - 1) * width;
            int yCenter = y * width;
            int yPlus = (y + 1) * width;
            int i1 = yMinus + xMinus;
            int i2 = yMinus + xCenter;
            int i3 = yMinus + xPlus;
            int i4 = yCenter + xMinus;
            int i5 = yCenter + xCenter;
            int i6 = yCenter + xPlus;
            int i7 = yPlus + xMinus;
            int i8 = yPlus + xCenter;
            int i9 = yPlus + xPlus;
            bBuf[0] = buf[i1]; gBuf[0] = buf[i1 + 1]; rBuf[0] = buf[i1 + 2];
            bBuf[1] = buf[i2]; gBuf[1] = buf[i2 + 1]; rBuf[1] = buf[i2 + 2];
            bBuf[2] = buf[i3]; gBuf[2] = buf[i3 + 1]; rBuf[2] = buf[i3 + 2];
            bBuf[3] = buf[i4]; gBuf[3] = buf[i4 + 1]; rBuf[3] = buf[i4 + 2];
            bBuf[4] = buf[i5]; gBuf[4] = buf[i5 + 1]; rBuf[4] = buf[i5 + 2];
            bBuf[5] = buf[i6]; gBuf[5] = buf[i6 + 1]; rBuf[5] = buf[i6 + 2];
            bBuf[6] = buf[i7]; gBuf[6] = buf[i7 + 1]; rBuf[6] = buf[i7 + 2];
            bBuf[7] = buf[i8]; gBuf[7] = buf[i8 + 1]; rBuf[7] = buf[i8 + 2];
            bBuf[8] = buf[i9]; gBuf[8] = buf[i9 + 1]; rBuf[8] = buf[i9 + 2];
        }
    }
}
