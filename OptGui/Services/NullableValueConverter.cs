using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OptGui.Services
{
    public class NullableValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }
            else
            {
                foreach (var c in value.ToString())
                {
                    if (char.IsSymbol(c) && !char.IsDigit(c))
                    {
                        return null;
                    }
                }
                return value;
            }
        }
    }
}
