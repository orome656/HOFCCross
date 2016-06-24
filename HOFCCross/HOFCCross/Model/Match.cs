using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Match
    {
        public string Equipe1 { get; set; }
        public string Equipe2 { get; set; }
        public int? Score1 { get; set; }
        public int? Score2 { get; set; }

        public string ScoreMessage { get
            {
                if (Score1 != null && Score2 != null)
                    return Score1 + " - " + Score2;
                else if (!String.IsNullOrEmpty(Commentaire))
                    return Commentaire;
                else
                    return String.Empty;
            }
        }
        public DateTime Date { get; set; }
        public Competition Competition { get; set; }
        public string Commentaire { get; set; }
        [JsonProperty(PropertyName = "idJournee")]
        public int? JourneeId { get; set; }
    }
}
