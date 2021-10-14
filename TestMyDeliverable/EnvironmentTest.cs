using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMyDeliverable
{
    public static class EnvironmentTest
    {
        public static void Set()
        {
            // テストスイートの場合、2回目以降？はすでに設定済み
            if (IsCurrentTestDataFolder())
            {
                return;
            }

            // テストの単体実行時
            Environment.CurrentDirectory += "../../../TestData";
            if (IsCurrentTestDataFolder())
            {
                return;
            }

            // テストスイートによる全テスト実行時
            Environment.CurrentDirectory += "../TestMyDeliverable";
            if (!IsCurrentTestDataFolder())
            {
                throw new System.IO.DirectoryNotFoundException($@"Not Found: {Environment.CurrentDirectory}\TestData");
            }
        }

        private static bool IsCurrentTestDataFolder()
        {
            return System.IO.Path.GetFileName(Environment.CurrentDirectory) == "TestData";
        }
    }
}
