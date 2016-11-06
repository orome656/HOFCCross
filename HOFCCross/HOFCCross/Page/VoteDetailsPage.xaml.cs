using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.Page
{
    public partial class VoteDetailsPage : ContentPage
    {
        public VoteDetailsPage()
        {
            InitializeComponent();

            this.ListView.ItemTapped += ListView_ItemTapped;
        }
        
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            ((ListView)sender).SelectedItem = null;
        }
    }
}
