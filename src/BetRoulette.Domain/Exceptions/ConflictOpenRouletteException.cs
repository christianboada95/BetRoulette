using System.Runtime.Serialization;

namespace BetRoulette.Domain.Exceptions
{
    [Serializable]
    public class ConflictOpenRouletteException : ConflictException
    {
        public ConflictOpenRouletteException()
        {
        }

        public ConflictOpenRouletteException(string? message) : base(message)
        {
        }

        public ConflictOpenRouletteException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ConflictOpenRouletteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

