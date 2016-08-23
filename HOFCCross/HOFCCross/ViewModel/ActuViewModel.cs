﻿using FreshMvvm;
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
    public class ActuViewModel: BaseViewModel
    {
        private List<Actu> _actus;
        public List<Actu> Actus
        {
            get
            {
                return _actus;
            }
            set
            {
                _actus = value;
                RaisePropertyChanged(nameof(Actus));
            }
        }
        IService Service;
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

        private async Task ReloadItems(bool forceRefresh = false)
        {
            IsLoading = true;
            try
            {
                Actus = await Service.GetActu(forceRefresh);
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
