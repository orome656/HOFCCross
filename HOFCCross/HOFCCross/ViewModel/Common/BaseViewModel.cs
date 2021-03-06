﻿using FreshMvvm;
using HOFCCross.Service;
using Plugin.Toasts;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel.Common
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel: FreshBasePageModel
    {
        public bool IsLoading { get; set; }
        protected IService _service;
        public string ErrorMessage { get; set; }
        public string Title { get; set; }

        public BaseViewModel(IService service)
        {
            _service = service;
        }

        protected async void DisplayError(string message)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            await notificator.Notify(new NotificationOptions()
            {
                IsClickable = false,
                Title = "Erreur",
                Description = message
            });
        }
    }
}
