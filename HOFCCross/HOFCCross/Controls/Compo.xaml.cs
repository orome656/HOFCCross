using HOFCCross.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HOFCCross.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Compo : ContentView
	{
        public static readonly BindableProperty TactiqueProperty =
               BindableProperty.Create(nameof(Tactique), typeof(Dictionary<string, int>), typeof(Compo), null, BindingMode.OneWay);

        public Dictionary<string, int> Tactique
        {
            get { return (Dictionary<string, int>)GetValue(TactiqueProperty); }
            set { SetValue(TactiqueProperty, value); }
        }

        public static readonly BindableProperty JoueursProperty =
               BindableProperty.Create(nameof(Joueurs), typeof(List<Joueur>), typeof(Compo), null, BindingMode.OneWay);

        public List<Joueur> Joueurs
        {
            get { return (List<Joueur>)GetValue(JoueursProperty); }
            set { SetValue(JoueursProperty, value); }
        }

        public Compo ()
		{
			InitializeComponent ();
		}
	}
}