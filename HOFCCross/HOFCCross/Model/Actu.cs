using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Actu
    {
        [PrimaryKey]
        [JsonProperty(PropertyName = "postid")]
        public int PostId { get; set; }
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "titre")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "texte")]
        public string Detail { get; set; }
        [JsonProperty(PropertyName = "image")]
        public string ImageUrl { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
