﻿using Codeer.Friendly.Windows;
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
            LabelDriver label = mwd.LabelTest;
            Assert.AreEqual(expected: "test1", actual: label.Content());
        }

        [TestMethod]
        public void Bind()
        {
            MainWindowDriver mwd = new MainWindowDriver(App);
            mwd.BindButton.EmulateClick();

            BindWindowDriver bwd = null;
            try
            {
                bwd = new BindWindowDriver(App);
                Assert.AreEqual(expected: "Init", actual: bwd.Text());

                bwd.OKButton.EmulateClick();
                Assert.AreEqual(expected: "OK clicked", actual: bwd.Text());

                bwd.CancelButton.EmulateClick();
                Assert.AreEqual(expected: "Cancel clicked", actual: bwd.Text());

            }
            finally
            {
                bwd?.CloseWindow();
            }
        }
    }
}
