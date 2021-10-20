using RM.Friendly.WPFStandardControls;
using System.Windows;    // DependencyObject
using System.Windows.Controls;

namespace TestWpfDeliverable
{
    // 要参照
    //  - Microsoft.CSharp
    //  - PresentationFramework
    //  - PresentationCore
    //  - WindowsBase

    public class LabelAdapter
    {
        public string LabelName { get; }

        public LabelAdapter(string labelName)
        {
            LabelName = labelName;
        }

        public string Content(IWPFDependencyObjectCollection<DependencyObject> logicalTree)
        {
            var label = logicalTree.ByType<Label>().ByName(LabelName).Single();
            string str = label.ToString().Replace("System.Windows.Controls.Label: ", "");
            str = str.Replace("System.Windows.Controls.Label", "");    // 文字列が空の場合の対応
            return str;
        }

        public double ContentNum(IWPFDependencyObjectCollection<DependencyObject> logicalTree)
        {
            string str = Content(logicalTree);
            str = str.Trim();
            return double.Parse(str);
        }
    }
}
