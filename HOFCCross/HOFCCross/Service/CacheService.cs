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
            List<Actu> actus = null;
            try
            {
                 actus = await BlobCache.LocalMachine.GetObject<List<Actu>>("Actus");
            } catch(KeyNotFoundException)
            {
                actus = await Service.GetActu();
                await BlobCache.LocalMachine.InsertObject("Actus", actus, DateTimeOffset.Now.AddDays(1));
            }
            return actus;
        }

        public async Task<List<ClassementEquipe>> GetClassements()
        {
            List<ClassementEquipe> classements = await BlobCache.LocalMachine.GetOrFetchObject<List<ClassementEquipe>>("Classements",
                                    async () => await Service.GetClassements(),
                                    DateTimeOffset.Now.AddDays(1));
            return classements;
        }

        public async Task<List<Match>> GetMatchs()
        {
            List<Match> matchs = null;
            try
            {
                matchs = await BlobCache.LocalMachine.GetObject<List<Match>>("Matchs");
            }
            catch (KeyNotFoundException)
            {
                matchs = await Service.GetMatchs();
                await BlobCache.LocalMachine.InsertObject("Matchs", matchs, DateTimeOffset.Now.AddDays(1));
            }
            return matchs;
        }

        public async Task SendNotificationToken(string token, DeviceType type)
        {
            await Service.SendNotificationToken(token, type);
        }
    }
}
