using System;
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
    }
}
