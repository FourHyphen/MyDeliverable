using System;

namespace WpfDeliverable
{
    public class ListViewData
    {
        public string Name { get; private set; }

        public string Date { get; private set; }

        public string Type { get; private set; }

        public string Size { get; private set; }

        public ListViewData(int index)
        {
            Name = $"Index{index}";
            Date = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            Type = $"Type{index}";
            Size = $"Size{index}";
        }
    }
}
