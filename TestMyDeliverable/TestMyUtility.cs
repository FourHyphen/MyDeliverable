using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMyDeliverable
{
    [TestClass]
    public class TestMyUtility
    {
        [TestMethod]
        public void Clipboard()
        {
            string str = @"test_clipboard";

            // 準備: 確認
            Assert.IsTrue(str != MyUtility.Clipboard.GetText());

            // Test: Get & Set
            MyUtility.Clipboard.SetText(str);
            Assert.IsTrue(str == MyUtility.Clipboard.GetText());

            // Test: Clear
            MyUtility.Clipboard.Clear();
            Assert.IsTrue(MyUtility.Clipboard.GetText() == "");
        }

        [TestMethod]
        public void CommandHistory()
        {
            MyUtility.CommandHistory<string> ch = new MyUtility.CommandHistory<string>();
            ch.Execute("one", "ToUpper");
            ch.Execute("two", "IndexOf", new object[] { 'a', 1 });    // overload メソッドの特定テスト

            // Undo -> 1 つ戻る
            string now = (string)ch.Undo(1);
            Assert.IsTrue(now == "one");

            // Redo -> 1 つ進む(最新の履歴を参照している)
            now = (string)ch.Redo(1);
            Assert.IsTrue(now == "two");

            // 最新からRedoしても変わらない＆クラッシュしない
            now = (string)ch.Redo(1);
            Assert.IsTrue(now == "two");

            // 最古以前に戻ろうとすると null が返ってくる＆クラッシュしない
            now = (string)ch.Undo(1);    // "one"
            now = (string)ch.Undo(1);
            Assert.IsTrue(now == null);

            // 履歴の途中で別操作を挟んだ場合、その時点以降の履歴が消え、今回挟んだ操作が最新になる
            ch.Redo(1);
            ch.Execute("three", "ToUpper");

            now = (string)ch.Undo(1);
            Assert.IsTrue(now == "one");
            now = (string)ch.Redo(1);
            Assert.IsTrue(now == "three");
        }
    }
}
