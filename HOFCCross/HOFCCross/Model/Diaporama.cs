using Newtonsoft.Json;
using SQLite;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class Diaporama
    {
        [PrimaryKey]
        public string Url { get; set; }

        [Ignore]
        public List<string> Images { get; set; }

        public string ImagesBlobbed
        {
            get { return JsonConvert.SerializeObject(Images); }
            set { Images = JsonConvert.DeserializeObject<List<string>>(value); }
        }

        public DateTime DateSync { get; set; }
    }
}
