using BetRoulette.Domain.Entities;

namespace BetRoulette.Application.Interfaces;

public interface IRouletteService
{
    Task<Roulette> Create(string rouletteName);
    Task<Roulette[]> ListAll();
    Task Open(string rouletteId);
    Task Close(string rouletteId);
}
