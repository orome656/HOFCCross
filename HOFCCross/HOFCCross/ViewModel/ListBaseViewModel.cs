using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public abstract class ListBaseViewModel<T>: BaseViewModel
    {
        private List<T> _items;
        public List<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        protected abstract Task ReloadItems(bool forceRefresh = false);
    }
}
