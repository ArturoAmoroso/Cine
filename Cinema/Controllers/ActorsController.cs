using Cinema.Exceptions;
using Cinema.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Services
{
    [Route("api/actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsServices actorsServices;

        public ActorsController(IActorsServices actorsServices)
        {
            this.actorsServices = actorsServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Actor>> GetActors(string orderBy = "Id"/*, bool showMovies = false*/)
        {
            try
            {
                return Ok(actorsServices.GetActors(orderBy));
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Actor> GetActor(int id, bool showMovies = false)
        {
            try
            {
                return Ok(actorsServices.GetActor(id, showMovies));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<Actor> CreateActor([FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(actorsServices.CreateActor(actor));
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<string> DeleteActor(int id)
        {
            try
            {
                if (actorsServices.DeleteActor(id))
                {
                    return Ok($"Actor: {id} removed");
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Actor: {id} couldn't remove");
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult<Actor> UpdateActor(int id, [FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                /*var Age = ModelState[nameof(actor.Age)];
                //var Age = ModelState["Age"];
                if (Age != null && Age.Errors.Any())
                    return BadRequest(Age.Errors);
                */
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(actorsServices.UpdateActor(id, actor));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
            catch(BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
