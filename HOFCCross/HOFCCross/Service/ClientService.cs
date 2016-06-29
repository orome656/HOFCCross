using System;
using System.Collections.Generic;
using HOFCCross.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using HOFCCross.Constantes;
using PushNotification.Plugin.Abstractions;
using System.Diagnostics;

namespace HOFCCross.Service
{
    public class ClientService : IService
    {


        public async Task<List<Actu>> GetActu()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(AppConstantes.SERVER_ACTU_URL);
            List<Actu> actus = JsonConvert.DeserializeObject<List<Actu>>(response);
            return actus;
        }

        public async Task<List<ClassementEquipe>> GetClassements()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(AppConstantes.SERVER_CLASSEMENT_URL);
            List<ClassementEquipe> classement = JsonConvert.DeserializeObject<List<ClassementEquipe>>(response);
            return classement;
        }

        public async Task<List<Match>> GetMatchs()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(AppConstantes.SERVER_MATCH_URL);
            List<Match> matchs = JsonConvert.DeserializeObject<List<Match>>(response);
            return matchs;
        }

        public async Task SendNotificationToken(string token, DeviceType device)
        {
            
            try
            {
                HttpClient client = new HttpClient();
                
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("notification_id", token));
                list.Add(new KeyValuePair<string, string>("platform", device.ToString()));

                var result = await client.PostAsync(AppConstantes.SERVER_NOTIFICATION_URL, new FormUrlEncodedContent(list)).ConfigureAwait(continueOnCapturedContext: false);
            } catch(Exception ex)
            {

            }
        }
    }
}
