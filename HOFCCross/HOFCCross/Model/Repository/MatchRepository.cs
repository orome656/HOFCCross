using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model.Repository
{
    public class MatchRepository: Repository<Match>
    {
        public override List<Match> GetWithChildren()
        {
            var matchs = _connection.Table<Match>().ToList();
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
                        var compet = _connection.Table<Competition>().Where(c => c.Id == match.CompetitionId).FirstOrDefault();
                        competitions.Add(compet.Id, compet);
                        match.Competition = compet;
                    }
                }
            }
            return matchs;
        }

        public override void InsertOrUpdateList(List<Match> entity)
        {
            Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
            foreach(var match in entity)
            {
                if(match.Competition != null &&  !competitions.ContainsKey(match.Competition.Id))
                {
                    var competResult = _connection.Update(match.Competition);
                    if (competResult == 0)
                        _connection.Insert(match.Competition);
                    match.CompetitionId = match.Competition.Id;
                }
                var result = _connection.Update(match);
                if (result == 0)
                    _connection.Insert(match);
            }
        }
    }
}
