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

        private List<MovieItemViewModel> _resultTopMoviesRated;
        public List<MovieItemViewModel> ResultsTopMoviesRated
        {
            get => _resultTopMoviesRated;
            set => SetProperty(ref _resultTopMoviesRated, value);
        }

        public AsyncCommand ShowDetailsCommand { get; private set; }

        public HomePageModel(
            NavigationContext navigationContext,
            IMovieService movieService
            )
            : base(navigationContext)
        {
            _movieService = movieService;

            ShowDetailsCommand = new AsyncCommand(Perform_View_Movie_Details);
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
                ResultsTopMoviesRated = topRatedMovies.Select(topMovie => new MovieItemViewModel(topMovie, new AsyncCommand(Perform_View_Movie_Details))).ToList();
                IsBusy = false;
                
            }            
        }

        private async Task Perform_View_Movie_Details(object sender, CancellationToken cancellationToken)
        {
            await NavigationContext.PushModalAsync<MovieDetailsPageModel>(pageModel =>
            {
                pageModel.CurrentMovie = (MovieItemViewModel)sender;
            });
        }
    }
}
