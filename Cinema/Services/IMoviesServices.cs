using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Services
{
    public interface IMoviesServices
    {
        IEnumerable<Movie> GetMovies(int idActor);
        Movie GetMovie(int idActor, int idMovie);
        Movie CreateMovie(int idActor, Movie movie);
        bool DeleteMovie(int idActor, int idMovie);
        Movie UpdateMovie(int idActor, int idMovie, Movie movie);
    }
}
