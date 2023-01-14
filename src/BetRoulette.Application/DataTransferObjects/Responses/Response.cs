namespace BetRoulette.Application.DataTransferObjects.Responses;

public class Response
{
    public object? Data { get; set; }
    public string Message { get; set; }

    public Response(string message)
    {
        Message = message;
    }

    public Response(object data, string message)
    {
        Data = data;
        Message = message;
    }

    public static Response Success(string message) => new(message);
    public static Response Success(object data, string message = "Process success") => new(data, message);
}
