using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class MatchInfos
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        
        public string ArbitresBlobbed { get; set; }
        [TextBlob("ArbitresBlobbed")]
        public List<string> Arbitres { get; set; }

        public DateTime SyncDate { get; set; }
    }
}
