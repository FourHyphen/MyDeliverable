// 要参照: PresentationCore

namespace MyUtility
{
    public static class Clipboard
    {
        public static string GetText()
        {
            return System.Windows.Clipboard.GetText();
        }

        public static void SetText(string str)
        {
            System.Windows.Clipboard.SetText(str);
        }

        public static void Clear()
        {
            System.Windows.Clipboard.Clear();
        }
    }
}
