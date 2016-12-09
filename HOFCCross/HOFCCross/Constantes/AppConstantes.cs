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
    #if DEBUG
        public static readonly string SERVER_BASE_URL = "http://local.webhofc.fr/";
    #else
        public static readonly string SERVER_BASE_URL = "http://v2.webhofc.fr/";
    #endif
        public static readonly string SERVER_ACTU_URL = SERVER_BASE_URL + "api/actus";
        public static readonly string SERVER_MATCH_URL = SERVER_BASE_URL + "api/matchs";
        public static readonly string SERVER_CLASSEMENT_URL = SERVER_BASE_URL + "api/classements";
        public static readonly string SERVER_NOTIFICATION_URL = SERVER_BASE_URL + "api/Notification";
        public static readonly string SERVER_PARSE_URL = SERVER_BASE_URL + "api/parsePage";
        public static readonly string SERVER_MATCH_INFOS_URL = SERVER_BASE_URL + "api/MatchInfos";

        public static readonly string PRIMARY_COLOR_HEX = "#08589D";

        public static readonly int CACHE_LIFE_IN_DAYS = 1;

        public class DATABASE
        {
            public static string SYNC_DATE_ACTU_NAME = "actus";
            public static string SYNC_DATE_MATCH_NAME = "matchs";
            public static string SYNC_DATE_CLASSEMENT_NAME = "classements";
        }
    }
}
