using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class ActuViewModel: FreshBasePageModel
    {
        public List<Actu> Actus { get; set; }
        IService Service;
        public ActuViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            Actus = await Service.GetActu();
            this.RaisePropertyChanged(nameof(Actus));
        }
    }
}
