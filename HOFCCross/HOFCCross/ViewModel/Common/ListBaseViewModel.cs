using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Service;

namespace HOFCCross.ViewModel.Common
{
    public abstract class ListBaseViewModel<T>: BaseViewModel
    {
        private List<T> _items;

        public ListBaseViewModel(IService service) : base(service)
        {
        }

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
