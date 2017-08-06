using System;
using System.Globalization;
using Xamarin.Forms;

namespace GitStatsApp
{
    public class DoubleToPercentageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percentageValue = (double)value * 100;
            return $"{percentageValue}%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueWithoutPercentageSign = ((string)value).TrimEnd('%');
            return double.Parse(valueWithoutPercentageSign) / 100;
        }
    }
}
