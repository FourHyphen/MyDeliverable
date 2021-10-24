using System.Windows.Input;

namespace WpfDeliverable
{
    public class EnableKey
    {
        /// <summary>
        /// アプリケーションで有効なキーボードショートカット
        /// </summary>
        public enum KeyShortcut
        {
            Single,
            Shift,
            CtrlShift,
            Cancel,
            Redo,
            Else
        }

        /// <summary>
        /// キーイベントをキーボードショートカットに変換する
        /// </summary>
        /// <returns></returns>
        public static KeyShortcut ToKeyShortcut(Key key, KeyboardDevice keyboard)
        {
            return ToKeyShortcut(key, keyboard.Modifiers);
        }

        /// <summary>
        /// キーイベントをキーボードショートカットに変換する
        /// </summary>
        /// <returns></returns>
        public static KeyShortcut ToKeyShortcut(Key key, ModifierKeys modifier)
        {
            KeyShortcut keyConbination = ToKeyShortcutWithModifier(key, modifier);
            if (keyConbination != KeyShortcut.Else)
            {
                return keyConbination;
            }

            return ToKeyShortcutOnlyKey(key);
        }

        private static KeyShortcut ToKeyShortcutWithModifier(Key key, ModifierKeys modifier)
        {
            // Ctrl + Shift + 何か
            if (modifier == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                if (key == Key.A)
                {
                    return KeyShortcut.CtrlShift;
                }
            }

            // Ctrl + 何か
            if (modifier == ModifierKeys.Control)
            {
                if (key == Key.Z)
                {
                    return KeyShortcut.Cancel;
                }
                else if (key == Key.Y)
                {
                    return KeyShortcut.Redo;
                }
            }

            // Shift + 何か
            if (modifier == ModifierKeys.Shift)
            {
                if (key == Key.A)
                {
                    return KeyShortcut.Shift;
                }
            }

            return KeyShortcut.Else;
        }

        private static KeyShortcut ToKeyShortcutOnlyKey(Key key)
        {
            if (key == Key.A)
            {
                return KeyShortcut.Single;
            }

            return KeyShortcut.Else;
        }
    }
}
