using Cinema.Models;
using Cinema.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    public class WinnersController: ControllerBase
    {
        private IWinnersService winnersService;

        public WinnersController(IWinnersService winnersService)
        {
            this.winnersService = winnersService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Winner>> GetWinners()
        {
            return Ok(winnersService.GetWinners());
        }
        [HttpPost]
        public async Task<ActionResult<Winner>> PostWinnersAsync([FromBody] Winner winner)
        {
            return Ok(await winnersService.CreateWinnerAsync(winner));

        }
    }
}
