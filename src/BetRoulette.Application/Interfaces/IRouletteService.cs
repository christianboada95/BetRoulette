using BetRoulette.Domain.Entities;

namespace BetRoulette.Application.Interfaces;

public interface IRouletteService
{
    Task<Roulette> Create(string rouletteName);
    Task<Roulette> Get(string rouletteId);
    Task<Roulette[]> ListAll();
    Task Open(string rouletteId);
    Task Close(string rouletteId);
}
