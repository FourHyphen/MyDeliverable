using System.IO.Compression;

namespace MyUtility
{
    public static class File
    {
        /// <summary>
        /// ファイルと同階層にzip圧縮ファイルを作成する。zipファイル名はファイルの拡張子を".zip"に置き換えたもの
        /// </summary>
        /// <param name="filePath"></param>
        public static void Zip(string filePath)
        {
            string ext = System.IO.Path.GetExtension(filePath);
            string zipName = System.IO.Path.GetFileName(filePath).Replace(ext, ".zip");
            string zipPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), zipName);

            Zip(filePath, zipPath);
        }

        /// <summary>
        /// ファイルをzip圧縮する
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="zipPath"></param>
        public static void Zip(string filePath, string zipPath)
        {
            // 要参照: System.IO.Compression および System.IO.Compression.FileStream
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                // CreateEntryFromFile() の呼び出しには using System.IO.Compression が必要
                archive.CreateEntryFromFile(filePath, System.IO.Path.GetFileName(filePath));
            }
        }
    }
}
