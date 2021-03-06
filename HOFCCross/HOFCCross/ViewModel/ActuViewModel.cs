﻿using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
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
        public ActuViewModel(IService service) : base(service)
        {
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
            bool loadEnd = false;
            var animation = Task.Delay(500).ContinueWith((t) => { if (!loadEnd) IsLoading = true; });
            try
            {
                Items = await _service.GetActu(forceRefresh);
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des actualités");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
            loadEnd = true;
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
