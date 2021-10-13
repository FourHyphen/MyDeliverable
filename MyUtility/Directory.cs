using Microsoft.VisualBasic.FileIO;    // 要参照: Microsoft.VisualBasic

namespace MyUtility
{
    public static class Directory
    {
        public static void Zip(string srcPath, string zipPath)
        {
            // System.IO.Compression.FileStream の参照が必要
            System.IO.Compression.ZipFile.CreateFromDirectory(srcPath, zipPath);
        }

        public static void ToRecycleBin(string folderPath)
        {
            FileSystem.DeleteDirectory(folderPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
    }
}
