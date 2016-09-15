using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace HOFCCross.Factory
{
    public static class AccountStoreFactory
    {
        public static Func<AccountStore> Create { get; set; } = () => AccountStore.Create();
    }
}
