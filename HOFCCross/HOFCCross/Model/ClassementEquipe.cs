using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class ClassementEquipe
    {
        public string Categorie { get; set; }
        public string Nom { get; set; }
        public int joue { get; set; }
        public int victoire { get; set; }
        public int nul { get; set; }
        public int defaite { get; set; }
        public int bp { get; set; }
        public int bc { get; set; }
    }
}
