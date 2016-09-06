﻿using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class MatchInfosViewModel: BaseViewModel
    {
        public MatchInfos Infos { get; set; }
        private IService _service;

        public MatchInfosViewModel(IService service)
        {
            _service = service;
        }

        public override async void Init(object initData)
        {
            Title = "Informations Match";
            IsLoading = true;
            base.Init(initData);
            string matchId = initData as string;
            Infos = await _service.GetMatchInfos(matchId);
            RaisePropertyChanged(nameof(Infos));
            IsLoading = false;
        }

        public Command CloseCommand
        {
            get
            {
                return new Command(() => { CoreMethods.PopPageModel(true); });
            }
        }
    }
}
