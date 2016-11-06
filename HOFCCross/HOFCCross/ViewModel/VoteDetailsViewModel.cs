using HOFCCross.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HOFCCross.Service;
using PropertyChanged;
using System.Windows.Input;
using Xamarin.Forms;
using HOFCCross.Model.Page;

namespace HOFCCross.ViewModel
{
    [ImplementPropertyChanged]
    public class VoteDetailsViewModel : BaseViewModel
    {
        public List<Model.Vote> Votes { get; set; }
        public List<Model.Joueur> MatchPlayers { get; set; }

        public VoteDetailsViewModel(IService service) : base(service)
        {
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            var id = initData as int?;

            Votes = await _service.GetUserMatchVote(id.ToString());
            MatchPlayers = await _service.GetPlayersForMatch(id.ToString());
        }

        private Action<string, Model.Joueur> PlayerChanged = new Action<string, Model.Joueur>((type, joueur) => {
            
        });

        public ICommand ItemTapCommand { get
            {
                return new Command(
                    async (o) => {
                        await CoreMethods.PushPageModel<DropDownViewModel>(new DropDownParameters<Model.Joueur>() { Items = MatchPlayers, Callback= PlayerChanged }, true);
                    },
                    (o) => {
                        return true;
                    });
            }
        }
    }
}
