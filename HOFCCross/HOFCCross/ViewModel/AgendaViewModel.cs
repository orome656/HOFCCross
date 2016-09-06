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
    class AgendaViewModel : FilteredListBaseViewModel<AgendaViewModel.Week, Match>
    {
        IService Service;

        public AgendaViewModel(IService service)
        {
            Service = service;
            Title = "Agenda";
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            IsLoading = true;
            
            await LoadListeSemaine();
            
            DateTime date = (DateTime)initData;
            Week weekDate = Filters.FirstOrDefault(w => w.Date.Date.CompareTo(date.StartOfWeek(DayOfWeek.Monday)) >= 0);

            if (weekDate == null) weekDate = Filters.Last();
            SelectedFilter = weekDate;

            IsLoading = false;
        }

        private async Task LoadListeSemaine()
        {
            try
            {
                var matchs = await Service.GetMatchs();
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
            IsLoading = true;

            try
            {
                List<Match> matchs = await Service.GetMatchs(forceRefresh);

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

            IsLoading = false;
        }

        public class Week
        {
            public string DisplayName { get { return Date.ToString("dd/MM/yyyy"); } }
            public DateTime Date { get; set; }
        }

        public Command InfosCommand
        {
            get
            {
                return new Command<string>(async (id) =>
                {
                    await CoreMethods.PushPageModel<MatchInfosViewModel>(id, true);
                });
            }
        }
    }
}
