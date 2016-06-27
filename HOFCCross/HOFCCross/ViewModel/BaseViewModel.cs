using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class BaseViewModel: FreshBasePageModel
    {
        public bool IsLoading { get; set; }
    }
}
