using Codeer.Friendly.Windows;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace TestWpfDeliverable
{
    // 必要なパッケージ
    //  -> Codeer.Friendly
    //  -> Codeer.Friendly.Windows         -> WindowsAppFriend()
    //  -> Codeer.Friendly.Windows.Grasp   -> WindowControl()
    //  -> RM.Friendly.WPFStandardControls -> 各種WPFコントロールを取得するために必要

    [TestClass]
    public class TestFeature
    {
        private string AttachExeName = "WpfDeliverable.exe";
        private WindowsAppFriend App;

        [TestInitialize]
        public void Init()
        {
            string exePath = System.IO.Path.GetFullPath(AttachExeName);
            App = new WindowsAppFriend(Process.Start(exePath));
        }

        [TestCleanup]
        public void Cleanup()
        {
            CloseWindow();
            App.Dispose();
        }

        private void CloseWindow()
        {
            Process exeMainProcess = Process.GetProcessById(App.ProcessId);
            exeMainProcess.CloseMainWindow();
            exeMainProcess.Dispose();
        }

        [TestMethod]
        public void TestInitAndCleanup()
        {
            // Init による MainWindow 立ち上げと Cleanup による MainWindow クローズが上手くいけば OK
        }

        [TestMethod]
        public void LabelAdapter()
        {
            MainWindowDriver mwd = new MainWindowDriver(App);
            Assert.AreEqual(expected: "test1", actual: mwd.GetLabelTest());
        }
    }
}
