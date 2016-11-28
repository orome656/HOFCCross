using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
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
        [TextBlob("ImagesBlobbed")]
        public List<string> Images { get; set; }
        public string ImagesBlobbed { get; set; }
        public DateTime DateSync { get; set; }
    }
}
