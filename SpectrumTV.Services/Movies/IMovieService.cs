using System;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;

namespace SpectrumTV.Services.Services
{
    public interface IMovieService
    {
        Task<SearchResultsResponse<Movie>> GetMoviesTopRated(int pageNumber = 1);
        Task<Movie> FindMovieWithId(int movieId);
    }
}
