using System;
using System.Globalization;
using Xamarin.Forms;

namespace SpectrumTV.Converters
{
    /// <summary>
    /// Use this converter to change the use the uppercase format from a text label:
    /// Example:
    /// <Label
    ///     Text="{Binding MovieTitle, Converter={StaticResource StringToUppercaseConverter}}"/>
    /// </summary>

    public class StringToUppercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.ToUpper();
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
