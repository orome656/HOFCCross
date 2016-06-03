using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using HOFCCross.Constantes;

namespace HOFCCross.Service
{
    class MockService : IService
    {
        public List<Actu> GetActu()
        {
            List<Actu> actus = new List<Actu>();
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Content = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Content = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            actus.Add(new Actu()
            {
                Title = "HOFC III – MARQUISAT II en images",
                Content = "Merci à Jean-Paul LAMARQUE CHOY pour les photos",
                Url = "http://www.hofc.fr/2016/03/hofc-iii-marquisat-ii-en-images/",
                ImageUrl = "http://www.hofc.fr/wp-content/themes/Canyon/timthumb.php?src=http://www.hofc.fr/wp-content/uploads/2016/02/HOFC-SEN-HOFC-III-1-3-MARQUISAT-II-20-02-2016-8.jpg&h=150&w=250&zc=1"
            });
            return actus;
        }

        public List<Match> GetMatchs()
        {
            List<Match> matchs = new List<Match>();

            matchs.Add(new Match()
            {
                Date = DateTime.Now,
                Equipe1 = AppConstantes.HOFC_NAME,
                Equipe2 = "Test2",
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
                Equipe1 = "Test1",
                Equipe2 = AppConstantes.HOFC_NAME,
                Score1 = 1,
                Score2 = 1,
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

            return matchs;
        }

        public List<ClassementEquipe> GetClassements()
        {
            List<ClassementEquipe> list = new List<ClassementEquipe>();

            list.Add(new ClassementEquipe()
            {
                Categorie = "equipe1",
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
                Categorie = "equipe1",
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
                Categorie = "equipe1",
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
                Categorie = "equipe2",
                Nom = "Equipe Bidon 4",
                Point = 9,
                Joue = 10,
                Victoire = 3,
                Nul = 0,
                Defaite = 7,
                Bp = 10,
                Bc = 30
            });

            return list;
        }
    }
}
