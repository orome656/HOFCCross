using FreshMvvm;
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
    class JourneeViewModel: ListMatchBaseViewModel<int?, Match>
    {
        private string _category;

        public JourneeViewModel(IService service) : base(service)
        {
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            _category = (string)initData;

            IsLoading = true;
            
            await LoadFilters();
            SelectedFilter = 1;
            
            IsLoading = false;
        }

        private async Task LoadFilters()
        {
            try
            {
                var matchs = await _service.GetMatchs();

                Filters = matchs.Where(m => m.JourneeId.HasValue && _category.Equals(m.Competition.Categorie))
                                 .Select(m => m.JourneeId)
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
            IsLoading = true;

            if (SelectedFilter != null)
            {
                try
                {
                    List<Match> matchs = await _service.GetMatchs(forceRefresh);

                    Items = matchs.Where(m => _category.Equals(m.Competition.Categorie) && m.JourneeId == SelectedFilter).ToList();
                }
                catch (Exception ex)
                {
                    DisplayError("Erreur lors de la récupération des Matchs");
                    Debug.WriteLine(ex);
                }
            }

            IsLoading = false;
        }
    }
}
