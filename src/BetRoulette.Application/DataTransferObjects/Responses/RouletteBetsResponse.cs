namespace BetRoulette.Application.DataTransferObjects.Responses;

public class RouletteBetsResponse : Response
{
    public RouletteBetsResponse(RouletteDto data, string message)
        : base(data, message) { }
}
