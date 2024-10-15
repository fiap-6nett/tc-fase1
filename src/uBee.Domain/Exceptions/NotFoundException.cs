using uBee.Domain.Core.Primitives;

namespace uBee.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public Error Error { get; }

        public NotFoundException(Error error) : base(error.Message)
            => Error = error;
    }
}
