using System;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;
using SpectrumTV.Services.ApiService;
using SpectrumTV.Services.Services;

namespace SpectrumTV.Services.Implementation
{
    public class MovieService : IMovieService
    {

        private readonly IApiService _apiService;

        public MovieService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<SearchResultsResponse<Movie>> GetMoviesTopRated(int pageNumber = 1)
        {
            string uri = $"{AppConfig.BaseUrl}movie/top_rated?api_key={AppConfig.ApiKey}";

            SearchResultsResponse<Movie> response = await _apiService.GetAsync<SearchResultsResponse<Movie>>(uri);

            return response;
        }

        public Task<Movie> FindMovieWithId(int movieId)
        {
            throw new NotImplementedException();
        }

    }
}
