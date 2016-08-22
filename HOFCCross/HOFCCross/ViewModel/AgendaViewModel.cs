using FreshMvvm;
using HOFCCross.Constantes;
using HOFCCross.Extension;
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
    class AgendaViewModel : BaseViewModel
    {
        private List<Match> _matchs;
        public List<Match> Matchs {
            get { return _matchs; }
            set
            {
                _matchs = value;
                RaisePropertyChanged(nameof(Matchs));
            }
        }
        IService Service;
        private List<Week> _semaines;
        public List<Week> Semaines {
            get { return _semaines; }
            set
            {
                _semaines = value;
                RaisePropertyChanged(nameof(Semaines));
            }
        }

        private Week _weekStartingDate;
        
        public Week WeekSelected {
            get { return _weekStartingDate; }
            set {
                _weekStartingDate = value;
                RaisePropertyChanged(nameof(WeekSelected));
                ReloadMatchs();
            }
        }

        public AgendaViewModel(IService service)
        {
            Service = service;
            Title = "Agenda";
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            IsLoading = true;

            try
            {
                await LoadListeSemaine();
                
                Week weekDate = null;
                DateTime date = (DateTime)initData;
                var lastDate = Semaines.Last();

                while(weekDate == null)
                {
                    if(weekDate != null && weekDate.Date.CompareTo(lastDate.Date.AddDays(7)) >= 0)
                    {
                        weekDate = lastDate;
                        break;
                    }

                    weekDate = Semaines.FirstOrDefault(w => w.Date.Date.CompareTo(date.StartOfWeek(DayOfWeek.Monday)) == 0);
                    date = date.AddDays(7);
                }
                WeekSelected = weekDate;
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
        }

        private async Task LoadListeSemaine()
        {
            var matchs = await Service.GetMatchs();

            Semaines = matchs.Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                             .Select(m => m.Date.StartOfWeek(DayOfWeek.Monday).Date)
                             .OrderBy(d => d)
                             .Distinct()
                             .Select(w => new Week() { Date = w })
                             .ToList();
        }

        private async void ReloadMatchs()
        {
            IsLoading = true;

            try
            {
                List<Match> matchs = await Service.GetMatchs();

                Matchs = matchs.Where(m => _weekStartingDate.Date.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                               .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                               .OrderBy(m => m.Date)
                               .ToList();
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }

            IsLoading = false;
        }

        public class Week
        {
            public string DisplayName { get { return Date.ToString("dd/MM/yyyy"); } }
            public DateTime Date { get; set; }
        }
    }
}
