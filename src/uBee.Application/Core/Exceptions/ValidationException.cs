using FluentValidation.Results;
using uBee.Domain.Core.Primitives;

namespace uBee.Application.Core.Exceptions
{
    public sealed class ValidationException : Exception
    {
        #region Properties

        public IReadOnlyCollection<Error> Errors { get; }

        #endregion

        #region Constructors

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("One or more validation failures has occurred.") =>
            Errors = failures
                .Distinct()
                .Select(failure => new Error(failure.ErrorCode, failure.ErrorMessage))
                .ToList();

        #endregion        
    }
}
