using HOFCCross.Enum;
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
        Task<List<Actu>> GetActu(bool forceRefresh = false);
        Task<List<Match>> GetMatchs(bool forceRefresh = false);
        Task<List<ClassementEquipe>> GetClassements(bool forceRefresh = false);
        Task SendNotificationToken(string token, DeviceType type);

        Task<ArticleDetails> GetArticleDetails(string Url);
        Task<List<string>> GetDiaporama(string initData);
        Task<MatchInfos> GetMatchInfos(string id);
        Task<List<Vote>> GetUserMatchVote(string id);
        Task<List<Joueur>> GetPlayersForMatch(string id);
    }
}
