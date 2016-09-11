using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    public interface ILoginService
    {
        bool IsAuthenticated();
        User GetUser();
        Task RequestUserInfo();
        Task RefreshToken();
    }
}
