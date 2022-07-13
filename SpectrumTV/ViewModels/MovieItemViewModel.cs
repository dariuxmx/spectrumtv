using System.Windows.Input;
using SpectrumTV.Models.Movie;

namespace SpectrumTV.ViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public Movie Movie { get; private set; }

        #region Binding properties
        public string MovieTitle { get; private set; }
        public string MovieCover { get; private set; }
        #endregion

        #region Commands
        public ICommand TapCommand { get; private set; }
        #endregion

        public MovieItemViewModel(Movie movie, ICommand tapCommand)
        {
            Movie = movie;
            MovieTitle = movie.Title;
            MovieCover = movie.BackdropPath;

            TapCommand = tapCommand;
        }
    }
}
