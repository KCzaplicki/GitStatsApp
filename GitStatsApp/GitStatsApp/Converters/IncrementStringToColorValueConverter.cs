using System;
using System.Globalization;
using Xamarin.Forms;

namespace GitStatsApp
{
    public class IncrementStringToColorValueConverter : IValueConverter
    {
        private readonly Color positiveValueColor = Color.FromHex("#4CAF50");
        private readonly Color negativeValueColor = Color.FromHex("#F44336");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueString = value.ToString();

            return valueString.StartsWith("-") ? negativeValueColor : positiveValueColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
