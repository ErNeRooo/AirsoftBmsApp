using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirsoftBmsApp.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool canBeParsed = bool.TryParse(value.ToString(), out bool boolValue);

            if(!canBeParsed) return false;

            return !boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool canBeParsed = bool.TryParse(value.ToString(), out bool boolValue);

            if (!canBeParsed) return false;

            return !boolValue;
        }
    }
}
