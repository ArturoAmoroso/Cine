using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cinema.Data;
using Cinema.Data.Entities;
using Cinema.Models;

namespace Cinema.Services
{
    public class WinnersService : IWinnersService
    {
        private ICineRepository cineRepository;
        private IMapper mapper;
        public WinnersService(ICineRepository cineRepository, IMapper mapper)
        {
            this.cineRepository = cineRepository;
            this.mapper = mapper;
        }
        public async Task<Winner> CreateWinnerAsync(Winner winner)
        {
            var winnerEntity = mapper.Map<WinnerEntity>(winner);
            cineRepository.CreateWinner(winnerEntity);
            if (await cineRepository.SaveChangesAsync())
            {
                return mapper.Map<Winner>(winnerEntity);

            }
            throw new Exception("There were an error with the DB");

        }

        public async Task<bool> DeleteWinnerAsync(int id)
        {
            await cineRepository.DeleteWinnerAsync(id);
            if (await cineRepository.SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        public IEnumerable<Winner> GetWinners()
        {
            var winnerEntities = cineRepository.GetWinners();
            var winners = mapper.Map<IEnumerable<Winner>>(winnerEntities);
            return winners;
        }
    }
}
