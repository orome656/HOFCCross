using HOFCCross.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HOFCCross.Converters
{
    public class TypeVoteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as TypeVote?;
            if(type.HasValue)
            {
                switch(type.Value)
                {
                    case TypeVote.DOWN:
                        return "Flop";
                    case TypeVote.TOP:
                        return "Top";
                    default:
                        return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = value as string;
            if(!string.IsNullOrWhiteSpace(type))
            {
                switch(type)
                {
                    case "Flop":
                        return TypeVote.DOWN;
                    case "Top":
                        return TypeVote.TOP;
                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
