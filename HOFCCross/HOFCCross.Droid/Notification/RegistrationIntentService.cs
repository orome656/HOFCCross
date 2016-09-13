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
using Android.Util;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Android.Gms.Gcm;
using Android.Gms.Gcm.Iid;
using HOFCCross.Service;
using HOFCCross.Enum;

namespace HOFCCross.Droid.Notification
{
    [Service(Exported = false)]
    public class RegistrationIntentService : IntentService
    {
        static object locker = new object();

        public RegistrationIntentService() : base("RegistrationIntentService") { }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                Log.Info("RegistrationIntentService", "Calling InstanceID.GetToken");
                lock (locker)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    var token = instanceID.GetToken(GetSenderId(), GoogleCloudMessaging.InstanceIdScope, null);

                    Log.Info("RegistrationIntentService", "GCM Registration Token: " + token);
                    SendRegistrationToAppServer(token);
                    Subscribe(token);
                }
            }
            catch (Exception e)
            {
                Log.Debug("RegistrationIntentService", "Failed to get a registration token", e);
                return;
            }
        }

        private string GetSenderId()
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(Assets.Open("config.json")))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    var json = serializer.Deserialize<JObject>(reader);
                    return json.Value<string>("SENDER_ID");
                }
            }
        }

        async void SendRegistrationToAppServer(string token)
        {
            var service = FreshMvvm.FreshIOC.Container.Resolve<IService>();
            Log.Debug(nameof(this.Class), string.Format("Push Notification - Device Registered - Token : {0}", token));
            await service.SendNotificationToken(token, DeviceType.Android);
        }

        void Subscribe(string token)
        {
            var pubSub = GcmPubSub.GetInstance(this);
            pubSub.Subscribe(token, "/topics/global", null);
        }
    }
}