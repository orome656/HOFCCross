using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HOFCCross.Behaviors
{
    public class ToolbarItemsBehavior: BehaviorBase<ContentPage>
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable<ToolbarItem>), typeof(ToolbarItemsBehavior), null, propertyChanged: OnItemsSourceChanged);
                
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var contentPage = (bindable as ToolbarItemsBehavior).AssociatedObject;
            var equipes = newValue as IEnumerable<ToolbarItem>;
            contentPage.ToolbarItems.Clear();
            foreach(ToolbarItem equipe in equipes)
            {
                contentPage.ToolbarItems.Add(equipe);
            }
        }
    }
}
