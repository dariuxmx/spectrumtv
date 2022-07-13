using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SpectrumTV.Models.Movie;
using Xamarin.Forms;

namespace SpectrumTV.ViewModels
{
    public class MovieItemViewModel : BaseViewModel
    {
        public Movie Movie { get; private set; }

        public string MovieTitle { get; private set; }
        public string MovieCover { get; private set; }

        public ICommand TapCommand { get; private set; }
        public Command SelectionChangedCommand { get; set; }

        public MovieItemViewModel(Movie movie, ICommand tapCommand)
        {
            Movie = movie;
            MovieTitle = movie.Title;
            MovieCover = movie.BackdropPath;

            TapCommand = tapCommand;
            //TapCommand = new Command(Perform_Tap);
        }

        //private async void Perform_Tap(object obj)
        //{
        //    Console.WriteLine("=== TAP MOVIE ITEM === {0}", obj);   
        //}
    }
}
