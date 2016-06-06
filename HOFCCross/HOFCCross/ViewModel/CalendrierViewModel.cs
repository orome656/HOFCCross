﻿using FreshMvvm;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Model;

namespace HOFCCross.ViewModel
{
    public class CalendrierViewModel: FreshBasePageModel
    {
        public Dictionary<string, List<Match>> Matchs { get; set; }
        IService Service;
        public CalendrierViewModel(IService service)
        {
            Service = service;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Matchs = FromModelList(Service.GetMatchs());
            this.RaisePropertyChanged(nameof(Matchs));
        }

        private Dictionary<string, List<Match>> FromModelList(List<Match> list)
        {
            Dictionary<string, List<Match>> matchs = new Dictionary<string, List<Match>>();
            foreach(Match match in list)
            {
                if(!matchs.ContainsKey(match.Competition.Categorie))
                {
                    matchs.Add(match.Competition.Categorie, new List<Match>());
                }
                matchs[match.Competition.Categorie].Add(match);
            }
            return matchs;
        }
    }
}
