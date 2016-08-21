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
        public List<Match> Matchs { get; set; }

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

        public List<string> Equipes { get; set; }
        
        IService Service;

        public CalendrierViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                List<Match> matchs = await Service.GetMatchs();
                if(matchs != null && matchs.Count > 0)
                {
                    Equipes = matchs.Select(m => m.Competition)
                                    .Select(c => c.Categorie)
                                    .Distinct()
                                    .OrderBy(c => c)
                                    .Select(c => c)
                                    .ToList();

                    _selectedEquipe = Equipes.First(c => c.Equals((string)initData));

                    Matchs = matchs.Where(m => m.Competition != null && _selectedEquipe.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
                                   .OrderBy(m => m.Date)
                                   .ToList();

                    this.RaisePropertyChanged(nameof(SelectedEquipe));
                    this.RaisePropertyChanged(nameof(Equipes));
                    this.RaisePropertyChanged(nameof(Matchs));
                }
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        private async void ReloadMatchs()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
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
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
