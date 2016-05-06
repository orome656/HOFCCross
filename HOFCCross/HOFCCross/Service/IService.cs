using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    interface IService
    {
        List<Actu> GetActu();
        List<Match> GetMatchs();
        List<ClassementEquipe> GetClassements();
    }
}
