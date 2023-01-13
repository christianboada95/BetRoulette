using BetRoulette.Domain.Common;
using BetRoulette.Domain.Enums;

namespace BetRoulette.Domain.Entities;

public class Bet : EntityBase
{
    public int Amount { get; private set; }
    public string User { get; private set; }

    public short? Value { get; set; }
    public Color? Color { get; set; }

    public BetState? State { get; set; }
    public int? Profits { get; set; }

    public Bet(int amount, string user)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        User = user;
        State = BetState.Progress;
    }
}
