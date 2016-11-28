using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Helpers
{
    public class Season
    {
        public static string GetSeasonIndex()
        {
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            if (month < 7)
            {
                return (year - 1) + "/" + year;
            }
            else
            {
                return year + "/" + (year + 1);
            }
        }
    }
}
