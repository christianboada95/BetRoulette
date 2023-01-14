using BetRoulette.Domain.Common;
using BetRoulette.Domain.Enums;

namespace BetRoulette.Domain.Entities;

public class Roulette : EntityBase
{
    public string Name { get; set; }
    public short? Result { get; set; }
    public List<Bet>? Bets { get; set; }
    public RouletteState State { get; set; }

    public Roulette(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        Bets = new List<Bet>();
        State = RouletteState.Close;
    }

    public int Rol()
    {
        int winningNumber = Random.Shared.Next(0, 36);
        Result = (short)winningNumber;
        return winningNumber;
    }

    public void Open() => State = RouletteState.Open;
    public void Close() => State = RouletteState.Close;
}
