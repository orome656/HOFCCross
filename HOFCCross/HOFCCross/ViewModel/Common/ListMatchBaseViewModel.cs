using HOFCCross.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HOFCCross.Service;

namespace HOFCCross.ViewModel.Common
{
    public abstract class ListMatchBaseViewModel<T, V> : FilteredListBaseViewModel<T, V>
    {
        private bool _isNavLoading;

        public ListMatchBaseViewModel(IService service) : base(service)
        {
        }

        public bool IsNavLoading
        {
            get
            {
                return _isNavLoading;
            }
            set
            {
                _isNavLoading = value;
                RaisePropertyChanged(nameof(IsNavLoading));
            }
        }

        public Command InfosCommand
        {
            get
            {
                return new Command<int>(async (id) =>
                {
                    await CoreMethods.PushPageModel<MatchDetailsViewModel>(id, true);
                });
            }
        }

        public Command NavCommand
        {
            get
            {
                return new Command<string>(async (id) =>
                {
                    IsNavLoading = true;
                    var infos = await _service.GetMatchInfos(id);
                    var adresse = string.Format("{0} {1}", infos.Adresse, infos.Ville);

                    IsNavLoading = false;
                    Maps.OpenMap(adresse);
                });
            }
        }
    }
}
