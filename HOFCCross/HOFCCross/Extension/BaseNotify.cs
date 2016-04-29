using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace System.ComponentModel
{
    public static class BaseNotify
    {

        public static bool SetProperty<T>(this PropertyChangedEventHandler handler, object sender, ref T currentValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
                return false;
            currentValue = newValue;
            if (handler == null)
                return true;
            handler.Invoke(sender, new PropertyChangedEventArgs(propertyName));
            return true;
        }
    }
}
