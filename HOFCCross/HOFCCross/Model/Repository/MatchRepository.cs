using HOFCCross.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model.Repository
{
    public class MatchRepository: Repository<Match>
    {
        public async override Task<List<Match>> GetWithChildren()
        {
            var seasonIndex = Season.GetSeasonIndex();
            var matchs = await _connection.QueryAsync<Match>("SELECT m.Id, m.Equipe1, m.Equipe2, m.Score1, m.Score2, m.Date, m.CompetitionId, m.Commentaire, m.JourneeId, m.Infos, m.VoteStatut, c.Nom, c.Categorie, c.IsChampionnat, c.Saison FROM Match as m join Competition as c on m.CompetitionId == c.Id WHERE c.Saison = ?", seasonIndex);
            Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
            foreach (var match in matchs)
            {
                if (match.CompetitionId != 0)
                {
                    if (competitions.ContainsKey(match.CompetitionId))
                    {
                        match.Competition = competitions[match.CompetitionId];
                    }
                    else
                    {
                        var compet = await _connection.Table<Competition>().Where(c => c.Id == match.CompetitionId).FirstOrDefaultAsync();
                        competitions.Add(compet.Id, compet);
                        match.Competition = compet;
                    }
                }
            }
            return matchs;
        }

        public override async Task InsertOrUpdateList(List<Match> entity)
        {
            List<Task> tasks = new List<Task>();
            Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
            foreach(var match in entity)
            {
                tasks.Add(Task.Run(async () => {
                    if (match.Competition != null && !competitions.ContainsKey(match.Competition.Id))
                    {
                        var competResult = await _connection.InsertOrReplaceAsync(match.Competition);
                        match.CompetitionId = match.Competition.Id;
                    }
                    await _connection.InsertOrReplaceAsync(match);
                }));
            }
            await Task.WhenAll(tasks.ToArray());
        }
    }
}
