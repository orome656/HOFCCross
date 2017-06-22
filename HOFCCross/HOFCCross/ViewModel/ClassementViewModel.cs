using FreshMvvm;
using HOFCCross.Model;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
using PropertyChanged;
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
    [AddINotifyPropertyChangedInterface]
    class ClassementViewModel: FilteredListBaseViewModel<string, ClassementEquipe>
    {
        public bool IsLandscape { get; set; }

        public ClassementViewModel(IService service) : base(service)
        {
        }

        private async Task LoadFilters()
        {
            try
            {
                var classements = await _service.GetClassements();
                if (classements != null && classements.Count > 0)
                {
                    Filters = classements.Select(c => c.Competition)
                                     .Select(c => c.Categorie)
                                     .Distinct()
                                     .OrderBy(c => c)
                                     .Select(c => c)
                                     .ToList();
                }
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
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
                    var classements = await _service.GetClassements(forceRefresh);
                    if (classements != null && classements.Count > 0)
                    {
                        Items = classements.Where(c => c.Competition != null && SelectedFilter.Equals(c.Competition.Categorie))
                                                 .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                                 .ToList();
                    }
                }
                catch (Exception ex)
                {
                    DisplayError("Erreur lors de la récupération des informations de classement");
                    Debug.WriteLine(ex);
                }
            }
            
            IsLoading = false;
            loadEnd = true;
        }

        public override async void Init(object initData)
        {
            bool loadEnd = false;
            var animation = Task.Delay(500).ContinueWith((t) => { if (!loadEnd) IsLoading = true; });
            base.Init(initData);

            IsLoading = true;

            await LoadFilters();
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
                DisplayError("Erreur lors de la récupération des informations de classement");
                Debug.WriteLine(ex);
            }

            IsLoading = false;
            loadEnd = true;
        }
    }
}
