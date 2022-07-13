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
    public class SearchResultsPageModel : BasePageModel
    {
        private readonly IMovieService _movieService;

        #region Binding properties
        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private List<MovieItemViewModel> _searchResults;
        public List<MovieItemViewModel> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
        }

        #endregion

        #region Commands
        public AsyncCommand SearchCommand { get; private set; }
        public AsyncCommand CloseCommand { get; private set; }
        #endregion

        public SearchResultsPageModel(
            NavigationContext navigationContext,
            IMovieService movieService)
            : base(navigationContext)
        {
            // Init service
            _movieService = movieService;

            // Init command
            SearchCommand = new AsyncCommand(Perform_Search_Movies, TimeSpan.FromSeconds(30));
            CloseCommand = new AsyncCommand(Perform_Close);
        }

        #region Api calls
        private async Task SearchMoviesByName(string movieName)
        {
            // Get movies from service
            var movieList = await _movieService.SearchMovie(movieName);

            // Storing the movie list results from api to a list of movie objects 
            List<Movie> movieResults = movieList.Results.ToList();

            if (movieResults?.Count > 0)
            {
                // Storing as viewModelItems the list of movie objects
                SearchResults = movieResults.Select(topMovie => new MovieItemViewModel(topMovie, new AsyncCommand(Perform_View_Movie_Details))).ToList();

                // Hide the activity loader
                IsBusy = false;
            }
        }

        #endregion

        #region Methods for commands
        private async Task Perform_Search_Movies(object sender, CancellationToken cancellationToken)
        {
            // Send the name of the movie type by the user to the api service
            var movieList = await _movieService.SearchMovie(sender.ToString());

            // Storing the movie list results from api to a list of movie objects 
            List<Movie> movieResults = movieList.Results.ToList();

            if (movieResults?.Count > 0)
            {
                // Storing as viewModelItems the list of movie objects
                SearchResults = movieResults.Select(topMovie => new MovieItemViewModel(topMovie, new AsyncCommand(Perform_View_Movie_Details))).ToList();

                // Hide the activity loader
                IsBusy = false;
            }
        }

        private async Task Perform_View_Movie_Details(object sender, CancellationToken cancellationToken)
        {
            await NavigationContext.PushModalAsync<MovieDetailsPageModel>(pageModel =>
            {
                // Sending the movie item view model to next screen
                pageModel.CurrentMovie = (MovieItemViewModel)sender;
            });
        }

        private Task Perform_Close(object sender, CancellationToken cancellationToken)
        {
            // Dismiss modal
            return NavigationContext.PopModalAsync();
        }

        #endregion
    }
}
