using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public abstract class FilteredListBaseViewModel<T, V>: ListBaseViewModel<V>
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
