using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class ClassementEquipe
    {
        public int Rank { get; set; }
        public Competition Competition { get; set; }
        public string Nom { get; set; }
        [JsonProperty(PropertyName = "points")]
        public int Point { get; set; }
        public int Joue { get; set; }
        public int Victoire { get; set; }
        public int Nul { get; set; }
        public int Defaite { get; set; }
        public int Bp { get; set; }
        public int Bc { get; set; }
        public int Diff { get { return Bp - Bc; } }
    }
}
