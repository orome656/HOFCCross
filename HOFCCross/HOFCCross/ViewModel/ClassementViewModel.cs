using FreshMvvm;
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
    class ClassementViewModel: FilteredListBaseViewModel<string, ClassementEquipe>
    {
        IService Service;

        public ClassementViewModel(IService service)
        {
            Service = service;
        }

        private async Task LoadFilters()
        {
            try
            {
                var classements = await Service.GetClassements();
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
            IsLoading = true;

            try
            {
                var classements = await Service.GetClassements(forceRefresh);
                if(classements != null && classements.Count > 0)
                {
                    Items = classements.Where(c => c.Competition != null && SelectedFilter.Equals(c.Competition.Categorie))
                                             .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                             .ToList();
                }
            }
            catch(Exception ex)
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
                Debug.WriteLine(ex);
            }

            IsLoading = false;
        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            IsLoading = true;

            await LoadFilters();
            if(Filters != null && Filters.Count > 0)
                SelectedFilter = Filters.First(c => c.Equals((string)initData));

            IsLoading = false;
        }
    }
}
