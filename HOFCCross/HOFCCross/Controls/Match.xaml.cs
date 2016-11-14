using HOFCCross.Constantes;
using HOFCCross.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.Controls
{
    public partial class Match : ViewCell
    {
        private static IsHOFCConverter Converter = new IsHOFCConverter(); 

        public static readonly BindableProperty Equipe1Property =
               BindableProperty.Create(nameof(Equipe1), typeof(string), typeof(Match), null, BindingMode.OneWay);

        public string Equipe1
        {
            get { return (string)GetValue(Equipe1Property); }
            set { SetValue(Equipe1Property, value); }
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(Match), null, BindingMode.OneWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty Equipe2Property =
            BindableProperty.Create(nameof(Equipe2), typeof(string), typeof(Match), null, BindingMode.OneWay);

        public string Equipe2
        {
            get { return (string)GetValue(Equipe2Property); }
            set { SetValue(Equipe2Property, value); }
        }


        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(Match), null, BindingMode.OneWay);

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(Match), null, BindingMode.OneWay);

        public DateTime? Date
        {
            get { return (DateTime?)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty InfosCommandProperty =
            BindableProperty.Create(nameof(Date), typeof(Command), typeof(Match), null, BindingMode.OneWay);

        public Command InfosCommand
        {
            get { return (Command)GetValue(InfosCommandProperty); }
            set { SetValue(InfosCommandProperty, value); }
        }

        public static readonly BindableProperty NavCommandProperty =
            BindableProperty.Create(nameof(Date), typeof(Command), typeof(Match), null, BindingMode.OneWay);

        public Command NavCommand
        {
            get { return (Command)GetValue(NavCommandProperty); }
            set { SetValue(NavCommandProperty, value); }
        }

        public static readonly BindableProperty InfosIdProperty =
            BindableProperty.Create(nameof(InfosId), typeof(string), typeof(Match), null, BindingMode.OneWay);

        public string InfosId
        {
            get { return (string)GetValue(InfosIdProperty); }
            set { SetValue(InfosIdProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if(BindingContext != null)
            {
                Equipe1Label.Text = Equipe1;
                if((bool)Converter.Convert(Equipe1, null, null, null))
                {
                    Equipe1Label.TextColor = Color.FromHex(AppConstantes.PRIMARY_COLOR_HEX);
                    Equipe2Label.TextColor = Color.Black;
                }
                else if ((bool)Converter.Convert(Equipe2, null, null, null))
                {
                    Equipe1Label.TextColor = Color.Black;
                    Equipe2Label.TextColor = Color.FromHex(AppConstantes.PRIMARY_COLOR_HEX);
                }

                Equipe2Label.Text = Equipe2;
                Equipe1Image.IsVisible = (bool)Converter.Convert(Equipe1, null, null, null);
                Equipe2Image.IsVisible = (bool)Converter.Convert(Equipe2, null, null, null);
                if(Date.HasValue)
                    DateLabel.Text = Date.Value.ToString("dd MMMM yyyy HH:mm");

                MessageLabel.Text = Message;
                TitleLabel.Text = Title;
                TapGestureRecognizer tapGesture = new TapGestureRecognizer();
                tapGesture.Command = InfosCommand;
                tapGesture.CommandParameter = InfosId;
                InfosButton.GestureRecognizers.Add(tapGesture);

                TapGestureRecognizer tapNav = new TapGestureRecognizer();
                tapNav.Command = NavCommand;
                tapNav.CommandParameter = InfosId;
                NaviButton.GestureRecognizers.Add(tapNav);
            }
        }

        public Match()
        {
            InitializeComponent();
        }
    }
}
