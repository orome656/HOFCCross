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

namespace Xamarin.Auth
{
    public class PlatformOAuthLoginPresenter
    {
        public void Login(Authenticator authenticator)
        {
            Auth.Context.StartActivity(authenticator.GetUI(Auth.Context));
        }
    }
}