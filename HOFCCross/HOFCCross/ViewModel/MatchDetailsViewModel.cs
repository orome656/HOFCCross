using HOFCCross.Model;
using HOFCCross.Service;
using HOFCCross.ViewModel.Common;
using System;
using System.Collections.Generic;
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

        public MatchDetailsViewModel(IService service): base(service)
        {
        }

        public override async void Init(object initData)
        {
            Title = "Détails Match";
            IsLoading = true;
            base.Init(initData);
            string matchId = initData as string;
            Match = await _service.GetMatchDetails(matchId);

            Geocoder geo = new Geocoder();
            IEnumerable<Position> pos = await geo.GetPositionsForAddressAsync($"{Match.MatchInfos.Adresse} {Match.MatchInfos.Ville}");
            Match.MatchInfos.Position = pos.FirstOrDefault();

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
