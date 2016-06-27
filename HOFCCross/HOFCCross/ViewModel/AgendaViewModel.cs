using FreshMvvm;
using HOFCCross.Constantes;
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
    class AgendaViewModel: BaseViewModel
    {
        public List<Match> Matchs { get; set; }
        IService Service;
        public List<ToolbarItem> Semaines { get; set; }
        private DateTime WeekStartingDate;
        public ICommand ChangeWeek { get; set; }
        public string Title { get { return "Agenda du " + WeekStartingDate.ToString("dd/MM/yyyy"); } }

        public AgendaViewModel(IService service)
        {
            Service = service;
            ChangeWeek = new Command<DateTime>((key) =>
            {
                WeekStartingDate = key;
                this.RaisePropertyChanged(nameof(Title));
                this.ReloadMatchs();
            });
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            base.ViewIsAppearing(sender, e);

            var matchs = await Service.GetMatchs();

            Semaines = matchs.Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                             .Select(m => m.Date.StartOfWeek(DayOfWeek.Monday).Date)
                             .OrderBy(d => d)
                             .Distinct()
                             .Select(w => new ToolbarItem() { Text = w.ToString("dd/MM/yyyy"), Command = ChangeWeek, CommandParameter = w, Order = ToolbarItemOrder.Secondary })
                             .ToList();

            var lastDate = DateTime.ParseExact(Semaines.Last().Text, "dd/MM/yyyy", null);
            if(WeekStartingDate.CompareTo(lastDate.AddDays(7)) > 0)
            {
                WeekStartingDate = lastDate;
                this.RaisePropertyChanged(nameof(Title));
            }

            Matchs = matchs.Where(m => WeekStartingDate.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                           .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                           .OrderBy(m => m.Date)
                           .ToList();

            this.RaisePropertyChanged(nameof(Matchs));
            this.RaisePropertyChanged(nameof(Semaines));
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            WeekStartingDate = ((DateTime)initData).StartOfWeek(DayOfWeek.Monday);
        }

        private async void ReloadMatchs()
        {
            IsLoading = true;
            RaisePropertyChanged(nameof(IsLoading));
            List<Match> matchs = await Service.GetMatchs();

            Matchs = matchs.Where(m => WeekStartingDate.Date.CompareTo(m.Date.StartOfWeek(DayOfWeek.Monday).Date) == 0)
                           .Where(m => m.Equipe1.Contains(AppConstantes.HOFC_NAME) || m.Equipe2.Contains(AppConstantes.HOFC_NAME))
                           .OrderBy(m => m.Date)
                           .ToList();

            this.RaisePropertyChanged(nameof(Matchs));
            IsLoading = false;
            RaisePropertyChanged(nameof(IsLoading));
        }
    }
}
