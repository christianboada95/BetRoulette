using BetRoulette.Domain.Enums;

namespace BetRoulette.Application.DataTransferObjects;

public record BetDto(int Amount, string User)
{
    public short? Value { get; set; }
    public Color? Color { get; set; }
    public BetState? State { get; set; }
    public double? Profits { get; set; }
}
