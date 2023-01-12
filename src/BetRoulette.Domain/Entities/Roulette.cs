﻿using BetRoulette.Domain.Common;
using BetRoulette.Domain.Enums;

namespace BetRoulette.Domain.Entities;

public class Roulette : EntityBase
{
    public string Name { get; set; }
    public short? Result { get; set; }
    public Bet[]? Bets { get; set; }
    public RouletteStates State { get; set; }
}