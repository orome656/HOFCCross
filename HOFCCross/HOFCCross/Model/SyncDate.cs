using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class SyncDate
    {
        [PrimaryKey]
        public string SyncName { get; set; }
        public DateTime LastSync { get; set; }
    }
}