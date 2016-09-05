using FreshMvvm;
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
    class JourneeViewModel: FilteredListBaseViewModel<int?, Match>
    {
        IService Service;
        private string _category;

        public JourneeViewModel(IService service)
        {
            Service = service;
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
                var matchs = await Service.GetMatchs();

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

            try
            {
                List<Match> matchs = await Service.GetMatchs(forceRefresh);

                Items = matchs.Where(m => _category.Equals(m.Competition.Categorie) && m.JourneeId == SelectedFilter).ToList();
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des Matchs");
                Debug.WriteLine(ex);
            }
            IsLoading = false;
        }

        public Command InfosCommand
        {
            get
            {
                return new Command<string>(async (id) =>
                {
                    MatchInfos infos = await Service.GetMatchInfos(id);
                    var texte = string.Format("Nom : {0}{1}Adresse : {2}{1}Ville : {3}{1}Arbitres {1}{4}", infos.Nom, Environment.NewLine, infos.Adresse, infos.Ville, infos.Arbitres?.Aggregate((a, b) => a + Environment.NewLine + b));
                    CoreMethods.DisplayAlert("Informations", texte, "Ok");
                    // should open dialog infos
                });
            }
        }
    }
}
