using HOFCCross.Service;
using Newtonsoft.Json.Linq;
using PushNotification.Plugin;
using PushNotification.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Notifications
{
    public class CrossPushNotificationListener : IPushNotificationListener
    {
        //Here you will receive all push notification messages
        //Messages arrives as a dictionary, the device type is also sent in order to check specific keys correctly depending on the platform.
        void IPushNotificationListener.OnMessage(JObject arameters, DeviceType deviceType)
        {
            Debug.WriteLine("Message Arrived");
        }
        //Gets the registration token after push registration
        async void IPushNotificationListener.OnRegistered(string token, DeviceType deviceType)
        {
            var service = FreshMvvm.FreshIOC.Container.Resolve<IService>();
            Debug.WriteLine(string.Format("Push Notification - Device Registered - Token : {0}", token));
            await service.SendNotificationToken(token, deviceType);
        }
        //Fires when device is unregistered
        void IPushNotificationListener.OnUnregistered(DeviceType deviceType)
        {
            Debug.WriteLine("Push Notification - Device Unnregistered");

        }

        //Fires when error
        void IPushNotificationListener.OnError(string message, DeviceType deviceType)
        {
            Debug.WriteLine(string.Format("Push notification error - {0}", message));
        }

        //Enable/Disable Showing the notification
        bool IPushNotificationListener.ShouldShowNotification()
        {
            return true;
        }
    }
}
