using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HOFCCross.Model
{
    public class OAuthSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }
        public string AuthorizeUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string AccessTokenUrl { get; set; }
        public ICommand SuccessCommand { get; set; }
    }
}
