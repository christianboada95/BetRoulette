using BetRoulette.Domain.Enums;

namespace BetRoulette.Application.DataTransferObjects;

public class BetDto
{
    public int Amount { get; set; }
    public string User { get; set; }

    public short? Value { get; set; }
    public Color? Color { get; set; }
}
