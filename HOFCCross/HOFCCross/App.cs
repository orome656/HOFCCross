using FreshMvvm;
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
            var masterDetail = new FreshMasterDetailNavigationContainer();

            masterDetail.Init("HOFC", "ic_menu_black_24dp.png");

            masterDetail.AddPage<ActuViewModel>("Actus", null);
            masterDetail.AddPage<CalendrierViewModel>("Calendrier", null);

            MainPage = masterDetail;

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
