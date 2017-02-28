using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace HOFCCross.Service
{
    public interface ILoginService
    {
        event EventHandler LoginStatusChanged;
        bool IsAuthenticated();
        Task AuthenticateAsync(Account account);
        User GetUser();
        void Disconnect();
    }
}
