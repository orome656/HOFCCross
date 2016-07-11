using FreshMvvm;
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
    class ClassementViewModel: BaseViewModel
    {
        public List<ClassementEquipe> Classements { get; set; }
        public string Category { get; set; }
        public List<ToolbarItem> Equipes { get; set; }
        IService Service;
        public ICommand ChangeTeam { get; set; }

        public ClassementViewModel(IService service)
        {
            Service = service;
            ChangeTeam = new Command<string>((key) =>
            {
                Category = key;
                this.ReloadRanks();
            });
        }

        private async void ReloadRanks()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                var classements = await Service.GetClassements();

                Classements = classements.Where(c => c.Competition != null && Category.Equals(c.Competition.Categorie))
                                         .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                         .ToList();

                this.RaisePropertyChanged(nameof(Classements));
            }
            catch
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));

            try
            {
                var classements = await Service.GetClassements();

                Equipes = classements.Select(c => c.Competition)
                                     .Select(c => c.Categorie)
                                     .Distinct()
                                     .OrderBy(c => c)
                                     .Select(c => new ToolbarItem() { Text = c, Command = ChangeTeam, CommandParameter = c, Order = ToolbarItemOrder.Secondary })
                                     .ToList();
                Classements = classements.Where(c => c.Competition != null && Category.Equals(c.Competition.Categorie))
                                         .Select((c, i) => new ClassementEquipe() { Bc = c.Bc, Bp = c.Bp, Competition = c.Competition, Defaite = c.Defaite, Joue = c.Joue, Nom = c.Nom, Nul = c.Nul, Point = c.Point, Victoire = c.Victoire, Rank = i + 1 })
                                         .ToList();
                this.RaisePropertyChanged(nameof(Classements));
                this.RaisePropertyChanged(nameof(Equipes));
            }
            catch (Exception ex)
            {
                DisplayError("Erreur lors de la récupération des informations de classement");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
