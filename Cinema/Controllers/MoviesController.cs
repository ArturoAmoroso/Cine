using Cinema.Exceptions;
using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Route("api/actors/{idActor}/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesServices moviesServices;
        public MoviesController(IMoviesServices moviesServices)
        {
            this.moviesServices = moviesServices;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetMovies(int idActor)
        {
            try
            {
                return Ok(moviesServices.GetMovies(idActor));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("{idMovie}")]
        public ActionResult<Movie> GetMovie(int idActor, int idMovie)
        {
            try
            {
                return Ok(moviesServices.GetMovie(idActor, idMovie));
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
        [HttpPost]
        public ActionResult<Movie> CreateMovie(int idActor, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(moviesServices.CreateMovie(idActor, movie));
                //return Created($"api/actors/{idActor}/movies/{movie.Id}",moviesServices.CreateMovie(idActor, movie));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{idMovie}")]
        public ActionResult<string> DeleteActor(int idActor, int idMovie)
        {
            try
            {
                if (moviesServices.DeleteMovie(idActor, idMovie))
                {
                    return Ok($"Movie: {idMovie} removed");
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Movie: {idMovie} couldn't remove");
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{idMovie}")]
        public ActionResult<Movie> UpdateMovie(int idActor, int idMovie, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(moviesServices.UpdateMovie(idActor, idMovie, movie));
            }
            catch (NotFoundEx ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestEx ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
