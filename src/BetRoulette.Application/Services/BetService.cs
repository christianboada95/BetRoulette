using BetRoulette.Application.DataTransferObjects;
using BetRoulette.Application.Interfaces;
using BetRoulette.Domain.Entities;
using BetRoulette.Domain.Enums;
using BetRoulette.Domain.Exceptions;
using BetRoulette.Domain.Interfaces;

namespace BetRoulette.Application.Services
{
    internal class BetService : IBetService
    {
        private readonly IRepository<Roulette> _rouletteRepository;

        public BetService(IRepository<Roulette> rouletteRepository)
        {
            _rouletteRepository = rouletteRepository;
        }

        private async Task<Roulette> GetCurrentOpenRoulette()
        {
            var list = await _rouletteRepository.ListAsync();
            if (!list.Any(x => x.State is RouletteState.Open))
                throw new ConflictOpenRouletteException("No Roulette is open.");

            if (list.Where(x => x.State is RouletteState.Open).Count() > 1)
                throw new ConflictOpenRouletteException("More than 1 Roulette is open.");

            return list.FirstOrDefault(x => x.State == RouletteState.Open)!;
        }

        private async Task<Roulette> GetRandomOpenRoulette()
        {
            var list = await _rouletteRepository.ListAsync();
            if (list is null)
                throw new NotFoundRouletteException($"No record found in Database");

            var openlist = list.Where(x => x.State is RouletteState.Open);
            if (!openlist.Any())
                throw new ConflictOpenRouletteException("No Roulette is open.");

            return list[Random.Shared.Next(openlist.Count())];
        }

        public async Task ToBet(BetDto betDto)
        {
            Bet bet = new Bet(betDto.Amount, betDto.User);
            bet.Value = betDto.Value;
            bet.Color = betDto.Color;

            var roulette = await GetRandomOpenRoulette();
            roulette.Bets!.Add(bet);
            await _rouletteRepository.UpdateAsync(roulette);
        }
    }
}
