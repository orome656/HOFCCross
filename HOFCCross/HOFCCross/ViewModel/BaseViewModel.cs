using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal bool ProcPropertyChanged<T>(ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            return PropertyChanged.SetProperty(this, ref currentValue, newValue, propertyName);
        }
        internal void ProcPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public virtual void ClearEvents()
        {
            var invocation = PropertyChanged?.GetInvocationList() ?? new Delegate[0];
            foreach (var p in invocation)
                PropertyChanged -= (PropertyChangedEventHandler)p;
        }

        public void Dispose()
        {
            ClearEvents();
        }
    }
}
