using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using HOFCCross.Enum;
using HOFCCross.Model.Repository;
using HOFCCross.Constantes;

namespace HOFCCross.Service
{
    public class CacheService : IService
    {
        private ClientService Service;
        Repository<Actu> _actuRepo;
        MatchRepository _matchRepo;
        Repository<SyncDate> _syncDateRepo;
        Repository<Competition> _competitionRepo;
        ClassementRepository _classementRepo;
        Repository<ArticleDetails> _articleRepo;
        Repository<Diaporama> _diaporamaRepo;
        Repository<MatchInfos> _matchsInfosRepo;

        public CacheService(ClientService service, 
                            Repository<Actu> actuRepo,
                            MatchRepository matchRepo,
                            Repository<SyncDate> syncDateRepo,
                            Repository<Competition> competitionRepo,
                            ClassementRepository classementRepo,
                            Repository<ArticleDetails> articleRepo,
                            Repository<Diaporama> diaporamaRepo,
                            Repository<MatchInfos> matchsInfosRepo)
        {
            Service = service;
            _actuRepo = actuRepo;
            _matchRepo = matchRepo;
            _syncDateRepo = syncDateRepo;
            _competitionRepo = competitionRepo;
            _classementRepo = classementRepo;
            _articleRepo = articleRepo;
            _diaporamaRepo = diaporamaRepo;
            _matchsInfosRepo = matchsInfosRepo;
        }

        public async Task<List<Actu>> GetActu(bool forceRefresh = false)
        {
            var lastSyncDate = (await _syncDateRepo.AsQueryable().Where(s => s.SyncName == AppConstantes.DATABASE.SYNC_DATE_ACTU_NAME).FirstOrDefaultAsync())?.LastSync;
            if (forceRefresh == true || lastSyncDate == null || lastSyncDate < DateTime.Now.AddDays(-7))
            {
                var actus = await Service.GetActu();
                if (actus != null && actus.Count > 0)
                {
                    List<Task> tasks = new List<Task>();
                    foreach (var actu in actus)
                        tasks.Add(_actuRepo.InsertOrUpdate(actu));
                    await Task.WhenAll(tasks.ToArray());
                    await _syncDateRepo.InsertOrUpdate(new SyncDate()
                    {
                        SyncName = AppConstantes.DATABASE.SYNC_DATE_ACTU_NAME,
                        LastSync = DateTime.Now
                    });
                }
            }

            return await _actuRepo.AsQueryable().OrderByDescending(a => a.Date).ToListAsync();
        }

        public async Task<List<Match>> GetMatchs(bool forceRefresh = false)
        {
            var lastSyncDate = (await _syncDateRepo.AsQueryable().Where(s => s.SyncName == AppConstantes.DATABASE.SYNC_DATE_MATCH_NAME).FirstOrDefaultAsync())?.LastSync;
            if (forceRefresh == true || lastSyncDate == null || lastSyncDate < DateTime.Now.AddDays(-AppConstantes.CACHE_LIFE_IN_DAYS))
            {
                var matchs = await Service.GetMatchs();
                if (matchs != null && matchs.Count > 0)
                {
                    _matchRepo.DeleteAll();
                    await _matchRepo.InsertOrUpdateList(matchs);

                    await _syncDateRepo.InsertOrUpdate(new SyncDate()
                    {
                        SyncName = AppConstantes.DATABASE.SYNC_DATE_MATCH_NAME,
                        LastSync = DateTime.Now
                    });
                }
            }
            return await _matchRepo.GetWithChildren();
        }

        public async Task<List<ClassementEquipe>> GetClassements(bool forceRefresh = false)
        {
            var lastSyncDate = (await _syncDateRepo.AsQueryable().Where(s => s.SyncName == AppConstantes.DATABASE.SYNC_DATE_CLASSEMENT_NAME).FirstOrDefaultAsync())?.LastSync;
            if (forceRefresh == true || lastSyncDate == null || lastSyncDate < DateTime.Now.AddDays(-AppConstantes.CACHE_LIFE_IN_DAYS))
            {
                var classements = await Service.GetClassements();
                if (classements != null && classements.Count > 0)
                {
                    _classementRepo.DeleteAll();
                    await _classementRepo.InsertOrUpdateList(classements);

                    await _syncDateRepo.InsertOrUpdate(new SyncDate()
                    {
                        SyncName = AppConstantes.DATABASE.SYNC_DATE_CLASSEMENT_NAME,
                        LastSync = DateTime.Now
                    });
                }
            }
            return (await _classementRepo.GetWithChildren()).OrderByDescending(c => c.Point).ThenByDescending(c => c.Diff).ToList();
        }

        public async Task SendNotificationToken(string token, DeviceType type)
        {
            await Service.SendNotificationToken(token, type);
        }

        public async Task<ArticleDetails> GetArticleDetails(string Url)
        {
            var article = await _articleRepo.Get(Url);
            if(article == null)
            {
                article = await Service.GetArticleDetails(Url);
                article.Url = Url;
                article.DateSync = DateTime.Now;
                await _articleRepo.InsertOrUpdate(article);

            }
            return article;
        }

        public async Task<Diaporama> GetDiaporama(string Url)
        {
            var diaporama = await _diaporamaRepo.Get(Url);
            if (diaporama == null)
            {
                diaporama = await Service.GetDiaporama(Url);
                diaporama.Url = Url;
                diaporama.DateSync = DateTime.Now;
                await _diaporamaRepo.InsertOrUpdate(diaporama);

            }
            return diaporama;
        }

        public async Task<MatchInfos> GetMatchInfos(string id)
        {
            var matchInfos = await _matchsInfosRepo.Get(id);
            if(matchInfos == null)
            {
                matchInfos = await Service.GetMatchInfos(id);
                matchInfos.Id = id;
                matchInfos.SyncDate = DateTime.Now;
                await _matchsInfosRepo.InsertOrUpdate(matchInfos);
            }
            return matchInfos;
        }
    }
}
