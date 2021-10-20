using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDeliverable
{
    public class BindData : NotifyPropertyChanged
    {
        private string _BindStr;

        public string BindStr
        {
            get => _BindStr;
            set => SetProperty(ref _BindStr, value);
        }
    }
}
