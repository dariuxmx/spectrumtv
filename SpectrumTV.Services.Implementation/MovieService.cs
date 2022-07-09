using System;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models;
using SpectrumTV.Models.Movie;
using SpectrumTV.Services.Services;

namespace SpectrumTV.Services.Implementation
{
    public class MovieService : IMovieService
    {
        public MovieService()
        {
        }

        public Task<Movie> GetMoviesTopRated(int pageNumber = 1)
        {
            string uri = $"{AppConfig.BaseUrl}movie/top_rated?api_key={AppConfig.ApiKey}";

            var response = await 
        }
    }
}
