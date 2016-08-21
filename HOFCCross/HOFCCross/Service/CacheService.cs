using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using Akavache;
using System.Reactive.Linq;
using PushNotification.Plugin.Abstractions;

namespace HOFCCross.Service
{
    public class CacheService : IService
    {
        private ClientService Service;

        public CacheService(ClientService service)
        {
            Service = service;
        }
        public async Task<List<Actu>> GetActu()
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("Actus",
                                    async () => await Service.GetActu(),
                                    DateTimeOffset.Now.AddDays(1));
        }

        public async Task<List<ClassementEquipe>> GetClassements()
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("Classements",
                                    async () => await Service.GetClassements(),
                                    DateTimeOffset.Now.AddDays(1));
        }

        public async Task<List<Match>> GetMatchs()
        {
            return await BlobCache.LocalMachine.GetOrFetchObject("Matchs",
                                    async () => await Service.GetMatchs(),
                                    DateTimeOffset.Now.AddDays(1));
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
    }
}
