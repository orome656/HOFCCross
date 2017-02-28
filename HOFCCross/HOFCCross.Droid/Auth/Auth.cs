using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Auth.Presenters;

namespace Xamarin.Auth
{
    public static class Auth
    {
        internal static Context Context { get; set; }

        public static void Init(Context context, Bundle bundle)
        {
            Auth.Context = context;

            OAuthLoginPresenter.PlatformLogin = (authenticator) => {
                var oauthLogin = new PlatformOAuthLoginPresenter();
                oauthLogin.Login(authenticator);
            };
        }
    }
}