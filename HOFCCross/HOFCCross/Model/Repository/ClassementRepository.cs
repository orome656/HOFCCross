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
        public override async Task<List<ClassementEquipe>> GetWithChildren()
        {
            var seasonIndex = Season.GetSeasonIndex();
            var classements = await _connection.QueryAsync<ClassementEquipe>("SELECT m.* FROM ClassementEquipe as m join Competition as c on m.CompetitionId == c.Id WHERE c.Saison = ?", seasonIndex);
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
                        var compet = await _connection.Table<Competition>().Where(c => c.Id == classement.CompetitionId).FirstOrDefaultAsync();
                        competitions.Add(compet.Id, compet);
                        classement.Competition = compet;
                    }
                }
            }
            return classements;
        }

        public override async Task InsertOrUpdateList(List<ClassementEquipe> entities)
        {
            List<Task> tasks = new List<Task>();
            Dictionary<int, Competition> competitions = new Dictionary<int, Competition>();
            foreach (var classement in entities)
            {
                tasks.Add(Task.Run(async () =>
                {
                    if (classement.Competition != null && !competitions.ContainsKey(classement.Competition.Id))
                    {
                        var competResult = await _connection.UpdateAsync(classement.Competition);
                        if (competResult == 0)
                            await _connection.InsertAsync(classement.Competition);
                        classement.CompetitionId = classement.Competition.Id;
                    }
                    var result = await _connection.UpdateAsync(classement);
                    if (result == 0)
                        await _connection.InsertAsync(classement);
                }));
            }
            await Task.WhenAll(tasks.ToArray());
        }
    }
}
