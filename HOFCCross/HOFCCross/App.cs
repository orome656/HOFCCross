using FreshMvvm;
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
            FreshMvvm.FreshIOC.Container.Register<ILoginService, LoginService>();

            // The root page of your application

            var master = FreshPageModelResolver.ResolvePageModel<MenuViewModel>();
            var detail = FreshPageModelResolver.ResolvePageModel<ActuViewModel>();


            var masterDetail = new MasterDetail(master, detail);

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
