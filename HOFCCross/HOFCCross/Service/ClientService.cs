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
using System.Diagnostics;
using HOFCCross.Enum;
using ModernHttpClient;
using HOFCCross.Factory;
using Xamarin.Auth;
using System.Globalization;
using HOFCCross.Exceptions;

namespace HOFCCross.Service
{
    public class ClientService : IService
    {

        private ILoginService _loginService;

        public ClientService(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<List<Actu>> GetActu(bool forceRefresh = false)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
                var response = await client.GetStringAsync(AppConstantes.SERVER_ACTU_URL);
                List<Actu> actus = JsonConvert.DeserializeObject<List<Actu>>(response);
                return actus;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<ClassementEquipe>> GetClassements(bool forceRefresh = false)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
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

        public async Task<List<Match>> GetMatchs(bool forceRefresh = false)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
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
                HttpClient client = new HttpClient(new NativeMessageHandler());
                
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
                HttpClient client = new HttpClient(new NativeMessageHandler());

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
                HttpClient client = new HttpClient(new NativeMessageHandler());

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

        public async Task<MatchInfos> GetMatchInfos(string id)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
                var response = await client.GetStringAsync(AppConstantes.SERVER_MATCH_INFOS_URL + "/" + id);
                MatchInfos matchInfos = JsonConvert.DeserializeObject<MatchInfos>(response);
                return matchInfos;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        private async Task<HttpClient> GetRequest()
        {
            var account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
            if (account != null && DateTime.Now.CompareTo(DateTime.ParseExact(account.Properties["expiration_date"], "O", CultureInfo.InvariantCulture)) > 0)
            {
                var token = account.Properties["refresh_token"];
                await _loginService.RefreshToken();
                account = AccountStoreFactory.Create().FindAccountsForService("HOFC").FirstOrDefault();
            }
            if (account == null)
                throw new NotLoggedException();
            else
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + account.Properties.First(c => "access_token".Equals(c.Key)).Value);
                return client;
            }
        }

        public async Task<List<Vote>> GetUserMatchVote(string id)
        {
            return await Task.Run(async () =>
            {
                try
                {
                    HttpClient client = await GetRequest();
                    var responsetext = await client.GetStringAsync(AppConstantes.SERVER_VOTE_URL + "/" + id);

                    return JsonConvert.DeserializeObject<List<Vote>>(responsetext);
                }
                catch (Exception e)
                {
                    // FIXME
                    return null;
                }
            });
        }

        public async Task<List<Joueur>> GetPlayersForMatch(string id)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
                var url = string.Format(AppConstantes.SERVER_JOUEUR_MATCH_URL, id);
                var response = await client.GetStringAsync(url).ConfigureAwait(continueOnCapturedContext: false);
                return JsonConvert.DeserializeObject<List<Joueur>>(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
