using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Container
{
    public class MasterDetail : MasterDetailPage, IFreshNavigationService
    {
        private FreshNavigationContainer DetailsPage;

        public string NavigationServiceName
        {
            get
            {
                return "CustomNavigationService";
            }
        }

        public MasterDetail(Xamarin.Forms.Page master, Xamarin.Forms.Page detail)
        {

            FreshIOC.Container.Register<IFreshNavigationService>(this, NavigationServiceName);

            master.Title = "HOFC";

            master.GetModel().CurrentNavigationServiceName = NavigationServiceName;
            this.Master = master;

            DetailsPage = new FreshNavigationContainer(detail, "DetailPageArea");
            this.Detail = DetailsPage;
        }

        public void NotifyChildrenPageWasPopped()
        {
            if (Detail is NavigationPage)
                ((NavigationPage)Detail).NotifyAllChildrenPopped();
        }

        public async Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
                await Navigation.PopModalAsync();
            else
                await((NavigationPage)DetailsPage.CurrentPage).PopAsync();
        }

        public async Task PopToRoot(bool animate = true)
        {
            await((NavigationPage)DetailsPage.CurrentPage).PopToRootAsync(animate);
        }

        public async Task PushPage(Xamarin.Forms.Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            var newDetail = new FreshNavigationContainer(page, "DetailPageArea");
            this.Detail = newDetail;

            IsPresented = false;

            DetailsPage.NotifyChildrenPageWasPopped();
            DetailsPage = newDetail;
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<T>() where T : FreshBasePageModel
        {
            throw new NotImplementedException();
        }
    }
}
