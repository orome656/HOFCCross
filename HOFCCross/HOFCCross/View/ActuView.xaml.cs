using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.View
{
    public partial class ActuView : ContentPage
    {
        ActuViewModel viewModel;

        public ActuView()
        {
            InitializeComponent();
            viewModel = new ActuViewModel();
            actusListView.ItemsSource = viewModel.Actus;
        }
    }
}
