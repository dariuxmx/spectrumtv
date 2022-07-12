using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;
using SpectrumTV.Services;
using SpectrumTV.ViewModels;

namespace SpectrumTV.PageModels
{
    public class HomePageModel : BasePageModel
    {
        //private Movie _movieModel;
        private readonly IMovieService _movieService;

        private List<MovieItemViewModel> _resultTopMoviesRated;
        public List<MovieItemViewModel> ResultsTopMoviesRated
        {
            get => _resultTopMoviesRated;
            set => SetProperty(ref _resultTopMoviesRated, value);
        }

        //private ObservableCollection<Movie> _topRatedMovieCollection;
        //public ObservableCollection<Movie> TopRatedMovieCollection
        //{
        //    get { return _topRatedMovieCollection; }
        //    set
        //    {
        //        _topRatedMovieCollection = value;
        //        OnPropertyChanged();
        //        //SetProperty(ref _topRatedMovieCollection, value);
        //    }
        //}

        public HomePageModel(
            NavigationContext navigationContext,
            IMovieService movieService
            )
            : base(navigationContext)
        {
            _movieService = movieService;

            IsBusy = true;
        }


        public override async void OnAppearing(object sender, EventArgs e)
        {
            base.OnAppearing(sender, e);
            await GetTopRatedList();
        }

        private async Task GetTopRatedList()
        {
            var movieList = await _movieService.GetMoviesTopRated();

            List<Movie> topRatedMovies = movieList.Results.ToList();

            if (topRatedMovies?.Count > 0)
            {
                ResultsTopMoviesRated = topRatedMovies.Select(topMovie => new MovieItemViewModel(topMovie)).ToList();
                IsBusy = false;
                //Movie movieItem = new Movie();
                //foreach (var movie in movieList.Results)
                //{
                //    movieItem.OriginalTitle = movie.OriginalTitle;
                //    Console.WriteLine("MOVIE ====> {0}", movieItem.OriginalTitle);

                //}

                //Movie movieItem = new Movie();
                //foreach (var movie in movieList.Results)
                //{
                //    movieItem.OriginalTitle = movie.OriginalTitle;
                //    Console.WriteLine("MOVIE ====> {0}", movieItem.OriginalTitle);

                //}


                //ResultTopMoviesRated = new ObservableCollection<Movie>(movieList.Results.Take(10));
            }
            
            //_topRatedMovieCollection = new ObservableCollection<Movie>(movieList.Results.Take(10));

            
        }
    }
}
