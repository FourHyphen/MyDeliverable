using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows.Grasp;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace TestWpfDeliverable
{
    // 要参照
    //  - Microsoft.CSharp
    //  - PresentationFramework
    //  - PresentationCore
    //  - WindowsBase

    public class LabelDriver
    {
        private dynamic Window { get; }

        private string LabelName { get; }

        public LabelDriver(dynamic window, string labelName)
        {
            Window = window;
            LabelName = labelName;
        }

        public string Content()
        {
            return GetLabel().Content.ToString();
        }

        private dynamic GetLabel()
        {
            var tree = new WindowControl(Window).LogicalTree();
            return tree.ByType<Label>().ByName(LabelName).Single().Dynamic();
        }
    }
}
