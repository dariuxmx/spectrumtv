using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Services;
using SpectrumTV.ViewModels;

namespace SpectrumTV.PageModels
{
    public class HomePageModel : BasePageModel
    {
        private readonly IMovieService _movieService;

        #region Binding properties
        private List<MovieItemViewModel> _resultTopMoviesRated;
        public List<MovieItemViewModel> ResultsTopMoviesRated
        {
            get => _resultTopMoviesRated;
            set => SetProperty(ref _resultTopMoviesRated, value);
        }

        private List<MovieItemViewModel> _resultsUpcomingMovies;
        public List<MovieItemViewModel> ResultsUpcomingMovies
        {
            get => _resultsUpcomingMovies;
            set => SetProperty(ref _resultsUpcomingMovies, value);
        }

        private string _movieCoverTrend;
        public string MovieCoverTrend
        {
            get => _movieCoverTrend;
            set => SetProperty(ref _movieCoverTrend, value);
        }
        #endregion

        #region Commands
        public AsyncCommand ShowDetailsCommand { get; private set; }
        public AsyncCommand SearchCommand { get; private set; }
        #endregion

        public HomePageModel(
            NavigationContext navigationContext,
            IMovieService movieService
            )
            : base(navigationContext)
        {
            // Init the movie service
            _movieService = movieService;

            // Init command
            ShowDetailsCommand = new AsyncCommand(Perform_View_Movie_Details);
            SearchCommand = new AsyncCommand(Perform_Open_Search_Screen);

            // Show the activity loader
            IsBusy = true;
        }

        public override async void OnAppearing(object sender, EventArgs e)
        {
            base.OnAppearing(sender, e);

            // Loading a static movie cover
            MovieCoverTrend = "https://image.tmdb.org/t/p/w1280/9Gtg2DzBhmYamXBS1hKAhiwbBKS.jpg";

            // Api calls
            await GetTopRatedList();
            await GetUpcomingMoviesList();
        }

        #region Api calls
        private async Task GetTopRatedList()
        {
            // Get movies from service
            var movieList = await _movieService.GetTopRatedMovies();

            // Storing the movie list results from api to a list of movie objects 
            List<Movie> topRatedMovies = movieList.Results.ToList();

            if (topRatedMovies?.Count > 0)
            {
                // Storing as viewModelItems the list of movie objects
                ResultsTopMoviesRated = topRatedMovies.Select(topMovie => new MovieItemViewModel(topMovie, new AsyncCommand(Perform_View_Movie_Details))).ToList();

                // Hide the activity loader
                IsBusy = false;
            }            
        }

        private async Task GetUpcomingMoviesList()
        {
            // Get movies from service
            var movieList = await _movieService.GetUpcomingMovies();

            // Storing the movie list results from api to a list of movie objects 
            List<Movie> topRatedMovies = movieList.Results.ToList();

            if (topRatedMovies?.Count > 0)
            {
                // Storing as viewModelItems the list of movie objects
                ResultsUpcomingMovies = topRatedMovies.Select(topMovie => new MovieItemViewModel(topMovie, new AsyncCommand(Perform_View_Movie_Details))).ToList();

                // Hide the activity loader
                IsBusy = false;
            }
        }
        #endregion

        #region Methods for commands
        private async Task Perform_View_Movie_Details(object sender, CancellationToken cancellationToken)
        {
            // Show the modal screen
            await NavigationContext.PushModalAsync<MovieDetailsPageModel>(pageModel =>
            {
                // Sending the movie item view model as parameter
                pageModel.CurrentMovie = (MovieItemViewModel)sender;
            });
        }

        private async Task Perform_Open_Search_Screen(object sender, CancellationToken cancellationToken)
        {
            // Show the modal screen
            await NavigationContext.PushModalAsync<SearchResultsPageModel>();
        }
        #endregion
    }
}