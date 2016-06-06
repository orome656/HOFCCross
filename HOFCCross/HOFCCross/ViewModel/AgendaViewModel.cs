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
    class AgendaViewModel: FreshBasePageModel
    {
        public Dictionary<string, List<Match>> Matchs { get; set; }
        IService Service;
        public AgendaViewModel(IService service)
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
            foreach (Match match in list)
            {
                if (!matchs.ContainsKey(match.Date.StartOfWeek(DayOfWeek.Monday).ToString("dd-MM-yyyy")))
                {
                    matchs.Add(match.Date.StartOfWeek(DayOfWeek.Monday).ToString("dd-MM-yyyy"), new List<Match>());
                }
                matchs[match.Date.StartOfWeek(DayOfWeek.Monday).ToString("dd-MM-yyyy")].Add(match);
            }
            return matchs;
        }
    }
}
