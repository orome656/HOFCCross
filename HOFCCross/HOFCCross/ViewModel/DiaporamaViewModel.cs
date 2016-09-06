using FreshMvvm;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class DiaporamaViewModel: BaseViewModel
    {
        public List<string> Images { get; set; }
        private IService _service;

        public DiaporamaViewModel(IService service)
        {
            _service = service;
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            Images = await _service.GetDiaporama((string)initData);
            RaisePropertyChanged(nameof(Images));
        }
    }
}
