using HOFCCross.View;
using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace HOFCCross
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            RegisterPages();
            NavigationService.SetRoot(new MainViewModel());
        }

        void RegisterPages()
        {
            SimpleIoC.RegisterPage<MainViewModel, MainView>();
            SimpleIoC.RegisterPage<ActuViewModel, ActuView>();
            SimpleIoC.RegisterPage<CalendrierViewModel, CalendarView>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
