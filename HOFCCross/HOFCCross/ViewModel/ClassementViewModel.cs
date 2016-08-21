using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    class ClassementViewModel: BaseViewModel
    {
        public List<ClassementEquipe> Classements { get; set; }


        private string _selectedEquipe;

        public string SelectedEquipe
        {
            get
            {
                return _selectedEquipe;
            }
            set
            {
                _selectedEquipe = value;
                RaisePropertyChanged(nameof(SelectedEquipe));
                ReloadRanks();
            }
        }

        public List<string> Equipes { get; set; } = new List<string>();

        IService Service;

        public ClassementViewModel(IService service)
        {
            Service = service;
        }

        private async void ReloadRanks()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                var classements = await Service.GetClassements();

                Classements = classements.Where(c => c.Competition != null && _selectedEquipe.Equals(c.Competition.Categorie))
                                         .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                         .ToList();

                RaisePropertyChanged(nameof(Classements));
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));

            try
            {
                var classements = await Service.GetClassements();
                if (classements != null && classements.Count > 0)
                {
                    Equipes = classements.Select(c => c.Competition)
                                     .Select(c => c.Categorie)
                                     .Distinct()
                                     .OrderBy(c => c)
                                     .Select(c => c)
                                     .ToList();

                    _selectedEquipe = Equipes.First(c => c.Equals((string)initData));

                    Classements = classements.Where(c => c.Competition != null && _selectedEquipe.Equals(c.Competition.Categorie))
                                             .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                             .ToList();

                    RaisePropertyChanged(nameof(Classements));
                    RaisePropertyChanged(nameof(SelectedEquipe));
                    RaisePropertyChanged(nameof(Equipes));
                }
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
