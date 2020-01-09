using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data.Entities;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data
{
    public class CineRepository : ICineRepository
    {
        List<Actor> actors;
        List<Movie> movies;
        List<Winner> winners;

        private CineDbContext cineDbContext;
        public CineRepository(CineDbContext cineDbContext)
        {
            this.cineDbContext = cineDbContext;
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
            winners = new List<Winner>() {
                new Winner()
                {
                    Id = 1,
                    Name = "Will",
                    Lastname = "Smith",
                    movie = "Geminis",
                    year = 2019
                    
                },
                new Winner()
                {
                    Id = 2,
                    Name = "Roberts",
                    Lastname = "DeNiro",
                    movie = "El irlandes",
                    year = 2019
                }
            };
        }
        public void CreateActor(ActorEntity actor)
        {
            //var lastActor = actors.OrderByDescending(a => a.Id).FirstOrDefault();
            //int nextId = lastActor == null ? 1 : lastActor.Id + 1;
            //actor.Id = nextId;
            //actors.Add(actor);
            //return actor;
            cineDbContext.Actors.Add(actor);
        }



        public void CreateMovie(MovieEntity movie)
        {
            //var lastMovie = movies.OrderByDescending(m => m.Id).FirstOrDefault();
            //var nextId = lastMovie == null ? 1 : lastMovie.Id + 1;
            //movie.Id = nextId;
            //movies.Add(movie);
            //return movie;
            cineDbContext.Entry(movie.Actor).State = EntityState.Unchanged;
            cineDbContext.Movies.Add(movie);
        }

        public void CreateWinner(WinnerEntity winner)
        {
            cineDbContext.Winners.Add(winner);
        }

        public async Task DeleteActorAsync(int id)
        {
            //return actors.Remove(actor);
            var actorToDelete = await cineDbContext.Actors.SingleAsync(a => a.Id == id);
            cineDbContext.Actors.Remove(actorToDelete);
        }


        public async Task DeleteMovieAsync(int id)
        {
            //return movies.Remove(movie);
            var movieToDelete = await cineDbContext.Movies.SingleAsync(a => a.Id == id);
            cineDbContext.Movies.Remove(movieToDelete);
        }

        public async Task<ActorEntity> GetActorAsync(int id, bool showMovies)
        {
            //var actor = actors.SingleOrDefault(a => a.Id == id);
            //if (showMovies == true)
            //    actor.Movies = movies.Where(m => m.ActorId == id);
            //else
            //    actor.Movies = null;
            //return actor;
            IQueryable<ActorEntity> query = cineDbContext.Actors;
            if (showMovies)
            {
                query = query.Include(a => a.Movies);
            }
            query = query.AsNoTracking();
            return await query.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<ActorEntity>> GetActorsAsync(bool showMovies, string orderBy)
        {
            //foreach (var actor in actors)
            //{
            //    if (showMovies == true)
            //        actor.Movies = movies.Where(m => m.ActorId == actor.Id);
            //    else
            //        actor.Movies = null;
            //}
            //return actors;

            IQueryable<ActorEntity> query = cineDbContext.Actors;
            if (showMovies)
            {
                query = query.Include(a => a.Movies);
            }

            switch (orderBy)
            {
                case "name":
                    query = query.OrderBy(a => a.Name);
                    break;
                case "lastname":
                    query = query.OrderBy(a => a.Lastname);
                    break;
                case "age":
                    query = query.OrderBy(a => a.Age);
                    break;
                default:
                    query = query.OrderBy(a => a.Id);
                    break;
            }
            query = query.AsNoTracking();
            return await query.ToArrayAsync();
        }

        public async Task<MovieEntity> GetMovieAsync(int idMovie, bool showActor)
        {
            //var movieFound = movies.SingleOrDefault(m => m.Id == idMovie);
            //return movieFound;
            IQueryable<MovieEntity> query = cineDbContext.Movies;
            query = query.AsNoTracking();
            if (showActor)
            {
                query = query.Include(b => b.Actor);
            }
            query = query.AsNoTracking();
            var movieE = await query.SingleAsync(b => b.Id == idMovie);
            return movieE;
        }

        public IEnumerable<MovieEntity> GetMovies()
        {
            IQueryable<MovieEntity> query = cineDbContext.Movies;
            query = query.AsNoTracking();
            query = query.Include(b => b.Actor);
            query = query.AsNoTracking();
            return query;
        }

        public IEnumerable<WinnerEntity> GetWinners()
        {
            return cineDbContext.Winners;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await cineDbContext.SaveChangesAsync()) > 0;
        }

        public void UpdateActor(ActorEntity actor)
        {
            //var actorFound = actors.SingleOrDefault(a => a.Id == actor.Id);
            //actorFound.Name = actor.Name;
            //actorFound.Lastname = actor.Lastname;
            //actorFound.Age = actor.Age;
            //return actorFound;
            cineDbContext.Actors.Update(actor);
        }


        public void UpdateMovie(MovieEntity movie)
        {
            //var movieFound = movies.SingleOrDefault(m => m.Id == movie.Id);
            //movieFound.Name = movie.Name;
            //movieFound.Duration = movie.Duration;
            //movieFound.Genre = movie.Genre;
            //movieFound.ActorId = movie.ActorId;
            //return movieFound;

            cineDbContext.Entry(movie.Actor).State = EntityState.Unchanged;
            cineDbContext.Movies.Update(movie);
            
        }
    }
}
