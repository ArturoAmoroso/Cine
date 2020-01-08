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
    public class ActorsServices : IActorsServices
    {
        private readonly ICineRepository cineRepository;
        private readonly IMapper mapper;
        private HashSet<string> allowebOrderBy;
        public ActorsServices(ICineRepository cineRepository, IMapper mapper)
        {
            this.cineRepository = cineRepository;
            this.mapper = mapper;

            allowebOrderBy = new HashSet<string>() { "name", "lastname", "age", "id"};
        }

        public async Task<Actor> CreateActorAsync(Actor actor)
        {
            var actorEntity = mapper.Map<ActorEntity>(actor);
            cineRepository.CreateActor(actorEntity);
            if (await cineRepository.SaveChangesAsync())
            {
                return mapper.Map<Actor>(actorEntity);
            }

            throw new Exception("There were an error with the DB");
        }

        public async Task<bool> DeleteActorAsync(int Id)
        {
            //var actorFound = validateActor(Id);
            //return cineRepository.DeleteActor(actorFound);
            await validateActor(Id);
            await cineRepository.DeleteActorAsync(Id);
            if (await cineRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public async Task<Actor> GetActorAsync(int Id, bool showMovies)
        {
            //var actorFound = validateActor(Id, showMovies);
            //return actorFound;
            var actor = await cineRepository.GetActorAsync(Id, showMovies);

            if (actor == null)
            {
                throw new NotFoundEx("actor not found");
            }

            return mapper.Map<Actor>(actor);
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync(string orderBy, bool showMovies)
        {
            if (orderBy == null)
                throw new BadRequestEx("OrderBy needs a value");

            //var ordeByLower = orderBy.ToLower();
            //if (!allowebOrderBy.Contains(ordeByLower))
            //{
            //    throw new BadRequestEx($"Actors cannot orderBy:{orderBy} , only by: {string.Join(" , ", allowebOrderBy) }");
            //}
            //var actors = cineRepository.GetActors(showMovies);
            //switch (ordebylower)
            //{
            //    case "name":
            //        return actors.orderby(x => x.name);
            //    case "lastname":
            //        return actors.orderby(x => x.lastname);
            //    case "age":
            //        return actors.orderby(x => x.age);
            //    default:
            //        return actors;
            //}
            var orderByLower = orderBy.ToLower();
            if (!allowebOrderBy.Contains(orderByLower))
            {
                throw new BadRequestEx($"invalid Order By value : {orderBy} the only allowed values are {string.Join(", ", allowebOrderBy)}");
            }
            var actorsEntities = await cineRepository.GetActorsAsync(showMovies, orderByLower);
            return mapper.Map<IEnumerable<Actor>>(actorsEntities);

        }
        public async Task<Actor> UpdateActorAsync(int Id, Actor actor)
        {
            //var actorFound = validateActor(Id);
            //if (actor.Id == 0)
            //{
            //    actor.Id = Id;
            //}
            //if (Id != actor.Id)
            //{
            //    throw new BadRequestEx("URL Id and Body Id must be equal");
            //}
            //cineRepository.UpdateActor(actor);
            //return actorFound;
            if (Id != actor.Id)
            {
                throw new InvalidOperationException("URL Id and Body Id must be equal");
            }
            await validateActor(Id);

            actor.Id = Id;
            var actorEntity = mapper.Map<ActorEntity>(actor);
            cineRepository.UpdateActor(actorEntity);
            if (await cineRepository.SaveChangesAsync())
            {
                return mapper.Map<Actor>(actorEntity);
            }

            throw new Exception("There were an error with the DB");
        }
        private async Task<ActorEntity> validateActor(int id, bool showMovies = false)
        {
            var actorFound = await cineRepository.GetActorAsync(id, showMovies);
            if (actorFound == null)
                throw new NotFoundEx($"There isn't an actor with Id: {id}");
            return actorFound;
        }
    }
}
