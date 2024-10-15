using uBee.Domain.Core.Primitives;

namespace uBee.Domain.Exceptions
{
    public class InvalidPermissionException : Exception
    {
        public Error Error { get; }

        public InvalidPermissionException(Error error) : base(error.Message)
            => Error = error;
    }
}
