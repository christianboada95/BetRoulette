namespace BetRoulette.Application.DataTransferObjects.Responses;

public class RouletteListResponse
{
    public List<RouletteDto> Roulettes { get; set; } = new();
}
