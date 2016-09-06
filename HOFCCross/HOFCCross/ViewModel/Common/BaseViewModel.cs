using FreshMvvm;
using HOFCCross.Service;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel.Common
{
    public class BaseViewModel: FreshBasePageModel
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(nameof(IsLoading));
            }
        }
        protected IService Service;
        public string ErrorMessage { get; set; }
        public string Title { get; set; }
        protected async void DisplayError(string message)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            bool tapped = await notificator.Notify(ToastNotificationType.Error, "Error", message, TimeSpan.FromSeconds(2));
        }
    }
}
