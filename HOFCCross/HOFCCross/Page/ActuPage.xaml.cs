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
        ActuViewModel viewModel;

        public ActuPage()
        {
            InitializeComponent();
            viewModel = new ActuViewModel();
            actusListView.ItemsSource = viewModel.Actus;
        }
    }
}
