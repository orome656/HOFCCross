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
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(AppConstantes.SERVER_ACTU_URL);
                List<Actu> actus = JsonConvert.DeserializeObject<List<Actu>>(response);
                return actus;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<ClassementEquipe>> GetClassements()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(AppConstantes.SERVER_CLASSEMENT_URL);
                List<ClassementEquipe> classement = JsonConvert.DeserializeObject<List<ClassementEquipe>>(response);
                return classement;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<Match>> GetMatchs()
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(AppConstantes.SERVER_MATCH_URL);
                List<Match> matchs = JsonConvert.DeserializeObject<List<Match>>(response);
                return matchs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
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
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<ArticleDetails> GetArticleDetails(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("url", url));
                var response = await client.PostAsync(AppConstantes.SERVER_PARSE_URL, new FormUrlEncodedContent(list)).ConfigureAwait(continueOnCapturedContext: false);
                ArticleDetails details = JsonConvert.DeserializeObject<ArticleDetails>(await response.Content.ReadAsStringAsync());
                return details;
            } catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<string>> GetDiaporama(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("url", url));
                var response = await client.PostAsync(AppConstantes.SERVER_PARSE_URL, new FormUrlEncodedContent(list)).ConfigureAwait(continueOnCapturedContext: false);
                return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
