using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using Akavache;
using System.Reactive.Linq;
using HOFCCross.Enum;
using HOFCCross.Model.Repository;
using HOFCCross.Constantes;

namespace HOFCCross.Service
{
    public class CacheService : IService
    {
        private ClientService Service;
        Repository<Actu> _actuRepo;
        Repository<SyncDate> _syncDateRepo;

        public CacheService(ClientService service, Repository<Actu> actuRepo, Repository<SyncDate> syncDateRepo)
        {
            Service = service;
            _actuRepo = actuRepo;
            _syncDateRepo = syncDateRepo;
        }

        public async Task<List<Actu>> GetActu(bool forceRefresh = false)
        {
            var lastSyncDate = _syncDateRepo.AsQueryable().Where(s => s.SyncName == AppConstantes.DATABASE.SYNC_DATE_ACTU_NAME).FirstOrDefault()?.LastSync;
            if (forceRefresh == true || lastSyncDate == null || lastSyncDate < DateTime.Now.AddDays(-7))
            {
                var actus = await Service.GetActu();
                if (actus != null && actus.Count > 0)
                {
                    foreach (var actu in actus)
                        _actuRepo.InsertOrUpdate(actu);

                    _syncDateRepo.InsertOrUpdate(new SyncDate()
                    {
                        SyncName = AppConstantes.DATABASE.SYNC_DATE_ACTU_NAME,
                        LastSync = DateTime.Now
                    });
                }
            }

            return _actuRepo.Get();
        }

        public async Task<List<Match>> GetMatchs(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var matchs = await Service.GetMatchs();
                if (matchs != null && matchs.Count > 0)
                {
                    await BlobCache.LocalMachine.InsertObject("Matchs", matchs, DateTimeOffset.Now.AddDays(1));
                    return matchs;
                }
                else
                {
                    return await BlobCache.LocalMachine.GetObject<List<Match>>("Matchs");
                }
            }
            else
            {
                return await BlobCache.LocalMachine.GetOrFetchObject("Matchs",
                                        async () => await Service.GetMatchs(),
                                        DateTimeOffset.Now.AddDays(1));
            }
        }

        public async Task<List<ClassementEquipe>> GetClassements(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                var classements = await Service.GetClassements();
                if (classements != null && classements.Count > 0)
                {
                    await BlobCache.LocalMachine.InsertObject("Classements", classements, DateTimeOffset.Now.AddDays(1));
                    return classements;
                }
                else
                {
                    return await BlobCache.LocalMachine.GetObject<List<ClassementEquipe>>("Classements");
                }
            }
            else
            {
                return await BlobCache.LocalMachine.GetOrFetchObject("Classements",
                                        async () => await Service.GetClassements(),
                                        DateTimeOffset.Now.AddDays(1));
            }
        }

        public async Task SendNotificationToken(string token, DeviceType type)
        {
            await Service.SendNotificationToken(token, type);
        }

        public async Task<ArticleDetails> GetArticleDetails(string Url)
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("Article" + Url,
                         async () => await Service.GetArticleDetails(Url),
                         DateTimeOffset.Now.AddDays(1));
        }

        public async Task<List<string>> GetDiaporama(string Url)
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("Diaporama" + Url,
                         async () => await Service.GetDiaporama(Url),
                         DateTimeOffset.Now.AddDays(1));
        }

        public async Task<MatchInfos> GetMatchInfos(string id)
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("MatchInfos" + id,
                          async () => await Service.GetMatchInfos(id),
                          DateTimeOffset.Now.AddDays(1));
        }
    }
}
