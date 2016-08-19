using FreshMvvm;
using HOFCCross.Constantes;
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
    class AgendaViewModel : BaseViewModel
    {
        public List<Match> Matchs { get; set; }
        IService Service;
        public List<Week> Semaines { get; set; }

        private Week _weekStartingDate;
        
        public Week WeekSelected {
            get { return _weekStartingDate; }
            set {
                _weekStartingDate = value;
                RaisePropertyChanged(nameof(WeekSelected));
                ReloadMatchs();
            }
        }

        public ICommand ChangeWeek { get; set; }
        public string Title { get { return "Agenda"; } }

        public AgendaViewModel(IService service)
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
                var matchs = await Service.GetMatchs();

                Semaines = matchs.Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                                 .Select(m => m.Date.StartOfWeek(DayOfWeek.Monday).Date)
                                 .OrderBy(d => d)
                                 .Distinct()
                                 .Select(w => new Week() { Date = w })
                                 .ToList();

                WeekSelected = Semaines.First( w => w.DisplayName.Equals((((DateTime)initData).StartOfWeek(DayOfWeek.Monday)).ToString("dd/MM/yyyy")));

                var lastDate = Semaines.Last().Date;
                if (_weekStartingDate.Date.CompareTo(lastDate.AddDays(7)) > 0)
                {
                    WeekSelected = Semaines.First(w => w.DisplayName.Equals(lastDate.ToString("dd/MM/yyyy")));
                }

                Matchs = matchs.Where(m => _weekStartingDate.Date.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                               .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                               .OrderBy(m => m.Date)
                               .ToList();

                this.RaisePropertyChanged(nameof(WeekSelected));
                this.RaisePropertyChanged(nameof(Matchs));
                this.RaisePropertyChanged(nameof(Semaines));
            }
            catch(Exception ex)
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
            try
            {
                List<Match> matchs = await Service.GetMatchs();

                Matchs = matchs.Where(m => _weekStartingDate.Date.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                               .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                               .OrderBy(m => m.Date)
                               .ToList();

                this.RaisePropertyChanged(nameof(Matchs));
            }
            catch
            {
                DisplayError("Erreur lors de la récupération des Matchs");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        public class Week
        {
            public string DisplayName { get { return Date.ToString("dd/MM/yyyy"); } }
            public DateTime Date { get; set; }
        }
    }
}
