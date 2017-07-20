using HOFCCross.Model;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace HOFCCross.ViewModel
{
    public class MatchDetailsViewModel : BaseViewModel
    {
        public Match Match { get; set; }
        public ObservableCollection<Position> Positions { get; set; } = new ObservableCollection<Position>();

        public MatchDetailsViewModel(IService service): base(service)
        {
        }

        public override async void Init(object initData)
        {
            Title = "Détails Match";
            IsLoading = true;
            base.Init(initData);
            int matchId = (int)initData;
            Match = await _service.GetMatchDetails(matchId);
            Positions.Clear();
            Geocoder geo = new Geocoder();
            List<Position> pos = (await geo.GetPositionsForAddressAsync($"{Match.MatchInfos.Adresse} {Match.MatchInfos.Ville}")).ToList();
            Match.MatchInfos.Position = pos.FirstOrDefault();
            if (pos.Any())
                Positions.Add(pos.First());
            RaisePropertyChanged(nameof(Match));
            IsLoading = false;
        }

        public Command CloseCommand
        {
            get
            {
                return new Command(() => { CoreMethods.PopPageModel(true); });
            }
        }
    }
}
