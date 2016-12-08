using HOFCCross.Page.Common;
using HOFCCross.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.Page
{
    public partial class ClassementPage : ListPage
    {
        double width;
        double height;

        public ClassementPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                var vm = BindingContext as ClassementViewModel;
                
                if (height < width)
                {
                    // Paysage
                    this.HVColumn.IsVisible = true;
                    this.HDColumn.IsVisible = true;
                    this.HNColumn.IsVisible = true;
                    this.HBPColumn.IsVisible = true;
                    this.HBCColumn.IsVisible = true;
                    vm.IsLandscape = true;
                }
                else
                {
                    // Portrait
                    this.HVColumn.IsVisible = false;
                    this.HDColumn.IsVisible = false;
                    this.HNColumn.IsVisible = false;
                    this.HBPColumn.IsVisible = false;
                    this.HBCColumn.IsVisible = false;
                    vm.IsLandscape = false;
                }
            }
        }

        private void ChangeGridVisibility()
        {

        }
    }
}
