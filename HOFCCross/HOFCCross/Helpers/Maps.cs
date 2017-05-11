using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Helpers
{
    public class Maps
    {
        public static void OpenMap(string address)
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Device.OpenUri(
                      new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case Device.Android:
                    Device.OpenUri(
                      new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case Device.Windows:
                case Device.WinPhone:
                    Device.OpenUri(
                      new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                    break;
            }
        }
    }
}
