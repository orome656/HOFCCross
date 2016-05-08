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
