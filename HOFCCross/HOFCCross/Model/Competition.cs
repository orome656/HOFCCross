using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Competition
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public bool IsChampionnat { get; set; }

        public string Saison { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Match> Matchs { get; set; }
    }
}
