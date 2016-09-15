using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace HOFCCross.UWP
{
    internal partial class UWPAccountStore : AccountStore
    {
        public override IEnumerable<Account> FindAccountsForService(string serviceId)
        {
            return FindAccountsForServiceAsync(serviceId).Result;
        }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        public override void Save(Account account, string serviceId)
        {
            SaveAsync(account, serviceId);
        }

        public override void Delete(Account account, string serviceId)
        {
            DeleteAsync(account, serviceId);
        }
#pragma warning restore 4014
    }
}