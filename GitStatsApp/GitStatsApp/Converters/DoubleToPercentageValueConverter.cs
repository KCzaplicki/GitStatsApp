using System;
using System.Globalization;
using Xamarin.Forms;

namespace GitStatsApp
{
    public class DoubleToPercentageValueConverter : IValueConverter
    {
        private const string percentageStringFormat = "{0:0.00}%";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percentageValue = (double)value * 100;
            return string.Format(percentageStringFormat, percentageValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueWithoutPercentageSign = ((string)value).TrimEnd('%');
            return double.Parse(valueWithoutPercentageSign) / 100;
        }
    }
}
