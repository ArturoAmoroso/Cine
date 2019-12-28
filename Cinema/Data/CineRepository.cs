using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Models;

namespace Cinema.Data
{
    public class CineRepository : ICineRepository
    {
        List<Actor> actors;
        List<Movie> movies;
        public CineRepository()
        {
            actors = new List<Actor>() {
                new Actor() {
                    Id = 111,
                    Name = "Leonardo",
                    Lastname = "DiCaprio",
                    Age = 56
                },
                new Actor(){
                    Id = 222,
                    Name = "Robert",
                    Lastname = "DeNiro",
                    Age = 75
                }
            };
            movies = new List<Movie>() {
                new Movie(){
                    Id = 1,
                    Name = "Titanic",
                    Duration = 3,
                    Genre = "Romatic",
                    ActorId = 111
                },
                new Movie(){
                    Id = 2,
                    Name = "Taxi Driver",
                    Duration = 2,
                    Genre = "Drama",
                    ActorId = 222
                }
            };
        }
        public Actor CreateActor(Actor actor)
        {
            var lastActor = actors.OrderByDescending(a => a.Id).FirstOrDefault();
            int nextId = lastActor == null ? 1 : lastActor.Id + 1;
            actor.Id = nextId;
            actors.Add(actor);
            return actor;
        }

        public Movie CreateMovie(Movie movie)
        {
            var lastMovie = movies.OrderByDescending(m => m.Id).FirstOrDefault();
            var nextId = lastMovie == null ? 1 : lastMovie.Id + 1;
            movie.Id = nextId;
            movies.Add(movie);
            return movie;
        }

        public bool DeleteActor(Actor actor)
        {
            return actors.Remove(actor);
        }

        public bool DeleteMovie(Movie movie)
        {
            return movies.Remove(movie);
        }

        public Actor GetActor(int id, bool showMovies)
        {
            var actor = actors.SingleOrDefault(a => a.Id == id);
            if (showMovies == true)
                actor.Movies = movies.Where(m => m.ActorId == id);
            else
                actor.Movies = null;
            return actor;
        }

        public IEnumerable<Actor> GetActors()
        {
            return actors;
        }

        public Movie GetMovie(int idMovie)
        {
            var movieFound = movies.SingleOrDefault(m => m.Id == idMovie);
            return movieFound;
        }

        public IEnumerable<Movie> GetMovies()
        {
            return movies;
        }

        public Actor UpdateActor(Actor actor)
        {
            var actorFound = actors.SingleOrDefault(a => a.Id == actor.Id);
            actorFound.Name = actor.Name;
            actorFound.Lastname = actor.Lastname;
            actorFound.Age= actor.Age;
            return actorFound;
        }

        public Movie UpdateMovie(Movie movie)
        {
            var movieFound = movies.SingleOrDefault(m => m.Id == movie.Id);
            movieFound.Name = movie.Name;
            movieFound.Duration = movie.Duration;
            movieFound.Genre = movie.Genre;
            movieFound.ActorId = movie.ActorId;
            return movieFound;
        }
    }
}
