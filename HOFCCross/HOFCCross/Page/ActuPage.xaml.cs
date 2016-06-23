using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.Page
{
    public partial class ActuPage : ContentPage
    {
        public ActuPage()
        {
            InitializeComponent();

            listActus.ItemTapped += ListActus_ItemTapped;
        }

        private void ListActus_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}
