using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System;
using System.Windows;
using System.Windows.Input;

namespace TestWpfDeliverable
{
    public class EnableKeysWindowDriver
    {
        private WindowControl EnableKeysWindow { get; }

        public WPFTextBox KeyTextBox { get; }

        public EnableKeysWindowDriver(WindowsAppFriend app)
        {
            EnableKeysWindow = WindowControl.IdentifyFromWindowText(app, "EnableKeysWindow");
            KeyTextBox = new WPFTextBox(EnableKeysWindow.AppVar.Dynamic().KeyTextBox);
        }

        public void KeyDown(Key key, ModifierKeys modifier = ModifierKeys.None)
        {
            EnableKeysWindow.AppVar.Dynamic().WindowKeyDown(key, modifier);
        }

        // DLL インジェクションして相手のプロセスから KeyTextBox.Text を取得したかった
        //public string GetKeyTextBoxText(WindowsAppFriend app)
        //{
        //    WindowsAppExpander.LoadAssembly(app, GetType().Assembly);
        //    dynamic type = app.Type(GetType());
        //    return type.GetKeyTextBoxTextForWindowProcess(EnableKeysWindow.AppVar.Dynamic());
        //}

        public void CloseWindow()
        {
            EnableKeysWindow.Close();
        }
    }
}