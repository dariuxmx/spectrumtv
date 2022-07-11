using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Services;

namespace SpectrumTV.PageModels
{
    public class HomePageModel : BasePageModel
    {
        //private Movie _movieModel;
        private readonly IMovieService _movieService;
        private ObservableCollection<Movie> _topRatedMovieCollection;
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
            _topRatedMovieCollection = new ObservableCollection<Movie>((System.Collections.Generic.IEnumerable<Movie>)movieList.Results.Take(10));

            IsBusy = false;
        }
    }
}
