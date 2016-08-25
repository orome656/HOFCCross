using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Page.Common
{
    public abstract class ListPage: ContentPage
    {
        public ListPage()
        {

            if (Device.OS == TargetPlatform.Windows)
            {
                var tbi = new ToolbarItem { Text = "Sync", Priority = 0, Icon = "ic_sync_black_24dp_2x.png", Order = ToolbarItemOrder.Primary };
                tbi.SetBinding(MenuItem.CommandProperty, new Binding("RefreshCommand"));
                ToolbarItems.Add(tbi);
            }
        }
    }
}
