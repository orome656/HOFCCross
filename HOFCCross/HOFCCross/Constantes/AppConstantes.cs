using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Constantes
{
    public class AppConstantes
    {
        public static readonly string HOFC_NAME = "HORGUES ODOS";
        public static readonly string SERVER_BASE_URL = "http://v2.webhofc.fr/";
        public static readonly string SERVER_ACTU_URL = SERVER_BASE_URL + "api/actus";
        public static readonly string SERVER_MATCH_URL = SERVER_BASE_URL + "api/matchs";
        public static readonly string SERVER_CLASSEMENT_URL = SERVER_BASE_URL + "api/classements";
        public static readonly string SERVER_NOTIFICATION_URL = SERVER_BASE_URL + "api/Notification";
        public static readonly string SERVER_PARSE_URL = SERVER_BASE_URL + "api/parsePage";
        public static readonly string SERVER_MATCH_INFOS_URL = SERVER_BASE_URL + "api/MatchInfos";

        public static readonly string PRIMARY_COLOR_HEX = "#08589D";

        public static readonly OAuthSettings OAUTH_SETTINGS = new OAuthSettings()
        {
            ClientId = "xamarin-auth",
            ClientSecret = "test", // TODO Extract this from code
            Scope = "openid profile email offline_access",
            AuthorizeUrl = SERVER_BASE_URL + "connect/authorize",
            RedirectUrl = "urn:ietf:wg:oauth:2.0:oob",
            AccessTokenUrl = SERVER_BASE_URL + "connect/token",
            SuccessCommand = null
        };

        public static readonly string USER_INFOS_URL = SERVER_BASE_URL + "connect/userinfo";
    }
}
