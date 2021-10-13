//using Microsoft.Win32;
//using Microsoft.WindowsAPICodePack.Dialogs;    // NuGet から WindowsAPICodePack-Shell をインストールする

//namespace MyUtility
//{
//    public static class Dialog
//    {
//        public static string ShowOpenFolderDialog()
//        {
//            CommonOpenFileDialog cofd = CreateCommonOpenFileDialog();
//            string folderPath = null;
//            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
//            {
//                folderPath = cofd.FileName;
//            }

//            return folderPath;
//        }

//        private static CommonOpenFileDialog CreateCommonOpenFileDialog()
//        {
//            CommonOpenFileDialog cofd = new CommonOpenFileDialog();

//            cofd.Title = "開くフォルダを選択してください";
//            cofd.RestoreDirectory = true;
//            cofd.IsFolderPicker = true;
//            return cofd;
//        }

//        public static string ShowOpenFileDialog()
//        {
//            OpenFileDialog ofd = CreateOpenFileDialog();
//            string filePath = "";
//            if (ofd.ShowDialog() == true)
//            {
//                filePath = ofd.FileName;
//            }

//            return filePath;
//        }

//        private static OpenFileDialog CreateOpenFileDialog()
//        {
//            OpenFileDialog ofd = new OpenFileDialog();

//            ofd.FileName = "";
//            ofd.Filter = "すべてのファイル(*.*)|*.*";
//            ofd.FilterIndex = 1;
//            ofd.Title = "開くファイルを選択してください";
//            ofd.RestoreDirectory = true;
//            ofd.CheckFileExists = true;
//            ofd.CheckPathExists = true;

//            return ofd;
//        }

//        public static string ShowSaveFileDialog()
//        {
//            SaveFileDialog sfd = CreateSaveFileDialog();
//            string filePath = "";
//            if (sfd.ShowDialog() == true)
//            {
//                filePath = sfd.FileName;
//            }

//            return filePath;
//        }

//        private static SaveFileDialog CreateSaveFileDialog()
//        {
//            SaveFileDialog sfd = new SaveFileDialog();
//            //sfd.InitialDirectory = "";
//            //sfd.FileName = "";
//            sfd.Title = "保存先を指定してください";
//            sfd.RestoreDirectory = true;
//            sfd.CheckFileExists = false;
//            sfd.CheckPathExists = false;

//            return sfd;
//        }
//    }
//}