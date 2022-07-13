using System;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;
using SpectrumTV.Services.ApiService;

namespace SpectrumTV.Services.Implementation
{
    public class MovieService : IMovieService
    {
        /// <summary>
        /// The MovieDB org api calls:
        /// Top rated and upcoming movies
        /// </summary>
        /// 
        private readonly IApiService _apiService;

        public MovieService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<SearchResultsResponse<Movie>> GetTopRatedMovies(string language="en", int pageNumber = 1)
        {
            // Build the complete url with parameters coming from AppConfig file
            string uri = $"{AppConfig.BaseUrl}movie/top_rated?api_key={AppConfig.ApiKey}&language={language}";

            // Store the results 
            SearchResultsResponse<Movie> response = await _apiService.GetAsync<SearchResultsResponse<Movie>>(uri);

            // Return the results
            return response;
        }

        public async Task<SearchResultsResponse<Movie>> GetUpcomingMovies(string language="en", int pageNumber = 1)
        {
            // Build the complete url with parameters coming from AppConfig file
            string uri = $"{AppConfig.BaseUrl}movie/upcoming?api_key={AppConfig.ApiKey}&language={language}";

            // Store the results 
            SearchResultsResponse<Movie> response = await _apiService.GetAsync<SearchResultsResponse<Movie>>(uri);

            // Return the results
            return response;
        }

        public async Task<SearchResultsResponse<Movie>> SearchMovie(string movieName, string language = "en")
        {
            // Build the complete url with parameters coming from AppConfig file and the movie name type by the user
            string uri = $"{AppConfig.BaseUrl}search/movie?api_key={AppConfig.ApiKey}&query={movieName}&language={language}";

            // Store the results 
            SearchResultsResponse<Movie> response = await _apiService.GetAsync<SearchResultsResponse<Movie>>(uri);

            // Return the results
            return response;
        }
    }
}
