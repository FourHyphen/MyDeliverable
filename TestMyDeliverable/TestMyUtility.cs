using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMyDeliverable
{
    [TestClass]
    public class TestMyUtility
    {
        [TestInitialize]
        public void Init()
        {
            // Current = "......\TestData"
            EnvironmentTest.Set("TestData");
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
            string imagePath = System.IO.Path.GetFullPath(@"ImageGetSize.jpg");

            (int Width, int Height) size = MyUtility.Image.GetSize(imagePath);
            Assert.AreEqual(expected: (3840, 2560), actual: size);
        }

        [TestMethod]
        public void ImageBytes()
        {
            string imagePath = System.IO.Path.GetFullPath(@"ImageBytes.jpg");

            using (Bitmap bitmap1 = new Bitmap(imagePath))
            {
                byte[] bytes = MyUtility.Image.Copy(bitmap1);
                Color c = bitmap1.GetPixel(0, 0);
                Assert.AreEqual(expected: c.B, actual: bytes[0]);
                Assert.AreEqual(expected: c.G, actual: bytes[1]);
                Assert.AreEqual(expected: c.R, actual: bytes[2]);

                using (Bitmap bitmap2 = MyUtility.Image.CreateBitmap(bytes, bitmap1.Width, bitmap1.Height))
                {
                    Color c1 = bitmap1.GetPixel(bitmap1.Width - 1, bitmap1.Height - 1);
                    Color c2 = bitmap2.GetPixel(bitmap2.Width - 1, bitmap2.Height - 1);
                    Assert.AreEqual(expected: c1, actual: c2);
                }
            }
        }

        [TestMethod]
        public void ImageTrim()
        {
            string imagePath = System.IO.Path.GetFullPath(@"ImageTrim.jpg");

            using (Bitmap bitmap = new Bitmap(imagePath))
            using (Bitmap trimmed = MyUtility.Image.Trim(bitmap, (10, 10), (30, 20)))
            {
                Assert.AreEqual(expected: 20, actual: trimmed.Width);
                Assert.AreEqual(expected: 10, actual: trimmed.Height);
                Color c1 = bitmap.GetPixel(25, 15);
                Color c2 = trimmed.GetPixel(15, 5);
                Assert.AreEqual(c1, c2);
            }
        }

        [TestMethod]
        public void MathMin()
        {
            int a = 2, b = 3;
            Assert.AreEqual(expected: a, actual: MyUtility.Math.Min(a, b));
            Assert.AreEqual(expected: a, actual: MyUtility.Math.Min(b, a));

            int c = -1, d = 0;
            Assert.AreEqual(expected: c, actual: MyUtility.Math.Min(c, d));
            Assert.AreEqual(expected: c, actual: MyUtility.Math.Min(d, c));

            int e = -4, f = -3;
            Assert.AreEqual(expected: e, actual: MyUtility.Math.Min(e, f));
            Assert.AreEqual(expected: e, actual: MyUtility.Math.Min(f, e));
        }

        [TestMethod]
        public void MathRound()
        {
            // 正数のみ / 負数の挙動は未定義
            Assert.AreEqual(expected: 1, actual: MyUtility.Math.Round(0.51));
            Assert.AreEqual(expected: 0, actual: MyUtility.Math.Round(0.49));
        }

        [TestMethod]
        public void MathCalcRotatePoint()
        {
            // 実装時の値を正とする
            (double X, double Y) p = MyUtility.Math.CalcRotatePoint((3.0, 2.0), (1.0, 1.0), 30);
            Assert.IsTrue(System.Math.Round(p.X, 10) == System.Math.Round(2.2320508075688776, 10));
            Assert.IsTrue(System.Math.Round(p.Y, 10) == System.Math.Round(2.8660254037844384, 10));
        }

        [TestMethod]
        public void Json()
        {
            string jsonPath = System.IO.Path.GetFullPath($"JsonTest_{MyUtility.Guid.Get()}.json");
            JsonTest test = new JsonTest(
                42,
                new string[] { "A", "B", "C" },
                new Dictionary<string, int> { { "MapA", 10 }, { "MapB", 20 } }
                );
            MyUtility.Json.CreateFile(test, jsonPath);
            Assert.IsTrue(System.IO.File.Exists(jsonPath));

            JsonTest test2 = MyUtility.Json.Read<JsonTest>(jsonPath);
            Assert.IsTrue(test == test2);

            // 後始末
            System.IO.File.Delete(jsonPath);
        }

        public class JsonTest
        {
            public int Id { get; private set; }

            public string[] Names { get; private set; }

            public Dictionary<string, int> Map { get; private set; }

            public JsonTest(int id, string[] names, Dictionary<string, int> map)
            {
                Id = id;
                Names = names;
                Map = map;
            }

            public static bool operator ==(JsonTest a, JsonTest b)
            {
                if (a.Id != b.Id) return false;

                // using System.Linq;
                if (!a.Names.SequenceEqual(b.Names)) return false;

                if (!a.Map.OrderBy(o => o.Key).SequenceEqual(b.Map.OrderBy(o => o.Key))) return false;

                return true;
            }

            public static bool operator !=(JsonTest a, JsonTest b)
            {
                return !(a == b);
            }
        }

        [TestMethod]
        public void ListAreEqual()
        {
            // 値型
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list2 = list1;
            List<int> list3 = new List<int>() { 2, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list4 = new List<int>() { 2, 1, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> list5 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> list6 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.IsTrue(MyUtility.List.AreEqual(list1, list2));
            Assert.IsFalse(MyUtility.List.AreEqual(list1, list3));
            Assert.IsFalse(MyUtility.List.AreEqual(list1, list4));
            Assert.IsFalse(MyUtility.List.AreEqual(list1, list5));
            Assert.IsTrue(MyUtility.List.AreEqual(list1, list6));

            // 参照型
            List<string> slist1 = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            List<string> slist2 = slist1;
            List<string> slist3 = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "I" };
            List<string> slist4 = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "J", "I" };
            List<string> slist5 = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            List<string> slist6 = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            Assert.IsTrue(MyUtility.List.AreEqual(slist1, slist2));
            Assert.IsFalse(MyUtility.List.AreEqual(slist1, slist3));
            Assert.IsFalse(MyUtility.List.AreEqual(slist1, slist4));
            Assert.IsFalse(MyUtility.List.AreEqual(slist1, slist5));
            Assert.IsTrue(MyUtility.List.AreEqual(slist1, slist6));
        }

        [TestMethod]
        public void ListShuffle()
        {
            List<string> list = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            List<string> shuffle1 = MyUtility.List.Shuffle(list);
            List<string> shuffle2 = MyUtility.List.Shuffle(list);
            Assert.IsFalse(MyUtility.List.AreEqual(list, shuffle1));
            Assert.IsFalse(MyUtility.List.AreEqual(shuffle1, shuffle2));
            Assert.IsTrue(MyUtility.List.AreEqual(shuffle1, shuffle1));
        }

        [TestMethod]
        public void PathIsFolder()
        {
            string guid = MyUtility.Guid.Get();
            string filePath = System.IO.Path.GetFullPath($"PathIsFolder_File_{guid}.txt");
            string folderPath = System.IO.Path.GetFullPath($"PathIsFolder_Folder_{guid}");

            System.IO.File.WriteAllText(filePath, "filePath");
            System.IO.Directory.CreateDirectory(folderPath);

            Assert.IsTrue(MyUtility.Path.IsFolder(folderPath));
            Assert.IsFalse(MyUtility.Path.IsFolder(filePath));

            // 後始末
            System.IO.File.Delete(filePath);
            System.IO.Directory.Delete(folderPath);
        }
    }
}
