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
        public Uri AuthorizeUrl { get; set; }
        public Uri RedirectUrl { get; set; }
        public Uri AccessTokenUrl { get; set; }
        public ICommand SuccessCommand { get; set; }
    }
}
