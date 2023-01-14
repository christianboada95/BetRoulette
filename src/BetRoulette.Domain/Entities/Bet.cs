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
    public double Profits { get; set; } = 0f;

    public Bet(int amount, string user)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        User = user;
        State = BetState.Progress;
    }

    public void ValidateResult(short result)
    {
        if (IsValueBet() && Value == result)
        {
            Profits = Amount * 5;
            State = BetState.Win;
            return;
        }
        if (IsColorBet() && ((IsPair(result) && Color is Enums.Color.Red) ||
                             !IsPair(result) && Color is Enums.Color.Black))
        {
            Profits = Amount * 1.8f;
            State = BetState.Win;
            return;
        }
        State = BetState.Lose;
    }
    private bool IsPair(short result) => result % 2 == 0;
    private bool IsColorBet() => Color is not null;
    private bool IsValueBet() => Value is not null;
}
