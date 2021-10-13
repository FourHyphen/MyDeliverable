using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;    // MethodImpl 属性に必要

namespace MyUtility
{
    public static class RecycleBin
    {
        /// <summary>
        /// ごみ箱に存在するファイル/フォルダ名(not path)を返す
        /// </summary>
        /// <returns></returns>
        // JIT のインライン展開を防止して確実に GC 処理させる
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static List<string> GetFileNames()
        {
            List<string> result = new List<string>();

            // 参考: http://nonsoft.la.coocan.jp/SoftSample/CS.NET/SampleRccList.html
            // 要作業(1): 参照に COM: Microsoft Shell Controls And Automation を追加 -> Shell32 と表示される
            // 要作業(2): 参照の Shell32 の "相互運用型の埋め込み" を False に変更する
            Shell32.ShellClass shl = new Shell32.ShellClass();
            Shell32.Folder fol = shl.NameSpace(10);

            foreach (Shell32.FolderItem folderItem in fol.Items())
            {
                // ごみ箱内のパスはもとのファイル名ではないので、ごみ箱内ファイルのもとのファイル名を取得
                // ex) folderItem.Path = C:\\$Recycle.Bin\\S-1-5-21-638183743-3077767593-1375336953-1001\\$R22YH79.txt
                string beforeFileName = fol.GetDetailsOf(folderItem, 0);    // 0 -> ファイル名 / 1 -> もとのフォルダパス / 2 -> 削除日 / etc....
                result.Add(beforeFileName);
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            return result;
        }
    }
}
