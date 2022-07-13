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
    public class MovieDetailsPageModel : BasePageModel
    {
        #region Binding properties
        private string _movieTitle;
        public string MovieTitle
        {
            get => _movieTitle;
            set => SetProperty(ref _movieTitle, value);
        }

        private string _movieOverview;
        public string MovieOverview
        {
            get => _movieOverview;
            set => SetProperty(ref _movieOverview, value);
        }

        private string _coverPath;
        public string CoverPath
        {
            get => _coverPath;
            set => SetProperty(ref _coverPath, value);
        }
        #endregion

        public MovieItemViewModel CurrentMovie { get; set; }
        public AsyncCommand CloseCommand { get; private set; }

        public MovieDetailsPageModel(
            NavigationContext navigationContext)
            : base(navigationContext)
        {
            CloseCommand = new AsyncCommand(Perform_Close);
        }

        public override async void OnAppearing(object sender, EventArgs e)
        {
            base.OnAppearing(sender, e);

            MovieTitle = CurrentMovie.Movie.Title;
            MovieOverview = CurrentMovie.Movie.Overview;
            CoverPath = CurrentMovie.Movie.PosterPath;
        }

        private Task Perform_Close(CancellationToken cancellationToken)
        {
            // Dismiss modal
            return NavigationContext.PopModalAsync();
        }


    }

}
