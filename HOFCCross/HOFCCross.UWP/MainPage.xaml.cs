using HOFCCross.Enum;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HOFCCross.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new HOFCCross.App());
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await InitNotification();
        }

        private async Task InitNotification()
        {
            // Get a channel URI from WNS.
            var channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

            var service = FreshMvvm.FreshIOC.Container.Resolve<IService>();
            await service.SendNotificationToken(channel.Uri, DeviceType.Windows);
        }
    }
}
