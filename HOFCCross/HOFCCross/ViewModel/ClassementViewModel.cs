﻿using FreshMvvm;
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
    class ClassementViewModel: FreshBasePageModel
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
            var classements = await Service.GetClassements();

            Classements = classements.Where(c => c.Competition != null && Category.Equals(c.Competition.Categorie)).ToList();

            this.RaisePropertyChanged(nameof(Classements));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            var classements = await Service.GetClassements();

            Equipes = classements.Select(c => c.Competition).Select(c => c.Categorie).Distinct().OrderBy(c => c).Select(c => new ToolbarItem() { Text = c, Command = ChangeTeam, CommandParameter = c, Order = ToolbarItemOrder.Secondary }).ToList();
            Classements = classements.Where(c => c.Competition != null && Category.Equals(c.Competition.Categorie)).ToList();
            this.RaisePropertyChanged(nameof(Classements));
            this.RaisePropertyChanged(nameof(Equipes));
        }
    }
}
