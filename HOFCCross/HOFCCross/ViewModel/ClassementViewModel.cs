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
        public Dictionary<string,List<ClassementEquipe>> Classements { get; set; }
        IService service;
        public ClassementViewModel()
        {
            service = FreshIOC.Container.Resolve<IService>();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Classements = FromModelList(service.GetClassements());
        }


        private Dictionary<string, List<ClassementEquipe>> FromModelList(List<ClassementEquipe> list)
        {
            Dictionary<string, List<ClassementEquipe>> classements = new Dictionary<string, List<ClassementEquipe>>();
            foreach (ClassementEquipe classement in list)
            {
                if (!classements.ContainsKey(classement.Categorie))
                {
                    classements.Add(classement.Categorie, new List<ClassementEquipe>());
                }
                classements[classement.Categorie].Add(classement);
            }
            return classements;
        }

    }
}
