﻿using FreshMvvm;
using HOFCCross.Extension;
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
    class JourneeViewModel: FreshBasePageModel
    {
        private string Category { get; set; }
        private int Journee;
        public List<Match> Matchs { get; set; }
        public List<ToolbarItem> Journees { get; set; }
        IService Service;
        public ICommand ChangeDay { get; set; }

        public JourneeViewModel(IService service)
        {
            Service = service;
            ChangeDay = new Command<int>((key) =>
            {
                Journee = key;
                this.ReloadMatchs();
            });
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            var matchs = await Service.GetMatchs();

            Journees = matchs.Where(m => m.JourneeId.HasValue && Category.Equals(m.Competition.Categorie))
                             .Select(m => m.JourneeId)
                             .Distinct()
                             .OrderBy(c => c)
                             .Select(c => new ToolbarItem() { Text = c + "", Command = ChangeDay, CommandParameter = c, Order = ToolbarItemOrder.Secondary })
                             .ToList();

            Matchs = matchs.Where(m => Category.Equals(m.Competition.Categorie) && m.JourneeId == Journee).ToList();

            this.RaisePropertyChanged(nameof(Journees));
            this.RaisePropertyChanged(nameof(Matchs));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
            Journee = 1;
        }

        private async void ReloadMatchs()
        {
            List<Match> matchs = await Service.GetMatchs();

            Matchs = matchs.Where(m => Category.Equals(m.Competition.Categorie) && m.JourneeId == Journee).ToList();

            this.RaisePropertyChanged(nameof(Matchs));
        }
    }
}
