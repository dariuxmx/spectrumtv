using System.Threading.Tasks;
using SpectrumTV.Models.Movie;
using SpectrumTV.Models.Responses;

namespace SpectrumTV.Services
{
    public interface IMovieService
    {
        Task<SearchResultsResponse<Movie>> GetMoviesTopRated(string language = "en", int pageNumber = 1);
        Task<Movie> FindMovieWithId(int movieId, string language = "en");
    }
}
