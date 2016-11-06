﻿using HOFCCross.Constantes;
using HOFCCross.Extension;
using HOFCCross.Factory;
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
            var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
            return account != null;
        }

        public User GetUser()
        {

            var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
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
            await Task.Run(async () =>
            {
                try
                {
                    var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
                    OAuth2Request request = new OAuth2Request(HttpMethod.Get.Method, new Uri(AppConstantes.USER_INFOS_URL), null, account);
                    var response = await request.GetResponseAsync();
                    User user = JsonConvert.DeserializeObject<User>(response.GetResponseText());
                    if (user != null)
                    {
                        await AccountStoreFactory.Create().DeleteAsync(account, "HOFC");
                        account.Properties.Add(nameof(User.Email), user.Email);
                        account.Properties.Add(nameof(User.Username), user.Username);
                        account.Properties.Add(nameof(User.Sub), user.Sub);
                        account.Username = user.Username;
                        await AccountStoreFactory.Create().SaveAsync(account, "HOFC");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    throw ex;
                }
            });
        }

        public async Task RefreshToken()
        {
            try
            {
                var auth = new OAuth2Authenticator(
                        AppConstantes.OAUTH_SETTINGS.ClientId,
                        AppConstantes.OAUTH_SETTINGS.ClientSecret,
                        AppConstantes.OAUTH_SETTINGS.Scope,
                        new Uri(AppConstantes.OAUTH_SETTINGS.AuthorizeUrl),
                        new Uri(AppConstantes.OAUTH_SETTINGS.RedirectUrl),
                        new Uri(AppConstantes.OAUTH_SETTINGS.AccessTokenUrl)
                    );
                auth.Completed += Auth_Completed;
                var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
                if(account != null)
                    await auth.RequestRefreshTokenAsync(account.Properties.FirstOrDefault(c => c.Key.Equals("refresh_token")).Value);

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
                if (account != null)
                {
                    await AccountStoreFactory.Create().DeleteAsync(account, "HOFC");
                    account.Properties.Remove("access_token");
                    account.Properties.Add("access_token", e.Account.Properties.First(c => c.Key.Equals("access_token")).Value);
                    account.Properties.Remove("refresh_token");
                    account.Properties.Add("refresh_token", e.Account.Properties.First(c => c.Key.Equals("refresh_token")).Value);
                    account.Properties.Remove("expiration_date");
                    account.Properties.Add("expiration_date", DateTime.Now.AddSeconds(int.Parse(e.Account.Properties.First(c => c.Key.Equals("expires_in")).Value)).ToString("O"));
                    await AccountStoreFactory.Create().SaveAsync(account, "HOFC");
                }
            }
        }
    }
}
