using FreshMvvm;
using HOFCCross.Auth;
using HOFCCross.Constantes;
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
        public ILoginService LoginService { get; set; }
        public List<MenuItem> Items { get; set; }
        public string Title { get; set; } = "HOFC";
        public bool IsAuthenticated { get {
                return LoginService.IsAuthenticated();
            }
        }

        public User User
        {
            get
            {
                if(LoginService.IsAuthenticated())
                {
                    return LoginService.GetUser();
                }
                else
                {
                    return null;
                }
            }
        }

        public MenuViewModel(ILoginService loginService)
        {
            LoginService = loginService;
            LoginService.LoginStatusChanged += LoginService_LoginStatusChanged;
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

        private void LoginService_LoginStatusChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(IsAuthenticated));
            RaisePropertyChanged(nameof(User));
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

        private Xamarin.Forms.Command _connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                _connectCommand = _connectCommand ?? new Xamarin.Forms.Command(() =>
                {
                    var authenticator = new HOFCCross.Auth.OAuth2Authenticator
                    (
                        AppConstantes.OAUTHSETTING.ClientId,
                        AppConstantes.OAUTHSETTING.ClientSecret,
                        AppConstantes.OAUTHSETTING.Scope,
                        new Uri(AppConstantes.OAUTHSETTING.AuthorizeUrl),
                        new Uri(AppConstantes.OAUTHSETTING.RedirectUrl),
                        new Uri(AppConstantes.OAUTHSETTING.AccessTokenUrl),
                        isUsingNativeUI: true
                    );
                    

                    authenticator.Completed += Authenticator_Completed;
                    authenticator.Error += Authenticator_Error;
                    AuthentificationState.Authenticator = authenticator;

                    Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
                    presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                    presenter.Login(authenticator);
                    
                });
                return _connectCommand;
            }
        }


        private Xamarin.Forms.Command _disconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                _disconnectCommand = _disconnectCommand ?? new Xamarin.Forms.Command(() =>
                {
                    LoginService.Disconnect();
                });
                return _disconnectCommand;
            }
        }

        private void Authenticator_Error(object sender, AuthenticatorErrorEventArgs e)
        {
            AuthentificationState.Authenticator = null;
        }

        private void Authenticator_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            AuthentificationState.Authenticator = null;
            if(e.IsAuthenticated)
            {
                LoginService.AuthenticateAsync(e.Account);
            }
            else
            {
                // Erreur d'authent
            }
        }
    }
}
