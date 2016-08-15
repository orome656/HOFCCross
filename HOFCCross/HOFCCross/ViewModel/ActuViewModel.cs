using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class ActuViewModel: BaseViewModel
    {
        public List<Actu> Actus { get; set; }
        IService Service;
        public ActuViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            base.Init(initData);
            try
            {
                Actus = await Service.GetActu();
                this.RaisePropertyChanged(nameof(Actus));
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des actualités");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        public Command ItemTapCommand
        {
            get
            {
                return new Command((o) =>
                {
                    var item = o as Actu;
                    if(item.Url.Contains("en-images"))
                    {
                        CoreMethods.PushPageModel<DiaporamaViewModel>(item.Url);
                    }
                    else
                    {
                        CoreMethods.PushPageModel<ArticleDetailsViewModel>(item.Url);
                    }
                });
            }
        }
    }
}
