using HOFCCross.Enum;
using Newtonsoft.Json;
using SQLite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Match
    {
        [PrimaryKey]
        public int Id { get; set; }
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

        public int CompetitionId { get; set; }

        [Ignore]
        public Competition Competition { get; set; }
        [JsonProperty(PropertyName = "message")]
        public string Commentaire { get; set; }
        [JsonProperty(PropertyName = "idJournee")]
        public int? JourneeId { get; set; }
        public string Infos { get; set; }

        public int VoteStatut { get; set; }
        
        [Ignore]
        [JsonIgnore]
        public StatutVote VoteStatutEnum { get { return (StatutVote)VoteStatut; } }

        public string MatchInfosId { get; set; }
        [Ignore]
        public MatchInfos MatchInfos { get; set; }
        
        [Ignore]
        public List<Joueur> Joueurs { get; set; }
    }
}
