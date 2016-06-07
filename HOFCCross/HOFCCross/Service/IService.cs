using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Service
{
    public interface IService
    {
        Task<List<Actu>> GetActu();
        Task<List<Match>> GetMatchs();
        Task<List<ClassementEquipe>> GetClassements();
    }
}
