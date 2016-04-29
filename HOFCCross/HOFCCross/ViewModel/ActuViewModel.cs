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
        IService service;
        public ActuViewModel()
        {
            service = FreshIOC.Container.Resolve<IService>();
            Actus = FromModelList(service.GetActu());
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
