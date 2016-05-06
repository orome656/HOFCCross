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
    class ClassementViewModel: FreshBasePageModel
    {
        public List<ClassementEquipe> Classements { get; set; }
        IService service;
        public ClassementViewModel()
        {
            service = FreshIOC.Container.Resolve<IService>();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Classements = service.GetClassements();
        }

    }
}
