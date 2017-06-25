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
                if (vm.Match != null && vm.Match.MatchInfos != null && vm.Match.MatchInfos.Position != null)
                {
                    MyMap.Pins.Add(new Pin()
                    {
                        Label = $"{vm.Match.MatchInfos.Adresse} {vm.Match.MatchInfos.Ville}",
                        Position = vm.Match.MatchInfos.Position
                    });
                }
            }
        }
    }
}