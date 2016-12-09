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
using HOFCCross.ViewModel.Common;

namespace HOFCCross.ViewModel
{
    public class CalendrierViewModel: ListMatchBaseViewModel<string, Match>
    {
        public CalendrierViewModel(IService service) : base(service)
        {
        }

        public override async void Init(object initData)
        {
            bool loadEnd = false;
            var animation = Task.Delay(500).ContinueWith((t) => { if (!loadEnd) IsLoading = true; });
            base.Init(initData);
            IsLoading = true;
            
            await LoadEquipes();
            try
            {
                if (Filters != null && Filters.Count > 0)
                {
                    var filter = Filters.FirstOrDefault(c => c.Equals((string)initData));
                    if (filter == null)
                        filter = Filters.First();
                    SelectedFilter = filter;
                }
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des matchs");
                Debug.WriteLine(ex);
            }

            IsLoading = false;
            loadEnd = true;
        }

        private async Task LoadEquipes()
        {
            try
            {
                List<Match> matchs = await _service.GetMatchs();
                Filters = matchs.Select(m => m.Competition)
                                .Select(c => c.Categorie)
                                .Distinct()
                                .OrderBy(c => c)
                                .Select(c => c)
                                .ToList();
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

            if(SelectedFilter != null)
            {
                try
                {
                    List<Match> matchs = await _service.GetMatchs(forceRefresh);
                    Items = matchs.Where(m => m.Competition != null && SelectedFilter.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
                                   .OrderBy(m => m.Date)
                                   .ToList();
                }
                catch(Exception ex)
                {
                    DisplayError("Erreur lors de la récupération des Matchs");
                    Debug.WriteLine(ex);
                }
            }
            IsLoading = false;
            loadEnd = true;
        }
    }
}
