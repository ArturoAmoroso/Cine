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
        public async Task<ActionResult<IEnumerable<Actor>>> GetActorsAsync(string orderBy = "Id", bool showMovies = false)
        {
            try
            {
                return Ok(await actorsServices.GetActorsAsync(orderBy,showMovies));
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Actor>> GetActorAsync(int id, bool showMovies = false)
        {
            try
            {
                return Ok(await actorsServices.GetActorAsync(id, showMovies));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Actor>> CreateActorAsync([FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await actorsServices.CreateActorAsync(actor));
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteActorAsync(int id)
        {
            //try
            //{
            //    if (actorsServices.DeleteActorAsync(id))
            //    {
            //        return Ok($"Actor: {id} removed");
            //    }
            //    else
            //        return StatusCode(StatusCodes.Status500InternalServerError, $"Actor: {id} couldn't remove");
            //}
            //catch (NotFoundEx ex)
            //{
            //    return NotFound(ex.Message);
            //}
            try
            {
                return Ok(await this.actorsServices.DeleteActorAsync(id));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Actor>> UpdateActorAsync(int id, [FromBody] Actor actor)
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
                return Ok(await actorsServices.UpdateActorAsync(id, actor));
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
