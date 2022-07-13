using System;
using System.Globalization;
using SpectrumTV.Models;
using Xamarin.Forms;

namespace SpectrumTV.Converters
{
    public class MovieCoverConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Return the complete url in each case
            if (parameter != null && parameter.Equals("Backdrop"))
            {
                return string.Format("{0}{1}", AppConfig.BackdropImageUrl, value);
            }
            else
            {
                return string.Format("{0}{1}", AppConfig.CoverImageUrl, value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
