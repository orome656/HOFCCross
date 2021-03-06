﻿using System;
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

namespace HOFCCross.Service
{
    public class ClientService : IService
    {


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

        public async Task<Diaporama> GetDiaporama(string url)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());

                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>("url", url));
                var response = await client.PostAsync(AppConstantes.SERVER_PARSE_URL, new FormUrlEncodedContent(list)).ConfigureAwait(continueOnCapturedContext: false);
                var images = JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
                return new Diaporama()
                {
                    Url = url,
                    Images = images
                };
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

        public async Task<Match> GetMatchDetails(int matchId)
        {
            try
            {
                HttpClient client = new HttpClient(new NativeMessageHandler());
                var response = await client.GetStringAsync(AppConstantes.SERVER_MATCH_URL + "/" + matchId);
                Match matchInfos = JsonConvert.DeserializeObject<Match>(response);
                return matchInfos;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
