using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class ArticleDetails
    {
        [PrimaryKey]
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Article { get; set; }
        public DateTime DateSync { get; set; }
    }
}
