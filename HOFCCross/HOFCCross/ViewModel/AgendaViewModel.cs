using FreshMvvm;
using HOFCCross.Constantes;
using HOFCCross.Extension;
using HOFCCross.Model;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
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
    class AgendaViewModel : ListMatchBaseViewModel<AgendaViewModel.Week, Match>
    {
        public AgendaViewModel(IService service) : base(service)
        {
            Title = "Agenda";
        }

        public override async void Init(object initData)
        {
            bool loadEnd = false;
            var animation = Task.Delay(500).ContinueWith((t) => { if (!loadEnd) IsLoading = true; });
            base.Init(initData);
            IsLoading = true;
            
            await LoadListeSemaine();
            
            DateTime date = (DateTime)initData;
            Week weekDate = Filters.FirstOrDefault(w => w.Date.Date.CompareTo(date.StartOfWeek(DayOfWeek.Monday)) >= 0);

            if (weekDate == null) weekDate = Filters.Last();
            SelectedFilter = weekDate;

            IsLoading = false;
            loadEnd = true;
        }

        private async Task LoadListeSemaine()
        {
            try
            {
                var matchs = await _service.GetMatchs();
                if(matchs != null && matchs.Count > 0)
                {
                    Filters = matchs.Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                                     .Select(m => m.Date.StartOfWeek(DayOfWeek.Monday).Date)
                                     .OrderBy(d => d)
                                     .Distinct()
                                     .Select(w => new Week() { Date = w })
                                     .ToList();

                }
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
        }

        protected override async Task ReloadItems(bool forceRefresh = false)
        {
            bool loadEnd = false;
            var animation = Task.Delay(500).ContinueWith((t) => { if (!loadEnd) IsLoading = true; });
            IsLoading = true;

            if (SelectedFilter != null)
            {
                try
                {
                    List<Match> matchs = await _service.GetMatchs(forceRefresh);

                    Items = matchs.Where(m => SelectedFilter.Date.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                                   .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                                   .OrderBy(m => m.Date)
                                   .ToList();
                }
                catch (Exception ex)
                {
                    DisplayError("Erreur lors de la récupération des Matchs");
                    Debug.WriteLine(ex);
                }
            }

            IsLoading = false;
            loadEnd = true;
        }

        public class Week
        {
            public string DisplayName { get { return Date.ToString("dd/MM/yyyy"); } }
            public DateTime Date { get; set; }
        }
    }
}
