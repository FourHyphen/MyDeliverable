using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public static class Image
    {
        public static (int Width, int Height) GetWidthAndHeight(string imagePath)
        {
            // 画像を開く際に検証処理しないことで高速に読み込む
            using (System.IO.FileStream fs = System.IO.File.OpenRead(imagePath))
            using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs, false, false))
            {
                return (image.Width, image.Height);
            }
        }
    }
}
