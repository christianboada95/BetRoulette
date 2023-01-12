using System.Runtime.Serialization;

namespace BetRoulette.Domain.Exceptions
{
    [Serializable]
    public class NotFoundUserException : NotFoundException
    {
        public NotFoundUserException()
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

