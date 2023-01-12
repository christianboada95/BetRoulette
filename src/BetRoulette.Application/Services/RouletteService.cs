﻿using BetRoulette.Application.Interfaces;
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

        public async Task<Roulette[]> ListAll()
        {
            var list = await _rouletteRepository.ListAsync().ConfigureAwait(false);
            return list.ToArray();
        }

        public async Task Open(string rouletteId)
        {
            var roulette = await Get(rouletteId);
            roulette.State = RouletteState.Open;

            await _rouletteRepository.UpdateAsync(roulette).ConfigureAwait(false);
        }

        public Task Close(string rouletteId)
        {
            throw new NotImplementedException();
        }
    }
}
