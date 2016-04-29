using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    public class ActuViewModel: BaseViewModel
    {
        public List<ActuDetailViewModel> Actus { get; set; }

        public ActuViewModel()
        {
            // TODO Faire avec de l'injection de dépendance
            ActuService service = new ActuService();
            Actus = FromModelList(service.GetAll());
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
