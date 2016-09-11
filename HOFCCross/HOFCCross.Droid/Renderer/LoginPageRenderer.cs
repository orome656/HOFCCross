using Android.App;
using Android.Net;
using HOFCCross.Constantes;
using HOFCCross.Droid.Renderer;
using HOFCCross.Page;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace HOFCCross.Droid.Renderer
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            
            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                AppConstantes.OAUTH_SETTINGS.ClientId,
                AppConstantes.OAUTH_SETTINGS.ClientSecret,
                AppConstantes.OAUTH_SETTINGS.Scope,
                AppConstantes.OAUTH_SETTINGS.AuthorizeUrl,
                AppConstantes.OAUTH_SETTINGS.RedirectUrl,
                AppConstantes.OAUTH_SETTINGS.AccessTokenUrl
                );


            auth.Completed += (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    AccountStore.Create(this.Context).Save(eventArgs.Account, "HOFC");
                    AppConstantes.OAUTH_SETTINGS.SuccessCommand.Execute(null);
                }
                else
                {
                    // The user cancelled
                }
            };

            activity.StartActivity(auth.GetUI(activity));

        }
    }
}