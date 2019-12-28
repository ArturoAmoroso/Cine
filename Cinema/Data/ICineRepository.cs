using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public interface ICineRepository
    {
        IEnumerable<Actor> GetActors();
        Actor GetActor(int id, bool showMovies = false);
        Actor CreateActor(Actor actor);
        bool DeleteActor(Actor actor);
        Actor UpdateActor(Actor actor);

        IEnumerable<Movie> GetMovies();
        Movie GetMovie(int idMovie);
        Movie CreateMovie(Movie movie);
        bool DeleteMovie(Movie movie);
        Movie UpdateMovie(Movie movie);
    }
}
