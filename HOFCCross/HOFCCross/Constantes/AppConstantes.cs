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
        public static readonly string SERVER_ACTU_URL = SERVER_BASE_URL + "api/Actu";
        public static readonly string SERVER_MATCH_URL = SERVER_BASE_URL + "api/Match";
        public static readonly string SERVER_CLASSEMENT_URL = SERVER_BASE_URL + "api/Classement";
        public static readonly string SERVER_NOTIFICATION_URL = SERVER_BASE_URL + "api/Notification";

        public static readonly string PRIMARY_COLOR_HEX = "#08589D";
    }
}
