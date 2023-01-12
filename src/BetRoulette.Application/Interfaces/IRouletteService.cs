using BetRoulette.Domain.Entities;

namespace BetRoulette.Application.Interfaces;

public interface IRouletteService
{
    Task<Roulette> Create();
}
