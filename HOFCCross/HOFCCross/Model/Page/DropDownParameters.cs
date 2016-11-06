using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model.Page
{
    public class DropDownParameters<T>
    {
        public List<T> Items { get; set; }
        public Action<string, T> Callback { get; set; }
    }
}
