using FreshMvvm;
using HOFCCross.Enum;
using HOFCCross.Model;
using HOFCCross.Service;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HOFCCross.ViewModel
{
    [ImplementPropertyChanged]
    public class ListeVotesOuvertsViewModel: FreshBasePageModel
    {
        private IService _service;
        public List<Match> OpenedVotes { get; set; }

        public ListeVotesOuvertsViewModel(IService service)
        {
            _service = service;
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            var matchs = await _service.GetMatchs();
            OpenedVotes = matchs.Where(m => StatutVote.OUVERT.Equals(m.VoteStatut)).ToList();
        }


        public Command VoteCommand {
            get {
                return new Command<int>(async (idMatch) =>
                {
                    await CoreMethods.PushPageModel<VoteDetailsViewModel>(idMatch);
                });
            }
        }
    }
}
