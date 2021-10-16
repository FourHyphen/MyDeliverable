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

        private static System.Drawing.Color UnsharpFilter(byte[] rBuf, byte[] gBuf, byte[] bBuf, double k)
        {
            // 参考: https://imagingsolution.blog.fc2.com/blog-entry-114.html
            double aroundRate = -k / 9.0;
            double centerRate = (8.0 * k + 9.0) / 9.0;
            byte r = UnsharpFilter(rBuf, aroundRate, centerRate);
            byte g = UnsharpFilter(gBuf, aroundRate, centerRate);
            byte b = UnsharpFilter(bBuf, aroundRate, centerRate);
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        private static byte UnsharpFilter(byte[] b, double aroundRate, double centerRate)
        {
            double tmp1 = b[0] * aroundRate + b[1] * aroundRate + b[2] * aroundRate;
            double tmp2 = b[3] * aroundRate + b[4] * centerRate + b[5] * aroundRate;
            double tmp3 = b[6] * aroundRate + b[7] * aroundRate + b[8] * aroundRate;
            double result = tmp1 + tmp2 + tmp3;

            if (result < 0.0) return (byte)0;
            if (result > 255.0) return (byte)255;
            return (byte)result;
        }

        private static System.Drawing.Color GetPixelColorFakePixelMixing(byte[] rBuf, byte[] gBuf, byte[] bBuf, int x, int y, int width, int height, (double X, double Y) rotate)
        {
            double directionX = rotate.X - (double)x;
            double directionY = rotate.Y - (double)y;

            if (!DoFitPointConsideringFilterSize(width, height, x, y, 1))
            {
                return Color.FromArgb(rBuf[4], gBuf[4], bBuf[4]);
            }

            (double X, double Y) p = (directionX, directionY);
            int ia, ib, ic, id;
            (double X, double Y) pa, pb, pc, pd;

            if (directionX < 0.0 && directionY < 0.0)
            {
                // c1, c2, c4, c5
                ia = 0;
                ib = 1;
                ic = 3;
                id = 4;
                pa = (-1.0, -1.0);
                pb = (0.0, -1.0);
                pc = (-1.0, 0.0);
                pd = (0.0, 0.0);
            }
            else if (directionX >= 0.0 && directionY < 0.0)
            {
                // c2, c3, c5, c6
                ia = 1;
                ib = 2;
                ic = 4;
                id = 5;
                pa = (0.0, -1.0);
                pb = (1.0, -1.0);
                pc = (0.0, 0.0);
                pd = (1.0, 0.0);
            }
            else if (directionX < 0.0 && directionY >= 0.0)
            {
                // c4, c5, c7, c8
                ia = 3;
                ib = 4;
                ic = 6;
                id = 7;
                pa = (-1.0, 0.0);
                pb = (0.0, 0.0);
                pc = (-1.0, 1.0);
                pd = (0.0, 1.0);
            }
            else
            {
                // c5, c6, c8, c9
                ia = 4;
                ib = 5;
                ic = 7;
                id = 8;
                pa = (0.0, 0.0);
                pb = (1.0, 0.0);
                pc = (0.0, 1.0);
                pd = (1.0, 1.0);
            }

            double da = 1.0 / MyUtility.Math.CalcDistance(pa, p);
            double db = 1.0 / MyUtility.Math.CalcDistance(pb, p);
            double dc = 1.0 / MyUtility.Math.CalcDistance(pc, p);
            double dd = 1.0 / MyUtility.Math.CalcDistance(pd, p);
            double sum = da + db + dc + dd;
            double rd = rBuf[ia] * da / sum + rBuf[ib] * db / sum + rBuf[ic] * dc / sum + rBuf[id] * dd / sum;    // sumでの除算をまとめるとテストに失敗する
            double gd = gBuf[ia] * da / sum + gBuf[ib] * db / sum + gBuf[ic] * dc / sum + gBuf[id] * dd / sum;
            double bd = bBuf[ia] * da / sum + bBuf[ib] * db / sum + bBuf[ic] * dc / sum + bBuf[id] * dd / sum;

            byte r = (rd > 255.0) ? (byte)255 : (byte)rd;
            byte g = (gd > 255.0) ? (byte)255 : (byte)gd;
            byte b = (bd > 255.0) ? (byte)255 : (byte)bd;
            return System.Drawing.Color.FromArgb(r, g, b);
        }

        private static bool DoFitPointConsideringFilterSize(int width, int height, int x, int y, int filter)
        {
            int xMax = width - filter;
            int yMax = height - filter;
            return (filter < x && x < xMax) && (filter < y && y < yMax);
        }
    }
}
