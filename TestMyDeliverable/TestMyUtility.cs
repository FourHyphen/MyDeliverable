using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMyDeliverable
{
    [TestClass]
    public class TestMyUtility
    {
        [TestInitialize]
        public void Init()
        {
            EnvironmentTest.Set();
        }

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

        [TestMethod]
        public void ReflectionDoStaticMethod()
        {
            string ins1 = "ins1";
            string ins2 = "Ins1";

            // オーバーロードメソッド特定テストのため引数違いをテストする
            // 大文字小文字の差を無視した値の比較 -> true を期待
            object res = MyUtility.Reflection.DoStaticMethod("System.String", "Equals", new object[] { ins1, ins2, StringComparison.OrdinalIgnoreCase });
            Assert.IsTrue(res is bool);
            Assert.IsTrue((bool)res);

            // 大文字小文字の差を無視しない値の比較 -> false を期待
            res = MyUtility.Reflection.DoStaticMethod("System.String", "Equals", new object[] { ins1, ins2 });
            Assert.IsTrue(res is bool);
            Assert.IsFalse((bool)res);
        }

        [TestMethod]
        public void ReflectionDoMethod()
        {
            string str = "01_02_03_04_05";

            // オーバーロードメソッド特定テストのため引数違いをテストする
            object res = MyUtility.Reflection.DoMethod(str, "Substring", new object[] { 3, 2 });
            Assert.IsTrue(res is string result && result == "02");

            res = MyUtility.Reflection.DoMethod(str, "Substring", new object[] { 3 });
            Assert.IsTrue(res is string result2 && result2 == "02_03_04_05");
        }

        [TestMethod]
        public void DirectoryZip()
        {
            // 準備
            string folderPath = System.IO.Path.GetFullPath("zipfolder_" + MyUtility.Guid.Get());
            string zipPath = System.IO.Path.GetFullPath("ziptext.zip");
            System.IO.Directory.CreateDirectory(folderPath);
            Assert.IsFalse(System.IO.File.Exists(zipPath));

            // Zip
            MyUtility.Directory.Zip(folderPath, zipPath);

            // ファイル確認
            Assert.IsTrue(System.IO.File.Exists(zipPath));

            // 後始末
            System.IO.Directory.Delete(folderPath);
            System.IO.File.Delete(zipPath);
        }

        [TestMethod]
        public void DirectoryToRecycleBin()
        {
            // 準備: 削除するフォルダを作成
            string folderName = "deletefolder_" + MyUtility.Guid.Get();
            string folderPath = System.IO.Path.GetFullPath(folderName);
            System.IO.Directory.CreateDirectory(folderPath);

            // フォルダのごみ箱への移動
            TestToRecycleBinCore(folderName, folderPath, System.IO.Directory.Exists, MyUtility.Directory.ToRecycleBin);
        }

        private void TestToRecycleBinCore(string name, string path, Func<string, bool> exists, Action<string> toRecycleBin)
        {
            // 準備: ごみ箱に存在しないこと
            var bins = MyUtility.RecycleBin.GetFileNames();
            Assert.IsFalse(bins.Contains(name));

            // 準備: ファイル/フォルダが存在すること
            Assert.IsTrue(exists(path));

            // ごみ箱への移動
            toRecycleBin(path);

            // 削除されたことの確認
            Assert.IsFalse(exists(path));

            // ごみ箱には存在することの確認
            bins = MyUtility.RecycleBin.GetFileNames();
            Assert.IsTrue(bins.Contains(name));
        }

        [TestMethod]
        public void FileZip()
        {
            // 準備
            string filePath = System.IO.Path.GetFullPath($"zipfile_{MyUtility.Guid.Get()}.txt");
            string zipPath = filePath.Replace(".txt", ".zip");
            System.IO.File.AppendAllText(filePath, "testtest");
            Assert.IsFalse(System.IO.File.Exists(zipPath));

            // Zip
            MyUtility.File.Zip(filePath);

            // ファイル確認
            Assert.IsTrue(System.IO.File.Exists(zipPath));

            // 後始末
            System.IO.File.Delete(filePath);
            System.IO.File.Delete(zipPath);
        }

        [TestMethod]
        public void ImageGetSize()
        {
            string imagePath = System.IO.Path.GetFullPath(@".\TestData\ImageGetSize.png");

            // 3840 x 2560
            Assert.Inconclusive();
        }
    }
}
