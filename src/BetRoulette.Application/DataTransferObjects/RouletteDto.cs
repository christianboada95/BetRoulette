using BetRoulette.Domain.Enums;

namespace BetRoulette.Application.DataTransferObjects;

public record RouletteDto(string Id, string Name, RouletteState State)
{
    public short? Result { get; set; } = null;
    public List<BetDto>? Bets { get; set; } = null;
};
