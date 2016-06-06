using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Competition
    {
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public bool IsChampionnat { get; set; }

        public int JourneeId { get; set; }
    }
}
