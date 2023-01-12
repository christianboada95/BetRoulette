using BetRoulette.Domain.Common;
using BetRoulette.Domain.Enums;

namespace BetRoulette.Domain.Entities;
public class Bet : EntityBase
{
    public short Value { get; set; }
    public BetColor Color { get; set; }
    public int Amount { get; set; }
    public string User { get; set; }

    public BetState? State { get; set; }
    public int? Profits { get; set; }
}
