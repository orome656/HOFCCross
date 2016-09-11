using HOFCCross.Constantes;
using HOFCCross.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace HOFCCross.Service
{
    public class LoginService : ILoginService
    {
        public bool IsAuthenticated()
        {
            var account = AccountStore.Create().FindAccountsForService("HOFC").FirstOrDefault();
            return account != null;
        }

        public User GetUser()
        {

            var account = AccountStore.Create().FindAccountsForService("HOFC").FirstOrDefault();
            if(account != null)
            {
                return new User() {
                    Username = account.Username
                };
            }
            return null;
            /*
            return new Model.User()
            {
                FirstName = "Monsieur",
                Name = "Dupont"
            };*/
        }

        public async Task RequestUserInfo()
        {
            try
            {
                var account = AccountStore.Create().FindAccountsForService("HOFC").FirstOrDefault();
                OAuth2Request request = new OAuth2Request(HttpMethod.Get.Method, new Uri(AppConstantes.USER_INFOS_URL), null, account);
                var response = await request.GetResponseAsync();
                User user = JsonConvert.DeserializeObject<User>(response.GetResponseText());
                if(user != null)
                {
                    await AccountStore.Create().DeleteAsync(account, "HOFC");
                    account.Properties.Add(nameof(User.Email), user.Email);
                    account.Properties.Add(nameof(User.Username), user.Username);
                    account.Properties.Add(nameof(User.Sub), user.Sub);
                    account.Username = user.Username;
                    await AccountStore.Create().SaveAsync(account, "HOFC");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
