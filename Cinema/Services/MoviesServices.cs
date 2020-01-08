using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Exceptions;
using Cinema.Models;

namespace Cinema.Services
{
    public class MoviesServices : IMoviesServices
    {
        private readonly ICineRepository cineRepository;
        private readonly IMapper mapper;
        public MoviesServices(ICineRepository cineRepository, IMapper mapper)
        {
            this.cineRepository = cineRepository;
            this.mapper = mapper;
        }
        public async Task<Movie> CreateMovieAsync(int idActor, Movie movie)
        {
           
            if (movie.ActorId != idActor)
                throw new BadRequestEx($"idActor:{idActor} in URL must be equal to Body:{movie.ActorId}");
            movie.ActorId = idActor;
            var actorEntity = await validateActor(idActor);
            var movieEntity = mapper.Map<MovieEntity>(movie);
            cineRepository.CreateMovie(movieEntity);
            
            if (await cineRepository.SaveChangesAsync())
            {
                return mapper.Map<Movie>(movieEntity);
            }
            throw new Exception("There were an error with the DB");
        }

        public async Task<bool> DeleteMovieAsync(int idActor, int idMovie)
        {
            /*if (idMovie == 0)
                throw new BadRequestEx($"idMovie URL es required to delete a movie");*/
           // var movieDelete = GetMovie(idActor, idMovie);
           
            await validateActor(idActor);
            await cineRepository.DeleteMovieAsync(idMovie);
            if (await cineRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<Movie> GetMovieAsync(int idActor, int idMovie)
        {
            await ValidateAuthorAndBook(idActor,idMovie);
            var movieEntity = await cineRepository.GetMovieAsync(idMovie);

            if (movieEntity == null)
                throw new NotFoundEx($"There isn't a movie with id:{idMovie}");
            //if (idActor != movieEntity.Actor.Id)
            //    throw new BadRequestEx($"Movie: {idMovie} doesn't belong to Actor: {idActor}");

            return mapper.Map<Movie>(movieEntity);
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(int idActor)
        {
            await validateActor(idActor);
            //var movies = cineRepository.GetMovies();
            //return movies.Where(m => m.ActorId == idActor);
            var moviesEntities = cineRepository.GetMovies();
            var moviesE = moviesEntities.Where(m=> m.Actor.Id == idActor);
            var movies = mapper.Map<IEnumerable<Movie>>(moviesE);
            return movies;
        }

        public async Task<Movie> UpdateMovieAsync(int idActor, int idMovie, Movie movie)
        {
            //GetMovie(idActor, idMovie);
            if (movie.Id != null && movie.Id != idMovie)
            {
                throw new InvalidOperationException("book URL id and book body id should be the same");
            }
            await ValidateAuthorAndBook(idActor, idMovie);
            movie.Id = idMovie;             //Para no enviar bookId en el Body
            //if (movie.ActorId == 0)
            //    movie.ActorId = idActor;
            var movieEntity = mapper.Map<MovieEntity>(movie);
            cineRepository.UpdateMovie(movieEntity);
            if (await cineRepository.SaveChangesAsync())
            {
                return mapper.Map<Movie>(movieEntity);
            }
            throw new Exception("There were an error with the DB");
        }
        private async Task<ActorEntity> validateActor(int id)
        {
            var actorFound = await cineRepository.GetActorAsync(id);   //showMovies = false
            if (actorFound == null)
                throw new NotFoundEx($"There isn't an actor with id: {id}");
            return actorFound;
        }

        private async Task<bool> ValidateAuthorAndBook(int actorId, int bookId)
        {

            var actor = await cineRepository.GetActorAsync(actorId);
            if (actor == null)
            {
                throw new NotFoundEx($"cannot found author with id {actorId}");
            }

            var movie = await cineRepository.GetMovieAsync(bookId, true);
            if (movie == null || movie.Actor.Id != actorId)
            {
                throw new NotFoundEx($"Book not found with id {bookId} for author {actorId}");
            }

            return true;
        }


    }
}
