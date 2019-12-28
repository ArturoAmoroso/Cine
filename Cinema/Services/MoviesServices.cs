using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Exceptions;
using Cinema.Models;

namespace Cinema.Services
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ICineRepository cineRepository;
        public MoviesServices(ICineRepository cineRepository)
        {
            this.cineRepository = cineRepository;
        }
        public Movie CreateMovie(int idActor, Movie movie)
        {
            validateActor(idActor);
            if (movie.ActorId == 0)
                movie.ActorId = idActor;
            if (movie.ActorId != idActor)
                throw new BadRequestEx($"idActor:{idActor} in URL must be equal to Body:{movie.ActorId}");
            return cineRepository.CreateMovie(movie);
        }

        public bool DeleteMovie(int idActor, int idMovie)
        {
            /*if (idMovie == 0)
                throw new BadRequestEx($"idMovie URL es required to delete a movie");*/
            var movieDelete = GetMovie(idActor, idMovie);
            return cineRepository.DeleteMovie(movieDelete);
        }

        public Movie GetMovie(int idActor, int idMovie)
        {
            validateActor(idActor);
            var movieRepo = cineRepository.GetMovie(idMovie);
            if (movieRepo == null)
                throw new NotFoundEx($"There isn't a movie with id:{idMovie}");
            if (idActor != movieRepo.ActorId)
                throw new BadRequestEx($"Movie: {idMovie} doesn't belong to Actor: {idActor}");
            return movieRepo;
        }

        public IEnumerable<Movie> GetMovies(int idActor)
        {
            validateActor(idActor);
            var movies = cineRepository.GetMovies();
            return movies.Where(m => m.ActorId == idActor);
        }

        public Movie UpdateMovie(int idActor, int idMovie, Movie movie)
        {
            GetMovie(idActor, idMovie);
            movie.Id = idMovie;             //Para no enviar bookId en el Body
            if (movie.ActorId == 0)
                movie.ActorId = idActor;
            return cineRepository.UpdateMovie(movie);
        }
        private Actor validateActor(int id)
        {
            var actorFound = cineRepository.GetActor(id);   //showMovies = false
            if (actorFound == null)
                throw new NotFoundEx($"There isn't an actor with id: {id}");
            return actorFound;
        }
    }
}
