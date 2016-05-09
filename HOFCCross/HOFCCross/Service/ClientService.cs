using System;
using System.Collections.Generic;
using HOFCCross.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace HOFCCross.Service
{
    class ClientService : IService
    {


        public List<Actu> GetActu()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("http://localhost:49360/api/Actu").Result;
            List<Actu> actus = JsonConvert.DeserializeObject<List<Actu>>(response);
            return actus;
        }

        public List<ClassementEquipe> GetClassements()
        {
            throw new NotImplementedException();
        }

        public List<Match> GetMatchs()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("http://localhost:49360/api/Match").Result;
            List<Match> matchs = JsonConvert.DeserializeObject<List<Match>>(response);
            return matchs;
        }
    }
}
