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

        [TestMethod]
        public void ImageWindow()
        {
            MainWindowDriver mwd = new MainWindowDriver(App);
            mwd.ImageButton.EmulateClick();

            ImageWindowDriver iwd = null;
            try
            {
                iwd = new ImageWindowDriver(App);

                iwd.MoveXPlusButton.EmulateClick();
                Assert.AreEqual(expected: (10, 0), actual: iwd.GetImageLeftTop());

                iwd.MoveYPlusButton.EmulateClick();
                iwd.MoveYPlusButton.EmulateClick();
                Assert.AreEqual(expected: (10, 20), actual: iwd.GetImageLeftTop());

                iwd.MoveXMinusButton.EmulateClick();
                Assert.AreEqual(expected: (0, 20), actual: iwd.GetImageLeftTop());

                iwd.MoveYMinusButton.EmulateClick();
                Assert.AreEqual(expected: (0, 10), actual: iwd.GetImageLeftTop());
            }
            finally
            {
                iwd?.CloseWindow();
            }
        }

        [TestMethod]
        public void MessageBox()
        {
            MainWindowDriver mwd = new MainWindowDriver(App);
            string message = mwd.ShowMessageBox();

            Assert.AreEqual(expected: "test_message", actual: message);
        }

        [TestMethod]
        public void EnableKeys()
        {
            MainWindowDriver mwd = new MainWindowDriver(App);
            mwd.EnableKeysButton.EmulateClick();

            EnableKeysWindowDriver ekwd = null;
            try
            {
                ekwd = new EnableKeysWindowDriver(App);

                ekwd.KeyDown(System.Windows.Input.Key.A);
                Assert.AreEqual(expected: "Single", actual: ekwd.KeyTextBox.Text);

                ekwd.KeyDown(System.Windows.Input.Key.Z, System.Windows.Input.ModifierKeys.Control);
                Assert.AreEqual(expected: "Cancel", actual: ekwd.KeyTextBox.Text);

                ekwd.KeyDown(System.Windows.Input.Key.A, System.Windows.Input.ModifierKeys.Shift);
                Assert.AreEqual(expected: "Shift", actual: ekwd.KeyTextBox.Text);

                ekwd.KeyDown(System.Windows.Input.Key.A, System.Windows.Input.ModifierKeys.Control | System.Windows.Input.ModifierKeys.Shift);
                Assert.AreEqual(expected: "CtrlShift", actual: ekwd.KeyTextBox.Text);
            }
            finally
            {
                ekwd.CloseWindow();
            }
        }
    }
}
