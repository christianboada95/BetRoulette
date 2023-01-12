using BetRoulette.Application.Interfaces;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Interfaces;

namespace BetRoulette.Application.Services
{
    internal class RouletteService : IRouletteService
    {
        private readonly IRepository<Roulette> _rouletteRepository;

        public RouletteService(IRepository<Roulette> rouletteRepository)
        {
            _rouletteRepository = rouletteRepository;
        }

        public Task Close(string rouletteId)
        {
            throw new NotImplementedException();
        }

        public async Task<Roulette> Create(string rouletteName)
        {
            var roulette = new Roulette(rouletteName);
            await _rouletteRepository.AddAsync(roulette).ConfigureAwait(false);
            return roulette;
        }

        public async Task<Roulette[]> ListAll()
        {
            var list = await _rouletteRepository.ListAsync().ConfigureAwait(false);
            return list.ToArray();
        }

        public Task Open(string rouletteId)
        {
            throw new NotImplementedException();
        }
    }
}
