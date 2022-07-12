using System;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using Xamarin.Forms;

namespace SpectrumTV.ViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public Movie Movie { get; private set; }

        public string MovieTitle { get; private set; }
        public string MovieCover { get; private set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (SetProperty(ref _isSelected, value))
                {
                    SelectionChangedCommand?.Execute(this);
                }
            }
        }

        public Command TapCommand { get; private set; }
        public Command SelectionChangedCommand { get; set; }

        public MovieItemViewModel(Movie movie)
        {
            Movie = movie;
            TapCommand = new Command(Perform_Tap);

            MovieTitle = movie.Title;
            MovieCover = movie.BackdropPath;
        }

        private async void Perform_Tap(object obj)
        {
            IsSelected = !IsSelected;
        }
    }
}
