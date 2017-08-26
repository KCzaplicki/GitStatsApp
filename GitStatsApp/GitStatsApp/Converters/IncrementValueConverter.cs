using System;
using System.Globalization;
using Xamarin.Forms;

namespace GitStatsApp
{
    public class IncrementValueConverter : IValueConverter
    {
        private const string percentageValueFormat = "{0:+0.00;-0.00;0.00}%";
        private const string numberValueFormat = "{0:+0;-0;0}";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                if (doubleValue == double.MinValue)
                {
                    return string.Empty;
                }

                return string.Format(percentageValueFormat, doubleValue != 0 ? doubleValue * 100 : doubleValue);
            }
            else if (value is int intValue)
            {
                return intValue != int.MinValue ? string.Format(numberValueFormat, intValue) : string.Empty;
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
