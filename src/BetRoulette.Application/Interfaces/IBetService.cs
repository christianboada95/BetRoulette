using BetRoulette.Application.DataTransferObjects;

namespace BetRoulette.Application.Interfaces;

public interface IBetService
{
    Task ToBet(BetDto betDto);
    //Task<bool> ToBetNumber(BetNumber betNumber);
    //Task<bool> ToBetColor(BetColor betColor);
}
