using BetRoulette.Application.Interfaces;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Enums;
using BetRoulette.Domain.Exceptions;
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

        public async Task<Roulette> Create(string rouletteName)
        {
            var roulette = new Roulette(rouletteName);
            await _rouletteRepository.AddAsync(roulette).ConfigureAwait(false);
            return roulette;
        }

        public async Task<Roulette> Get(string rouletteId)
        {
            var roulette = await _rouletteRepository.GetByIdAsync(rouletteId).ConfigureAwait(false);
            if (roulette is null)
                throw new NotFoundRouletteException($"{rouletteId} not found in Database");

            return roulette;
        }

        public async Task<List<Roulette>> ListAll()
        {
            var list = await _rouletteRepository.ListAsync().ConfigureAwait(false);
            if (list is null)
                throw new NotFoundRouletteException($"No record found in Database");

            return list;
        }

        public async Task Open(string rouletteId)
        {
            var roulette = await Get(rouletteId);
            //if (ListAll().Result.Any(x => x.State == RouletteState.Open))
            //    throw new ConflictOpenRouletteException("Another roulette is already open");

            roulette.Open();
            await _rouletteRepository.UpdateAsync(roulette).ConfigureAwait(false);
        }

        public async Task Close(string rouletteId)
        {
            var roulette = await Get(rouletteId);
            if (roulette.State is RouletteState.Close)
                throw new ConflictOpenRouletteException("Roulette is already close");

            // Generar numero al azar
            int result = roulette.Rol();
            // Solo las apuestas nuevas
            foreach (Bet x in roulette.Bets!.Where(b => b.State is BetState.Progress))
            {
                x.ValidateResult((short)result);
            }
            // Cambiar estado de ruleta a cerrado
            roulette.Close();
            await _rouletteRepository.UpdateAsync(roulette).ConfigureAwait(false);
        }
    }
}
