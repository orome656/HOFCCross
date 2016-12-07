using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Converters
{
    public class ColorIfHOFCConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IsHOFCConverter converter = new Converters.IsHOFCConverter();
            return (bool)converter.Convert(value, targetType, parameter, culture) ? Color.FromHex("#08589D") : Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
