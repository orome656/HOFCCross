using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class User
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + Name;
            }
        }
    }
}
