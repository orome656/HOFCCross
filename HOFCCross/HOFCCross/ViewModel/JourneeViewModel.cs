using FreshMvvm;
using HOFCCross.Extension;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    class JourneeViewModel: BaseViewModel
    {
        private string Category { get; set; }
        private int _journee;
        public int Journee
        {
            get { return _journee; }
            set
            {
                _journee = value;
                RaisePropertyChanged(nameof(Journee));
                ReloadMatchs();
            }
        }
        public List<Match> Matchs { get; set; }
        public List<int?> Journees { get; set; }
        IService Service;

        public JourneeViewModel(IService service)
        {
            Service = service;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
            Journee = 1;

            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                var matchs = await Service.GetMatchs();

                Journees = matchs.Where(m => m.JourneeId.HasValue && Category.Equals(m.Competition.Categorie))
                                 .Select(m => m.JourneeId)
                                 .Distinct()
                                 .OrderBy(c => c)
                                 .Select(c => c)
                                 .ToList();

                Matchs = matchs.Where(m => Category.Equals(m.Competition.Categorie) && m.JourneeId == Journee).ToList();

                this.RaisePropertyChanged(nameof(Journees));
                this.RaisePropertyChanged(nameof(Matchs));
            }
            catch
            {
                DisplayError("Erreur lors de la récupération des Matchs");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        private async void ReloadMatchs()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            List<Match> matchs = await Service.GetMatchs();

            Matchs = matchs.Where(m => Category.Equals(m.Competition.Categorie) && m.JourneeId == Journee).ToList();

            this.RaisePropertyChanged(nameof(Matchs));
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
