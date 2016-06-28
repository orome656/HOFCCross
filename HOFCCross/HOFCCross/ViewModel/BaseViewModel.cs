using FreshMvvm;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    public class BaseViewModel: FreshBasePageModel
    {
        public bool IsLoading { get; set; }
        public string ErrorMessage { get; set; }

        protected async void DisplayError(string message)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            bool tapped = await notificator.Notify(ToastNotificationType.Error, "Error", message, TimeSpan.FromSeconds(2));
        }
    }
}
