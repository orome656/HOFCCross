using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using HOFCCross.Constantes;
using PushNotification.Plugin.Abstractions;
using System.Diagnostics;

namespace HOFCCross.Service
{
    class MockService : IService
    {
        public Task<List<Actu>> GetActu(bool forceRefresh = false)
        {
            List<Actu> actus = new List<Actu>();
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Detail = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Detail = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Detail = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            actus.Add(new Actu()
            {
                Title = "Article Test",
                Detail = "Article Test",
                Url = "http://www.hofc.fr/2016/03/article-test/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            return Task.FromResult(actus);
        }

        public Task<List<Match>> GetMatchs(bool forceRefresh = false)
        {
            List<Match> matchs = new List<Match>();

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = AppConstantes.HOFC_NAME,
                Equipe2 = "Test2",
                JourneeId = 1,
                Competition = new Competition()
                {
                    Nom = "Excellence",
                    Categorie = "equipe1",
                    IsChampionnat = true,
                }
            });

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = "Test1",
                Equipe2 = AppConstantes.HOFC_NAME,
                Score1 = 1,
                Score2 = 1,
                JourneeId = 2,
                Competition = new Competition()
                {
                    Nom = "Excellence",
                    Categorie = "equipe1",
                    IsChampionnat = true
                }
            });

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = AppConstantes.HOFC_NAME,
                Equipe2 = "Test2",
                Competition = new Competition()
                {
                    Nom = "Autre",
                    Categorie = "equipe1",
                    IsChampionnat = false
                }
            });

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = AppConstantes.HOFC_NAME,
                Equipe2 = "Test2",
                JourneeId = 1,
                Competition = new Competition()
                {
                    Nom = "Autre",
                    Categorie = "equipe2",
                    IsChampionnat = true
                }
            });

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = AppConstantes.HOFC_NAME,
                Equipe2 = "Test2",
                Competition = new Competition()
                {
                    Nom = "Autre",
                    Categorie = "equipe2",
                    IsChampionnat = false
                }
            });

            return Task.FromResult(matchs);
        }

        public Task<List<ClassementEquipe>> GetClassements(bool forceRefresh = false)
        {
            List<ClassementEquipe> list = new List<ClassementEquipe>();

            list.Add(new ClassementEquipe()
            {
                Competition = new Competition() { Categorie = "equipe1" },
                Nom = "Equipe Bidon 1",
                Point = 21,
                Joue = 10,
                Victoire = 7,
                Nul = 0,
                Defaite = 3,
                Bp = 30,
                Bc = 10
            });
            list.Add(new ClassementEquipe()
            {
                Competition = new Competition() { Categorie = "equipe1" },
                Nom = "Equipe Bidon 2",
                Point = 15,
                Joue = 10,
                Victoire = 5,
                Nul = 0,
                Defaite = 5,
                Bp = 15,
                Bc = 15
            });
            list.Add(new ClassementEquipe()
            {
               Competition = new Competition() { Categorie = "equipe1" },
                Nom = "Equipe Bidon 3",
                Point = 9,
                Joue = 10,
                Victoire = 3,
                Nul = 0,
                Defaite = 7,
                Bp = 10,
                Bc = 30
            });
            list.Add(new ClassementEquipe()
            {
                Competition = new Competition() { Categorie = "equipe2" },
                Nom = "Equipe Bidon 4",
                Point = 9,
                Joue = 10,
                Victoire = 3,
                Nul = 0,
                Defaite = 7,
                Bp = 10,
                Bc = 30
            });

            return Task.FromResult(list);
        }

        public Task SendNotificationToken(string token, DeviceType type)
        {
            Debug.WriteLine("Sending notification token");
            return Task.FromResult(0);
        }

        public Task<ArticleDetails> GetArticleDetails(string Url)
        {
            return Task.FromResult(new ArticleDetails()
            {
                Article = "<p>Test<br/>Test retour a la ligne</p>",
                Title = "Titre",
                Date = DateTime.Now
            });
        }

        public Task<List<string>> GetDiaporama(string initData)
        {
            return Task.FromResult(new List<string>()
            {
                "http://www.hofc.fr/wp-content/gallery/sen-semeac-ii-0-6-hofc-29-05-2016/HOFC-SEN-SEMEAC-II-0-6-HOFC-29-05-2016-.jpg",
                "http://www.hofc.fr/wp-content/gallery/sen-semeac-ii-0-6-hofc-29-05-2016/HOFC-SEN-SEMEAC-II-0-6-HOFC-29-05-2016-2.jpg",
                "http://www.hofc.fr/wp-content/gallery/sen-semeac-ii-0-6-hofc-29-05-2016/HOFC-SEN-SEMEAC-II-0-6-HOFC-29-05-2016-3.jpg",
                "http://www.hofc.fr/wp-content/gallery/sen-semeac-ii-0-6-hofc-29-05-2016/HOFC-SEN-SEMEAC-II-0-6-HOFC-29-05-2016-4.jpg"
            });
        }
    }
}
