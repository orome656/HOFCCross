using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class ActuViewModel: ListBaseViewModel<Actu>
    {
        public ActuViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            await ReloadItems();
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await ReloadItems(true);
                });
            }
        }

        protected override async Task ReloadItems(bool forceRefresh = false)
        {
            IsLoading = true;
            try
            {
                Items = await Service.GetActu(forceRefresh);
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des actualités");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
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
