using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.ViewModel.Common;
using FreshMvvm;
using PropertyChanged;
using HOFCCross.Model.Page;

namespace HOFCCross.ViewModel.Common
{
    [ImplementPropertyChanged]
    public class DropDownViewModel: FreshBasePageModel
    {
        public List<object> Items { get; set; }

        public override void Init(object initData)
        {
            base.Init(initData);
        }
    }
}
