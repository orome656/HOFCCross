using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Container
{
    class FreshMasterDetailNavigationContainer: FreshMvvm.FreshMasterDetailNavigationContainer
    {
        protected override Xamarin.Forms.Page CreateContainerPage(Xamarin.Forms.Page page)
        {
            if (page is NavigationPage || page is MasterDetailPage)
                return page;

            return new NavigationPage(page);
        }
    }
}
