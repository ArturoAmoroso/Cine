using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Services
{
    public interface IWinnersService
    {
        IEnumerable<Winner> GetWinners();
        Task<Winner> CreateWinnerAsync(Winner winner);
    }
}
