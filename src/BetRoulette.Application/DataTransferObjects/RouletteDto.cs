using BetRoulette.Domain.Enums;

namespace BetRoulette.Application.DataTransferObjects;

public record RouletteDto(string Id, string Name, RouletteState State);
