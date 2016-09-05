using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class MatchInfos
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public List<string> Arbitres { get; set; }
    }
}
