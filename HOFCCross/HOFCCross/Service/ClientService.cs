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
    public class ClientService : IService
    {


        public async Task<List<Actu>> GetActu()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost/api/Actu");
            List<Actu> actus = JsonConvert.DeserializeObject<List<Actu>>(response);
            return actus;
        }

        public async Task<List<ClassementEquipe>> GetClassements()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost/api/Classement");
            List<ClassementEquipe> classement = JsonConvert.DeserializeObject<List<ClassementEquipe>>(response);
            return classement;
        }

        public async Task<List<Match>> GetMatchs()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost/api/Match");
            List<Match> matchs = JsonConvert.DeserializeObject<List<Match>>(response);
            return matchs;
        }
    }
}
