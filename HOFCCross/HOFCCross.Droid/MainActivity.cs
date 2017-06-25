using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using Plugin.Toasts;
using Xamarin.Forms;
using Android.Content;
using HOFCCross.Droid.Notification;
using Android.Gms.Common;
using Android.Preferences;
using HOFCCross.Droid.Auth;

namespace HOFCCross.Droid
{
    [Activity(Label = "HOFC", Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
            
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, bundle);

            global::Xamarin.Auth.CustomTabsConfiguration.ActionLabel = null;
            global::Xamarin.Auth.CustomTabsConfiguration.MenuItemTitle = null;
            global::Xamarin.Auth.CustomTabsConfiguration.AreAnimationsUsed = true;
            global::Xamarin.Auth.CustomTabsConfiguration.IsShowTitleUsed = false;
            global::Xamarin.Auth.CustomTabsConfiguration.IsUrlBarHidingUsed = false;
            global::Xamarin.Auth.CustomTabsConfiguration.IsCloseButtonIconUsed = true;
            global::Xamarin.Auth.CustomTabsConfiguration.IsActionButtonUsed = false;
            global::Xamarin.Auth.CustomTabsConfiguration.IsActionBarToolbarIconUsed = false;
            global::Xamarin.Auth.CustomTabsConfiguration.IsDefaultShareMenuItemUsed = false;

            LoadApplication(new App());

            var preferenceManager = PreferenceManager.GetDefaultSharedPreferences(Application.ApplicationContext);
            if(!preferenceManager.Contains("notification_key"))
            {
                if (IsPlayServicesAvailable())
                {
                    var intent = new Intent(this, typeof(RegistrationIntentService));
                    StartService(intent);
                }
            }

            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init(this);
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

