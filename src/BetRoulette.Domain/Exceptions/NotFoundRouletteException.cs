using System.Runtime.Serialization;

namespace BetRoulette.Domain.Exceptions
{
    [Serializable]
    public class NotFoundRouletteException : NotFoundException
    {
        public NotFoundRouletteException()
        {
        }

        public NotFoundRouletteException(string? message) : base(message)
        {
        }

        public NotFoundRouletteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundRouletteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

