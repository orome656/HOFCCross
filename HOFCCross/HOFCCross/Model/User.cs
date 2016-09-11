using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model
{
    public class User
    {
        public string Sub { get; set; }
        public string Email { get; set; }
        [JsonProperty(PropertyName = "preferred_username")]
        public string Username { get; set; }
    }
}
