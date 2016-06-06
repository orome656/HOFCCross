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
        public List<ActuDetailViewModel> Actus { get; set; }
        IService Service;
        public ActuViewModel(IService service)
        {
            Service = service;
            Actus = FromModelList(Service.GetActu());
            this.RaisePropertyChanged(nameof(Actus));
        }

        public List<ActuDetailViewModel> FromModelList(List<Actu> actusModel)
        {
            List<ActuDetailViewModel> viewModels = new List<ActuDetailViewModel>();
            foreach(Actu actu in actusModel)
            {
                viewModels.Add(actu);
            }
            return viewModels;
        }
    }
}
