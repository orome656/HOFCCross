using FreshMvvm;
using HOFCCross.Constantes;
using HOFCCross.Container;
using HOFCCross.Enum;
using HOFCCross.Model;
using HOFCCross.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;

namespace HOFCCross.ViewModel
{
    public class MenuViewModel : FreshBasePageModel
    {
        public List<MenuItem> Items { get; set; }
        public string Title { get; set; } = "HOFC";
        public bool IsAuthenticated { get {
                return FreshMvvm.FreshIOC.Container.Resolve<ILoginService>().IsAuthenticated();
            }
        }

        public User User
        {
            get
            {
                if(FreshMvvm.FreshIOC.Container.Resolve<ILoginService>().IsAuthenticated())
                {
                    return FreshMvvm.FreshIOC.Container.Resolve<ILoginService>().GetUser();
                }
                else
                {
                    return null;
                }
            }
        }

        public MenuViewModel()
        {
            Items = new List<MenuItem>();
            Items.Add(new MenuItem()
            {
                Titre = "Actus",
                Icon = "accueil_icon.png",
                PageName = PageNameEnum.ACTU
            });
            Items.Add(new MenuItem()
            {
                Titre = "Calendriers",
                Icon = "calendar_icon.png",
                PageName = PageNameEnum.CALENDRIER
            });
            Items.Add(new MenuItem()
            {
                Titre = "Classements",
                Icon = "classement_icon.png",
                PageName = PageNameEnum.CLASSEMENT
            });
            Items.Add(new MenuItem()
            {
                Titre = "Agenda",
                Icon = "calendar_icon.png",
                PageName = PageNameEnum.AGENDA
            });
            Items.Add(new MenuItem()
            {
                Titre = "Journées Excellence",
                Icon = "calendar_icon.png",
                PageName = PageNameEnum.JOURNEE_EQUIPE_1
            });
            Items.Add(new MenuItem()
            {
                Titre = "Journées Premiere Div.",
                Icon = "calendar_icon.png",
                PageName = PageNameEnum.JOURNEE_EQUIPE_2
            });
            Items.Add(new MenuItem()
            {
                Titre = "Journées Promotion Premiere Div.",
                Icon = "calendar_icon.png",
                PageName = PageNameEnum.JOURNEE_EQUIPE_3
            });
        }
        //Commande pour le changement de page à partir du menu
        private Xamarin.Forms.Command _menuItemCommand;
        public ICommand MenuItemCommand
        {
            get
            {
                _menuItemCommand = _menuItemCommand ?? new Xamarin.Forms.Command<MenuItem>(async (item) =>
                {
                    switch(item.PageName)
                    {
                        case PageNameEnum.ACTU:
                            await CoreMethods.PushPageModel<ActuViewModel>();
                            break;
                        case PageNameEnum.CALENDRIER:
                            await CoreMethods.PushPageModel<CalendrierViewModel>("equipe1");
                            break;
                        case PageNameEnum.CLASSEMENT:
                            await CoreMethods.PushPageModel<ClassementViewModel>("equipe1");
                            break;
                        case PageNameEnum.AGENDA:
                            await CoreMethods.PushPageModel<AgendaViewModel>(DateTime.Now);
                            break;
                        case PageNameEnum.JOURNEE_EQUIPE_1:
                            await CoreMethods.PushPageModel<JourneeViewModel>("equipe1");
                            break;
                        case PageNameEnum.JOURNEE_EQUIPE_2:
                            await CoreMethods.PushPageModel<JourneeViewModel>("equipe2");
                            break;
                        case PageNameEnum.JOURNEE_EQUIPE_3:
                            await CoreMethods.PushPageModel<JourneeViewModel>("equipe3");
                            break;
                    }
                });
                return _menuItemCommand;
            }
        }

        public ICommand StartLoginCommand
        {
            get
            {
                return new Xamarin.Forms.Command(async () =>
                {
                    AppConstantes.OAUTH_SETTINGS.SuccessCommand = LoginSuccessCommand;

                    var mainPage = App.Current.MainPage as MasterDetail;
                    mainPage.IsPresented = false;

                    var detail = mainPage.Detail as Xamarin.Forms.NavigationPage;
                    var model = detail.CurrentPage.GetModel();
                    await detail.CurrentPage.GetModel().CoreMethods.PushPageModel<LoginViewModel>(null, true);
                });
            }
        }

        public ICommand LoginSuccessCommand
        {
            get
            {
                return new Xamarin.Forms.Command(async () =>
                {
                    await FreshMvvm.FreshIOC.Container.Resolve<ILoginService>().RequestUserInfo();

                    RaisePropertyChanged(nameof(User));
                });
            }
        }
    }
}
