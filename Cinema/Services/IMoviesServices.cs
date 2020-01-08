using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Services
{
    public interface IMoviesServices
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(int idActor);
        Task<Movie> GetMovieAsync(int idActor, int idMovie);
        Task<Movie> CreateMovieAsync(int idActor, Movie movie);
        Task<bool> DeleteMovieAsync(int idActor, int idMovie);
        Task<Movie> UpdateMovieAsync(int idActor, int idMovie, Movie movie);
    }
}
