using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDeliverable
{
    public class ListViewDataList : NotifyPropertyChanged
    {
        private object _SelectedItem;

        public object SelectedItem
        {
            get => _SelectedItem;
            set => SetProperty(ref _SelectedItem, value);
        }

        private List<ListViewData> _List = new List<ListViewData>();

        public List<ListViewData> List
        {
            get => _List;
            set => SetProperty(ref _List, value);
        }

        public ListViewDataList()
        {
            for (int i = 0; i < 4; i++)
            {
                _List.Add(new ListViewData(i));
            }
        }
    }
}
