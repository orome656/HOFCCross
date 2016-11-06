using HOFCCross.Model;
using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.Page
{
    public partial class ListeVotesOuvertsPage : ContentPage
    {
        public ListeVotesOuvertsPage()
        {
            InitializeComponent();
            this.ListView.ItemTapped += ListView_ItemTapped;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            ((ListView)sender).SelectedItem = null;
        }

        public void VoteClick(object sender, EventArgs e)
        {
            var viewModel = this.BindingContext as ListeVotesOuvertsViewModel;
            var button = sender as Button;
            var match = button.BindingContext as Match;
            viewModel.VoteCommand.Execute(match.Id);
        }
    }
}
