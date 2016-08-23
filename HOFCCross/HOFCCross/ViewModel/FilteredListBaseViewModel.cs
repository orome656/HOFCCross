using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public abstract class FilteredListBaseViewModel<T, V>: BaseViewModel
    {
        private List<T> _filters;
        public List<T> Filters
        {
            get { return _filters; }
            set
            {
                _filters = value;
                RaisePropertyChanged(nameof(Filters));
            }
        }

        private T _selectedFilter;
        public T SelectedFilter {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                RaisePropertyChanged(nameof(SelectedFilter));
                ReloadItems();
            }
        }


        private List<V> _items;
        public List<V> Items {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }

        protected abstract Task ReloadItems(bool forceRefresh = false);

        public Command RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    ReloadItems(true);
                });
            }
        }
    }
}
