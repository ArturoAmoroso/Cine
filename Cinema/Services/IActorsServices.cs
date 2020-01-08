using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Services
{
    public interface IActorsServices
    {
        Task<IEnumerable<Actor>> GetActorsAsync(string orderBy, bool showMovies);
        Task<Actor> GetActorAsync(int Id, bool showMovies/* = false*/);
        Task<Actor> CreateActorAsync(Actor actor);
        Task<bool> DeleteActorAsync(int Id);
        Task<Actor> UpdateActorAsync(int Id, Actor actor);
    }
}
