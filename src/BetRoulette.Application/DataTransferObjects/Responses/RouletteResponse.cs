namespace BetRoulette.Application.DataTransferObjects.Responses;

public class RouletteResponse : Response
{
    public RouletteResponse(string message) : base(message)
    {
    }

    public RouletteResponse(RouletteDto data, string message)
        : base(data, message)
    {
    }
}
