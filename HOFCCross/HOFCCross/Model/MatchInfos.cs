using Newtonsoft.Json;
using SQLite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace HOFCCross.Model
{
    public class MatchInfos
    {
        [PrimaryKey]
        public string Id { get; set; }
        public int MatchId { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        
        [Ignore]
        public List<string> Arbitres { get; set; }
        public string ArbitresBlobbed
        {
            get { return JsonConvert.SerializeObject(Arbitres); }
            set { Arbitres = JsonConvert.DeserializeObject<List<string>>(value); }
        }

        public DateTime SyncDate { get; set; }

        [Ignore]
        public Position Position { get; set; }
        public string PositionString {
            get { return JsonConvert.SerializeObject(Position); }
            set { Position = JsonConvert.DeserializeObject<Position>(value); }
        }
    }
}
