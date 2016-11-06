using HOFCCross.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Vote
    {
        public Match Match { get; set; }
        public Joueur Joueur { get; set; }
        public TypeVote TypeVote { get; set; }
    }
}
