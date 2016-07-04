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
using PushNotification.Plugin;
using HOFCCross.Notifications;
using Newtonsoft.Json.Linq;
using Android.Content.Res;
using Newtonsoft.Json;
using System.IO;

namespace HOFCCross.Droid
{
    [Application]
    public class HOFCApplication: Application
    {
        public static Context AppContext;

        public HOFCApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
                
        }

        public override void OnCreate()
        {
            base.OnCreate();

            AppContext = this.ApplicationContext;
            
            CrossPushNotification.Initialize<CrossPushNotificationListener>(GetSenderId());

            //This service will keep your app receiving push even when closed.             
            StartPushService();
        }

        private string GetSenderId()
        {
            var serializer = new JsonSerializer();
            using(var sr = new StreamReader(Assets.Open("config.json")))
            {
                using(var reader = new JsonTextReader(sr))
                {
                    var json = serializer.Deserialize<JObject>(reader);
                    return json.Value<string>("SENDER_ID");
                }
            }
            
            
        }

        public static void StartPushService()
        {
            AppContext.StartService(new Intent(AppContext, typeof(PushNotificationService)));

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {

                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }

        public static void StopPushService()
        {
            AppContext.StopService(new Intent(AppContext, typeof(PushNotificationService)));
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Kitkat)
            {
                PendingIntent pintent = PendingIntent.GetService(AppContext, 0, new Intent(AppContext, typeof(PushNotificationService)), 0);
                AlarmManager alarm = (AlarmManager)AppContext.GetSystemService(Context.AlarmService);
                alarm.Cancel(pintent);
            }
        }
    }
}