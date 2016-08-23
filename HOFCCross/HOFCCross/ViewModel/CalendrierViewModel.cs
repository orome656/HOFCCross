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
    public class CalendrierViewModel: FilteredListBaseViewModel<string, Match>
    {
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
                SelectedFilter = Filters.First(c => c.Equals((string)initData));
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
            Filters = matchs.Select(m => m.Competition)
                            .Select(c => c.Categorie)
                            .Distinct()
                            .OrderBy(c => c)
                            .Select(c => c)
                            .ToList();

        }

        protected override async Task ReloadItems(bool forceRefresh = false)
        {
            IsLoading = true;
            try
            {
                List<Match> matchs = await Service.GetMatchs(forceRefresh);
                Items = matchs.Where(m => m.Competition != null && SelectedFilter.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
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
    }
}
