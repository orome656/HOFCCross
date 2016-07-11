using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    public class LoginService : ILoginService
    {
        public bool IsAuthenticated()
        {
            return false;
        }

        public void Authenticate()
        {

        }

        public User GetUser()
        {
            return null;
            /*
            return new Model.User()
            {
                FirstName = "Monsieur",
                Name = "Dupont"
            };*/
        }
    }
}
