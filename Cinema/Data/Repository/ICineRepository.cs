using Cinema.Data.Entities;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data
{
    public interface ICineRepository
    {
        Task<bool> SaveChangesAsync();

        //Actors 
        Task<IEnumerable<ActorEntity>> GetActorsAsync(bool showMovies=false, string orderBy = "id");
        Task<ActorEntity> GetActorAsync(int id, bool showMovies = false);
        void CreateActor(ActorEntity actor);
        Task DeleteActorAsync(int id);
        void UpdateActor(ActorEntity actor);


        //Movies
        IEnumerable<MovieEntity> GetMovies();
        Task<MovieEntity> GetMovieAsync(int idMovie, bool showActor=false);
        void CreateMovie(MovieEntity movie);
        Task DeleteMovieAsync(int id);
        void UpdateMovie(MovieEntity movie);

        //Winners
        IEnumerable<WinnerEntity> GetWinners();
        void CreateWinner(WinnerEntity winner);
        Task DeleteWinnerAsync(int id);
    }
}
