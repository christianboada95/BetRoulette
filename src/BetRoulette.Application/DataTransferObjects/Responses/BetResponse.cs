namespace BetRoulette.Application.DataTransferObjects.Responses;

public class BetResponse : Response
{
    public BetResponse(string message) : base(message)
    {
    }

    public BetResponse(BetDto data, string message) 
        : base(data, message) { }
}
