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
using HOFCCross.Extension;

namespace HOFCCross.Service
{
    public class LoginService : ILoginService
    {
        private AccountStore _accountStore;
        private Account _account;

        public event EventHandler LoginStatusChanged;
        public virtual void OnLoginStatusChanged()
        {
            LoginStatusChanged?.Invoke(this, null);
        }

        public LoginService()
        {
            this._accountStore = AccountStore.Create();
            this._account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
            InitAccessToken();
        }

        public bool IsAuthenticated()
        {
            return this._account != null;
        }

        private async void InitAccessToken()
        {
            if(this._account != null)
            {
                if(!this._account.Properties.ContainsKey("refresh_token"))
                {
                    Debug.Write("There is no refresh token in this account");
                    await this._accountStore.DeleteAsync(this._account, "HOFC");
                    OnLoginStatusChanged();
                }
                else
                {
                    string dateExpString = this._account.Properties.First(p => p.Key.Equals("expiration_date")).Value;
                    var dateExp = DateTime.Parse(dateExpString);
                    if (dateExp.CompareTo(DateTime.Now) < 0)
                    {
                        await RefreshToken();
                    }
                }
            }
        }

        public async Task AuthenticateAsync(Account account)
        {
            if (this._account != null)
                this._accountStore.Delete(this._account, "HOFC");
            account.Properties.Remove("expiration_date");
            account.Properties.Add("expiration_date", DateTime.Now.AddSeconds(int.Parse(account.Properties.First(c => c.Key.Equals("expires_in")).Value)).ToString("O"));
            await this.RequestUserInfoAndSave(account);
            OnLoginStatusChanged();
        }

        public User GetUser()
        {
            if(this._account != null)
            {
                return new User()
                {
                    Username = this._account.Username
                };
            }
            return null;
        }
        public async Task RequestUserInfoAndSave(Account account = null)
        {
            await Task.Run(async () =>
            {
                try
                {
                    if(account == null)
                        account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
                    OAuth2Request request = new HOFCCross.Auth.OAuth2Request(HttpMethod.Get.Method, new Uri(AppConstantes.USER_INFOS_URL), null, account);
                    var response = await request.GetResponseAsync();
                    User user = JsonConvert.DeserializeObject<User>(response.GetResponseText());
                    if (user != null)
                    {
                        await this._accountStore.DeleteAsync(account, "HOFC");
                        account.Properties.Add(nameof(User.Email), user.Email);
                        account.Properties.Add(nameof(User.Username), user.Username);
                        account.Properties.Add(nameof(User.Sub), user.Sub);
                        account.Username = user.Username;
                        await this._accountStore.SaveAsync(account, "HOFC");
                        this._account = account;
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
                var auth = new Auth.OAuth2Authenticator(
                        AppConstantes.OAUTHSETTING.ClientId,
                        AppConstantes.OAUTHSETTING.ClientSecret,
                        AppConstantes.OAUTHSETTING.Scope,
                        new Uri(AppConstantes.OAUTHSETTING.AuthorizeUrl),
                        new Uri(AppConstantes.OAUTHSETTING.RedirectUrl),
                        new Uri(AppConstantes.OAUTHSETTING.AccessTokenUrl)
                    );
                auth.Completed += Auth_Completed;
                auth.Error += Auth_ErrorAsync;
                var account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
                if (account != null)
                    await auth.RequestRefreshTokenAsync(account.Properties.FirstOrDefault(c => c.Key.Equals("refresh_token")).Value);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private async void Auth_ErrorAsync(object sender, AuthenticatorErrorEventArgs e)
        {
            this._account = null;
            var account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
            if (account != null)
                await this._accountStore.DeleteAsync(account, "HOFC");
            OnLoginStatusChanged();
        }

        private async void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                var account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
                if (account != null)
                {
                    await this._accountStore.DeleteAsync(account, "HOFC");
                    account.Properties.Remove("access_token");
                    account.Properties.Add("access_token", e.Account.Properties.First(c => c.Key.Equals("access_token")).Value);
                    account.Properties.Remove("refresh_token");
                    account.Properties.Add("refresh_token", e.Account.Properties.First(c => c.Key.Equals("refresh_token")).Value);
                    account.Properties.Remove("expiration_date");
                    account.Properties.Add("expiration_date", DateTime.Now.AddSeconds(int.Parse(e.Account.Properties.First(c => c.Key.Equals("expires_in")).Value)).ToString("O"));
                    await this._accountStore.SaveAsync(account, "HOFC");
                    this._account = account;
                    OnLoginStatusChanged();
                }
            }
            else
            {
                this._account = null;
                var account = this._accountStore.FindAccountsForService("HOFC").FirstOrDefault();
                if(account != null)
                    await this._accountStore.DeleteAsync(account, "HOFC");
                OnLoginStatusChanged();
            }
        }

        public void Disconnect()
        {
            if(this._account != null)
            {
                this._accountStore.Delete(this._account, "HOFC");
                this._account = null;
                OnLoginStatusChanged();
            }
        }
    }
}
