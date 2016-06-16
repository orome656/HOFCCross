using HOFCCross.Container;
using HOFCCross.Model;
using HOFCCross.Page;
using HOFCCross.Service;
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
            FreshMvvm.FreshIOC.Container.Register<IService, CacheService>();

            // The root page of your application
            var masterDetail = new FreshMasterDetailNavigationContainer();

            masterDetail.Init("HOFC", "ic_menu_black_24dp.png");

            masterDetail.AddPage<ActuViewModel>("Actus", null);
            masterDetail.AddPage<CalendrierViewModel>("Calendriers", "equipe1");
            masterDetail.AddPage<ClassementViewModel>("Classements", "equipe1");
            masterDetail.AddPage<AgendaViewModel>("Agendas", null);
            masterDetail.AddPage<JourneeViewModel>("Journees Excellence", "equipe1");
            masterDetail.AddPage<JourneeViewModel>("Journees Premiere Div.", "equipe2");
            masterDetail.AddPage<JourneeViewModel>("Journees Promotion Premiere Div.", "equipe3");

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
