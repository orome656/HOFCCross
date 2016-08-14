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
        public static readonly string SERVER_BASE_URL = "http://192.168.1.81:49360/";
        public static readonly string SERVER_ACTU_URL = SERVER_BASE_URL + "api/actus";
        public static readonly string SERVER_MATCH_URL = SERVER_BASE_URL + "api/matchs";
        public static readonly string SERVER_CLASSEMENT_URL = SERVER_BASE_URL + "api/classements";
        public static readonly string SERVER_NOTIFICATION_URL = SERVER_BASE_URL + "api/notifications";
        public static readonly string SERVER_PARSE_URL = SERVER_BASE_URL + "api/parsePage";

        public static readonly string PRIMARY_COLOR_HEX = "#08589D";
    }
}
