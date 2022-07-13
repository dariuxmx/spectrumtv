using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;

namespace SpectrumTV.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Interface methods to call rated and upcoming movies
        /// </summary>
        
        Task<SearchResultsResponse<Movie>> GetTopRatedMovies(string language="en", int pageNumber = 1);
        Task<SearchResultsResponse<Movie>> GetUpcomingMovies(string language = "en", int pageNumber = 1);
    }
}
