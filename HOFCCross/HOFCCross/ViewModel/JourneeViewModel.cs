using FreshMvvm;
using HOFCCross.Extension;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOFCCross.ViewModel
{
    class JourneeViewModel: FreshBasePageModel
    {
        private string Category { get; set; }
        public Dictionary<int, List<Match>> Matchs { get; set; }
        IService Service;
        public JourneeViewModel(IService service)
        {
            Service = service;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            Matchs = FromModelList((await Service.GetMatchs()).Where(m => Category.Equals(m.Competition.Categorie) && m.Competition.IsChampionnat).ToList());
            this.RaisePropertyChanged(nameof(Matchs));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Category = (string)initData;
        }

        private Dictionary<int, List<Match>> FromModelList(List<Match> list)
        {
            Dictionary<int, List<Match>> matchs = new Dictionary<int, List<Match>>();
            foreach (Match match in list)
            {
                if (!matchs.ContainsKey(match.Competition.JourneeId))
                {
                    matchs.Add(match.Competition.JourneeId, new List<Match>());
                }
                matchs[match.Competition.JourneeId].Add(match);
            }
            return matchs;
        }
    }
}
