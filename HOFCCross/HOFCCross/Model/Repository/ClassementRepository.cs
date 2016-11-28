using HOFCCross.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.Model.Repository
{
    public class ClassementRepository: Repository<ClassementEquipe>
    {
        public override List<ClassementEquipe> GetWithChildren()
        {
            var seasonIndex = Season.GetSeasonIndex();
            var classements = _connection.Query<ClassementEquipe>("SELECT m.* FROM ClassementEquipe as m join Competition as c on m.CompetitionId == c.Id WHERE c.Saison = ?", seasonIndex).ToList();
            Dictionary <int, Competition> competitions = new Dictionary<int, Competition>();
            foreach(var classement in classements)
            {
                if(classement.CompetitionId != 0)
                {
                    if (competitions.ContainsKey(classement.CompetitionId))
                    {
                        classement.Competition = competitions[classement.CompetitionId];
                    }
                    else
                    {
                        var compet = _connection.Table<Competition>().Where(c => c.Id == classement.CompetitionId).FirstOrDefault();
                        competitions.Add(compet.Id, compet);
                        classement.Competition = compet;
                    }
                }
            }
            return classements;
        }

        public override void InsertOrUpdateList(List<ClassementEquipe> entities)
        {
            Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
            foreach (var classement in entities)
            {
                if (classement.Competition != null && !competitions.ContainsKey(classement.Competition.Id))
                {
                    var competResult = _connection.Update(classement.Competition);
                    if (competResult == 0)
                        _connection.Insert(classement.Competition);
                    classement.CompetitionId = classement.Competition.Id;
                }
                var result = _connection.Update(classement);
                if (result == 0)
                    _connection.Insert(classement);
            }
        }
    }
}
