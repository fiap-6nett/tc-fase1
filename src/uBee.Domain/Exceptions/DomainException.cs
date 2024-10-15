using uBee.Domain.Core.Primitives;

namespace uBee.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public Error Error { get; }

        public DomainException(Error error) : base(error.Message)
            => Error = error;
    }
}
