namespace MyUtility
{
    public static class Path
    {
        /// <summary>
        /// 存在するファイル/フォルダのパスがフォルダかを返す
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFolder(string path)
        {
            return System.IO.File.GetAttributes(path).HasFlag(System.IO.FileAttributes.Directory);
        }
    }
}
