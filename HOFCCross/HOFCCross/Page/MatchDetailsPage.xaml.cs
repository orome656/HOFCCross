using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace HOFCCross.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchDetailsPage : ContentPage
    {
        public MatchDetailsPage()
        {
            InitializeComponent();
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext != null)
            {
                var vm = BindingContext as MatchDetailsViewModel;
                vm.Positions.CollectionChanged += Positions_CollectionChanged;
            }
        }

        private void Positions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var vm = BindingContext as MatchDetailsViewModel;
            if(vm.Positions.Any())
            {

                MyMap.Pins.Clear();
                MyMap.Pins.Add(new Pin()
                {
                    Label = $"{vm.Match.MatchInfos.Adresse} {vm.Match.MatchInfos.Ville}",
                    Position = vm.Positions.First()
                });
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(vm.Positions.First(), Distance.FromKilometers(1)));
            }
        }
    }
}