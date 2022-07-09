using System;
using System.Threading;
using System.Threading.Tasks;
using SpectrumTV.Models.Movie;

namespace SpectrumTV.Services.Services
{
    public interface IMovieService
    {
        Task<Movie> GetMoviesTopRated(int pageNumber = 1);
    }
}
