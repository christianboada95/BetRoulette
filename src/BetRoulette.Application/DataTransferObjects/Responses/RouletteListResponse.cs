namespace BetRoulette.Application.DataTransferObjects.Responses;

public class RouletteListResponse : Response
{
    public RouletteListResponse(List<RouletteDto> data, string message) 
        : base(data, message) { }
}
