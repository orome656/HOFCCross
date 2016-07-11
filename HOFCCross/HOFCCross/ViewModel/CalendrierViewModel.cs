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

namespace HOFCCross.ViewModel
{
    public class CalendrierViewModel: BaseViewModel
    {
        public List<Match> Matchs { get; set; }
        public string Category { get; set; }
        public List<ToolbarItem> Equipes { get; set; }
        IService Service;
        public ICommand ChangeTeam { get; set; }


        public CalendrierViewModel(IService service)
        {
            Service = service;
            ChangeTeam = new Command<string>((key) =>
            {
                Category = key;
                this.ReloadMatchs();
            });
        }

        public override async void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                List<Match> matchs = await Service.GetMatchs();

                Equipes = matchs.Select(m => m.Competition)
                                .Select(c => c.Categorie)
                                .Distinct()
                                .OrderBy(c => c)
                                .Select(c => new ToolbarItem() { Text = c, Command = ChangeTeam, CommandParameter = c, Order = ToolbarItemOrder.Secondary })
                                .ToList();

                Matchs = matchs.Where(m => m.Competition != null && Category.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
                               .OrderBy(m => m.Date)
                               .ToList();

                this.RaisePropertyChanged(nameof(Equipes));
                this.RaisePropertyChanged(nameof(Matchs));
            }
            catch
            {
                DisplayError("Erreur lors de la récupération des Matchs");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        private async void ReloadMatchs()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            try
            {
                List<Match> matchs = await Service.GetMatchs();
                Matchs = matchs.Where(m => m.Competition != null && Category.Equals(m.Competition.Categorie) && (m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME)))
                               .OrderBy(m => m.Date)
                               .ToList();
                this.RaisePropertyChanged(nameof(Matchs));
            }
            catch
            {
                DisplayError("Erreur lors de la récupération des Matchs");
            }
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
