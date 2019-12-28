using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinema.Data;
using Cinema.Exceptions;
using Cinema.Models;

namespace Cinema.Services
{
    public class ActorsServices : IActorsServices
    {
        private readonly ICineRepository cineRepository;
        private HashSet<string> allowebOrderBy;
        public ActorsServices(ICineRepository cineRepository)
        {
            this.cineRepository = cineRepository;
            allowebOrderBy = new HashSet<string>() { "name", "lastname", "age", "id"};
        }

        public Actor CreateActor(Actor actor)
        {
            return cineRepository.CreateActor(actor);
        }

        public bool DeleteActor(int Id)
        {
            var actorFound = validateActor(Id);
            return cineRepository.DeleteActor(actorFound);
        }

        public Actor GetActor(int Id, bool showMovies)
        {
            var actorFound = validateActor(Id, showMovies);
            return actorFound;
        }

        public IEnumerable<Actor> GetActors(string orderBy)
        {
            if (orderBy == null)
                throw new BadRequestEx("OrderBy needs a value");

            var ordeByLower = orderBy.ToLower();
            if (!allowebOrderBy.Contains(ordeByLower))
            {
                throw new BadRequestEx($"Actors cannot orderBy:{orderBy} , only by: {string.Join(" , ", allowebOrderBy) }");
            }
            var actors = cineRepository.GetActors();
            switch (ordeByLower)
            {
                case "name":
                    return actors.OrderBy(x => x.Name);
                case "lastname":
                    return actors.OrderBy(x => x.Lastname);
                case "age":
                    return actors.OrderBy(x => x.Age);
                default:
                    return actors;
            }
        }
        public Actor UpdateActor(int Id, Actor actor)
        {
            var actorFound = validateActor(Id);
            if (actor.Id == 0)
            {
                actor.Id = Id;
            }
            if (Id != actor.Id)
            {
                throw new BadRequestEx("URL Id and Body Id must be equal");
            }
            cineRepository.UpdateActor(actor);
            return actorFound;
        }
        private Actor validateActor(int id, bool showMovies = false)
        {
            var actorFound = cineRepository.GetActor(id, showMovies);
            if (actorFound == null)
                throw new NotFoundEx($"There isn't an actor with Id: {id}");
            return actorFound;
        }
    }
}
