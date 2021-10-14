using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMyDeliverable
{
    public static class EnvironmentTest
    {
        public static void Set(string testDataFolderName)
        {
            // テストスイートの場合、2回目以降？はすでに設定済み
            if (IsCurrentTestDataFolder(testDataFolderName))
            {
                return;
            }

            // テストの単体実行時
            Environment.CurrentDirectory += "../../../" + testDataFolderName;
            if (IsCurrentTestDataFolder(testDataFolderName))
            {
                return;
            }

            // テストスイートによる全テスト実行時
            Environment.CurrentDirectory += "../../TestMyDeliverable/" + testDataFolderName;
            if (!IsCurrentTestDataFolder(testDataFolderName))
            {
                throw new System.IO.DirectoryNotFoundException($@"Not Found: {Environment.CurrentDirectory}\{testDataFolderName}");
            }
        }

        private static bool IsCurrentTestDataFolder(string testDataFolderName)
        {
            return System.IO.Path.GetFileName(Environment.CurrentDirectory) == testDataFolderName;
        }
    }
}
