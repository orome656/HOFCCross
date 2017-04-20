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
        internal static Activity Activity { get; set; }

        public static void Init(Activity context, Bundle bundle)
        {
            Auth.Activity = context;

            OAuthLoginPresenter.PlatformLogin = (authenticator) => {
                var oauthLogin = new PlatformOAuthLoginPresenter();
                oauthLogin.Login(authenticator);
            };
        }
    }
}