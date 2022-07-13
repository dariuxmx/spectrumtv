using System;
using System.Globalization;
using SpectrumTV.Models;
using Xamarin.Forms;

namespace SpectrumTV.Converters
{
    /// <summary>
    /// Use this converter to build the complete url for cover movies:
    /// Example: https://image.tmdb.org/t/p/w1280/9Gtg2DzBhmYamXBS1hKAhiwbBKS.jpg
    /// </summary>

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
