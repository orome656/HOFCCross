using HOFCCross.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace HOFCCross.View
{
    public partial class MasterPage : ContentPage
    {
        public List<MasterPageItem> Items { get; set; }
        public ListView ListView { get { return listView; } }

        public MasterPage()
        {
            InitializeComponent();
            
            Items = new List<MasterPageItem>();

            Items.Add(new MasterPageItem()
            {
                Title = "Actus",
                TargetType = typeof(ActuView),
                IconSource = "accueil_icon.png"
            });

            Items.Add(new MasterPageItem()
            {
                Title = "Calendrier",
                TargetType = typeof(CalendarView),
                IconSource = "calendar_icon.png"
            });

            listView.ItemsSource = Items;
        }
    }
}
