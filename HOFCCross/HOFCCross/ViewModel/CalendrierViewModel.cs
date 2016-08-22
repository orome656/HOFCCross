using FreshMvvm;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using HOFCCross.Constantes;
using System.Diagnostics;

namespace HOFCCross.ViewModel
{
    public class CalendrierViewModel: BaseViewModel
    {
        private List<Match> _matchs;
        public List<Match> Matchs
        {
            get { return _matchs; }
            set
            {
                _matchs = value;
                RaisePropertyChanged(nameof(Matchs));
            }
        }

        private string _selectedEquipe;

        public string SelectedEquipe {
            get {
                return _selectedEquipe;
            }
            set {
                _selectedEquipe = value;
                RaisePropertyChanged(nameof(SelectedEquipe));
                ReloadMatchs();
            }
        }

        private List<string> _equipes;
        public List<string> Equipes
        {
            get { return _equipes; }
            set
            {
                _equipes = value;
                RaisePropertyChanged(nameof(Equipes));
            }
        }
        
        IService Service;

        public CalendrierViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            IsLoading = true;

            try
            {
                await LoadEquipes();
                SelectedEquipe = Equipes.First(c => c.Equals((string)initData));
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
        }

        private async Task LoadEquipes()
        {
            List<Match> matchs = await Service.GetMatchs();
            Equipes = matchs.Select(m => m.Competition)
                                    .Select(c => c.Categorie)
                                    .Distinct()
                                    .OrderBy(c => c)
                                    .Select(c => c)
                                    .ToList();

        }

        private async void ReloadMatchs()
        {
            IsLoading = true;
            try
            {
                List<Match> matchs = await Service.GetMatchs();
                Matchs = matchs.Where(m => m.Competition != null && _selectedEquipe.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
                               .OrderBy(m => m.Date)
                               .ToList();
                this.RaisePropertyChanged(nameof(Matchs));
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
        }
    }
}
