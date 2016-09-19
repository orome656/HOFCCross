using HOFCCross.Page;
using HOFCCross.UWP.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using System.ComponentModel;
using HOFCCross.Constantes;
using Windows.Security.Authentication.Web;
using Windows.Web.Http;
using Newtonsoft.Json;
using Xamarin.Auth;
using HOFCCross.Factory;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace HOFCCross.UWP.Renderer
{
    class LoginPageRenderer: PageRenderer
    {
        private bool _isShown;
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (_isShown) return;
            _isShown = true;

            var code = await AuthenticateUsingWebAuthenticationBroker();
            var account = await ConvertCodeToAccount(code);

            AccountStoreFactory.Create().Save(account, "HOFC");
            AppConstantes.OAUTH_SETTINGS.SuccessCommand.Execute(null);
            //await AuthenticationHelper.FetchGoogleEmailAndPicture(account);
        }
        
        private async Task<string> AuthenticateUsingWebAuthenticationBroker()
        {
            var googleUrl = AppConstantes.OAUTH_SETTINGS.AuthorizeUrl + "?client_id=" +
                            Uri.EscapeDataString(AppConstantes.OAUTH_SETTINGS.ClientId);
            googleUrl += "&redirect_uri=" + AppConstantes.OAUTH_SETTINGS.RedirectUrl;
            googleUrl += "&response_type=code";
            googleUrl += "&scope=" + Uri.EscapeDataString(AppConstantes.OAUTH_SETTINGS.Scope);

            var startUri = new Uri(googleUrl);

            var webAuthenticationResult =
              await
                WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri,
                  AppConstantes.OAUTH_SETTINGS.RedirectUrl);
            return webAuthenticationResult.ResponseStatus != WebAuthenticationStatus.Success ? null : webAuthenticationResult.ResponseData.Substring(webAuthenticationResult.ResponseData.IndexOf('=') + 1);
        }


        private static async Task<Account> ConvertCodeToAccount(string code)
        {
            var httpClient = new HttpClient();
            IHttpContent content = new HttpFormUrlEncodedContent(new Dictionary<string, string> {
        {"code", code},
        {"client_id", AppConstantes.OAUTH_SETTINGS.ClientId},
        {"client_secret", AppConstantes.OAUTH_SETTINGS.ClientSecret},
        {"redirect_uri", AppConstantes.OAUTH_SETTINGS.RedirectUrl.AbsoluteUri},
        {"grant_type", "authorization_code"},
      });
            var accessTokenResponse = await httpClient.PostAsync(AppConstantes.OAUTH_SETTINGS.AccessTokenUrl, content);
            var responseDict =
              JsonConvert.DeserializeObject<Dictionary<string, string>>(accessTokenResponse.Content.ToString());

            return new Account(null, responseDict);
        }
    }
}
